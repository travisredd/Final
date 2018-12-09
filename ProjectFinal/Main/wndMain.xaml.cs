using ProjectFinal.Items;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Search;

namespace Main
{
    /// <summary>
    /// class logic for the window for the wndMain class
    /// </summary>
    public partial class wndMain : Window
    {

        #region Variables
        /// <summary>
        /// Create MainLogic object reference so that we can use that class in this main window, wndMain class. 
        /// </summary>
        clsMainLogic MainLogic;

        /// <summary>
        /// Create list object reference to pass a list of our invoices into
        /// </summary>
        List<clsInvoice> newLstInvoice;

        /// <summary>
        /// 
        /// </summary>
        List<clsLineItems> newLstLineItems;

        /// <summary>
        /// 
        /// </summary>
        List<clsItemDesc> newLstItemDesc;

        /// <summary>
        /// 
        /// </summary>
        List<clsInvoiceItems> lstInvoiceItems;

        /// <summary>
        /// 
        /// </summary>
        //bool bIsDeleting;

        bool bAddItem;//delete later


        #endregion


        #region MainWindow() - Constructor
        /// <summary>
        /// MainWindow() - Constructor
        /// </summary>
        public wndMain()
        {
            //loads window wndMain
            InitializeComponent();

            //To close application -  to be included in all WPF applications - makes sure other windows close and the objects that have been instantiated will be cleared from memory.
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            //instantiate MainLogic object so that we can use methods from the clsMainLogic class
            MainLogic = new clsMainLogic();

            //instantiate newLstInvoice object so that we can pass our list from MainLogic.lstInvoice into newLstInvoice
            newLstInvoice = new List<clsInvoice>();

            newLstLineItems = new List<clsLineItems>();

            newLstItemDesc = new List<clsItemDesc>();

            //^^^^^^^^Might not need the new list

            lstInvoiceItems = new List<clsInvoiceItems>();

            editItem_btn.IsEnabled = false;
            addItem_button.IsEnabled = false;
            deleteItem_btn.IsEnabled = false;

            items_cbo.IsEnabled = false;

            bAddItem = false;//delete later

            
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //to get the value of the selected item and delete or 

        }

        #region Search - MenuItem_Click()
        /// <summary>
        /// Menu -> File -> Search - Opens "search" window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {


            this.Hide();
            //open "search" window. 
            wndSearch searchWindow = new wndSearch();
            searchWindow.ShowDialog();

            this.Show();


        }
        #endregion


        #region Item - MenuItem_Click()
        /// <summary>
        /// Menu -> Edit -> Item - Opens "items" window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Hide();

            wndItems items = new wndItems();
            items.ShowDialog();

