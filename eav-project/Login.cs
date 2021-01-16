using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace eav_project {
    public partial class Login : Form {
        public Login() {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e) {
            // check if fields are filled 
            // check db and find user. 
            // if user exists, go to main form
            if (txtUsername.Text != "" && txtPassword.Text != "" ) {
                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");

                SqlCommand cmd = new SqlCommand("select * from users where user_name=@username and user_pass=@password", conn);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", txtPassword.Text);
              
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapt.Fill(dt);

                conn.Open();
                cmd.ExecuteReader();
                conn.Close();
                cmd.Dispose();
                conn.Dispose();

                if( dt.Rows.Count == 1) {
                    //TODO : check if banned or not 
                    if (dt.Rows[0]["user_active"].ToString().Equals("True")) {
                        MainPage frm = new MainPage(txtUsername.Text, bool.Parse(dt.Rows[0]["user_admin"].ToString()));
                        this.Hide();
                        frm.ShowDialog();
                        this.Close();
                    } else {
                        MessageBox.Show("User is banned!",
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                    }
                   
                } else {
                    MessageBox.Show("Invalid Credentials!",
                       "Error",
                       MessageBoxButtons.OK,
                       MessageBoxIcon.Exclamation,
                       MessageBoxDefaultButton.Button1);
                }
            } else {
                MessageBox.Show("All fields are required!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e) {
            Register fm = new Register();
            this.Hide();
            fm.ShowDialog();
            this.Close();
        }

        private void Login_Load(object sender, EventArgs e) {

        }
    }
}
