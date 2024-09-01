using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Form2
{
    public partial class WebForm2 : System.Web.UI.Page

    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))


                {
                    connection.Open();
                    string query = "SELECT * FROM FormData";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                }
            }
        }
        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
        Boolean validateForm()
        {
            if (TextBox1.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give first Name";
                return false;
            }
            else if (TextBox3.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give last Name";
                return false;
            }
            else if (TextBox4.Text.Length != 10)
            {
                MessageLabel.Text = "Phone Number must be of length 10";
                return false;
            }
            else if (TextBox5.Text.Length == 0)
            {
                MessageLabel.Text = "Please give Date Of Birth";
                return false;
            }
            else if (TextBox6.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give your email";
                return false;
            }
            else if (TextBox7.Text.Length == 0)
            {
                MessageLabel.Text = "Please Give your address";
                return false;
            }
            else if (!RadioButton1.Checked && !RadioButton2.Checked)
            {
                MessageLabel.Text = "Please give your gender";
                return false;
            }
            else if (DropDownList1.SelectedIndex == 0)
            {
                MessageLabel.Text = "Please slect a stream";
                return false;
            }
            return true;
        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (!validateForm())
            {
                return;
            }
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            SqlConnection con = new SqlConnection(sqlConnectionString);

            try
            {

                string query = @"
               INSERT INTO [dbo].[FormData]
           ([First Name]
           ,[Middle Name]
           ,[Last Name]
           ,[Phone No.]
           ,[Date of Birth]
           ,[Email Id]
           ,[Address]
           ,[Sex]
           ,[Stream])
                VALUES (@FirstName, @MiddleName, @LastName, @PhoneNumber, @DateOfBirth , @EmailID, @Address, @Sex, @Stream)";


                SqlCommand cmd = new SqlCommand(query, con);


                cmd.Parameters.AddWithValue("@FirstName", TextBox1.Text);
                cmd.Parameters.AddWithValue("@MiddleName", TextBox2.Text);
                cmd.Parameters.AddWithValue("@LastName", TextBox3.Text);
                cmd.Parameters.AddWithValue("@PhoneNumber", TextBox4.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(TextBox5.Text));

                cmd.Parameters.AddWithValue("@EmailID", TextBox6.Text);
                cmd.Parameters.AddWithValue("@Address", TextBox7.Text);
                string sex = "";
                if (RadioButton1.Checked)
                {
                    sex = "Male";
                }
                else if (RadioButton2.Checked)
                {
                    sex = "Female";
                }


                cmd.Parameters.AddWithValue("@Sex", sex);

                cmd.Parameters.AddWithValue("@Stream", DropDownList1.SelectedValue);


                con.Open();


                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageLabel.Text = "Successfully inserted!";

                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    TextBox3.Text = "";
                    TextBox4.Text = "";
                    TextBox5.Text = "";
                    TextBox6.Text = "";
                    TextBox7.Text = "";
                    RadioButton1.Checked = false;

                    RadioButton2.Checked = false;
                    DropDownList1.SelectedIndex = 0;
                }
                else
                {
                    MessageLabel.Text = "No rows were inserted. Please check your data.";
                }
            }
            catch (SqlException ex)
            {
                MessageLabel.Text = "An error occurred: " + ex.Message;
            }
            finally
            {

                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        private void ViewRecord(string firstName)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            using (SqlConnection
     conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM YourTableName WHERE FirstName = @FirstName", conn);
                cmd.Parameters.AddWithValue("@FirstName", TextBox1.Text);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Populate form fields with the retrieved data
                    TextBox1.Text = reader["FirstName"].ToString();
                    TextBox2.Text = reader["MiddleName"].ToString();
                    TextBox3.Text = reader["LastName"].ToString();
                    TextBox4.Text = reader["PhoneNo"].ToString();

                    TextBox5.Text = reader["DateOfBirth"].ToString();
                    TextBox6.Text = reader["EmailID"].ToString();
                    TextBox7.Text = reader["Address"].ToString();
                    RadioButton1.Checked = reader["Sex"].ToString() == "Male";
                    RadioButton2.Checked = reader["Sex"].ToString() == "Female";
                    DropDownList1.SelectedValue = reader["Stream"].ToString();
                    ButtonUpdate.Enabled = true;
                    ButtonDelete.Enabled = true;
                }
                else
                {

                    MessageLabel.Text = "Record not found.";
                    ButtonUpdate.Enabled = false;
                    ButtonDelete.Enabled = false;
                }

                reader.Close();
            }
        }


        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            
            string firstName = TextBox1.Text; 
            string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            using (SqlConnection
     conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE YourTableName SET MiddleName = @MiddleName, LastName = @LastName, PhoneNo = @PhoneNo, DateOfBirth = @DateOfBirth, EmailID = @EmailID, Address = @Address, Sex = @Sex, Stream = @Stream WHERE FirstName = @OldFirstName", conn);

                cmd.Parameters.AddWithValue("@MiddleName", TextBox2.Text);
                cmd.Parameters.AddWithValue("@LastName", TextBox3.Text);
                cmd.Parameters.AddWithValue("@PhoneNo",
 TextBox4.Text);
                cmd.Parameters.AddWithValue("@DateOfBirth", TextBox5.Text);
                cmd.Parameters.AddWithValue("@EmailID", TextBox6.Text);
                cmd.Parameters.AddWithValue("@Address", TextBox7.Text);
                cmd.Parameters.AddWithValue("@Sex", RadioButton1.Checked ? "Male" : "Female");
                cmd.Parameters.AddWithValue("@Stream", DropDownList1.SelectedValue);

                cmd.Parameters.AddWithValue("@OldFirstName", Request.QueryString["firstName"]); 

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageLabel.Text = "Record updated successfully.";
                }
                else
                {
                    MessageLabel.Text = "Error updating record.";
                }
            }
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            
            string firstName = Request.QueryString["firstName"];
            string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

            using (SqlConnection
     conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM YourTableName WHERE FirstName = @FirstName", conn);
                cmd.Parameters.AddWithValue("@FirstName", firstName);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageLabel.Text = "Record deleted successfully.";
                    Response.Redirect("WebForm2.aspx"); 
                }
                else
                {
                    MessageLabel.Text = "Error deleting record.";
                }
            }
        }
    }
}