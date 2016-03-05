using System;
using System.Collections.Generic;
using System.Text;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class PricingModel
    {
        private PriceModel priceInformation = new PriceModel();

        private StringBuilder query = new StringBuilder();

        private DataAccess db = new DataAccess();

        public PriceModel GetPrice(string itemCode, string priceCategory, string priceDate)
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
                    default:
                        break;
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
                    default:
                        break;
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
            query.Append("SELECT ");
            query.Append("max(STR_TO_DATE(effective_from, '%m/%d/%Y')) as date ");   //select the latest date
            query.Append("FROM ");
            query.Append("price_selling WHERE code = '" + itemCode + "' ");         // from price_selling table for the current item
            query.Append("AND ");
            query.Append("STR_TO_DATE(effective_from, '%m/%d/%Y') ");                // where the effective date
            query.Append("BETWEEN ");
            if (category == "3 Months Ago")
            {                                                                       // is between the date 3 months ago and
                query.Append("(SELECT date_sub(max(STR_TO_DATE(effective_from, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_selling WHERE code = '" + itemCode + "') ");
                query.Append("AND ");                                                // the latest date 
                query.Append("(SELECT max(STR_TO_DATE(effective_from, '%m/%d/%Y')) FROM price_selling WHERE code = '" + itemCode + "') ");
            }
            else if (category == "6 Months Ago")
            {                                                                       // or is between the date 6 months ago and
                query.Append("(SELECT date_sub(max(STR_TO_DATE(effective_from, '%m/%d/%Y')), INTERVAL 6 MONTH) FROM price_selling WHERE code = '" + itemCode + "') ");
                query.Append("AND ");                                                // the date 3 months ago 
                query.Append("(SELECT date_sub(max(STR_TO_DATE(effective_from, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_selling WHERE code = '" + itemCode + "') ");
            }

            date = db.Select(query.ToString());

            query.Clear();

            if (date != "") // if date contains a value
            {
                date = DateTime.Parse(date).ToString("%M/%d/yyyy");

                priceType = db.Select("SELECT price_type FROM price_selling " +
                    "WHERE code = '" + itemCode + "' AND effective_from = '" + date + "'");

                price = Decimal.Parse(db.Select("select selling_price from price_selling " +
                    "where code = '" + itemCode + "' and effective_from = '" + date + "' and price_type = '" + priceType + "'"));

                priceInformation.Price = price / Convert.ToDecimal(MyGlobals.SelectedItem.PiecePerUnit);
                priceInformation.PriceType = priceType;
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
            string query;

            query = ("SELECT max(STR_TO_DATE(effective_from, '%m/%d/%Y')) AS date FROM price_selling " +
                "WHERE code = '" + itemCode + "'");

            date = DateTime.Parse(db.Select(query)).ToString("%M/%d/yyyy");

            query = "SELECT selling_price FROM price_selling " +
                "WHERE code = '" + itemCode + "' and effective_from = '" + date + "'";

            price = Decimal.Parse(db.Select(query));

            query = "SELECT price_type from price_selling " +
                "WHERE code = '" + itemCode + "' and effective_from = '" + date + "'";

            priceType = db.Select(query);

            priceInformation.Price = price / Convert.ToDecimal(MyGlobals.SelectedItem.PiecePerUnit);
            priceInformation.PriceType = priceType;
        }

        #endregion

        #region Purchase Price
        private void previousPurchasePrice(string category, string itemCode)
        {
            string priceType = "PL1";
            decimal price;
            string date;

            query.Clear();
            query.Append("SELECT ");
            query.Append("max(STR_TO_DATE(effective_date, '%m/%d/%Y')) as date ");   //select the latest date
            query.Append("FROM ");
            query.Append("price_purchase WHERE pcode = '" + itemCode + "' ");         // from price_purchase table for the current item
            query.Append("AND ");
            query.Append("STR_TO_DATE(effective_date, '%m/%d/%Y') ");                // where the effective date
            query.Append("BETWEEN ");
            if (category == "3 Months Ago")
            {                                                                       // is between the date 3 months ago and
                query.Append("(SELECT DATE_SUB(max(STR_TO_DATE(effective_date, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_purchase WHERE pcode = '" + itemCode + "') ");
                query.Append("AND ");                                                // the latest date 
                query.Append("(SELECT max(STR_TO_DATE(effective_date, '%m/%d/%Y')) FROM price_purchase WHERE pcode = '" + itemCode + "') ");
            }
            else if (category == "6 Months Ago")
            {                                                                       // or is between the date 6 months ago and
                query.Append("(SELECT DATE_SUB(max(STR_TO_DATE(effective_date, '%m/%d/%Y')), INTERVAL 6 MONTH) FROM price_purchase WHERE pcode = '" + itemCode + "') ");
                query.Append("AND ");                                                // the date 3 months ago 
                query.Append("(SELECT DATE_SUB(max(STR_TO_DATE(effective_date, '%m/%d/%Y')), INTERVAL 3 MONTH) FROM price_purchase WHERE pcode = '" + itemCode + "') ");
            }

            date = db.Select(query.ToString());

            query.Clear();

            if (date != "")
            {
                date = DateTime.Parse(date).ToString("%M/%d/yyyy");

                price = Decimal.Parse(db.Select("select purchase_price from price_purchase " +
                    "where pcode = '" + itemCode + "' and effective_date = '" + date + "'"));

                priceInformation.Price = price / Convert.ToDecimal(MyGlobals.SelectedItem.PiecePerUnit);
                priceInformation.PriceType = priceType;
            }
        }

        private void currentPurchasePrice(string itemCode)
        {
            decimal price = 0;
            string priceType = "PL1";
            string date;

            string qry;
            string result;

            qry = "SELECT max(STR_TO_DATE(effective_date, '%m/%d/%Y')) FROM price_purchase WHERE pcode = '" + itemCode + "'";

            date = DateTime.Parse(db.Select(qry)).ToString("%M/%d/yyyy");
            if (date.StartsWith("0"))
                date = date.TrimStart('0');

            qry = "select purchase_price from price_purchase where pcode = '" + itemCode + "' and effective_date = '" + date + "'";

            result = db.Select(qry);

            if (!string.IsNullOrEmpty(result))
            {
                price = Convert.ToDecimal(result);
                price = price / Convert.ToDecimal(MyGlobals.SelectedItem.PiecePerUnit);
            }

            priceInformation.Price = price;
            priceInformation.PriceType = priceType;
        }

        #endregion
    }
}
