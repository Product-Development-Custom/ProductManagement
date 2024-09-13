using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace ProductManagement
{
    public partial class Product : System.Web.UI.Page
    {
        public SqlConnection con = new SqlConnection("Data Source=DESKTOP-6SRC66K\\SQLEXPRESS;Initial Catalog=Product;Integrated Security=True;TrustServerCertificate=True");
        public static int productId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                DispalyRecords();
        }
        private void DispalyRecords()
        {
            SqlCommand cmd = new SqlCommand("FetchData", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Action", "SELECT");
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            productId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "EditProduct")
            {
                SqlCommand cmd = new SqlCommand("Fetchupdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UPDATEID", productId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    txt_ProductName.Text = dr["ProductName"].ToString();
                    txt_description.Text = dr["ProductDescription"].ToString();
                    txt_c_name.Text = dr["CategoryName"].ToString();
                    txt_Username.Text = dr["Username"].ToString();
                }

            }
            else if (e.CommandName == "DeleteProduct")
            {
                SqlCommand cmd = new SqlCommand("FetchData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Action", "DELETE");
                cmd.Parameters.Add("@UPDATEID", productId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                DispalyRecords();
                productId = 0;
                Clear();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Deleted Successfully')", true);
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string ProductName = txt_ProductName.Text;
            string ProductDescription = txt_description.Text;
            string CategoryName = txt_c_name.Text;
            string Username = txt_Username.Text;
            if (Validatetxt())
            {
                if (productId != 0)
                {
                    SqlCommand cmd = new SqlCommand("FetchData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", "UPDATE");
                    cmd.Parameters.Add("@UPDATEID", productId);
                    cmd.Parameters.Add("@ProductName", ProductName);
                    cmd.Parameters.Add("@ProductDescription", ProductDescription);
                    cmd.Parameters.Add("@CategoryName", CategoryName);
                    cmd.Parameters.Add("@UserName", Username);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DispalyRecords();
                    productId = 0;
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Updated Successfully')", true);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("FetchData", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Action", "INSERT");
                    cmd.Parameters.Add("@ProductName", ProductName);
                    cmd.Parameters.Add("@ProductDescription", ProductDescription);
                    cmd.Parameters.Add("@CategoryName", CategoryName);
                    cmd.Parameters.Add("@UserName", Username);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    DispalyRecords();
                    Clear();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter All Details')", true);
            }
        }
        protected void btn_Search_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Search.Text))
            {
                string Search = txt_Search.Text;
                SqlCommand cmd = new SqlCommand("SearchProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Username_Category", Search);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Username Or Category')", true);

            }
        }
        private void Clear()
        {
            txt_ProductName.Text = string.Empty;
            txt_description.Text = string.Empty;
            txt_c_name.Text = string.Empty;
            txt_Username.Text = string.Empty;
        }

        protected void btn_Load_Click(object sender, EventArgs e)
        {
            DispalyRecords();
        }
        private bool Validatetxt()
        {
            if (!string.IsNullOrEmpty(txt_ProductName.Text) && !string.IsNullOrEmpty(txt_description.Text) &&
                   !string.IsNullOrEmpty(txt_c_name.Text) && !string.IsNullOrEmpty(txt_Username.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}