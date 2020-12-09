﻿namespace eav_project {
    partial class MainPage {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ticketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myTicketsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.myAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administratorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(636, 412);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ticketsToolStripMenuItem,
            this.myTicketsToolStripMenuItem,
            this.myOrdersToolStripMenuItem,
            this.myAccountToolStripMenuItem,
            this.administratorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(787, 29);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ticketsToolStripMenuItem
            // 
            this.ticketsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.ticketsToolStripMenuItem.Name = "ticketsToolStripMenuItem";
            this.ticketsToolStripMenuItem.Size = new System.Drawing.Size(69, 25);
            this.ticketsToolStripMenuItem.Text = "Tickets";
            this.ticketsToolStripMenuItem.Click += new System.EventHandler(this.ticketsToolStripMenuItem_Click);
            // 
            // myTicketsToolStripMenuItem
            // 
            this.myTicketsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.myTicketsToolStripMenuItem.Name = "myTicketsToolStripMenuItem";
            this.myTicketsToolStripMenuItem.Size = new System.Drawing.Size(95, 25);
            this.myTicketsToolStripMenuItem.Text = "My Tickets";
            this.myTicketsToolStripMenuItem.Click += new System.EventHandler(this.myTicketsToolStripMenuItem_Click);
            // 
            // myOrdersToolStripMenuItem
            // 
            this.myOrdersToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.myOrdersToolStripMenuItem.Name = "myOrdersToolStripMenuItem";
            this.myOrdersToolStripMenuItem.Size = new System.Drawing.Size(96, 25);
            this.myOrdersToolStripMenuItem.Text = "My Orders";
            this.myOrdersToolStripMenuItem.Click += new System.EventHandler(this.myOrdersToolStripMenuItem_Click);
            // 
            // myAccountToolStripMenuItem
            // 
            this.myAccountToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.myAccountToolStripMenuItem.Name = "myAccountToolStripMenuItem";
            this.myAccountToolStripMenuItem.Size = new System.Drawing.Size(104, 25);
            this.myAccountToolStripMenuItem.Text = "My Account";
            this.myAccountToolStripMenuItem.Click += new System.EventHandler(this.myAccountToolStripMenuItem_Click);
            // 
            // administratorToolStripMenuItem
            // 
            this.administratorToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.administratorToolStripMenuItem.Name = "administratorToolStripMenuItem";
            this.administratorToolStripMenuItem.Size = new System.Drawing.Size(118, 25);
            this.administratorToolStripMenuItem.Text = "Administrator";
            this.administratorToolStripMenuItem.Click += new System.EventHandler(this.administratorToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(433, 412);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Welcome back,";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "label3";
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 452);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainPage";
            this.Text = "MainPage";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem ticketsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administratorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem myTicketsToolStripMenuItem;
        private System.Windows.Forms.Label label3;
    }
}