using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Main;

namespace Search {
    class clsSearchLogic {

        #region Attributes

        /// <summary>
        /// Gives public access to the clsDataAccess class.
        /// </summary>
        private clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// Gives public access to the clsSearchSQL class.
        /// </summary>
        private clsSearchSQL sql = new clsSearchSQL();        

        /// <summary>
        /// Used to hold the data.
        /// </summary>
        private DataSet ds;

        /// <summary>
        /// Used to show how many rows were returned in the SQL query.
        /// </summary>
        private int iRetVal = 0;

        #endregion



        #region Methods

        /// <summary>
        /// Returns a list of all of the invoices
        /// </summary>
        /// <param name="dt"></param>
        public void getInvoices(ref DataTable dt) {
            try {
                // Resets the data table
                dt.Rows.Clear();
                
                // Instantiates ds
                ds = new DataSet();

                // Get query needed to find invoice
                ds = db.ExecuteSQLStatement(sql.getAllInvoices(), ref iRetVal);

                // Fill the list up with the specified data
                for (int i = 0; i < iRetVal; i++) {
                    // Instantiates the new row
                    DataRow row = dt.NewRow();

                    // Sets values for Invoice ID, date, and price
                    row[0] = (int)ds.Tables[0].Rows[i][0];
                    row[1] = ds.Tables[0].Rows[i][1].ToString();
                    row[2] = ds.Tables[0].Rows[i][2].ToString();

                    // Adds the new invoice to the data table
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        public void getInvoices(ref DataTable dt, int invoiceID, string date, string price) {
            try {
                // Resets the data table
                dt.Rows.Clear();

                // Instantiates ds
                ds = new DataSet();

                //Get query needed to find invoice
                // If only the invoiceID is set
                if(date == "" && price == "") {
                    ds = db.ExecuteSQLStatement(sql.filterByID(invoiceID), ref iRetVal);
                } // If only the date is set
                else if(invoiceID == -1 && price == "") {
                    ds = db.ExecuteSQLStatement(sql.filterByDate(date), ref iRetVal);
                } // If only the price is set
                else if(invoiceID == -1 && date == "") {
                    ds = db.ExecuteSQLStatement(sql.filterByPrice(price), ref iRetVal);
                } // If only the invoiceId and date are set
                else if(price == "") {
                    ds = db.ExecuteSQLStatement(sql.filterByIDAndDate(invoiceID, date), ref iRetVal);
                } // If only the invoiceId and price are set
                else if(date == "") {
                    ds = db.ExecuteSQLStatement(sql.filterByIDAndPrice(invoiceID, price), ref iRetVal);
                } // If only the date and price are set
                else if(invoiceID == -1) {
                    ds = db.ExecuteSQLStatement(sql.filterByDateAndPrice(date, price), ref iRetVal);
                }

                // Fill the list up with the specified data
                for(int i = 0; i < iRetVal; i++) {
                    // Instantiates the new row
                    DataRow row = dt.NewRow();

                    // Sets values for InvoiceID, date, and price
                    row[0] = (int)ds.Tables[0].Rows[i][0];
                    row[1] = ds.Tables[0].Rows[i][1].ToString();
                    row[2] = ds.Tables[0].Rows[i][2].ToString();

                    // Adds the new invoice to the data table
                    dt.Rows.Add(row);
                }
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion
    }
}
