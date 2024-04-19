using System;
using System.Configuration; // Add this for ConfigurationManager
using System.Data.SqlClient; // Add this for SqlConnection and other SQL-related classes
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace F_F
{
    public partial class success : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string username = Session["username"].ToString();
            string orderId = Request.QueryString["orderId"];
            string paymentId = Request.QueryString["paymentId"];

            // Establish connection to the SQL Server database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the SQL query with parameters to avoid SQL injection
                string query = "INSERT INTO Payment (Username, OrderId, PaymentId) VALUES (@Username, @OrderId, @PaymentId)";

                // Create and open the SqlCommand object
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the SqlCommand object
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@PaymentId", paymentId);

                    // Open the database connection
                    connection.Open();

                    // Execute the SQL query
                    command.ExecuteNonQuery();
                }
            }

            // Display the orderId and paymentId
            Label1.Text= username;
            Label2.Text = orderId;
            Label3.Text = paymentId;
        }

        
    }
}
