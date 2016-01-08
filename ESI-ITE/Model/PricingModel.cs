using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class PricingModel
    {
        private List<object> priceInformation = new List<object>();

        private StringBuilder query = new StringBuilder();

        private DataAccess db = new DataAccess();

        public List<object> GetPrice(string itemCode, string priceCategory, string priceDate)
        {

            if (priceCategory == "Purchase Price")
            {
                switch (priceDate)
                {
                    case "Current":
                        currentPurchasePrice(itemCode);
                        break;
                    case "3 Months Ago":
                        previousPurchasePrice("3 Months Ago", itemCode);
                        break;
                    case "6 Months Ago":
                        previousPurchasePrice("6 Months Ago", itemCode);
                        break;
                    default: break;
                }

                return priceInformation;
            }
            else if (priceCategory == "Selling Price")
            {
                switch (priceDate)
                {
                    case "Current":
                        currentSellingPrice(itemCode);
                        break;
                    case "3 Months Ago":
                        previousSellingPrice("3 Months Ago", itemCode);
                        break;
                    case "6 Months Ago":
                        previousSellingPrice("6 Months Ago", itemCode);
                        break;
                    default: break;
                }
                return priceInformation;
            }
            else
            {
                return priceInformation;
            }
        }

        #region Selling Price
        private void previousSellingPrice(string category, string itemCode)
        {
            string priceType;
            decimal price;
            string date;

            query.Clear();
            query.Append("SELECT");
            query.Append("max(STR_TO_DATE(effective_from, '%m/%d/%Y')) as date");   //select the latest date
            query.Append("FROM");
            query.Append("price_selling WHERE code = '" + itemCode + "' ");         // from price_selling table for the current item
            query.Append("AND");
            query.Append("STR_TO_DATE(effective_from, '%m/%d/%Y')");                // where the effective date
            query.Append("BETWEEN");
            if (category == "3 Months Ago")
            {                                                                       // is between the date 3 months ago and
                query.Append("(SELECT date_sub(max(STR_TO_DATE(effective_from, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_selling WHERE code = '" + itemCode + "')");
                query.Append("AND");                                                // the latest date 
                query.Append("(SELECT max(STR_TO_DATE(effective_from, '%m/%d/%Y')) FROM price_selling WHERE code = '" + itemCode + "')");
            }
            else if (category == "6 Months Ago")
            {                                                                       // or is between the date 6 months ago and
                query.Append("(SELECT date_sub(max(STR_TO_DATE(effective_from, '%m/%d/%Y')), INTERVAL 6 MONTH) FROM price_selling WHERE code = '" + itemCode + "')");
                query.Append("AND");                                                // the date 3 months ago 
                query.Append("(SELECT date_sub(max(STR_TO_DATE(effective_from, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_selling WHERE code = '" + itemCode + "')");
            }

            date = db.Select(query.ToString());

            query.Clear();

            if (date != "") // if date contains a value
            {
                date = DateTime.Parse(date).ToString("MM/dd/yyyy");

                priceType = db.Select("SELECT price_type FROM price_selling " +
                    "WHERE code = '" + itemCode + "' AND effective_from = '" + date + "'");

                price = Decimal.Parse(db.Select("select selling_price from price_selling " +
                    "where code = '" + itemCode + "' and effective_from = '" + date + "' and price_type = '" + priceType + "'"));

                priceInformation.Clear();
                priceInformation.Add(price);
                priceInformation.Add(priceType);
            }
            else
            {
                currentSellingPrice(itemCode);
            }
        }

        private void currentSellingPrice(string itemCode)
        {
            string priceType;
            decimal price;
            string date;

            date = DateTime.Parse(db.Select("SELECT max(STR_TO_DATE(effective_from, '%m/%d/%Y')) AS date FROM price_selling" +
                "WHERE code = '" + itemCode + "'")).ToString("MM/dd/yyyy");

            price = Decimal.Parse(db.Select("SELECT selling_price FROM price_selling " +
                "WHERE code = '" + itemCode + "' and effective_from = '" + date + "'"));

            priceType = db.Select("SELECT price_type from price_selling " +
                "WHERE code = '" + itemCode + "' and effective_from = '" + date + "'");

            priceInformation.Clear();
            priceInformation.Add(price);
            priceInformation.Add(priceType);
        }

        #endregion

        #region Purchase Price
        private void previousPurchasePrice(string category, string itemCode)
        {
            string priceType = "PL1";
            decimal price;
            string date;

            query.Clear();
            query.Append("SELECT");
            query.Append("max(STR_TO_DATE(effective_date, '%m/%d/%Y')) as date");   //select the latest date
            query.Append("FROM");
            query.Append("price_purchase WHERE pcode = '" + itemCode + "' ");         // from price_purchase table for the current item
            query.Append("AND");
            query.Append("STR_TO_DATE(effective_date, '%m/%d/%Y')");                // where the effective date
            query.Append("BETWEEN");
            if (category == "3 Months Ago")
            {                                                                       // is between the date 3 months ago and
                query.Append("(SELECT DATE_SUB(max(STR_TO_DATE(effective_date, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_purchase WHERE pcode = '" + itemCode + "')");
                query.Append("AND");                                                // the latest date 
                query.Append("(SELECT max(STR_TO_DATE(effective_date, '%m/%d/%Y')) FROM price_purchase WHERE pcode = '" + itemCode + "')");
            }
            else if (category == "6 Months Ago")
            {                                                                       // or is between the date 6 months ago and
                query.Append("(SELECT DATE_SUB(max(STR_TO_DATE(effective_date, '%m/%d/%Y')), INTERVAL 6 MONTH) FROM price_purchase WHERE pcode = '" + itemCode + "')");
                query.Append("AND");                                                // the date 3 months ago 
                query.Append("(SELECT DATE_SUB(max(STR_TO_DATE(effective_date, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_purchase WHERE pcode = '" + itemCode + "')");
            }

            date = db.Select(query.ToString());

            query.Clear();

            if(date != "")
            {
                date = DateTime.Parse(date).ToString("MM/dd/yyyy");

                price = Decimal.Parse(db.Select("select purchase_price from price_purchase " +
                    "where pcode = '" + itemCode + "' and effective_date = '" + date + "'"));

                priceInformation.Clear();
                priceInformation.Add(price);
                priceInformation.Add(priceType);
            }
        }

        private void currentPurchasePrice(string itemCode)
        {
            decimal price;
            string priceType = "PL1";
            string date;

            date = DateTime.Parse(db.Select("SELECT max(STR_TO_DATE(effective_date, '%m/%d/%Y')) FROM price_purchase " +
                "WHERE pcode = '" + itemCode + "'")).ToString("MM/dd/yyyy");

            price = Decimal.Parse(db.Select("select price from price_purchase " +
                "where pcode = '" + itemCode + "' and effective_date = '" + date + "'"));

            priceInformation.Clear();
            priceInformation.Add(price);
            priceInformation.Add(priceType);
        }

        #endregion
    }
}
