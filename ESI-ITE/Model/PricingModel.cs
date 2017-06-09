using System;
using System.Collections.Generic;
using System.Text;
using ESI_ITE.Data_Access;
using System.Globalization;

namespace ESI_ITE.Model
{
    public class PricingModel
    {
        private PriceModel priceInformation = new PriceModel();

        private StringBuilder query = new StringBuilder();

        private DataAccess db = new DataAccess();

        public PriceModel GetPrice( string itemCode, string priceCategory, string priceDate )
        {
            if ( priceCategory == "Current Price" )
            {
                CurrentPrice(itemCode, priceCategory);
            }
            else
            {
                PreviousPrice(priceCategory, itemCode, priceDate);
            }

            return priceInformation;
        }

        private void PreviousPrice( string priceCategory, string itemCode, string priceDate )
        {
            string query = "";
            string itemCodeColumn = "";
            string priceColumn = "";
            string priceType = "";
            decimal price = 0;
            DateTime latestDate = new DateTime();
            DateTime _3MonthsAgo = new DateTime();
            DateTime _6MonthsAgo = new DateTime();
            string[,] priceList;

            if ( priceCategory == "Selling Price" )
            {
                query = "SELECT code, Selling_Price, str_to_date(Effective_From, ('%m/%d/%Y')) as xdate, price_type FROM esidb2.price_selling where code = '434355' ORDER BY xdate desc";
                itemCodeColumn = "code";
                priceColumn = "selling_price";

            }
            else
            {
                query = "SELECT pcode, Purchase_Price, str_to_date(Effective_Date,'%m/%d/%Y') as xdate FROM esidb2.price_purchase where pcode='434355' order by xdate desc";
                itemCodeColumn = "pcode";
                priceColumn = "purchase_price";
            }

            var result = db.SelectMultiple(query);

            priceList = new string[result.Count, 4];

            int index = 0;
            foreach ( var row in result )
            {
                var clone = row.Clone();
                priceList[index, 0] = row[itemCodeColumn];
                priceList[index, 1] = row[priceColumn];
                priceList[index, 2] = row["xdate"];
                if ( priceCategory == "Selling Price" )
                {
                    priceList[index, 3] = row["price_type"];
                }
                else
                {
                    priceList[index, 3] = "PL1";
                }
                index++;
            }
            latestDate = DateTime.Parse(priceList[0, 2], CultureInfo.CreateSpecificCulture("en-US"));
            _3MonthsAgo = latestDate.AddMonths(-3);
            _6MonthsAgo = latestDate.AddMonths(-6);

            for ( int i = 0;i < (priceList.Length / 4);i++ )
            {
                var tempDate = DateTime.Parse(priceList[i, 2], CultureInfo.CreateSpecificCulture("en-US"));
                if ( priceDate == "3 Months Ago" )
                {
                    if ( Between(tempDate, _3MonthsAgo, latestDate) )
                    {
                        price = decimal.Parse(priceList[i, 1]);
                        priceType = priceList[i, 3];
                        break;
                    }
                }
                else
                {
                    if ( Between(tempDate, _6MonthsAgo, _3MonthsAgo) )
                    {
                        price = decimal.Parse(priceList[i, 1]);
                        priceType = priceList[i, 3];
                        break;
                    }
                }
            }

            if ( price != 0 && !string.IsNullOrWhiteSpace(priceType) )
            {
                priceInformation.Price = price / Convert.ToDecimal(MyGlobals.SelectedItem.PackSize);
                priceInformation.PriceType = priceType;
            }
            else
            {
                CurrentPrice(itemCode, priceCategory);
            }

        }

        private void CurrentPrice( string itemCode, string priceCategory )
        {
            string query = "";
            string dateQuery = "";
            string priceColumn = "";
            string priceType = "";
            decimal price = 0;

            switch ( priceCategory )
            {
                case "Selling Price":
                    dateQuery = "SELECT max(STR_TO_DATE(effective_from, '%m/%d/%Y')) as date FROM price_selling WHERE code = '" + itemCode + "'";
                    query = "select * from price_selling where code ='" + itemCode + "' and STR_TO_DATE(effective_from, '%m/%d/%Y') = (" + dateQuery + ") limit 1";
                    priceColumn = "selling_price";
                    break;
                case "Purchase Price":
                    dateQuery = "SELECT max(STR_TO_DATE(effective_date, '%m/%d/%Y')) as date FROM price_purchase WHERE pcode = '" + itemCode + "'";
                    query = "select * from price_purchase where pcode = '" + itemCode + "' and STR_TO_DATE(effective_date, '%m/%d/%Y') = (" + dateQuery + ") limit 1";
                    priceColumn = "purchase_price";
                    break;
            }

            var result = db.SelectMultiple(query);
            foreach ( var row in result )
            {
                var clone = row.Clone();

                price = decimal.Parse(row[priceColumn]);

                if ( priceCategory == "Selling Price" )
                {
                    priceType = row["price_type"];
                }
                else
                {
                    priceType = "PL1";
                }

                priceInformation.Price = price / Convert.ToDecimal(MyGlobals.SelectedItem.PackSize);
                priceInformation.PriceType = priceType;
            }
        }

        private static bool Between( DateTime input, DateTime date1, DateTime date2 )
        {
            return (input >= date1 && input < date2);
        }

    }
}
