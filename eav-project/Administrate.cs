using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eav_project {
    public partial class Administrate : Form {
        public Administrate() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            AdminManageUsers frm = new AdminManageUsers();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) {
            AdminManageTickets frm = new AdminManageTickets();
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e) {
            AdminManageOrders frm = new AdminManageOrders();
            frm.ShowDialog();
        }

        private void Administrate_Load(object sender, EventArgs e) {

        }
    }
}
