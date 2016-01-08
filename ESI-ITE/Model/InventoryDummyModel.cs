using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class InventoryDummyModel
    {
        //public int Id { get; set; }
        public string WareHouse { get; set; }
        public string ItemCode { get; set; }
        public string Location { get; set; }
        public int Cases { get; set; }
        public int Pieces { get; set; }
        public DateTime Expiration { get; set; }
        public int TransactionId { get; set; }
        public decimal PricePerPiece { get; set; }
        public decimal LineValue { get; set; }

        public List<InventoryDummyModel> dummy = new List<InventoryDummyModel>();

        public void addItem(InventoryDummyModel idummy)
        {
            dummy.Add(idummy);
        }
        
    }
}
