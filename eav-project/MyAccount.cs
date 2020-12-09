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
    public partial class MyAccount : Form {
        private int userID;
        DataTable dt = new DataTable();
        public MyAccount(int userID) {
            InitializeComponent();
            this.userID = userID;

            loadUserData();
        }

        private void loadUserData() {
            
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE user_id=@userID", conn);
            cmd.Parameters.AddWithValue("@userID", userID);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(dt);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();

            txtUsername.Text = dt.Rows[0]["user_name"].ToString();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            updateUserDetails();
        }

        private void updateUserDetails() {
            if(txtNewPassword.Text != "" && txtNewPasswordConfirm.Text != "") {

                if(txtNewPassword.Text == txtNewPasswordConfirm.Text) {
                    if (txtCurrentPassword.Text == dt.Rows[0]["user_pass"].ToString()) {
                        if (!usernameExists(txtUsername.Text)) {
                            //make update
                            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
                            SqlCommand cmd = new SqlCommand("update users set user_name=@username, user_pass=@password where user_id=@userID", conn);
                            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@userID", userID.ToString());
                            cmd.Parameters.AddWithValue("@password", txtNewPassword.Text);
                            conn.Open();
                            cmd.ExecuteReader();
                            conn.Close();
                            cmd.Dispose();
                            conn.Dispose();

                            MessageBox.Show("Account Updated!");
                            this.Close();
                        } else {
                            MessageBox.Show("This username already exists!");
                        }
                    } else {
                        MessageBox.Show("Wrong Current Password!");
                    }


                } else {
                    MessageBox.Show("Passwords do not match!");
                }

            } else {

                if (txtCurrentPassword.Text == dt.Rows[0]["user_pass"].ToString()) {
                    if(!usernameExists(txtUsername.Text)) {
                        //make update
                        SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
                        SqlCommand cmd = new SqlCommand("update users set user_name=@username where user_id=@userID" , conn);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@userID", userID.ToString());
                        conn.Open();
                        cmd.ExecuteReader();
                        conn.Close();
                        cmd.Dispose();
                        conn.Dispose();

                        MessageBox.Show("Account Updated!");
                        this.Close();
                    } else {
                        MessageBox.Show("This username already exists!");
                    }
                } else {
                    MessageBox.Show("Wrong Current Password!");
                }
            }
        }

        private bool usernameExists(string username) {
            DataTable finduser = new DataTable();
            SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");
            SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE user_name=@username", conn);
            cmd.Parameters.AddWithValue("@username", username);
            SqlDataAdapter sa = new SqlDataAdapter(cmd);
            conn.Open();
            sa.Fill(finduser);
            conn.Close();
            sa.Dispose();
            cmd.Dispose();
            conn.Dispose();

            if (finduser.Rows.Count != 0) return true;
            return false;
        }
    }
}
