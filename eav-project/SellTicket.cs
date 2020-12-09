using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eav_project {
    public partial class SellTicket : Form {
        private int userID;
        public SellTicket(int userID) {
            InitializeComponent();
            this.userID = userID;
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnSell_Click(object sender, EventArgs e) {
            if(txtTicketTitle.Text == "" || txtTicketPrice.Text == "" || txtTicketLocation.Text == "") {
                MessageBox.Show("Enter data in all fields!");
            } else {
                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");

                SqlCommand cmd = new SqlCommand("insert into tickets (ticket_title, ticket_price, ticket_date, ticket_location, user_id) values" +
                    "(@ticketTitle, @ticketPrice, @ticketDate, @ticketLocation, @ticketUserID);", conn);
                cmd.Parameters.AddWithValue("@ticketTitle", txtTicketTitle.Text);
                cmd.Parameters.AddWithValue("@ticketPrice", txtTicketPrice.Text);
                cmd.Parameters.AddWithValue("@ticketDate", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@ticketLocation", txtTicketLocation.Text);
                cmd.Parameters.AddWithValue("@ticketUserId", userID.ToString());

                conn.Open();
                cmd.ExecuteReader();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
                this.Close();
                
            }
        }
    }
}
