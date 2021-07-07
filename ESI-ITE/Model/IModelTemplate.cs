using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    interface IModelTemplate
    {
        List<Object> FetchAll( );
        object Fetch( string id, string type );
        void AddNew( object item );
        void UpdateItem( string qry );
        void DeleteItem( string qry );
    }
}
