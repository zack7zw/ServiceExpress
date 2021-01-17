using AjaxControlToolkit;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;


namespace ServiceExpress
{
    public partial class InvoiceMonthlyReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BindAjaxChart(string startDate, string endDate)
        {
            LineChart2.Style["display"] = "block";
            DataTable dt = getData(startDate, endDate);
            string category = "";
            decimal[] values = new decimal[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                category = category + "," + dt.Rows[i]["Invoice Date"].ToString();
                values[i] = Convert.ToDecimal(dt.Rows[i]["Total Price"]);

            }

            LineChart2.CategoriesAxis = category.Remove(0, 1);
            LineChart2.Series.Add(new AjaxControlToolkit.LineChartSeries { Name = "Total Price", Data = values });
            //    LineChart2.ChartTitle = string.Format("try");

            //    LineChart2.Series.Add(new LineChart { Data = price, BarColor = "#2fd1f9", Name = "Reorder Quantity" });
            //    Label1.Text = values[0].ToString() ;
            //   Label2.Text = values[1].ToString();
        }
        protected DataTable getData(string startDate, string endDate)
        {
            DalDbConn dbConn = new DalDbConn();
            SqlConnection myConnect = dbConn.GetConnection();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            myConnect.Open();
            string cmdstr = "select CONVERT(varchar, invoiceDate, 103) 'Invoice Date', sum(grandTotal) 'Total Price' from Invoice " +
            "where invoiceDate between @startDate and @endDate  group by invoiceDate";
            SqlCommand cmd = new SqlCommand(cmdstr, myConnect);
            cmd.Parameters.AddWithValue("@startDate", startDate);
            cmd.Parameters.AddWithValue("@endDate", endDate);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            dt = ds.Tables[0];
            return dt;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            BindAjaxChart(txtDOB.Text, TextBox1.Text);
            bindchart(txtDOB.Text, TextBox1.Text);
            Button1.Visible = true;
        }
        protected void btnPDF_Click(object sender, EventArgs e)
        {
            Chart1.Style["display"] = "block";
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            using (MemoryStream stream = new MemoryStream())
            {
                Chart1.SaveImage(stream, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                chartImage.ScalePercent(75f);
                pdfDoc.Add(chartImage);
            }
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" +
                                           "filename=DailySalesChart.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
            Chart1.Style["display"] = "none";
        }
        protected void bindchart(string startDate, string endDate)
        {

            SqlDataSource1.SelectCommand = 
            "select CONVERT(varchar, invoiceDate, 103) 'Invoice Date', sum(grandTotal) 'Total Price' from Invoice " +
            "where invoiceDate between  '" + startDate + "' and '" + endDate + "' group by invoiceDate";

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Chart1.Style["display"] = "block";
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            using (MemoryStream stream = new MemoryStream())
            {
                Chart1.SaveImage(stream, ChartImageFormat.Png);
                iTextSharp.text.Image chartImage = iTextSharp.text.Image.GetInstance(stream.GetBuffer());
                chartImage.ScalePercent(75f);
                pdfDoc.Add(chartImage);
            }
            pdfDoc.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" +
                                           "filename=DailySalesChart.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
            Chart1.Style["display"] = "none";
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Invoice.aspx");
        }
    }
}