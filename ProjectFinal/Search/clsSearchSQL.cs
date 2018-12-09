using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wndSearch {
    class clsSearchSQL {
        private string sSQL;

        /// <summary>
        /// Used to populate the datagrid with all invoices.
        /// </summary>
        /// <returns>A string containing the query to return all invoices.</returns>
        public string getAllInvoices() {
            try {
                sSQL = "SELECT * FROM Invoices ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filters datagrid based off the InvoiceID.
        /// </summary>
        /// <param name="invoiceID">InvoiceID to retrieve all data</param>
        /// <returns>A string containing the query to return all invoices filtered by selected Invoice ID.</returns>
        public string filterByID(int invoiceID) {
            try {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceID + " ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filters datagrid based off the InvoiceID and date
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="date">Invoice date to retrieve all data</param>
        /// <returns>A string containing the query to return all invoices filtered by ID and date</returns>
        public string filterByIDAndDate(int invoiceID, string date) {
            try {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND InvoiceDate = #" + date + "# ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filters datagrid based off the InvoiceID and price
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="price">Invoice price to retrieve all data</param>
        /// <returns>A string containing the query to return all invoices filtered by ID and total cost</returns>
        public string filterByIDAndPrice(int invoiceID, string price) {
            try {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND TotalCost = " + price + " ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filters datagrid based off the InvoiceDate.
        /// </summary>
        /// <param name="date">Invoice date to retrieve all data</param>
        /// <returns>A string containing the query to return all invoices filtered by selected invoice date.</returns>
        public string filterByDate(string date) {
            try {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "# ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filters datagrid based off the date and price
        /// </summary>
        /// <param name="date">Invoice date to retrieve all data</param>
        /// <param name="price">Invoice price to retrieve all data</param>
        /// <returns>A string containing the query to return all invoices filtered by date and total cost</returns>
        public string filterByDateAndPrice(string date, string price) {
            try {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + date + "# AND TotalCost = " + price + " ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Filters datagrid based off the InvoicePrice.
        /// </summary>
        /// <param name="price">Invoice price to retrieve all data</param>
        /// <returns>A string containing the query to return all invoices filtered by selected invoice price.</returns>
        public string filterByPrice(string price) {
            try {
                sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + price + " ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
