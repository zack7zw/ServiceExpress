using ServiceExpress.BLL;
using ServiceExpress.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceExpress
{
    public partial class ProdCatalogueGV : System.Web.UI.Page
    {
        ProductBLL pBLL = new ProductBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                /*  List<ProductDAL> pAll = new List<ProductDAL>();

                  pAll = pBLL.getAllProduct();

                  gvProduct.DataSource = pAll;
                  gvProduct.DataBind();*/

                BindGridView();
            }
        }

        private void BindGridView()
        {

            // TO DO: Complete codes to bind check box list to data reader
            gvProduct.DataSource = getReader();
            gvProduct.DataBind();
        }

        private DataTable getReader()
        {
            //get connection string from web.config
            string strConnectionString = ConfigurationManager.ConnectionStrings["ServiceExpressConnectionString"].ConnectionString;
            SqlConnection myConnect = new SqlConnection(strConnectionString);

            string strCommandText = "SELECT prodID, prodName, image, price, prodDescription, prodCategory from Product";

            SqlCommand cmd = new SqlCommand(strCommandText, myConnect);
            myConnect.Open();
            // CommandBehavior.CloseConnection will automatically close connection
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(reader);

            return dt;
        }


        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int newPageIndex = e.NewPageIndex; //get new page
            gvProduct.PageIndex = newPageIndex; //index to new page

            BindGridView();
        }


        protected void gvProduct_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            /*   List<ProductDAL> pAll = new List<ProductDAL>();

               pAll = pBLL.getAllProduct();

               gvProduct.DataSource = pAll;*/

            gvProduct.EditIndex = -1;
            //  gvProduct.DataBind();
            BindGridView();
        }

        protected void gvProduct_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ProductBLL pBLL = new ProductBLL();
            string prodID = (string)gvProduct.DataKeys[e.RowIndex].Value;
            pBLL.ProductDelete(prodID);
            Response.Redirect("ProdCatalogueGV.aspx");

        }

        protected void gvProduct_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //     List<ProductDAL> pAll = new List<ProductDAL>();

            //       pAll = pBLL.getAllProduct();
            //         gvProduct.DataSource = pAll;
            //           gvProduct.DataBind();

            gvProduct.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void gvProduct_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            //Get the new Values
            GridViewRow row = (GridViewRow)gvProduct.Rows[e.RowIndex];
            string prodIDD = gvProduct.DataKeys[e.RowIndex].Values["prodID"].ToString();

            TextBox tbname = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tbname");
            TextBox FileUpload1 = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("FileUpload1");
            TextBox tbprice = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tbprice");
            TextBox tbproddesc = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tbproddesc");

            TextBox tbcategory = (TextBox)gvProduct.Rows[e.RowIndex].FindControl("tbcategory");

            ProductBLL pBLL = new ProductBLL();


            string msg = pBLL.ProductUpdate(prodIDD, tbname.Text, FileUpload1.Text, Double.Parse(tbprice.Text), tbproddesc.Text, tbcategory.Text);


            //     List<ProductDAL> pAll = new List<ProductDAL>();

            //   pAll = pBLL.getAllProduct();

            //     gvProduct.DataSource = pAll;


            gvProduct.EditIndex = -1;
            BindGridView();
            //      gvProduct.DataBind(); 


        }

        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the currently selected row.
            GridViewRow row = gvProduct.SelectedRow;
            // Get Product ID from the selected row, which is the 
            // first row, i.e. index 0.
            string pID = row.Cells[0].Text;
            // Redirect to next page, with the Product Id added to the URL,
            Response.Redirect("ProdCatalogueGV.aspx?pID=" + pID);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<ProductDAL> pAll = new List<ProductDAL>();

            pAll = pBLL.getAllProductSearch(tbSearch.Text);

            gvProduct.DataSource = pAll;

            gvProduct.EditIndex = -1;
            gvProduct.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProdCatalogueCreate.aspx");
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            pnlPopUp.Visible = true;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            ProductBLL pBLL = new ProductBLL();

            string msg = pBLL.ProductInsert("P00" + CreateRandomID(), tbname.Text, FileUpload1.FileName, double.Parse(tbprice.Text), tbproddesc.Text, tbcategory.Text);

            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("~/Images/") + FileUpload1.FileName);
            }
            else
            {
                Response.Write("Please select file to upload");
            }

            Response.Redirect("ProdCatalogueGV.aspx");
        }

        public static string CreateRandomID()  //If you are always going to want 8 characters then there is no need to pass a length argument
        {
            string _allowedChars = "0123456789";
            Random randNum = new Random((int)DateTime.Now.Ticks);  //Don't forget to seed your random, or else it won't really be random
            char[] chars = new char[4];
            //again, no need to pass this a variable if you always want 8

            for (int i = 0; i < 4; i++)
            {
                chars[i] = _allowedChars[randNum.Next(_allowedChars.Length)];
                //No need to over complicate this, passing an integer value to Random.Next will "Return a nonnegative random number less than the specified maximum."
            }
            return new string(chars);
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
    }
}