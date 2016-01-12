using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESI_ITE.Data_Access;

namespace ESI_ITE.Model
{

    public class PostingModel
    {
        private int warehouseId;
        private int transactionId;
        private List<Dictionary<string, string>> inventory_dummy = new List<Dictionary<string, string>>();
        private Dictionary<string, string> _piecePerUnit = new Dictionary<string, string>();
        private Dictionary<string, List<int>> itemTotal = new Dictionary<string, List<int>>();

        private DataAccess db = new DataAccess();
        private StringBuilder sb = new StringBuilder();


        public string Post(string transactionNumber)
        {
            if (isPrinted(transactionNumber))
            {
                fetchItems(transactionNumber);

                if (calculateAndPost())
                {
                    return "Posting Successful";
                }
                else
                {
                    return "Posting Failed";
                }

            }
            else
            {
                return "Transaction Not Printed";
            }
        }

        private bool calculateAndPost()
        {
            List<string> transactionStrings = new List<string>();
            string[,] inventoryMaster = new string[inventory_dummy.Count, 3];

            int index = 0;
            string[] result;
            int totalCases, totalPieces;
            int tempCases, tempPieces;
            List<int> casesPiecesList = new List<int>();

            foreach (Dictionary<string, string> row in inventory_dummy)
            {
                result = db.Select("select i_id, i_cases, i_pieces from inventory_master where " +
                    "warehouse_code = '" + warehouseId + "' and " +
                    "item_id_link = '" + row["item_link"] + "' and " +
                    "location_link = '" + row["location_link"] + "' and " +
                    "expiration_date = '" + row["expiration_date"] + "'" +
                    "").Split('|');

                inventoryMaster[index, 0] = result[0];
                inventoryMaster[index, 1] = result[1];
                inventoryMaster[index, 2] = result[2];

                index++;
            }

            index = 0;

            foreach (Dictionary<string, string> row in inventory_dummy)
            {
                if (string.IsNullOrEmpty(inventoryMaster[index, 1]))
                {
                    //casesPiecesList.Add(Int32.Parse(row["cases"]));
                    //casesPiecesList.Add(Int32.Parse(row["pieces"]));

                    //itemTotal.Add(row["item_link"], casesPiecesList);

                    sb.Append("insert into inventory_master values(");
                    sb.Append("null,");
                    sb.Append("'" + warehouseId + "',");
                    sb.Append("'" + row["item_link"] + "',");
                    sb.Append("'" + row["location_link"] + "',");
                    sb.Append("'" + Int32.Parse(row["cases"]) + "',");
                    sb.Append("'" + Int32.Parse(row["pieces"]) + "',");
                    sb.Append("'" + row["expiration_date"] + "'");
                    sb.AppendLine(");");

                    //casesPiecesList.Clear();

                    index++;
                }
                else
                {
                    tempCases = Int32.Parse(inventoryMaster[index, 1]) + Int32.Parse(row["cases"]);
                    tempPieces = Int32.Parse(inventoryMaster[index, 2]) + Int32.Parse(row["pieces"]);
                    totalCases = tempCases + (tempPieces / Int32.Parse(_piecePerUnit[row["item_link"]]));
                    totalPieces = tempPieces % Int32.Parse(_piecePerUnit[row["item_link"]]);

                    //casesPiecesList.Add(totalCases);
                    //casesPiecesList.Add(totalPieces);

                    //itemTotal.Add(row["item_link"], casesPiecesList);

                    sb.Append("update inventory_master set ");
                    sb.Append("i_cases = '" + Int32.Parse(row["cases"]) + "',");
                    sb.Append("i_pieces = '" + Int32.Parse(row["pieces"]) + "' ");
                    sb.Append("where i_id = '" + inventoryMaster[index, 0] + "'");
                    sb.AppendLine(");");

                    //casesPiecesList.Clear();
                    index++;
                }
            }

            sb.Append("delete * from inventory_dummy where transaction_link = '" + transactionId + "'");

            db.RunMySqlTransaction(sb.ToString());

            return true;
        }

        private void fetchItems(string transactionNumber)
        {
            string piecePerUnit;

            transactionId = Int32.Parse(db.Select("select entry_id from transaction_entry " +
                "where trans_no = '" + transactionNumber + "'"));

            inventory_dummy = db.SelectMultiple("Select * from inventory_dummy " +
                "where transaction_link = '" + transactionId + "'");

            warehouseId = Int32.Parse(db.Select("select source_wh_link from transaction_entry " +
                "where trans_no = '" + transactionNumber + "'"));

            foreach (Dictionary<string, string> row in inventory_dummy)
            {
                piecePerUnit = db.Select("select pieces_per_unit from item_master where item_id = '" + row["item_link"] + "'");
                _piecePerUnit.Add(row["item_link"], piecePerUnit);
            }
        }

        private bool isPrinted(string transactionNumber)
        {
            string status;

            status = db.Select("select status from transaction_entry " +
                "where trans_no = '" + transactionNumber + "'");

            if (status == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
