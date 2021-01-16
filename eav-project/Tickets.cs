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
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace eav_project {
    public partial class Tickets : Form {
        DataTable dt = new DataTable();
        private int userID;
        private string ticketId;
        public  Tickets(int userId) {
            this.userID = userId;
            InitializeComponent();
            LoadData();
            ShowTickets();

            webBrowser1.Navigate("https://google.com/maps/place/romania");
            webBrowser1.ScriptErrorsSuppressed = true;
            txtTicketTitle.ReadOnly = true;
            txtTicketLocation.ReadOnly = true;
            txtTicketPrice.ReadOnly = true;
            txtTicketSoldBy.ReadOnly = true;
            txtTicketDate.ReadOnly = true;
            btnBuy.Enabled = false;
        }

        public void LoadData() {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT tickets.*, users.user_name FROM tickets  INNER JOIN users ON tickets.user_id = users.user_id WHERE order_id IS NULL",conn);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
        }

        public void SearchTicket() {
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT tickets.* FROM tickets WHERE ticket_title LIKE '%"+txtSearch.Text+ "%' and order_id IS NULL", conn);
            
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
        }
      public class myTicket {
            public int id;
            public string title;
            public override string ToString() {
                return title;
            }
        }

        public string getUsernameFromID(string id) {
            DataTable user = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE user_id=@userID", conn);
            cmd.Parameters.AddWithValue("@userID", id);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(user);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
            return user.Rows[0]["user_name"].ToString();
        }

        public void ShowTickets() {
            int i;
            for(i = 0; i< dt.Rows.Count; i++) {
                myTicket ticket = new myTicket();
                ticket.id = Convert.ToInt32(dt.Rows[i]["ticket_id"]);
                ticket.title = Convert.ToString(dt.Rows[i]["ticket_title"]);

                listBox1.Items.Add(ticket);
            }
        }

        public void ClearAll() {
            dt = new DataTable();
            listBox1.Items.Clear();
            txtTicketTitle.Text = "";
            txtTicketPrice.Text = "";
            txtTicketDate.Text = "";
            txtTicketLocation.Text = "";
            txtTicketSoldBy.Text = "";
        }

        private void list_selectedIndexChanged(object sender, EventArgs e) {
            int index = listBox1.SelectedIndex;

            if(dt.Rows.Count > 0 && index >= 0) {
                
                txtTicketTitle.Text = Convert.ToString(dt.Rows[index]["ticket_title"]);
                txtTicketPrice.Text = Convert.ToString(dt.Rows[index]["ticket_price"]);
                txtTicketDate.Text = Convert.ToString(dt.Rows[index]["ticket_date"]);
                txtTicketLocation.Text = Convert.ToString(dt.Rows[index]["ticket_location"]);
                txtTicketSoldBy.Text = getUsernameFromID(dt.Rows[index]["user_id"].ToString());
               // txtTicketSoldBy.Text = Convert.ToString(dt.Rows[index]["user_name"]);
                webBrowser1.Navigate("https://google.com/maps/place/" + Convert.ToString(dt.Rows[index]["ticket_location"]));
                ticketId = Convert.ToString(dt.Rows[index]["ticket_id"]);
                btnBuy.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            SellTicket dialog = new SellTicket(userID);
            dialog.ShowDialog();

            ClearAll();
            LoadData();
            ShowTickets();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) {
            ClearAll();
            SearchTicket();
            ShowTickets();
        }

        private void btnBuy_Click(object sender, EventArgs e) {

            
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");

            SqlCommand cmd = new SqlCommand("insert into orders (ticket_id, user_id, order_date, order_price) OUTPUT INSERTED.order_id values" +
                "(@ticketId, @userId, @orderDate, @orderPrice);", conn);
            cmd.Parameters.AddWithValue("@ticketId", ticketId);
            cmd.Parameters.AddWithValue("@userId", userID);
            cmd.Parameters.AddWithValue("@orderDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@orderPrice", txtTicketPrice.Text);
           // SqlParameter IDParameter = new SqlParameter("@ID", Sqldb)

           
           
            conn.Open();
            Int32 orderId = (Int32)cmd.ExecuteScalar();
           
             
            

            conn.Close();
            cmd.Dispose();
            conn.Dispose();

            //get order id created
            SqlConnection conn2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd2 = new SqlCommand("update tickets set order_id=@orderId where ticket_id=@ticketId", conn2);
            cmd2.Parameters.AddWithValue("@orderId", orderId) ;
            cmd2.Parameters.AddWithValue("ticketId", ticketId);

            conn2.Open();
            cmd2.ExecuteReader();
            conn2.Close();
            cmd2.Dispose();
            conn2.Dispose();

            MessageBox.Show("Order Created!");
            ClearAll();
            LoadData();
            ShowTickets();
        }

        private void Tickets_Load(object sender, EventArgs e) {

        }
    }
}
