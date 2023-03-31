using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace CRUDOperationWithStoredProcedure
{
    public partial class CRUDProject : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetProductList();
            }
            
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-AHJIV92H;Initial Catalog=TestingDB;Integrated Security=True");

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(TextBox1.Text);
            string iname = TextBox2.Text, specialization = TextBox3.Text, unit = DropDownList1.SelectedValue;
            DateTime cdate = DateTime.Parse(TextBox6.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec ProductDetail '" + productId + "','" + iname + "','" + specialization + "','" + unit + "','" +  cdate + "'", con);

            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this,this.GetType(), "script", "alert(successfully inserted.');",true);

            GetProductList();
        }
        void GetProductList()
        {
            SqlCommand cmd = new SqlCommand("exec ProductList", con);
            SqlDataAdapter sd = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(TextBox1.Text);
            string iname = TextBox2.Text, specialization = TextBox3.Text, unit = DropDownList1.SelectedValue;
            DateTime cdate = DateTime.Parse(TextBox6.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("exec UpdateProduct_SP '" + productId + "','" + iname + "','" + specialization + "','" + unit + "','" + cdate + "'", con);

            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(successfully Updated.');", true);

            GetProductList();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(TextBox1.Text);
            
            con.Open();
            SqlCommand cmd = new SqlCommand("exec DeleteProduct_SP '" + productId + "'", con);

            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert(successfully Deleted.');", true);

            GetProductList();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int productId = int.Parse(TextBox1.Text);

            con.Open();
            SqlCommand cmd = new SqlCommand("exec SearchProduct_SP '" + productId + "'", con);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            GetProductList();
        }
    }
}