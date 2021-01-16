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
    public partial class AdminManageTickets : Form {
        public AdminManageTickets() {
            InitializeComponent();
            LoadTickets(dataGridView1);
        }


        public void LoadTickets(DataGridView table) {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT tickets.*, users.user_name FROM tickets  INNER JOIN users ON tickets.user_id = users.user_id", conn);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);

            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
            table.DataSource = dt;    
        }

        public void Clear() {
            txtTicketID.Text = "";
            txtTicketTitle.Text = "";
            txtTicketPrice.Text = "";
            txtTicketLocation.Text = "";
            txtSoldBy.Text = "";
            txtOrderID.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) {
            Clear();

            if(dataGridView1.SelectedRows.Count > 0) {
                txtOrderID.Text = dataGridView1.SelectedRows[0].Cells["order_id"].Value.ToString();
                txtSoldBy.Text = dataGridView1.SelectedRows[0].Cells["user_name"].Value.ToString();
                txtTicketID.Text = dataGridView1.SelectedRows[0].Cells["ticket_id"].Value.ToString();
                txtTicketID.Enabled = false;
                txtTicketLocation.Text = dataGridView1.SelectedRows[0].Cells["ticket_location"].Value.ToString();
                txtTicketPrice.Text = dataGridView1.SelectedRows[0].Cells["ticket_price"].Value.ToString();
                txtTicketTitle.Text = dataGridView1.SelectedRows[0].Cells["ticket_title"].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["ticket_date"].Value.ToString());
                txtOrderID.Enabled = false;
                txtSoldBy.Enabled = false;

                if (!dataGridView1.SelectedRows[0].Cells["order_id"].Value.ToString().Equals("")) {
                    txtTicketLocation.Enabled = false;
                    txtTicketPrice.Enabled = false;
                    txtTicketTitle.Enabled = false;
                    dateTimePicker1.Enabled = false;
                } else {
                    txtTicketLocation.Enabled = true;
                    txtTicketPrice.Enabled = true;
                    txtTicketTitle.Enabled = true;
                    dateTimePicker1.Enabled = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (dataGridView1.SelectedRows.Count > 0) {
                if(dataGridView1.SelectedRows[0].Cells["order_id"].Value.ToString().Equals("")) {
                    SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
                    SqlCommand cmd = new SqlCommand("Delete from tickets where ticket_id=@ticketId", conn);
                    cmd.Parameters.AddWithValue("@ticketId", dataGridView1.SelectedRows[0].Cells["ticket_id"].Value.ToString());

                    conn.Open();
                    cmd.ExecuteReader();
                    conn.Close();
                    cmd.Dispose();
                    conn.Dispose();
                    LoadTickets(dataGridView1);
                } else {
                    MessageBox.Show("Can not delete a ticket that has been ordered!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e) {
            if(txtTicketTitle.Text != "" && txtTicketPrice.Text != "" && txtTicketLocation.Text != "") {

                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
                SqlCommand cmd = new SqlCommand("update tickets set ticket_title=@ticketTitle, ticket_price=@ticketPrice, ticket_location=@ticketLocation, ticket_date=@ticketDate where ticket_id=@ticketId", conn);
                cmd.Parameters.AddWithValue("@ticketTitle", txtTicketTitle.Text);
                cmd.Parameters.AddWithValue("@ticketPrice", txtTicketPrice.Text);
                cmd.Parameters.AddWithValue("@ticketLocation", txtTicketLocation.Text);
                cmd.Parameters.AddWithValue("@ticketId", txtTicketID.Text);
                cmd.Parameters.AddWithValue("@ticketDate", dateTimePicker1.Value.ToString());

                conn.Open();
                cmd.ExecuteReader();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                LoadTickets(dataGridView1);
                Clear();
                MessageBox.Show("Ticket Updated!");

            } else {
                MessageBox.Show("Fields can not be empty!");
            }
        }

        private void AdminManageTickets_Load(object sender, EventArgs e) {

        }
    }
}
