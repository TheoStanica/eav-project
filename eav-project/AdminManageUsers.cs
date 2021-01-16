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
    public partial class AdminManageUsers : Form {
        public AdminManageUsers() {
            InitializeComponent();
            LoadUsers(dataGridView1);
          
        }

        public void LoadUsers(DataGridView table) {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("Select * from users", conn);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);

            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();
            table.DataSource = dt;
            table.Columns["user_pass"].Visible = false;
        }
         public void Clear() {
            txtUsername.Text = "";
            txtUserID.Text = "";
            txtCountryCode.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            checkBoxUserActive.Checked = false;
            checkBoxUserAdmin.Checked = false;
        }

        private void dgvFiles_selectionChanged(object sender, EventArgs e) {
            Clear();
            if(dataGridView1.SelectedRows.Count > 0) {
                txtUserID.Text = dataGridView1.SelectedRows[0].Cells["user_id"].Value.ToString();
                txtUsername.Text = dataGridView1.SelectedRows[0].Cells["user_name"].Value.ToString();
                txtCountryCode.Text = dataGridView1.SelectedRows[0].Cells["user_country_code"].Value.ToString();
                dateTimePicker1.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells["user_register_date"].Value.ToString());
                if (dataGridView1.SelectedRows[0].Cells["user_active"].Value.ToString() == "True") {
                    checkBoxUserActive.Checked = true;
                } else {
                    checkBoxUserActive.Checked = false;
                }
                if (dataGridView1.SelectedRows[0].Cells["user_admin"].Value.ToString() == "True") {
                    checkBoxUserAdmin.Checked = true;
                } else {
                    checkBoxUserAdmin.Checked = false;
                }
                txtUserID.Enabled = false;
                dateTimePicker1.Enabled = false;

            }
        }

        private void btnModify_Click(object sender, EventArgs e) {
            if( txtUsername.Text != "" && txtCountryCode.Text != "") {
                if (txtUsername.Text != dataGridView1.SelectedRows[0].Cells["user_name"].Value.ToString()) {
                    if (checkIfUserExists(txtUsername.Text)) {
                        MessageBox.Show("This username is in use!");
                        return;
                    } 
                }

                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
                SqlCommand cmd = new SqlCommand("update users set user_name=@newUsername, user_country_code=@countryCode, user_active=@userActive, user_admin=@userAdmin where user_id=@userId", conn);
                cmd.Parameters.AddWithValue("@newUsername", txtUsername.Text);
                cmd.Parameters.AddWithValue("@countryCode", txtCountryCode.Text);
                cmd.Parameters.AddWithValue("@userId", txtUserID.Text);
                cmd.Parameters.AddWithValue("@userActive", checkBoxUserActive.Checked ? "1" : "0");
                cmd.Parameters.AddWithValue("@userAdmin", checkBoxUserAdmin.Checked ? "1" : "0");

                conn.Open();
                cmd.ExecuteReader();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                LoadUsers(dataGridView1);
                Clear();
                MessageBox.Show("User Updated!");


            } else {
                MessageBox.Show("Please enter valid data in all fields!");
            }
        }


        private bool checkIfUserExists(string username) {
            DataTable user = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE user_name=@userName", conn);
            cmd.Parameters.AddWithValue("@userName", username);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(user);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();

            if(user.Rows.Count != 0) {
                return true;
            }
            return false;
        }

        private void btnRemove_Click(object sender, EventArgs e) {
            if(dataGridView1.SelectedRows.Count > 0) {
                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
                SqlCommand cmd = new SqlCommand("Delete from users where user_id=@userId", conn);
                cmd.Parameters.AddWithValue("@userId", dataGridView1.SelectedRows[0].Cells["user_id"].Value.ToString());

                conn.Open();
                cmd.ExecuteReader();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();
                LoadUsers(dataGridView1);
            }
        }

        private void AdminManageUsers_Load(object sender, EventArgs e) {

        }
    }

    
}
