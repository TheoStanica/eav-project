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
    public partial class Register : Form {
        public Register() {
            InitializeComponent();

            string[] countries = { "AF", "AX", "AL", "DZ", "AS", "AD", "AO", "AI", "AQ", "AG", "AR", "AM", "AW", "AU", "AT", "AZ", "BH", "BS", "BD", "BB", "BY", "BE", "BZ", "BJ", "BM", "BT", "BO", "BQ", "BA", "BW", "BV", "BR", "IO", "BN", "BG", "BF", "BI", "KH", "CM", "CA", "CV", "KY", "CF", "TD", "CL", "CN", "CX", "CC", "CO", "KM", "CG", "CD", "CK", "CR", "CI", "HR", "CU", "CW", "CY", "CZ", "DK", "DJ", "DM", "DO", "EC", "EG", "SV", "GQ", "ER", "EE", "ET", "FK", "FO", "FJ", "FI", "FR", "GF", "PF", "TF", "GA", "GM", "GE", "DE", "GH", "GI", "GR", "GL", "GD", "GP", "GU", "GT", "GG", "GN", "GW", "GY", "HT", "HM", "VA", "HN", "HK", "HU", "IS", "IN", "ID", "IR", "IQ", "IE", "IM", "IL", "IT", "JM", "JP", "JE", "JO", "KZ", "KE", "KI", "KP", "KR", "KW", "KG", "LA", "LV", "LB", "LS", "LR", "LY", "LI", "LT", "LU", "MO", "MK", "MG", "MW", "MY", "MV", "ML", "MT", "MH", "MQ", "MR", "MU", "YT", "MX", "FM", "MD", "MC", "MN", "ME", "MS", "MA", "MZ", "MM", "NA", "NR", "NP", "NL", "NC", "NZ", "NI", "NE", "NG", "NU", "NF", "MP", "NO", "OM", "PK", "PW", "PS", "PA", "PG", "PY", "PE", "PH", "PN", "PL", "PT", "PR", "QA", "RE", "RO", "RU", "RW", "BL", "SH", "KN", "LC", "MF", "PM", "VC", "WS", "SM", "ST", "SA", "SN", "RS", "SC", "SL", "SG", "SX", "SK", "SI", "SB", "SO", "ZA", "GS", "SS", "ES", "LK", "SD", "SR", "SJ", "SZ", "SE", "CH", "SY", "TW", "TJ", "TZ", "TH", "TL", "TG", "TK", "TO", "TT", "TN", "TR", "TM", "TC", "TV", "UG", "UA", "AE", "GB", "US", "UM", "UY", "UZ", "VU", "VE", "VN", "VG", "VI", "WF", "EH", "YE", "ZM", "ZW" };
            for( int i = 0; i< countries.Length; i++) {
                comboBox1.Items.Add(countries[i]);
            }
        }

        private void btnBack_Click(object sender, EventArgs e) {
            Login frm = new Login();
            this.Hide();
            frm.ShowDialog();
            this.Close();
        }

        private void btnRegister_Click(object sender, EventArgs e) {
            if(txtUsername.Text != "" && txtPassword.Text != "" && comboBox1.SelectedItem.ToString() != "") {
                // try to add user into db
                DataTable dt = new DataTable();
                SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");

               
                SqlCommand cmdUserCheck = new SqlCommand("SELECT * from users where user_name=@username",conn);
                cmdUserCheck.Parameters.AddWithValue("@username", txtUsername.Text);
                SqlDataAdapter sa = new SqlDataAdapter(cmdUserCheck);
                conn.Open();
                sa.Fill(dt);
               
                conn.Close();
                sa.Dispose();
                cmdUserCheck.Dispose();
                conn.Dispose();

                if(dt.Rows.Count == 0) {
                    SqlConnection conn2 = new SqlConnection(@"Data Source=.\SQLEXPRESS;" + "Initial Catalog=eav-project;Integrated Security=SSPI;");

                    SqlCommand cmd = new SqlCommand("insert into users( user_name, user_pass, user_register_date, user_country_code, user_active, user_admin)" +
                    "values (@username, @password, @register_date, @country_code, 1, 0);", conn2);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@register_date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@country_code", comboBox1.SelectedItem.ToString());
            
                    conn2.Open();
                    cmd.ExecuteReader();
                    conn2.Close();
                    cmd.Dispose();
                    conn2.Dispose();

                    // after user is created, redirect to Main Page
                    MainPage frm = new MainPage(txtUsername.Text, false);
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                } else {
                    MessageBox.Show("User already exists!",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                }
            } else {
                MessageBox.Show("Enter data in all field!",
                   "Error",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
            }
        }

        private void Register_Load(object sender, EventArgs e) {

        }
    }
}
