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
        public int PurchasePriceLink { get; set; }
        public int SellingPriceLink { get; set; }



        DataAccess db = new DataAccess();

        public DataTable FetchTable()
        {
            List<CloneableDictionary<string, string>> table = new List<CloneableDictionary<string, string>>();

            DataTable itemTable = new DataTable();

            //table column definition
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

            //data insert
            foreach (Dictionary<string, string> row in table)
            {
                DataRow itemRow = itemTable.NewRow();

                itemRow["Id"] = Int32.Parse(row["item_id"]);
                itemRow["Code"] = row["item_code"];
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

        public List<ItemModel> FetchAll()
        {
            var itemsList = new List<ItemModel>();
            var itemTable = db.SelectMultiple("Select * from item_master");

            foreach (var row in itemTable)
            {
                var itemModel = new ItemModel();
                var clone = row.Clone();

                itemModel.Id = row["item_id"];
                itemModel.Code = row["item_code"];
                itemModel.Description = row["item_description"];
                itemModel.SmallestUnitMeasure = row["smallest_unit_measure"];
                itemModel.StockUnitMeasure = row["stock_unit_measure"];
                itemModel.PiecePerUnit = row["pieces_per_unit"];
                itemModel.WeightPerUnit = row["weight_per_stock_unit"];
                itemModel.TaxRate = row["tax_rate"];
                itemModel.TargetWeek = row["target_week"];
                itemModel.Supplier = row["supplier"];
                itemModel.Source = row["source"];
                itemModel.Brand = row["brand"];
                itemModel.Category = row["category"];
                itemModel.PiecesPerBO = row["pieces_per_bo"];
                itemModel.OPG = row["opg"];
                itemModel.Active = row["active"];
                itemModel.LotControl = row["lot_controll"];
                itemModel.PurchasePriceLink = Convert.ToInt32(row["purchase_price_link"]);
                itemModel.SellingPriceLink = Convert.ToInt32(row["selling_price_link"]);

                itemsList.Add(itemModel);
            }

            return itemsList;
        }

        public ItemModel Fetch(string itemCode)
        {
            ItemModel item = new ItemModel();

            string[] result = db.Select("select * from item_master where item_code = '" + itemCode + "'").Split('|');

            item.Id = result[0];
            item.Code = result[1];
            item.Description = result[2];
            item.SmallestUnitMeasure = result[3];
            item.StockUnitMeasure = result[4];
            item.PiecePerUnit = result[5];
            item.WeightPerUnit = result[6];
            item.TaxRate = result[7];
            item.TargetWeek = result[8];
            item.Supplier = result[9];
            item.Source = result[10];
            item.Brand = result[11];
            item.Category = result[12];
            item.PiecesPerBO = result[13];
            item.OPG = result[14];
            item.Active = result[15];
            item.LotControl = result[16];
            item.PurchasePriceLink = Convert.ToInt32(result[17]);
            item.SellingPriceLink = Convert.ToInt32(result[18]);

            return item;

        }
    }


}
