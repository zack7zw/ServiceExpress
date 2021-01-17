using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class DailySalesReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string date = "2/10/2015" ;
                bindchart(date);
                BindAjaxChart(date);
             
            }
        }


        private DataSet getDataSource(string date)
        {
            PurchaseOrderWS pobll = new PurchaseOrderWS();
            DataSet ds = new DataSet();
            ds = pobll.dailySales(date);
      
            return ds;
        }
        protected void bindchart(string date)
        {
            SqlDataSource1.SelectCommand = "SELECT Product.prodName 'Product Name', sum(PurOrderItem.price) 'Unit Price'" +
           " FROM PurOrderItem INNER JOIN" +
                   " Product ON PurOrderItem.prodID = Product.prodID" +
                    " INNER JOIN" +
                   " PurOrder ON PurOrderItem.purOrderNo = PurOrder.purOrderNo" +
                       " WHERE (PurOrder.orderDate = '" + date + "') " +
                       "Group by Product.prodName";

        }
        protected void BindAjaxChart(string date)
        {

            DataSet dt = getDataSource(date);
            decimal totalPrice = 0;
            decimal values = 0;
            decimal price = 0;
            DataTable ds = dt.Tables[0];
            for (int i = 0; i < ds.Rows.Count; i++)
            {

                values = Convert.ToDecimal(ds.Rows[i]["price"]);
                price = Convert.ToDecimal(ds.Rows[i]["quantity"]);
                totalPrice = totalPrice + (values * price);

            }
            lblTotalPrice.Text = "$"+totalPrice.ToString();
            GVDailySales.DataSource = dt;

            GVDailySales.DataBind();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToShortDateString();
            DataSet ds = getDataSource(date);
            DataTable dt = ds.Tables[0];
            Document document = new Document(PageSize.A4, 50, 50, 25, 25);

            // Create a new PdfWrite object, writing the output to a MemoryStream
            
                PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);


            using (MemoryStream memoryStream = new MemoryStream())
            {
                document.Open();


                // Open the Document for writing

                //   document.Open();

                // First, create our fonts... (For more on working w/fonts in iTextSharp, see: http://www.mikesdotnetting.com/Article/81/iTextSharp-Working-with-Fonts
                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

                // Add the "Northwind Traders Receipt" title
                document.Add(new Paragraph("Daily Sales Report", titleFont));

                // Now add the "Thank you for shopping at Northwind Traders. Your order details are below." message
                document.Add(new Paragraph("Fly Asia daily Sales Report Generated", bodyFont));
                document.Add(Chunk.NEWLINE);

                // Add the "Order Information" subtitle
                document.Add(new Paragraph("Product Sales", subTitleFont));


                // Create the Order Details table
                var orderDetailsTable = new PdfPTable(6);
                orderDetailsTable.HorizontalAlignment = 0;
                orderDetailsTable.SpacingBefore = 10;
                orderDetailsTable.SpacingAfter = 35;
                orderDetailsTable.DefaultCell.Border = 0;

                orderDetailsTable.AddCell(new Phrase("Product #:", boldTableFont));
                orderDetailsTable.AddCell(new Phrase("Product Name:", boldTableFont));
                orderDetailsTable.AddCell(new Phrase("Quantity:", boldTableFont));
                orderDetailsTable.AddCell(new Phrase("Unit Price:", boldTableFont));
                orderDetailsTable.AddCell(new Phrase("Order Date:", boldTableFont));
                orderDetailsTable.AddCell(new Phrase("Purchase Order #:", boldTableFont));

                //change here
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    string prodID = dt.Rows[j]["prodID"].ToString();
                    string prodName = (dt.Rows[j]["prodName"].ToString());
                    string quantity = (dt.Rows[j]["quantity"].ToString());
                    string price = dt.Rows[j]["price"].ToString();
                    string orderDate = (dt.Rows[j]["orderDate"].ToString());
                    string purOrderNo = (dt.Rows[j]["purOrderNo"].ToString());

                    orderDetailsTable.AddCell(prodID);
                    orderDetailsTable.AddCell(prodName);
                    orderDetailsTable.AddCell(quantity);

                    orderDetailsTable.AddCell(price);
                    orderDetailsTable.AddCell(orderDate);
                    orderDetailsTable.AddCell(purOrderNo);

                }



                document.Add(orderDetailsTable);

                document.Add(new Paragraph("The total price for the sale :" + lblTotalPrice.Text, bodyFont));
                document.Add(Chunk.NEWLINE);

                /* // Create the Order Information table - see http://www.mikesdotnetting.com/Article/86/iTextSharp-Introducing-Tables for more info
                 document.Add(new Paragraph("Product Sales Chart", titleFont));

                 chartDailySales.SaveImage(memoryStream, ChartImageFormat.Png);
                 iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(memoryStream.GetBuffer());
                 chartImage.ScalePercent(75f);
                 document.Add(chartImage);*/

                // Add ending message
                var endingMessage = new Paragraph("Thank you for your business! If you have any questions about your order, please contact us at 800-555-NORTH.", endingMessageFont);
                endingMessage.SetAlignment("Center");
                document.Add(endingMessage);

                // Finally, add an image in the upper right corner
                var logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Images/LogoSvcExp.gif"));
                logo.SetAbsolutePosition(440, 800);
                document.Add(logo);

                document.Close();
                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=DailySalesReport-{0}.pdf", DateTime.Now));

                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);




                Response.Write(document);
                Response.End();
            }
        }
    }
}