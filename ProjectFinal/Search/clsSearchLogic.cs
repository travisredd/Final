using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        /// Used for datagrid.
        /// </summary>
        private DataTable dt;

        /// <summary>
        /// Used to hold the data.
        /// </summary>
        private DataSet ds;

        #endregion



        #region Methods

        /// <summary>
        /// Populates the datagrid.
        /// </summary>
        public void populateDategrid() {
            try {
                // Get query needed to find invoice.

                // Fill the dt up with the specified data.

                // Fill the datagrid.

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
