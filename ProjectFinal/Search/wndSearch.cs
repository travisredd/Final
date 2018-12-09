using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Search {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndSearch : Window {

        #region Attributes

        /// <summary>
        /// Allows other windows to know which invoice was selected by the user.
        /// </summary>
        public string sInvoiceNum;

        /// <summary>
        /// Gives public access to the clsSearchLogic class.
        /// </summary>
        private clsSearchLogic searchLogic = new clsSearchLogic();

        
        private DataTable invoiceList = new DataTable();

        #endregion



        #region Top Level Methods
        /// <summary>
        /// Default constructor
        /// </summary>
        public wndSearch() {
            try {

                InitializeComponent();

                // Populates the datagridview
                populateDataGrid();

                // Populates the combo boxes
                populateInvoiceID();
                populateInvoiceDate();
                populateInvoicePrice();

            }
            catch (Exception ex) {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Selects highlighted invoice.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSelect_Click(object sender, RoutedEventArgs e) {
            try {

                // Sets the selected invoice number to the public variable sInvoiceNum.
                    // This allows other windows to access which invoice was selected.

                // Closes the search window.
                this.Close();

                // Return to the main window.

            }
            catch (Exception ex) {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Cancels the search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCancel_Click(object sender, RoutedEventArgs e) {
            try {

                // Closes the search window.
                this.Close();

                // Return to the main window.

            }
            catch (Exception ex) {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Filters data based on invoice ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboxInvoiceID_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if(cmboxInvoiceID.SelectedIndex >= 0) {
                    // If neither a date or price is selected
                    if (cmboxInvoiceDate.SelectedIndex == -1 && cmboxInvoicePrice.SelectedIndex == -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), "", "");
                    } // If a date is selected
                    else if (cmboxInvoiceDate.SelectedIndex != -1 && cmboxInvoicePrice.SelectedIndex == -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), cmboxInvoiceDate.SelectedItem.ToString(), "");
                    } // If a price is selected 
                    else if (cmboxInvoiceDate.SelectedIndex == -1 && cmboxInvoicePrice.SelectedIndex != -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), "", cmboxInvoicePrice.SelectedItem.ToString());
                    } // If both a date and price is selected
                    else if (cmboxInvoiceDate.SelectedIndex != -1 && cmboxInvoicePrice.SelectedIndex != -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), cmboxInvoiceDate.SelectedItem.ToString(), cmboxInvoicePrice.SelectedItem.ToString());
                    }

                    // Populates combo boxes
                    populateInvoiceID();
                    populateInvoiceDate();
                    populateInvoicePrice();
                }
            }
            catch (Exception ex) {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Filters data based on date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboxInvoiceDate_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if(cmboxInvoiceDate.SelectedIndex >= 0) {
                    // If neither a ID or price is selected
                    if (cmboxInvoiceID.SelectedIndex == -1 && cmboxInvoicePrice.SelectedIndex == -1) {
                        // Limit data in the datagridview
                        populateDataGrid(-1, cmboxInvoiceDate.SelectedItem.ToString(), "");
                    } // If a ID is selected
                    else if (cmboxInvoiceID.SelectedIndex != -1 && cmboxInvoicePrice.SelectedIndex == -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), cmboxInvoiceDate.SelectedItem.ToString(), "");
                    } // If a price is selected 
                    else if (cmboxInvoiceID.SelectedIndex == -1 && cmboxInvoicePrice.SelectedIndex != -1) {
                        // Limit data in the datagridview
                        populateDataGrid(-1, cmboxInvoiceDate.SelectedItem.ToString(), cmboxInvoicePrice.SelectedItem.ToString());
                    } // If both a ID and price is selected
                    else if (cmboxInvoiceID.SelectedIndex != -1 && cmboxInvoicePrice.SelectedIndex != -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), cmboxInvoiceDate.SelectedItem.ToString(), cmboxInvoicePrice.SelectedItem.ToString());
                    }

                    // Populates combo boxes
                    populateInvoiceID();
                    populateInvoiceDate();
                    populateInvoicePrice();
                }
            }
            catch (Exception ex) {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Filters data based on amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmboxInvoicePrice_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            try {
                if(cmboxInvoicePrice.SelectedIndex >= 0) {
                    // If neither an ID or price is selected
                    if (cmboxInvoiceID.SelectedIndex == -1 && cmboxInvoiceDate.SelectedIndex == -1) {
                        // Limit data in the datagridview
                        populateDataGrid(-1, "", cmboxInvoicePrice.SelectedItem.ToString());
                    } // If an ID is selected
                    else if (cmboxInvoiceID.SelectedIndex != -1 && cmboxInvoiceDate.SelectedIndex == -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), "", cmboxInvoicePrice.SelectedItem.ToString());
                    } // If a date is selected 
                    else if (cmboxInvoiceID.SelectedIndex == -1 && cmboxInvoiceDate.SelectedIndex != -1) {
                        // Limit data in the datagridview
                        populateDataGrid(-1, cmboxInvoiceDate.SelectedItem.ToString(), cmboxInvoicePrice.SelectedItem.ToString());
                    } // If both an ID and price is selected
                    else if (cmboxInvoiceID.SelectedIndex != -1 && cmboxInvoiceDate.SelectedIndex != -1) {
                        // Limit data in the datagridview
                        populateDataGrid(Int32.Parse(cmboxInvoiceID.SelectedItem.ToString()), cmboxInvoiceDate.SelectedItem.ToString(), cmboxInvoicePrice.SelectedItem.ToString());
                    }

                    // Populates combo boxes
                    populateInvoiceID();
                    populateInvoiceDate();
                    populateInvoicePrice();
                }
            }
            catch (Exception ex) {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion


        #region Populate Methods

        /// <summary>
        /// Populates the datagridview
        /// </summary>
        private void populateDataGrid() {
            try {
                // Creates column names and adds it to the datagridview
                DataColumn invoiceID = new DataColumn("Invoice ID", typeof(int));
                DataColumn date = new DataColumn("Date", typeof(string));
                DataColumn price = new DataColumn("Price", typeof(string));

                invoiceList.Columns.Add(invoiceID);
                invoiceList.Columns.Add(date);
                invoiceList.Columns.Add(price);

                // Populates invoiceList
                searchLogic.getInvoices(ref invoiceList);

                // Binds invoiceList to the datagridview
                invoiceGrid.ItemsSource = invoiceList.DefaultView;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Populates a filtered datagridview
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <param name="date"></param>
        /// <param name="price"></param>
        private void populateDataGrid(int invoiceID, string date, string price) {
            try {
                // Clears the Invoice ID combo box
                cmboxInvoiceID.Items.Clear();

                // Populates invoiceList
                searchLogic.getInvoices(ref invoiceList, invoiceID, date, price);

                // Binds invoiceList to the datagridview
                invoiceGrid.ItemsSource = invoiceList.DefaultView;
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Populates the invoice ID combo box
        /// </summary>
        private void populateInvoiceID() {
            try {
                

                // Places invoice ID's into the invoice ID combo box
                for(int i = 0; i < invoiceList.Rows.Count; i++) {
                    cmboxInvoiceID.Items.Add(invoiceList.Rows[i][0].ToString());
                }
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Populates the invoice date combo box
        /// </summary>
        private void populateInvoiceDate() {
            try {
                // Clears the Invoice date combo box
                cmboxInvoiceDate.Items.Clear();

                // Places invoice dates into the invoice date combo box
                for(int i = 0; i < invoiceList.Rows.Count; i++) {
                    cmboxInvoiceDate.Items.Add(invoiceList.Rows[i][1].ToString());
                }
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Populates the invoice price combo box
        /// </summary>
        private void populateInvoicePrice() {
            try {
                // Clears the Invoice Price combo box
                cmboxInvoicePrice.Items.Clear();

                // Places invoice prices into the invoice price combo box
                for(int i = 0; i < invoiceList.Rows.Count; i++) {
                    cmboxInvoicePrice.Items.Add(invoiceList.Rows[i][2].ToString());
                }
            }
            catch (Exception ex) {
                //Just throw the exception
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion


        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage) {
            try {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex) {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}