            this.Show();
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void items_cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //each combo box change we set the text inside the text box to the MainLogic.ListItemDesc index location of the selected combo box. 
            itemCost_txtbox.Text = MainLogic.lstItemDesc[items_cbo.SelectedIndex].sCost.ToString();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testEnterInvoice_Click(object sender, RoutedEventArgs e)//delete - just for testing purposes
        {
            UploadInvoice(5000);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addItem_button_Click(object sender, RoutedEventArgs e)
        {
            bAddItem = true;//delete later

            if (items_cbo.SelectedItem != null)
            {
                //create local var to hold each cost that we retrieve from the newLstItemDescs sCost and convert that string to int while we get the sum of the list.
                var iTotal = 0;


                newLstItemDesc.Add(new clsItemDesc { sItemDesc = items_cbo.SelectedItem.ToString(), sCost = MainLogic.lstItemDesc[items_cbo.SelectedIndex].sCost });
                //bind the data from newLstItemDesc to the datagrid
                dataGridView1.ItemsSource = newLstItemDesc;

                for (int t = 0; t < newLstItemDesc.Count; t++)
                {
                    iTotal += int.Parse(newLstItemDesc[t].sCost);
                    //set the text box Invoice Cost to the total added cost items
                    invoiceCost_txtbox.Text = "$" + iTotal.ToString();
                }

                /*
                MessageBoxResult result = MessageBox.Show("Save item: " + items_cbo.SelectedItem + " to invoice? ", "Save Item", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                }
                */

                //refresh the datagrid
                dataGridView1.Items.Refresh();
            }
        }

        private void EditInvoice_btn_Click(object sender, RoutedEventArgs e)
        {
            editItem_btn.IsEnabled = true;
            deleteItem_btn.IsEnabled = true;

            items_cbo.IsEnabled = true;
            addItem_button.IsEnabled = true;

            editInvoice_btn.Visibility = Visibility.Hidden;
            deleteInvoice_btn.Visibility = Visibility.Hidden;
            save_btn.Visibility = Visibility.Visible;

            invoiceNum_txtbox.IsReadOnly = false;
            invoiceData_txtbox.IsReadOnly = false;
            invoiceCost_txtbox.IsReadOnly = false;
        }

        private void EditItem_btn_Click(object sender, RoutedEventArgs e)
        {
            //goes to items page
            this.Hide();
            //open "search" window. 
            wndItems itemsWindow = new wndItems();
            itemsWindow.ShowDialog();

            this.Show();

        }

        #region Delete
        /// <summary>
        /// Deletes selected item from data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteInvoice_btn_Click(object sender, RoutedEventArgs e)
        {
            //delete invoice at current InvoiceNum
        }
        #endregion

        private void DeleteItem_btn_Click(object sender, RoutedEventArgs e)
        {
            //delete item in database at current InvoiceNum
        }

        public void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            //updates database at current InvoiceNum location with new list items
        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)/***************************Do NOW*******************************/
        {

            /**********************************************************************************************************/
            testEnterInvoice_btn.IsEnabled = false;

            create_btn.Visibility = Visibility.Hidden;
            save_btn.Visibility = Visibility.Visible;

            invoiceNum_txtbox.IsReadOnly = true;
            invoiceData_txtbox.IsReadOnly = false;
            invoiceCost_txtbox.IsReadOnly = true;

            invoiceNum_txtbox.Clear();
            invoiceData_txtbox.Clear();
            invoiceCost_txtbox.Clear();

            invoiceNum_txtbox.Text = "TBD";

            date_label.Visibility = Visibility.Visible;

            dataGridView1.ItemsSource = "";
            items_cbo.Items.Clear();

            addItem_button.IsEnabled = false;
            /************************************************************************************************************/

            //MainLogic.CreateRow();

            //user may enter data pertaining to that invoice. 
            //auto generated number from the database is given for the invoice number. 
            //invoice date will be assigned by the user. - use date control to make life easier. 
            //different items will be entered by the user. Items will be selected from a dropdown box and the cost for that item will be put into a read only textbox which is default cost of the item.
            //once item is selected user can add that item, as many items as needed should be able to add to invoice. 
            //all items entered should be displayed in a list on something like a datagrid, items may be deleted from that list, a running total of cost will be displayed as items are added or deleted. 
            //save invoice, lock data in invoice for viewing only, save all data to database
            //user may choose to edit invoice or delete.

        }/******************************************************************************************Do NOW*********************************************************************************/

        private void InvoiceData_txtbox_TextChanged(object sender, TextChangedEventArgs e)
        {

            items_cbo.IsEnabled = true;
            addItem_button.IsEnabled = true;

            MainLogic.GetItemDesc();

            for (int i = 0; i < MainLogic.lstItemDesc.Count; i++)
            {
                items_cbo.Items.Add(MainLogic.lstItemDesc[i].sItemDesc);
            }

            deleteItem_btn.IsEnabled = true;
            editItem_btn.IsEnabled = true;

        }

        private void InvoiceData_txtbox_GotFocus(object sender, RoutedEventArgs e)
        {
            date_label.Visibility = Visibility.Hidden;

            addItem_button.IsEnabled = false;
            items_cbo.IsEnabled = false;
        }

        public  void UploadInvoice(int iInvoiceNum)
        {
            //wndSearch Search = new wndSearch();
            if (bAddItem != true)//delete
            {

                /**************************************Testing - needs to go in another method and called in the constructor - when search sends InvoiceNum, call this**************************************/
                items_cbo.IsEnabled = false;
                addItem_button.IsEnabled = false;

                create_btn.Visibility = Visibility.Hidden;
                editInvoice_btn.Visibility = Visibility.Visible;
                deleteInvoice_btn.Visibility = Visibility.Visible;

                //items_cbo.IsEnabled = true;
                //addItem_button.IsEnabled = true;

                MainLogic.GetInvoice(iInvoiceNum);//move thees to the constructor and pass in the InvoiceNum from "search" call these after the search screen selects the items. . . 
                newLstItemDesc = MainLogic.GetInvoiceItemDesc(iInvoiceNum);//move thees to the constructor and pass in the InvoiceNum from "search"
                dataGridView1.ItemsSource = newLstItemDesc;


                invoiceNum_txtbox.Text = MainLogic.lstInvoice[0].iInvoiceNum.ToString();

                invoiceData_txtbox.Text = MainLogic.lstInvoice[0].sInvoiceDate.ToString();

                invoiceCost_txtbox.Text = "$" + MainLogic.lstInvoice[0].sTotalCost.ToString();

                MainLogic.GetItemDesc();

                for (int i = 0; i < MainLogic.lstItemDesc.Count; i++)
                {
                    items_cbo.Items.Add(MainLogic.lstItemDesc[i].sItemDesc);
                }
                /**************************************Testing - needs to go in another method and called in the constructor - when search sends InvoiceNum, call this**************************************/
            }
        }
    }
}
