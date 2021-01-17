using ServiceExpress.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class CustomerOrder1 : System.Web.UI.Page
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

            gvPO0.Style["display"] = "block";

            gvPO0.DataSource = getDataSource();

            gvPO0.DataBind();

        }
        private DataTable getDataSource()
        {
            PurchaseOrderWSBLL pobll = new PurchaseOrderWSBLL();
            DataTable dt = new DataTable();
            dt = pobll.poall();


            // string date = dt.Rows[0]["startDate"].ToString();
            //    date = DateTime.Parse(date).ToString("yyyy-MM-dd");

            return dt;
        }

        protected void gvPO_SelectedIndexChanged(object sender, EventArgs e)
        {

            string purOrderID = gvPO.SelectedRow.Cells[0].Text;
            string status = gvPO.SelectedRow.Cells[5].Text;
            if (status.Equals("Create") || status.Equals("Process Updated"))
            {
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
                    ModalPopupExtender1.Show();

                }
            }
            else
            {
                lblResultPO.Text = "unable to acknowledge Fail"; ModalPopupExtender1.Show();
            }


        }
        private DataTable getDataSourceSearch()
        {
            string status = ddlStatus.SelectedValue;
            string startDate = txtPOFromDate.Text;
            string endDate = txtPOEndDate.Text;
            string Users = ddlUsers.SelectedValue;

            PurchaseOrderWSBLL pobll = new PurchaseOrderWSBLL();
            DataTable dt = new DataTable();
            dt = pobll.searchPOall(status, startDate, endDate, Users);
            int count = dt.Rows.Count;

            if (status.Equals("Create") || status.Equals("Process Updated"))
            {
                gvPO.DataSource = dt;
                gvPO.DataBind();
                gvPO.Style["display"] = "block";
            }
            else
            {
                gvPO0.DataSource = dt;
                gvPO0.DataBind();
                gvPO0.Style["display"] = "block";
            }


            // string date = dt.Rows[0]["startDate"].ToString();
            //    date = DateTime.Parse(date).ToString("yyyy-MM-dd");

            return dt;
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvPO.Style["display"] = "none";
            gvPO0.Style["display"] = "none";
            getDataSourceSearch();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            gvPO_ModalPopupExtender.Show();
        }

        protected void gvPO0_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex; //get new page
            gvPO0.PageIndex = newPageIndex; //index to new page



            if (!txtPOEndDate.Equals("") || (!ddlStatus.Equals("Please Select Status")) || (!ddlUsers.Equals("Please Select Users")))
            {
                getDataSourceSearch();
            }
            else
            {
                BindGridView();
            }



        }

        protected void gvPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex; //get new page
            gvPO.PageIndex = newPageIndex; //index to new page

            if (!txtPOEndDate.Equals("") || (!ddlStatus.Equals("Please Select Status")) || (!ddlUsers.Equals("Please Select Users")))
            {
                getDataSourceSearch();
            }
            else
            {
                BindGridView1();
            }
        }
        private void BindGridView1()
        {

            gvPO.DataSource = getDataSource();

            gvPO.DataBind();
            gvPO.Style["display"] = "block";

        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("DailySalesReport.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("POdailySaleGraph.aspx");
        }
    }
}