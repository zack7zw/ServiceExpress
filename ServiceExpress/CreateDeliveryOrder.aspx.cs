using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using ServiceExpress.BLL;
using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class CreateDeliveryOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string pur = Request.QueryString["purOrderNo"];
                BindDeliveryList(pur);
                bindlabels(pur);
            }

        }

        private void bindlabels(string pur)
        {
            lblDeliveryid.Text = "DO00" + GeneratePW.Generate(3);
            DeliveryOrderWSDAL mydelivery = new DeliveryOrderWSDAL();

            string[] storevalue = new string[4];
            storevalue = mydelivery.getAllLabels(pur);

            lblPerson.Text = storevalue[0];
            lblAdd.Text = storevalue[1];
            lblContact.Text = storevalue[2];
            lblOrderDate.Text = storevalue[3];

        }
        private void BindDeliveryList(string pur)
        {
            DeliveryOrderWSBLL mydelivery = new DeliveryOrderWSBLL();
            DataSet ds;
            lblOrderID.Text = pur;
            Session["pur"] = pur;
            ds = mydelivery.getAllPurchaseProducts(pur);
            GvDelivery.DataSource = ds;
            GvDelivery.DataBind();
        }

        protected void BtnCreate_Click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {


                string deliveryStatus = "Processing";
                DateTime deliveryDate = Convert.ToDateTime(tbDate.Text);
                //ddlEventType.SelectedItem.Text;
                string purOrderNo = Session["pur"].ToString();
                string deliverySuppName = tbPersonnel.Text;
                // insert record
                string deliveryNo = lblDeliveryid.Text;

                DeliveryOrderWSBLL deliverybll = new DeliveryOrderWSBLL();

                PurchaseOrderWS poUpdate = new PurchaseOrderWS();
                int resultUpdate = poUpdate. updateStatusWS( "Delivery Created",  purOrderNo);
                resultUpdate += poUpdate. updateStatusWS( "Delivery Created",  purOrderNo);
                int result = deliverybll.InsertDelivery(deliveryNo, deliveryStatus, deliveryDate, purOrderNo, deliverySuppName);
                if (result > 0)
                {
                    string display = "Record has been inserted successfully!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
                }
                else
                {
                    string display = "Record has been inserted fail!";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);

                }




            }

        }

        protected void GvDelivery_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string pur = Request.QueryString["purOrderNo"];
            int newPageIndex = e.NewPageIndex; //get new page
            GvDelivery.PageIndex = newPageIndex; //index to new page
            BindDeliveryList(pur);
        }

        public void pdf()
        {
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GvDelivery.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());

            HTMLWorker htmlparser = new HTMLWorker(document);
            PdfWriter.GetInstance(document, Response.OutputStream);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);


                document.Open();

                // First, create our fonts... (For more on working w/fonts in iTextSharp, see: http://www.mikesdotnetting.com/Article/81/iTextSharp-Working-with-Fonts
                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

                // Add the "Northwind Traders Receipt" title
                document.Add(new Paragraph("Service Express Delivery Order", titleFont));

                // Now add the "Thank you for shopping at Northwind Traders. Your order details are below." message


                // Add the "Order Information" subtitle
                document.Add(new Paragraph("Delivery Order Information", subTitleFont));

                // Create the Order Information table - see http://www.mikesdotnetting.com/Article/86/iTextSharp-Introducing-Tables for more info

                document.Add(new Paragraph("", subTitleFont));



                var DeliveryInfoTable = new PdfPTable(2);
                DeliveryInfoTable.HorizontalAlignment = 0;
                DeliveryInfoTable.SpacingBefore = 5;
                DeliveryInfoTable.SpacingAfter = 5;
                DeliveryInfoTable.DefaultCell.Border = 0;

                DeliveryInfoTable.SetWidths(new int[] { 1, 2});
                DeliveryInfoTable.AddCell(new Phrase("Delivery Order #:", boldTableFont));
                DeliveryInfoTable.AddCell(lblDeliveryid.Text);
                DeliveryInfoTable.AddCell(new Phrase("Customer's Name:", boldTableFont));
                DeliveryInfoTable.AddCell(lblPerson.Text);
                DeliveryInfoTable.AddCell(new Phrase("Customer's Address:", boldTableFont));
                DeliveryInfoTable.AddCell(lblAdd.Text);
                DeliveryInfoTable.AddCell(new Phrase("Customer's Contact #:", boldTableFont));
                DeliveryInfoTable.AddCell(lblContact.Text);
                DeliveryInfoTable.AddCell(new Phrase("Delivery Date:", boldTableFont));
                DeliveryInfoTable.AddCell(tbDate.Text);

                document.Add(DeliveryInfoTable);

                var Delivery1InfoTable = new PdfPTable(2);
                Delivery1InfoTable.HorizontalAlignment = 0;
                Delivery1InfoTable.SpacingBefore = 20;
                Delivery1InfoTable.SpacingAfter = 20;
                Delivery1InfoTable.DefaultCell.Border = 0;

                Delivery1InfoTable.SetWidths(new int[] { 1, 2 });
                Delivery1InfoTable.AddCell(new Phrase("Purchase Order #:", boldTableFont));
                Delivery1InfoTable.AddCell(lblOrderID.Text);
                
                Delivery1InfoTable.AddCell(new Phrase("Purchase Order Date:", boldTableFont));
                Delivery1InfoTable.AddCell(lblOrderDate.Text);

                document.Add(Delivery1InfoTable);


                // Add the "Items In Your Order" subtitle
                document.Add(new Paragraph("Items In Your Order", subTitleFont));


                // Create the Order Details table
                htmlparser.Parse(sr);

                document.Add(new Paragraph("Items received are in good conditions.", subTitleFont));
                document.Add(new Paragraph("", subTitleFont));

                var Delivery2InfoTable = new PdfPTable(2);
                Delivery2InfoTable.HorizontalAlignment = 0;
                Delivery2InfoTable.SpacingBefore = 5;
                Delivery2InfoTable.SpacingAfter = 5;
                Delivery2InfoTable.DefaultCell.Border = 0;

                Delivery2InfoTable.SetWidths(new int[] { 1, 2 });
                Delivery2InfoTable.AddCell(new Phrase("Receiver's Name:", boldTableFont));
                Delivery2InfoTable.AddCell("_________________");
                Delivery2InfoTable.AddCell(new Phrase("Receiver's Signature:", boldTableFont));
                Delivery2InfoTable.AddCell("_________________");
                Delivery2InfoTable.AddCell(new Phrase("Date Received:", boldTableFont));
                Delivery2InfoTable.AddCell("_________________");
                Delivery2InfoTable.AddCell(new Phrase("DeliveryMan:", boldTableFont));
                Delivery2InfoTable.AddCell(tbPersonnel.Text);




                document.Add(Delivery2InfoTable);
                //  htmlparser.Parse(sr);
                // Add ending message


                // Finally, add an image in the upper right corner
                var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/LogoSvcExp.gif"));
                logo.SetAbsolutePosition(440, 800);
                document.Add(logo);

                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=DeliveryOrder.pdf"));
                Response.BinaryWrite(memoryStream.ToArray());




                //document.Close();
                Response.Write(document);
                Response.End();
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
        protected void btnPdf_Click(object sender, EventArgs e)
        {
            try
            {
                pdf();

            }
            catch (Exception ex)
            {
                Label2.Text = ex.ToString();
            }

        }
    }
}