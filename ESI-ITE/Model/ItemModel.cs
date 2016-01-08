using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class ItemModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Desc { get; set; }
        public string SmallestUnitMeasure { get; set; }
        public string StockUnitMeasure { get; set; }
        public string PiecePerUnit { get; set; }
        public string WeightPerUnit { get; set; }
        public string TaxRate { get; set; }
        public string TargetWeek { get; set; }
        public string Supplier { get; set; }
        public string Source { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string PiecesPerBO { get; set; }
        public string OPG { get; set; }
        public string Active { get; set; }
        public string LotControl { get; set; }
    }
}
