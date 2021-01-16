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
    public partial class MainPage : Form {
        int userID;
        string userName;
        public MainPage(string username, bool isAdmin) {
            InitializeComponent();
            label1.Text = username;
            userName = username;

            getUserID();

            

            if (!isAdmin) {
                administratorToolStripMenuItem.Visible = false;
            }
        }

        private void ticketsToolStripMenuItem_Click(object sender, EventArgs e) {
            Tickets frm = new Tickets(userID);
            //frm.MdiParent = this;
            frm.Show();
            

        }

        private void getUserID() {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");


            SqlCommand cmdUserCheck = new SqlCommand("SELECT * from users where user_name=@username", conn);
            cmdUserCheck.Parameters.AddWithValue("@username", userName);
            SqlDataAdapter sa = new SqlDataAdapter(cmdUserCheck);
            conn.Open();
            sa.Fill(dt);

            conn.Close();
            sa.Dispose();
            cmdUserCheck.Dispose();
            conn.Dispose();
            userID = Convert.ToInt32(dt.Rows[0]["user_id"]);
        }

        private void myAccountToolStripMenuItem_Click(object sender, EventArgs e) {
            MyAccount frm = new MyAccount(userID);
            frm.MdiParent = this;
            frm.Show();
        }

        private void administratorToolStripMenuItem_Click(object sender, EventArgs e) {
            Administrate frm = new Administrate();
            frm.MdiParent = this;
            frm.Show();
        }

        private void myTicketsToolStripMenuItem_Click(object sender, EventArgs e) {
            MyTickets frm = new MyTickets(userID);
            frm.ShowDialog();
        }

        private void myOrdersToolStripMenuItem_Click(object sender, EventArgs e) {
            MyOrders frm = new MyOrders(userID);
            frm.ShowDialog();
        }

        private void MainPage_Load(object sender, EventArgs e) {

        }

        private void administratorToolStripMenuItem_MouseHover(object sender, EventArgs e) {
            administratorToolStripMenuItem.BackColor = Color.Red;
        }
    }
}
