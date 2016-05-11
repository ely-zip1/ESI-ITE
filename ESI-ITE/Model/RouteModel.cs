using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    class RouteModel
    {

        #region Properties

        private int routeId;

        public int RouteId
        {
            get { return routeId; }
            set { routeId = value; }
        }

        private string routeCode;

        public string RouteCode
        {
            get { return routeCode; }
            set { routeCode = value; }
        }

        private string routeDescription;

        public string RouteDescription
        {
            get { return routeDescription; }
            set { routeDescription = value; }
        }
        #endregion
    }
}
