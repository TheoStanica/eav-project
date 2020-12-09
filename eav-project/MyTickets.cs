using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace eav_project {
    public partial class MyTickets : Form {
        private int userID;
        public MyTickets(int userId) {
            InitializeComponent();
            this.userID = userId;

            LoadUserTickets(dataGridView1);
        }

        private void btnSellTicket_Click(object sender, EventArgs e) {
            SellTicket frm = new SellTicket(userID);
            frm.ShowDialog();

            LoadUserTickets(dataGridView1);
        }

        private void LoadUserTickets(DataGridView table) {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM tickets where user_id=@userId", conn);
            cmd.Parameters.AddWithValue("@userId", userID);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);

            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
            table.DataSource = dt;
        }

        private void Clear() {
            txtTicketId.Text = "";
            txtTicketLocation.Text = "";
            txtTicketPrice.Text = "";
            txtTicketTitle.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) {
            if (dataGridView1.SelectedRows.Count > 0) {
                txtTicketId.Text = dataGridView1.SelectedRows[0].Cells["ticket_id"].Value.ToString();
                txtTicketId.Enabled = false;
                txtTicketLocation.Text = dataGridView1.SelectedRows[0].Cells["ticket_location"].Value.ToString();
                txtTicketPrice.Text = dataGridView1.SelectedRows[0].Cells["ticket_price"].Value.ToString();
                txtTicketTitle.Text = dataGridView1.SelectedRows[0].Cells["ticket_title"].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["ticket_date"].Value.ToString());

                if (!dataGridView1.SelectedRows[0].Cells["order_id"].Value.ToString().Equals("")) {
                    btnRemove.Enabled = false;
                    btnSave.Enabled = false;
                } else {
                    btnRemove.Enabled = true;
                    btnSave.Enabled = true;
                    
                }
            } 
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if (txtTicketTitle.Text != "" && txtTicketPrice.Text != "" && txtTicketLocation.Text != "") {

                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
                SqlCommand cmd = new SqlCommand("update tickets set ticket_title=@ticketTitle, ticket_price=@ticketPrice, ticket_location=@ticketLocation, ticket_date=@ticketDate where ticket_id=@ticketId", conn);
                cmd.Parameters.AddWithValue("@ticketTitle", txtTicketTitle.Text);
                cmd.Parameters.AddWithValue("@ticketPrice", txtTicketPrice.Text);
                cmd.Parameters.AddWithValue("@ticketLocation", txtTicketLocation.Text);
                cmd.Parameters.AddWithValue("@ticketId", txtTicketId.Text);
                cmd.Parameters.AddWithValue("@ticketDate", dateTimePicker1.Value.ToString());

                conn.Open();
                cmd.ExecuteReader();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                LoadUserTickets(dataGridView1);
                Clear();
                MessageBox.Show("Ticket Updated!");

            } else {
                MessageBox.Show("Fields can not be empty!");
            }
        }

        public void SearchTicket(DataGridView table) {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT tickets.* FROM tickets WHERE ticket_title LIKE '%" + txtSearch.Text + "%' and user_id=@userId", conn);
            cmd.Parameters.AddWithValue("@userId", userID);

            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
            table.DataSource = dt;

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            Clear();
            SearchTicket(dataGridView1);

        }
    }
}
