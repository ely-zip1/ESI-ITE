using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    class SupplierModel: IModelTemplate
    {
        #region Properties

        DataAccess db = new DataAccess();

        private int supplierId;

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        private string supplierCode;

        public string SupplierCode
        {
            get { return supplierCode; }
            set { supplierCode = value; }
        }

        private string supplierName;

        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string contactPerson;

        public string ContactPerson
        {
            get { return contactPerson; }
            set { contactPerson = value; }
        }

        private string contactnumber;

        public string ContacNumber
        {
            get { return contactnumber; }
            set { contactnumber = value; }
        }

        private int termId;

        public int TermId
        {
            get { return termId; }
            set { termId = value; }
        }

        #endregion

        public List<object> FetchAll( )
        {
            List<object> list = new List<object>();

            var record = db.SelectMultiple("select * from supplier");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                list.Clear();
                var supplier = new SupplierModel();
                var clone = row.Clone();

                supplier.SupplierId = int.Parse(row["supplier_id"]);
                supplier.SupplierCode = row["supplier_code"].ToString();
                supplier.SupplierName = row["supplier_name"].ToString();
                supplier.Address = row["address"].ToString();
                supplier.ContactPerson = row["contact_person"].ToString();
                supplier.ContacNumber = row["contact_number"].ToString();
                supplier.TermId = int.Parse(row["term_id"]);

                list.Add(supplier);
            }

            return list;
        }

        public object Fetch( string qry )
        {
            var supplier = new SupplierModel();

            var record = db.SelectMultiple("select * from supplier where supplier_code = '" + qry + "'");

            IFormatProvider culture = new System.Globalization.CultureInfo("en-US", true);

            foreach ( var row in record )
            {
                var clone = row.Clone();

                supplier.SupplierId = int.Parse(row["supplier_id"]);
                supplier.SupplierCode = row["supplier_code"].ToString();
                supplier.SupplierName = row["supplier_name"].ToString();
                supplier.Address = row["address"].ToString();
                supplier.ContactPerson = row["contact_person"].ToString();
                supplier.ContacNumber = row["contact_number"].ToString();
                supplier.TermId = int.Parse(row["term_id"]);
            }

            return supplier;
        }

        public void AddNew( object item )
        {
            throw new NotImplementedException();
        }

        public void UpdateItem( string qry )
        {
            throw new NotImplementedException();
        }

        public void DeleteItem( string qry )
        {
            throw new NotImplementedException();
        }

    }
}
