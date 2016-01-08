using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class PricingModel
    {
        private List<object> priceInformation = new List<object>();

        private DataAccess db = new DataAccess();

        public List<object> GetPrice(string itemCode, string priceCategory, string priceType)
        {

            if (priceCategory == "Purchase Price")
            {
                switch (priceType)
                {
                    case "Current":
                        currentPurchasePrice(itemCode);
                        break;
                    case "3 Months Ago":
                        purchasePriceThreeMonthsAgo(itemCode);
                        break;
                    case "6 Months Ago":
                        purchasePriceSixMonthsAgo(itemCode);
                        break;
                    default: break;
                }

                return priceInformation;
            }
            else if (priceCategory == "Selling Price")
            {
                switch (priceType)
                {
                    case "Current":
                        currentSellingPrice(itemCode);
                        break;
                    case "3 Months Ago":
                        sellingPriceThreeMonthsAgo(itemCode);
                        break;
                    case "6 Months Ago":
                        sellingPriceSixMonthsAgo(itemCode);
                        break;
                    default: break;
                }
                return priceInformation;
            }
            else
            {
                return priceInformation
            }
        }

        private void sellingPriceSixMonthsAgo(string itemCode)
        {
            
        }

        private void sellingPriceThreeMonthsAgo(string itemCode)
        {
            throw new NotImplementedException();
        }

        private void currentSellingPrice(string itemCode)
        {
            string 
        }

        private void purchasePriceSixMonthsAgo(string itemCode)
        {
            throw new NotImplementedException();
        }

        private void purchasePriceThreeMonthsAgo(string itemCode)
        {
            throw new NotImplementedException();
        }

        private void currentPurchasePrice(string itemCode)
        {
            throw new NotImplementedException();
        }
    }
}
