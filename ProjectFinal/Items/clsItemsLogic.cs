using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectFinal.Items
{
    /// <summary>
    /// A class containing static methods used with the wndItems.xaml.cs
    /// </summary>
    class clsItemsLogic
    {
        /*******************************
         * Public Static Methods
        *******************************/

        /// <summary>
        /// getItemList()
        /// 
        /// A method that takes a referenced datatable clears the rows of any existing data,
        /// and then fills in the rows based on information returned from the ItemDesc table
        /// Calls static method clsItemsSQL.itemList()
        /// </summary>
        public static void getItemList(ref DataTable dt)
        {
            try
            {
                // Clear out old data
                dt.Rows.Clear();

                // Create a DataSet to collect the information
                DataSet ds = new DataSet();

                // Create an int to capture the number of rows returned
                int iRet = 0;

                // Get DataSet from the items table using clsItemsSQL.itemList()
                ds = clsItemsSQL.itemList(ref iRet);

                // Loop through the values returned
                for (int i = 0; i < iRet; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow[0] = ds.Tables[0].Rows[i][0].ToString();
                    newRow[1] = ds.Tables[0].Rows[i][1].ToString();
                    newRow[2] = ds.Tables[0].Rows[i][2].ToString();

                    dt.Rows.Add(newRow);
                }

            } // Be prepared to Catch and Raise an Exception!
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// addItem()
        /// 
        /// Validates the input of string cost and string description
        /// If the validation is passed, calls clsItemsSQL.addItem() using
        /// the validated data.
        /// </summary>
        /// <param name="code">A string containing the unique item code for the new item. Must contain only 4 letters or digits</param>
        /// <param name="cost">A string containing the cost of the item. Should contain only numeric digits. May have a single '.' character if it is followed by exactly two digits</param>
        /// <param name="description">A string containing the description of the item. Should only contain letters, whitespace, and '/' or '-' characters.</param>
        public static void addItem(string code, string cost, string description)
        {
            try
            {
                /// <summary>
                /// Used to validate the input for a the item code in a new item
                /// </summary>
                Regex checkCode = new Regex("^[A-Z\\d][A-Z\\d]?[A-Z\\d]?[A-Z\\d]?$");

                // Validate input of code - Compare to regex ^[A-Z\d][A-Z\d]?[A-Z\d]?[A-Z\d]?$, one to four uppercase alphabet characters or digits
                // Throw new Exception if it is invalid
                if (!checkCode.IsMatch(code))
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + "The item code input is invalid. It must contain 1-4 alphabet characters or number digits.");
                }

                // Confirm that the code is not already included in the database
                    // Throw exception if is            
                if(getAllItemCodes().Contains(code))
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + "The item code input already exists in the database.");
                }

                // Validate input of cost - May throw exception
                validateCost(cost);

                // Validate input of description - May throw exception
                validateDescription(description);

                // Run clsItemsSQL.addItem() with the validated code, cost and description strings
                // Catch and Raise Exception if addItem() throws one.
                clsItemsSQL.addItem(cost, description, code);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// updateItem()
        /// 
        /// Validates the input of string cost and string description
        /// If the validation is passed, calls clsItemsSQL.updateItem() using
        /// the validated data and the passed in Item.
        /// </summary>
        /// <param name="newCost">A string containing the updated cost of the item. Should contain only numeric digits. May have a single '.' character if it is followed by exactly two digits</param>
        /// <param name="newDescription">A string containing the updated description of the item. Should only contain letters, whitespace, and '/' or '-' characters.</param>
        /// <param name="originalItem">The item whose cost and/or description are to be updated</param>
        public static void updateItem(string newCost, string newDescription, Item originalItem)
        {
            try
            {
                // Validate input of cost - May throw exception
                validateCost(newCost);

                // Validate input of description - May throw exception
                validateDescription(newDescription);

                // Run clsItemsSQL.updateItem() using the validated cost and description strings and the code from originalItem.
                clsItemsSQL.updateItem(originalItem.Code, newCost, newDescription);
                
                // TODO
                // Inform any open windows displaying invoice data that an update has occurred.
                // This will be done by updating a boolean value indicating that a change has been made
                // Then passing a list of invoice numbers affected by the change.
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// deleteItem()
        /// 
        /// Takes an Item argument and deletes the corresponding item from the database
        /// </summary>
        /// <param name="originalItem">The item to be deleted</param>
        public static void deleteItem(Item originalItem)
        {
            try
            {
                clsItemsSQL.deleteItem(originalItem);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// checkForInvoices()
        /// 
        /// Takes an item, compares it to the database and adds the invoice number of any invoices
        /// that contain the item to the referenced invoiceList.
        /// </summary>
        /// <param name="invoiceList">A list of invoice numbers. Invoices that contain the included item will be added to this list.</param>
        /// <param name="itemToCheck">The item to compare to the database.</param>
        /// <returns>True if invoices were found, false if none were found</returns>
        public static bool checkForInvoices(ref List<string> invoiceList, Item itemToCheck)
        {
            try
            { 
                /// <summary>
                /// Used with checkForInvoices()
                /// This will hold the number of invoices returned from the query
                /// </summary>
                int numberOfExistingInvoices = 0;

                /// <summary>
                /// Used with chekcForInvoices()
                /// This will hold any 
                /// </summary>
                DataSet ds = new DataSet();

                // call clsItemsSQL.checkForInvoices() to populate numberOfExistingInvoices and invoiceInformation.
                clsItemsSQL.checkForInvoices(ref ds, ref numberOfExistingInvoices, itemToCheck);

                // Check numberOfExistingInvoices
                if (numberOfExistingInvoices > 0)
                {
                    // If greater than 0 - Invoices exist
                    // Iterate through invoiceInformation and add the invoice numbers as strings to invoiceList;
                    for(int i = 0; i < numberOfExistingInvoices; i++)
                    {
                        invoiceList.Add(ds.Tables[0].Rows[i][0].ToString());
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /*******************************
         * Private Static Methods
        *******************************/

        /// <summary>
        /// Gets a list of all item codes in the ItemDesc database
        /// </summary>
        /// <returns>A list of all item codes, as strings</returns>
        private static List<string> getAllItemCodes()
        {
            try
            {
                List<string> returnThis = new List<string>();

                DataSet ds = new DataSet();
                int rows = 0;

                // Get list from database
                ds = clsItemsSQL.getItemCodes(ref rows);

                // Pass on to the list
                for(int x = 0; x < rows; x++)
                {
                    returnThis.Add(ds.Tables[0].Rows[x][0].ToString());
                }

                return returnThis;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Used to validate that the input cost is valid, and is safe to input into the database
        /// </summary>
        /// <param name="cost">The string to be validated</param>
        private static void validateCost(string cost)
        {
            try
            {
                /// <summary>
                /// Used to validate the input for the cost of an item
                /// </summary>
                Regex checkCost = new Regex("^[0-9]+(\\.[0-9][0-9])?$");

                // Validate input of cost
                // Throw new Exception if invalid
                if (!checkCost.IsMatch(cost))
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + "The cost input is invalid. It must contain only numeric digits");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Used to validate that the input cost is valid, and is safe to input into the database
        /// </summary>
        /// <param name="description">The string to be validated</param>
        private static void validateDescription(string description)
        {
            try
            {
                /// <summary>
                /// Used to validate the input for the description of an item
                /// </summary>
                Regex checkDescription = new Regex("^[A-Za-z\\/\\s\\-]+$");

                if(!checkDescription.IsMatch(description))
                {
                    throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + "The description input is invalid. It may only contain letters, spaces, '/' characters, and '-' characters");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
