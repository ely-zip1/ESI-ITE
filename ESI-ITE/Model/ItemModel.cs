using System;
using System.Collections.Generic;
using System.Data;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class ItemModel
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
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

        DataAccess db = new DataAccess();

        public DataTable FetchAll()
        {
            List<CloneableDictionary<string, string>> table = new List<CloneableDictionary<string, string>>();

            DataTable itemTable = new DataTable();

            itemTable.Columns.Add("Id", typeof(int));
            itemTable.Columns.Add("Code", typeof(string));
            itemTable.Columns.Add("Description", typeof(string));
            itemTable.Columns.Add("SmallestUnitMeasure", typeof(string));
            itemTable.Columns.Add("StockUnitMeasure", typeof(string));
            itemTable.Columns.Add("PiecePerUnit", typeof(int));
            itemTable.Columns.Add("WeightPerUnit", typeof(decimal));
            itemTable.Columns.Add("TaxRate", typeof(decimal));
            itemTable.Columns.Add("TargetWeek", typeof(string));
            itemTable.Columns.Add("Supplier", typeof(string));
            itemTable.Columns.Add("Source", typeof(string));
            itemTable.Columns.Add("Brand", typeof(string));
            itemTable.Columns.Add("Category", typeof(string));
            itemTable.Columns.Add("PiecesPerBO", typeof(int));
            itemTable.Columns.Add("OPG", typeof(int));
            itemTable.Columns.Add("Active", typeof(bool));
            itemTable.Columns.Add("LotControl", typeof(int));

            table = db.SelectMultiple("SELECT * FROM item_master");

            foreach (Dictionary<string, string> row in table)
            {
                DataRow itemRow = itemTable.NewRow();

                itemRow["Id"] = Int32.Parse(row["item_id"]);
                itemRow["Code"] = row["itemcode"];
                itemRow["Description"] = row["item_description"];
                itemRow["SmallestUnitMeasure"] = row["smallest_unit_measure"];
                itemRow["StockUnitMeasure"] = row["stock_unit_measure"];
                itemRow["PiecePerUnit"] = Int32.Parse(row["pieces_per_unit"]);
                itemRow["WeightPerUnit"] = Decimal.Parse(row["weight_per_stock_unit"]);
                itemRow["TaxRate"] = Decimal.Parse(row["tax_rate"]);
                itemRow["TargetWeek"] = row["target_week"];
                itemRow["Supplier"] = row["supplier"];
                itemRow["Source"] = row["source"];
                itemRow["Brand"] = row["brand"];
                itemRow["Category"] = row["category"];
                itemRow["PiecesPerBO"] = Int32.Parse(row["pieces_per_bo"]);
                itemRow["OPG"] = Int32.Parse(row["opg"]);

                if (row["active"] == "1")
                {
                    itemRow["Active"] = true;
                }
                else
                {
                    itemRow["Active"] = false;
                }

                itemRow["LotControl"] = Int32.Parse(row["lot_controll"]);

                itemTable.Rows.Add(itemRow);
            }

            return itemTable;
        }
    }


}
