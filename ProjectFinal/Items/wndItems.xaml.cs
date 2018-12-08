using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace ProjectFinal.Items
{
    /// <summary>
    /// Interaction logic for wndItems.xaml
    /// </summary>
    public partial class wndItems : Window
    {
        /// <summary>
        /// A DataTable of Items pulled from the database.
        /// </summary>
        private DataTable itemList = new DataTable();

        /// <summary>
        /// Default Constructor
        /// </summary>
        public wndItems()
        {
            try
            { 
                InitializeComponent();

                DataColumn code = new DataColumn("Code", typeof(string));
                DataColumn cost = new DataColumn("Cost", typeof(string));
                DataColumn description = new DataColumn("Description", typeof(string));

                itemList.Columns.Add(code);
                itemList.Columns.Add(cost);
                itemList.Columns.Add(description);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// OnLoaded()
        /// 
        /// Called with the wndItems Loaded event
        /// </summary>
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                // Call clsItemsLogic.getItemList() to populate itemList when the window loads.
                clsItemsLogic.getItemList(ref itemList);

                // Bind itemList to the display
                listItemDisplay.ItemsSource = itemList.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// DataGrid_SelectionChanged()
        /// 
        /// Called when the user clicks on a new item in the item list.
        /// Populates the text fields in the edit item group box with the data
        /// from the newly selected item.
        /// Enables the user to edit the text in the fields in the edit item group box.
        /// Enables the edit item button.
        /// Enables the delete item button.
        /// </summary>
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Check that the newly selected row is not null!
                if (((DataGrid)(sender)).SelectedItem != null)
                {
                    // Update the text fields in the edit item area with data from the selected row
                    inputEditCost.Text = ((string)(((DataRowView)(((DataGrid)sender).SelectedItem)).Row[1]));
                    inputEditDescription.Text = ((string)(((DataRowView)(((DataGrid)sender).SelectedItem)).Row[2]));

                    // Allow tab stop for edit item text fields if still disabled
                    inputEditCost.IsTabStop = true;
                    inputEditDescription.IsTabStop = true;

                    // Disable isReadOnly for edit item text fields if still enabled
                    inputEditCost.IsReadOnly = false;
                    inputEditDescription.IsReadOnly = false;

                    // Enable Edit Item button
                    buttonEditItem.IsEnabled = true;

                    // Enable Delete Item button
                    buttonDeleteItem.IsEnabled = true;
                }
                else // No item is selected
                {
                    // Update the text fields in the edit item area to be blank
                    inputEditCost.Text = "";
                    inputEditDescription.Text = "";

                    // Disallow tab stop for edit item text fields
                    inputEditCost.IsTabStop = false;
                    inputEditDescription.IsTabStop = false;

                    // enable isReadOnly for edit item text fields
                    inputEditCost.IsReadOnly = true;
                    inputEditDescription.IsReadOnly = true;

                    // disable edit item button
                    buttonEditItem.IsEnabled = false;

                    // disable delete item button
                    buttonDeleteItem.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// ButtonAddItem_Click()
        /// 
        /// Called when the user clicks on the Add New Item button.
        /// Pulls text from the text fields in the Add Item Group box and passes them into clsItemsLogic.addItem()
        /// </summary>
        private void ButtonAddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                // Pass text from area code, cost and description textboxes to clsItemsLogic.addItem() static method          
                clsItemsLogic.addItem(inputAddItemCode.Text, inputAddCost.Text, inputAddDescription.Text);

                // Call clsItemsLogic.getItemList() to update itemList
                clsItemsLogic.getItemList(ref itemList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// ButtonEditItem_Click()
        /// 
        /// Called when the user clicks on the Edit Existing Item Button
        /// Confirms that the user wants to edit the existing item, then
        /// pulls the data from the text boxes in the Edit Item Group box and 
        /// the currently selected item from the datagrid then
        /// passes them to clsItemsLogic.updateItem()
        /// </summary>
        private void ButtonEditItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Display MessageBox to confirm the user wants to process the update
                MessageBoxResult userSelection = MessageBox.Show("Are you sure you want to perform this update?", "Confirm Item Edit", MessageBoxButton.YesNo);

                // On confirmation
                if (userSelection == MessageBoxResult.Yes)
                {
                    // Collect the item that has been selected from the datagrid
                    Item originalItem = new Item(((string)(((DataRowView)((listItemDisplay).SelectedItem)).Row[0])), 
                                                 ((string)(((DataRowView)((listItemDisplay).SelectedItem)).Row[1])), 
                                                 ((string)(((DataRowView)((listItemDisplay).SelectedItem)).Row[2])));

                    // Pass text from area cost and description textboxes, and the item selected in the list to clsItemsLogic.updateItem() static method
                    clsItemsLogic.updateItem(inputEditCost.Text, inputEditDescription.Text, originalItem);

                    // Call clsItemsLogic.getItemList() to update itemList
                    clsItemsLogic.getItemList(ref itemList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// ButtonDeleteItem_Click()
        /// 
        /// Called when the user clicks on the Delete Item Button
        /// Confirms that the user wants to delete the selected item
        /// Calls clsItemsLogic.checkForInvoices() to check for existing invoices, and
        /// displays a messagebox if invoices exist.
        /// If no invoices exist, calls clsItemsLogic.deleteItem() passing in the selected item.
        /// </summary>
        private void ButtonDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                /// <summary>
                /// listOfInvoices
                /// 
                /// Used to display to the user what invoices a given item is associated with
                /// as an item cannot be deleted if it associated with any invoices.
                /// </summary>
                List<string> listOfInvoices = new List<string>();

                // Collect the item that has been selected from the datagrid
                Item selectedItem = new Item(((string)(((DataRowView)((listItemDisplay).SelectedItem)).Row[0])),
                                             ((string)(((DataRowView)((listItemDisplay).SelectedItem)).Row[1])),
                                             ((string)(((DataRowView)((listItemDisplay).SelectedItem)).Row[2])));
                
                // Display System.Windows.MessageBox to confirm the deletion
                MessageBoxResult userSelection = MessageBox.Show("Are you sure you want to delete this item: " + selectedItem.ToString() + "?", "Confirm Item Edit", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (userSelection == MessageBoxResult.Yes)
                {
                    // On confirmation
                    // Pass listOfInvoices and the selected item to clsItemsLogic.checkForInvoices() - Check returned value
                    if (clsItemsLogic.checkForInvoices(ref listOfInvoices, selectedItem))
                    {
                        // True = Invoices were found!

                        // Use listOfInvoices to display which invoices are involved.
                        string itemList = listOfInvoices[0];
                        for(int x = 1; x < listOfInvoices.Count; x++)
                        {
                            itemList = itemList + ", " + listOfInvoices[x];
                        }

                        // Display a message box informing the user that invoices exist with that item.
                        MessageBox.Show("The following invoices contain the selected item. Unable to delete item unless it is not contained in any invoices.\n" + itemList, "Unable to procede with item deletion!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        // False = No invoices exist. Delete the item

                        // Pass item selected in the list to clsItemsLogic.deleteItem()
                        clsItemsLogic.deleteItem(selectedItem);

                        // Call clsItemsLogic.getItemList() to update itemList
                        clsItemsLogic.getItemList(ref itemList);
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// ButtonExit_Click()
        /// 
        /// Called when the user clicks on the return button.
        /// Closes the item window
        /// </summary>
        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
