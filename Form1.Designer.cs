namespace ControlLazerApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLazerApplicaitonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.laserPathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Ok_setting_btn = new System.Windows.Forms.Button();
            this.Edii_setting_btn = new System.Windows.Forms.Button();
            this.plan_tab = new System.Windows.Forms.TabControl();
            this.menuStrip1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.plan_tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(693, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLazerApplicaitonToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openLazerApplicaitonToolStripMenuItem
            // 
            this.openLazerApplicaitonToolStripMenuItem.Name = "openLazerApplicaitonToolStripMenuItem";
            this.openLazerApplicaitonToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openLazerApplicaitonToolStripMenuItem.Text = "Open Lazer Applicaiton";
            this.openLazerApplicaitonToolStripMenuItem.Click += new System.EventHandler(this.openLazerApplicaitonToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filePathToolStripMenuItem,
            this.laserPathToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // filePathToolStripMenuItem
            // 
            this.filePathToolStripMenuItem.Name = "filePathToolStripMenuItem";
            this.filePathToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.filePathToolStripMenuItem.Text = "File Path";
            this.filePathToolStripMenuItem.Click += new System.EventHandler(this.filePathToolStripMenuItem_Click);
            // 
            // laserPathToolStripMenuItem
            // 
            this.laserPathToolStripMenuItem.Name = "laserPathToolStripMenuItem";
            this.laserPathToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.laserPathToolStripMenuItem.Text = "Laser Path";
            this.laserPathToolStripMenuItem.Click += new System.EventHandler(this.laserPathToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Controls.Add(this.Ok_setting_btn);
            this.tabPage2.Controls.Add(this.Edii_setting_btn);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(685, 263);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Parameter";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(679, 257);
            this.dataGridView1.TabIndex = 10;
            // 
            // Ok_setting_btn
            // 
            this.Ok_setting_btn.Location = new System.Drawing.Point(1195, 55);
            this.Ok_setting_btn.Name = "Ok_setting_btn";
            this.Ok_setting_btn.Size = new System.Drawing.Size(75, 23);
            this.Ok_setting_btn.TabIndex = 8;
            this.Ok_setting_btn.Text = "OK";
            this.Ok_setting_btn.UseVisualStyleBackColor = true;
            this.Ok_setting_btn.Visible = false;
            this.Ok_setting_btn.Click += new System.EventHandler(this.Ok_setting_btn_Click);
            // 
            // Edii_setting_btn
            // 
            this.Edii_setting_btn.Location = new System.Drawing.Point(1195, 26);
            this.Edii_setting_btn.Name = "Edii_setting_btn";
            this.Edii_setting_btn.Size = new System.Drawing.Size(75, 23);
            this.Edii_setting_btn.TabIndex = 9;
            this.Edii_setting_btn.Text = "Edit";
            this.Edii_setting_btn.UseVisualStyleBackColor = true;
            this.Edii_setting_btn.Visible = false;
            this.Edii_setting_btn.Click += new System.EventHandler(this.Edii_setting_btn_Click);
            // 
            // plan_tab
            // 
            this.plan_tab.Controls.Add(this.tabPage2);
            this.plan_tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plan_tab.Location = new System.Drawing.Point(0, 24);
            this.plan_tab.Name = "plan_tab";
            this.plan_tab.SelectedIndex = 0;
            this.plan_tab.Size = new System.Drawing.Size(693, 289);
            this.plan_tab.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 313);
            this.Controls.Add(this.plan_tab);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OEE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.plan_tab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filePathToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLazerApplicaitonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem laserPathToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button Ok_setting_btn;
        private System.Windows.Forms.Button Edii_setting_btn;
        private System.Windows.Forms.TabControl plan_tab;
    }
}

