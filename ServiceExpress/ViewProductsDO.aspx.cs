using ServiceExpress.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class ViewProductsDO : System.Web.UI.Page
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

            string purOrderNo = Request.QueryString["purOrderNo"];

            gvPurchaseOrders.DataSource = deliverybll.getPurchaseOrderForDo(purOrderNo);

            gvPurchaseOrders.DataBind();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("RetrievePOforDO.aspx");
        }
    }
}