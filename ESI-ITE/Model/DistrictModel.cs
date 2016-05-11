using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class DistrictModel
    {
        #region Properties

        private int districtId;

        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }

        private string districtNumber;

        public string DistrictNumber
        {
            get { return districtNumber; }
            set { districtNumber = value; }
        }

        private string area;

        public string Area
        {
            get { return area; }
            set { area = value; }
        }

        private int salesman;

        public int Salesman
        {
            get { return salesman; }
            set { salesman = value; }
        }

        private decimal target;

        public decimal Target
        {
            get { return target; }
            set { target = value; }
        }

        private int warehouse;

        public int Warehouse
        {
            get { return warehouse; }
            set { warehouse = value; }
        }


        #endregion
    }
}
