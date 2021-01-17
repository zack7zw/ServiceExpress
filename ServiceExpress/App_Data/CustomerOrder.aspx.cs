using ServiceExpress.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class CustomerOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }
        private void BindGridView()
        {



            gvPO.DataSource = getDataSource();

            gvPO.DataBind();

        }
        private DataTable getDataSource()
        {
            PurchaseOrderWSBLL pobll = new PurchaseOrderWSBLL();
            DataTable dt = new DataTable();
            dt = pobll.poall();
            gvPO.DataSource = dt;
            gvPO.DataBind();

            // string date = dt.Rows[0]["startDate"].ToString();
            //    date = DateTime.Parse(date).ToString("yyyy-MM-dd");

            return dt;
        }

        protected void gvPO_SelectedIndexChanged(object sender, EventArgs e)
        {
            string purOrderID = gvPO.SelectedRow.Cells[0].Text;
            PurchaseOrderWSBLL pobll = new PurchaseOrderWSBLL();
            int result = pobll.updateStatusWS("Process Create", purOrderID);
            int result1 = pobll.updateStatus("Process Create", purOrderID);
            // Display the first name from the selected row.
            // In this example, the third column (index 2) contains
            // the first name.
            if ((result > 0) && (result1 > 0))
            {
                Response.Redirect("CreateDeliveryOrder.aspx?purOrderNo=" + purOrderID);
            }
            else
            {
                lblResultPO.Text = "Update Fail";

            }


        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string status = ddlStatus.SelectedValue;
            string startDate = txtPOFromDate.Text;
            string endDate = txtPOEndDate.Text;
            string Users = ddlUsers.SelectedValue;

            PurchaseOrderWSBLL pobll = new PurchaseOrderWSBLL();
            DataTable dt = new DataTable();
            dt = pobll.searchPOall(status, startDate, endDate, Users);
            gvPO.DataSource = dt;
            gvPO.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            gvPO_ModalPopupExtender.Show();
        }
    }
}