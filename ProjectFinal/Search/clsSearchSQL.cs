using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Search {
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


        #region Filter

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

        /// <summary>
        /// Filters datagrid based off the ID, date, and price
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="date">Invoice date</param>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of invoices filtered by selected id, date, and price</returns>
        public string filterByIDAndDateAndPrice(int invoiceID, string date, string price) {
            try {
                sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND InvoiceDate = #" + date + "# AND TotalCost = " + price + " ORDER BY InvoiceNum";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion


        #region Get Date

        /// <summary>
        /// Gets a list of all dates
        /// </summary>
        /// <returns>A list of dates</returns>
        public string getAllDates() {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// Gets a list of dates from an ID
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <returns>A list of dates</returns>
        public string getDateWithID(int invoiceID) {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE InvoiceNum = " + invoiceID + " ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of dates from a date
        /// </summary>
        /// <param name="date">Invoice date</param>
        /// <returns>A list of dates</returns>
        public string getDateWithDate(string date) {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE InvoiceDate = #" + date + "# ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of dates from a price
        /// </summary>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of dates</returns>
        public string getDateWithPrice(string price) {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE TotalCost = " + price + " ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of dates from an ID and a date
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="date">Invoice date</param>
        /// <returns>A list of dates</returns>
        public string getDateWithIDAndDate(int invoiceID, string date) {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND InvoiceDate = #" + date + "# ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of dates from and ID and a price
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of dates</returns>
        public string getDateWithIDAndPrice(int invoiceID, string price) {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND TotalCost = " + price + " ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of dates from a date and a price
        /// </summary>
        /// <param name="date">Invoice date</param>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of dates</returns>
        public string getDateWithDateAndPrice(string date, string price) {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE InvoiceDate = #" + date + "# AND TotalCost = " + price + " ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Gets a list of dates from an ID, date, and price
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="date">Invoice date</param>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of dates</returns>
        public string getDateWithIDAndDateAndPrice(int invoiceID, string date, string price) {
            try {
                sSQL = "SELECT DISTINCT InvoiceDate FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND InvoiceDate = #" + date + "# AND TotalCost = " + price + " ORDER BY InvoiceDate";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion


        #region Get Price

        /// <summary>
        /// Gets a list of all total costs
        /// </summary>
        /// <returns>A list of prices</returns>
        public string getAllPrices() {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices ORDER BY TotalCost";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of prices from an ID
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <returns>A list of prices</returns>
        public string getPriceWithID(int invoiceID) {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceID + " ORDER BY TotalCost";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of prices from a date
        /// </summary>
        /// <param name="date">Invoice date</param>
        /// <returns>A list of prices</returns>
        public string getPriceWithDate(string date) {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceDate = #" + date + "# ORDER BY TotalCost";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of prices from a price
        /// </summary>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of prices</returns>
        public string getPriceWithPrice(string price) {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE TotalCost = " + price + " ORDER BY TotalCost";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of prices from an ID and a date
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="date">Invoice date</param>
        /// <returns>A list of prices</returns>
        public string getPriceWithIDAndDate(int invoiceID, string date) {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND InvoiceDate = #" + date + "# ORDER BY TotalCost";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of prices from an ID and a price
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of prices</returns>
        public string getPriceWithIDAndPrice(int invoiceID, string price) {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND TotalCost = " + price + " ORDER BY TotalCost";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of prices from a date and a price
        /// </summary>
        /// <param name="date">Invoice date</param>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of prices</returns>
        public string getPriceWithDateAndPrice(string date, string price) {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceDate = #" + date + "# AND TotalCost = " + price + " ORDER BY TotalCost";

                return sSQL;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Gets a list of prices from an ID, date, and price
        /// </summary>
        /// <param name="invoiceID">Invoice ID</param>
        /// <param name="date">Invoice date</param>
        /// <param name="price">Invoice total cost</param>
        /// <returns>A list of prices</returns>
        public string getPriceWithIDAndDateAndPrice(int invoiceID, string date, string price) {
            try {
                sSQL = "SELECT DISTINCT TotalCost FROM Invoices WHERE InvoiceNum = " + invoiceID + " AND InvoiceDate = #" + date + "# AND TotalCost = " + price + " ORDER BY TotalCost";

                return sSQL;
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
