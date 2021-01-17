using ServiceExpress.BLL;
using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class RetrievePOforDO : System.Web.UI.Page
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

            DeliveryOrderWSBLL deliverybll = new DeliveryOrderWSBLL();

            gvPurchaseOrders.DataSource = deliverybll.getAllDelivery();

            gvPurchaseOrders.DataBind();

        }

        protected void gvPurchaseOrders_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            //    int selectedRow = gvPurchaseOrders.SelectedIndex;
            //  GridViewRow row = gvPurchaseOrders.Rows[selectedRow];

            var lb = (ImageButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label purOrderNo = null;
            if (row != null)
            {
                purOrderNo = row.FindControl("deliveryNo") as Label;
            }
            string purOrderId = purOrderNo.Text;
            DeliveryOrderWSBLL deliverybll = new DeliveryOrderWSBLL();

            string status = "Shipped";
            int result = deliverybll.UpdateDeliveryOrder(purOrderId, status);
            if (result > 0)
            {
                BindGridView();
            }
        }
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            var lb = (ImageButton)sender;
            var row = (GridViewRow)lb.NamingContainer;
            Label purOrderNo = null;
            if (row != null)
            {
                purOrderNo = row.FindControl("purOrderNo") as Label;
            }
            // string purOrderId = (string)gvPurchaseOrders.DataKeys[row.RowIndex].Value;
            string purOrderId = purOrderNo.Text;
            Response.Redirect("ViewProductsDo.aspx?purOrderNo=" + purOrderId);
        }

        protected void gvPurchaseOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex; //get new page
            gvPurchaseOrders.PageIndex = newPageIndex; //index to new page
            BindGridView();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DeliveryOrderWSDAL deldal = new DeliveryOrderWSDAL();
            gvPurchaseOrders.DataSource = deldal.getDeliveryByStatus(DropDownList1.SelectedValue);

            gvPurchaseOrders.DataBind();

        }

        protected void gvPurchaseOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  int stock_qty = Convert.ToInt32(e.Row.Cells[4].Text);


                string deliveryStatus = e.Row.Cells[5].Text;

                ImageButton img = (ImageButton)e.Row.FindControl("ImageButton1");

                if (deliveryStatus.Equals("Received"))
                {
                    img.Visible = false;
                }

                else if (deliveryStatus.Equals("Shipped"))
                {
                    img.Visible = false;
                }

                else
                {
                    img.Visible = true;

                }

            }
        }
    }
}