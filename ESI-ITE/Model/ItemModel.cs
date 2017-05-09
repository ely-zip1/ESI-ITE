using System;
using System.Collections.Generic;
using System.Data;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{
    public class ItemModel
    {

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private string smallestUnitMeasure;
        public string SmallestUnitMeasure
        {
            get { return smallestUnitMeasure; }
            set { smallestUnitMeasure = value; }
        }

        private string stockUnitMeasure;
        public string StockUnitMeasure
        {
            get { return stockUnitMeasure; }
            set { stockUnitMeasure = value; }
        }

        private string piecePerUnit;
        public string PiecePerUnit
        {
            get { return piecePerUnit; }
            set { piecePerUnit = value; }
        }

        private string weightPerUnit;
        public string WeightPerUnit
        {
            get { return weightPerUnit; }
            set { weightPerUnit = value; }
        }

        private string taxRate;
        public string TaxRate
        {
            get { return taxRate; }
            set { taxRate = value; }
        }

        private string targetWeek;
        public string TargetWeek
        {
            get { return targetWeek; }
            set { targetWeek = value; }
        }

        private string supplier;
        public string Supplier
        {
            get { return supplier; }
            set { supplier = value; }
        }

        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        private string brand;
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        private string category;
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private string piecesPerBO;
        public string PiecesPerBO
        {
            get { return piecesPerBO; }
            set { piecesPerBO = value; }
        }

        private string opg;
        public string OPG
        {
            get { return opg; }
            set { opg = value; }
        }

        private string active;
        public string Active
        {
            get { return active; }
            set { active = value; }
        }

        private string lotControl;
        public string LotControl
        {
            get { return lotControl; }
            set { lotControl = value; }
        }

        private int purchasePriceLink;
        public int PurchasePriceLink
        {
            get { return purchasePriceLink; }
            set { purchasePriceLink = value; }
        }

        private int sellingPriceLink;
        public int SellingPriceLink
        {
            get { return sellingPriceLink; }
            set { sellingPriceLink = value; }
        }

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

        public List<ItemModel> Fetch(string itemCode)
        {
            var itemsList = new List<ItemModel>();
            var itemTable = db.SelectMultiple("Select * from item_master where item_code = '" + itemCode + "' limit 1");

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
    }


}
