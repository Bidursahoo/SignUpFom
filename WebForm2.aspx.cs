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

           GetDataToFillGridView();
       }
   }


   void GetDataToFillGridView()
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
               GetDataToFillGridView();
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
       // Get the first name from the TextBox1
       string firstName = TextBox1.Text.Trim();

       // Retrieve the connection string from the Web.config file
       string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

       try
       {
           // Establish a connection to the database
           using (SqlConnection conn = new SqlConnection(connectionString))
           {
               // SQL command to update the record in the database
               string query = @"UPDATE FormData 
                        SET [Middle Name] = @MiddleName,
                            [Last Name] = @LastName,
                            [Phone No.] = @PhoneNo,
                            [Date of Birth] = @DateOfBirth,
                            [Email Id] = @EmailID,
                            [Address] = @Address,
                            [Sex] = @Sex,
                            [Stream] = @Stream
                        WHERE [First Name] = @OldFirstName";

               using (SqlCommand cmd = new SqlCommand(query, conn))
               {
                   // Add parameters with the values from the form fields
                   cmd.Parameters.AddWithValue("@MiddleName", TextBox2.Text.Trim());
                   cmd.Parameters.AddWithValue("@LastName", TextBox3.Text.Trim());
                   cmd.Parameters.AddWithValue("@PhoneNo", TextBox4.Text.Trim());
                   cmd.Parameters.AddWithValue("@DateOfBirth", Convert.ToDateTime(TextBox5.Text.Trim()));
                   cmd.Parameters.AddWithValue("@EmailID", TextBox6.Text.Trim());
                   cmd.Parameters.AddWithValue("@Address", TextBox7.Text.Trim());

                   // Determine the sex based on which radio button is selected
                   string sex = RadioButton1.Checked ? "Male" : "Female";
                   cmd.Parameters.AddWithValue("@Sex", sex);

                   // Set the stream value from the DropDownList
                   cmd.Parameters.AddWithValue("@Stream", DropDownList1.SelectedValue.Trim());

                   // The first name to identify the record to update
                   cmd.Parameters.AddWithValue("@OldFirstName", oldLabel.Text);

                   // Open the connection to the database
                   conn.Open();

                   // Execute the update command
                   int rowsAffected = cmd.ExecuteNonQuery();

                   // Check if the update was successful
                   if (rowsAffected > 0)
                   {
                       MessageLabel.Text = "Record updated successfully.";
                       GetDataToFillGridView();
                   }
                   else
                   {
                       MessageLabel.Text = "No record found with the provided First Name.";
                   }
               }
           }
       }
       catch (Exception ex)
       {
           // Handle any errors that occurred during the update process
           MessageLabel.Text = "Error updating record: " + ex.Message;
       }
   }


   protected void ButtonDelete_Click(object sender, EventArgs e)
   {
       // Retrieve the first name from the query string
       string firstName = oldLabel.Text;

       if (string.IsNullOrEmpty(firstName))
       {
           MessageLabel.Text = "No First Name provided.";
           return;
       }

       // Retrieve the connection string from the Web.config file
       string connectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

       try
       {
           // Establish a connection to the database
           using (SqlConnection conn = new SqlConnection(connectionString))
           {
               // SQL command to delete the record from the database
               string query = "DELETE FROM FormData WHERE [First Name] = @FirstName";

               using (SqlCommand cmd = new SqlCommand(query, conn))
               {
                   // Add parameter with the value from the query string
                   cmd.Parameters.AddWithValue("@FirstName", firstName);

                   // Open the connection to the database
                   conn.Open();

                   // Execute the delete command
                   int rowsAffected = cmd.ExecuteNonQuery();

                   // Check if the deletion was successful
                   if (rowsAffected > 0)
                   {
                       MessageLabel.Text = "Record deleted successfully.";
                       GetDataToFillGridView();
                   }
                   else
                   {
                       MessageLabel.Text = "No record found with the provided First Name.";
                   }
               }
           }
       }
       catch (Exception ex)
       {
           // Handle any errors that occurred during the deletion process
           MessageLabel.Text = "Error deleting record: " + ex.Message;
       }
   }

   protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
   {

   }
   protected void btnView_Click(object sender, EventArgs e)
   {
       // Get the GridViewRow that contains the clicked button
       GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;

       // Get the First Name from the selected row
       string firstName = row.Cells[1].Text;

       if (!string.IsNullOrEmpty(firstName))
       {
           // Fetch data from the database
           FetchAndBindData(firstName);

           // Enable the Update and Delete buttons
           ButtonUpdate.Enabled = true;
           ButtonDelete.Enabled = true;
       }
   }

   private void FetchAndBindData(string firstName)
   {
       string connectionString = WebConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

       using (SqlConnection conn = new SqlConnection(connectionString))
       {
           string query = "SELECT [First Name], [Middle Name], [Last Name], [Phone No.], [Date of Birth], [Email Id], [Address], [Sex], [Stream] FROM [FormData] WHERE [First Name] = @FirstName";

           using (SqlCommand cmd = new SqlCommand(query, conn))
           {
               cmd.Parameters.AddWithValue("@FirstName", firstName);

               conn.Open();
               SqlDataReader reader = cmd.ExecuteReader();

               if (reader.Read())
               {
                   // Bind data to form controls
                   TextBox1.Text = reader["First Name"].ToString();
                   oldLabel.Text = reader["First Name"].ToString();
                   TextBox2.Text = reader["Middle Name"].ToString();
                   TextBox3.Text = reader["Last Name"].ToString();
                   TextBox4.Text = reader["Phone No."].ToString();
                   TextBox5.Text = Convert.ToDateTime(reader["Date of Birth"]).ToString("yyyy-MM-dd");
                   TextBox6.Text = reader["Email Id"].ToString();
                   TextBox7.Text = reader["Address"].ToString();

                   // Bind Sex
                   string sex = reader["Sex"].ToString().Replace(" ", string.Empty);

                   if (sex.Equals("M", StringComparison.OrdinalIgnoreCase) || sex.Equals("Male", StringComparison.OrdinalIgnoreCase))
                   {
                       RadioButton1.Checked = true; // Male
                   }
                   else if (sex.Equals("F", StringComparison.OrdinalIgnoreCase) || sex.Equals("Female", StringComparison.OrdinalIgnoreCase))
                   {
                       RadioButton2.Checked = true; // Female
                   }

                   // Bind Stream
                   string streamFromDb = reader["Stream"].ToString().Replace(" ", string.Empty);

                   foreach (ListItem item in DropDownList1.Items)
                   {
                       if (item.Value.Equals(streamFromDb, StringComparison.OrdinalIgnoreCase))
                       {
                           DropDownList1.SelectedValue = item.Value;
                           break;
                       }
                   }
               }

               reader.Close();
           }
       }
   }


      
    }
}
