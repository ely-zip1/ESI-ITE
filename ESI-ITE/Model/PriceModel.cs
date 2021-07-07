using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public struct PriceModel
    {
        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private string priceType;   
        public string PriceType
        {
            get { return priceType; }
            set { priceType = value; }
        }
    }
}
