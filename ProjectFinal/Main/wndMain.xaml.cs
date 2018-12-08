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
using wndSearch;

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



        List<clsInvoiceItems> lstInvoiceItems;

        


        /// <summary>
        /// 
        /// </summary>
        //bool bIsDeleting;





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















            /*
            //get the lstInvoice from MainLogic and pass it into our new list called newLstInvoice
            newLstInvoice = MainLogic.GetInvoice();

            //bind the data from our new list onto our datagrid called dataGridView
            dataGridView1.ItemsSource = newLstInvoice;
            */
            create_btn.IsEnabled = false;
            editItem_btn.IsEnabled = false;
            addItem_button.IsEnabled = false;
            delete_btn.IsEnabled = false;

        }
        #endregion













        #region Delete
        /// <summary>
        /// Deletes selected item from data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {





















            /*
            if (dataGridView1.SelectedIndex < newLstInvoice.Count && dataGridView1.SelectedItem != null)
            {
                //create and instanitate a new Invoice object 
                clsInvoice Invoice = new clsInvoice();

                //set the new object called Invoice to the selected item in the datagrid called dataGridView
                Invoice = (clsInvoice)dataGridView1.SelectedItem;

                bIsDeleting = true;

                //delete the Line item at the current selected item. 
                MainLogic.DeleteLineItemSQL(Invoice.iInvoiceNum);

                //delete the Invoice at the current selected item.
                MainLogic.DeleteInvoiceSQL(Invoice.iInvoiceNum);


                //set the new list called newLstInvoice to get the lstInvoice that will return the data in the dataset. 
                newLstInvoice = MainLogic.GetInvoice();

                //bind the data from our new list into the datagrid called dataGridView
                dataGridView1.ItemsSource = newLstInvoice;

                bIsDeleting = false;

                //commits changes to database.
                MainLogic.ds.AcceptChanges();

                //refresh items in the datagrid called dataGridView
                dataGridView1.Items.Refresh();
            }
            */
        }
        #endregion

        private void dataGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {




















            /*
            newLstLineItems.Clear();
            dataGridView2.Items.Refresh();
            //checks to make sure that we cannot select a row without invoice data. 
            if (dataGridView1.SelectedIndex < newLstInvoice.Count && bIsDeleting == false)
            {

                clsInvoice Invoice = new clsInvoice();
                Invoice = (clsInvoice)dataGridView1.SelectedItem;

                newLstLineItems = MainLogic.GetLineItems(Invoice.iInvoiceNum);
                dataGridView2.ItemsSource = newLstLineItems;


                dataGridView2.Columns[1].Visibility = System.Windows.Visibility.Hidden;

            }
            */

        }





        private void DataGridView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
























            /*
            newLstItemDesc.Clear();
            dataGridView3.Items.Refresh();
            if (dataGridView2.SelectedIndex < newLstLineItems.Count && bIsDeleting == false)
            {
                clsLineItems LineItems = new clsLineItems();
                LineItems = (clsLineItems)dataGridView2.SelectedItem;

                for (int i = 0; i < newLstLineItems.Count; i++)
                {

                    newLstItemDesc = MainLogic.GetItemDesc(LineItems.sItemCode);


                }

                dataGridView3.ItemsSource = newLstItemDesc;
            }
            */

        }



        #region Search - MenuItem_Click()
        /// <summary>
        /// Menu -> File -> Search - Opens "search" window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {



            //open "search" window. 
            
            this.Hide();

            MainWindow searchWindow = new MainWindow();
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

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            MainLogic.CreateRow();

            //user may enter data pertaining to that invoice. 
            //auto generated number from the database is given for the invoice number. 
            //invoice date will be assigned by the user. - use date control to make life easier. 
            //different items will be entered by the user. Items will be selected from a dropdown box and the cost for that item will be put into a read only textbox which is default cost of the item.
            //once item is selected user can add that item, as many items as needed should be able to add to invoice. 
            //all items entered should be displayed in a list on something like a datagrid, items may be deleted from that list, a running total of cost will be displayed as items are added or deleted. 
            //save invoice, lock data in invoice for viewing only, save all data to database
            //user may choose to edit invoice or delete.


        }

        private void items_cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemCost_txtbox.Text = MainLogic.lstItemDesc[items_cbo.SelectedIndex].sCost;
        }

        private void testEnterInvoice_Click(object sender, RoutedEventArgs e)
        {

            addItem_button.IsEnabled = true;
            //string s;
            //testing - automatically select an invoice... 

            MainLogic.GetInvoice(5000);

            invoiceNum_txtbox.Text = MainLogic.lstInvoice[0].iInvoiceNum.ToString();

            invoiceData_txtbox.Text = MainLogic.lstInvoice[0].sInvoiceDate.ToString();

            invoiceCost_txtbox.Text = MainLogic.lstInvoice[0].sTotalCost.ToString();

            MainLogic.GetItemDesc();

            for (int i = 0; i < MainLogic.lstItemDesc.Count; i++)
            {
                items_cbo.Items.Add(MainLogic.lstItemDesc[i].sItemDesc);
            }
        }

        private void addItem_button_Click(object sender, RoutedEventArgs e)
        {
            //add to the list - maybe create a list.

            //lstInvoiceItems[0].sItems = items_cbo.SelectedItem.ToString();

            lstInvoiceItems.Add(new clsInvoiceItems { sItems = items_cbo.SelectedItem.ToString(), sCost = MainLogic.lstItemDesc[items_cbo.SelectedIndex].sCost});

            dataGridView1.ItemsSource = lstInvoiceItems;

            dataGridView1.Items.Refresh();
        }
    }
}
