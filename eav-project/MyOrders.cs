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
    public partial class MyOrders : Form {
        private int userID;
        public MyOrders(int userId) {
            this.userID = userId;
            InitializeComponent();
            LoadOrders(dataGridView1);

            txtEventLocation.Enabled = false;
            txtEventPrice.Enabled = false;
            txtEventTitle.Enabled = false;
            txtOrderId.Enabled = false;
            dtpEventDate.Enabled = false;
            dtpOrderDate.Enabled = false;
            btnCancelOrder.Enabled = false;
        }

        public void LoadOrders(DataGridView table) {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT orders.*, users.user_name, tickets.ticket_title, tickets.ticket_date, tickets.ticket_location  FROM orders  INNER JOIN users ON orders.user_id = users.user_id INNER JOIN tickets ON orders.ticket_id=tickets.ticket_id WHERE users.user_id=@userId" , conn);
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
            txtEventLocation.Text = "";
            txtEventPrice.Text = "";
            txtEventTitle.Text = "";
            dtpOrderDate.Value = DateTime.Now;
            txtOrderId.Text = "";
            dtpEventDate.Value = DateTime.Now;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e) {
            Clear();

            if (dataGridView1.SelectedRows.Count > 0) {
               txtOrderId.Text = dataGridView1.SelectedRows[0].Cells["order_id"].Value.ToString();
                dtpOrderDate.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["order_date"].Value.ToString());
                txtEventTitle.Text = dataGridView1.SelectedRows[0].Cells["ticket_title"].Value.ToString();
                dtpEventDate.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["ticket_date"].Value.ToString());
                txtEventLocation.Text = dataGridView1.SelectedRows[0].Cells["ticket_location"].Value.ToString();
                txtEventPrice.Text = dataGridView1.SelectedRows[0].Cells["order_price"].Value.ToString();

                if(DateTime.Parse(dataGridView1.SelectedRows[0].Cells["ticket_date"].Value.ToString()) > DateTime.Now) {
                    btnCancelOrder.Enabled = true;
                } else {
                    btnCancelOrder.Enabled = false;
                }
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e) {
            // delete selected order id from orders table
            // update order_id of ticket with ticket_id to be NULL

        }
    }
}
