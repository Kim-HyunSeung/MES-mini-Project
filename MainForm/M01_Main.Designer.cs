namespace ClassLibrary1
{
    partial class M01_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(M01_Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.사용자프로그램ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Form03_WorkSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.관리자프로그램ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Form01_FCS_Master = new System.Windows.Forms.ToolStripMenuItem();
            this.Form02_broken_management = new System.Windows.Forms.ToolStripMenuItem();
            this.Form04_Chart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Dolnquire = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.myTabControl1 = new Assemble.MyTabControl();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.menuStrip1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 49);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.사용자프로그램ToolStripMenuItem,
            this.관리자프로그램ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 17);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(794, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 사용자프로그램ToolStripMenuItem
            // 
            this.사용자프로그램ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Form03_WorkSystem});
            this.사용자프로그램ToolStripMenuItem.Name = "사용자프로그램ToolStripMenuItem";
            this.사용자프로그램ToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.사용자프로그램ToolStripMenuItem.Text = "사용자프로그램";
            this.사용자프로그램ToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.사용자프로그램ToolStripMenuItem_DropDownItemClicked);
            // 
            // Form03_WorkSystem
            // 
            this.Form03_WorkSystem.Name = "Form03_WorkSystem";
            this.Form03_WorkSystem.Size = new System.Drawing.Size(146, 22);
            this.Form03_WorkSystem.Text = "현장프로그램";
            // 
            // 관리자프로그램ToolStripMenuItem
            // 
            this.관리자프로그램ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Form01_FCS_Master,
            this.Form02_broken_management,
            this.Form04_Chart});
            this.관리자프로그램ToolStripMenuItem.Name = "관리자프로그램ToolStripMenuItem";
            this.관리자프로그램ToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.관리자프로그램ToolStripMenuItem.Text = "관리자프로그램";
            this.관리자프로그램ToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.사용자프로그램ToolStripMenuItem_DropDownItemClicked);
            // 
            // Form01_FCS_Master
            // 
            this.Form01_FCS_Master.Name = "Form01_FCS_Master";
            this.Form01_FCS_Master.Size = new System.Drawing.Size(180, 22);
            this.Form01_FCS_Master.Text = "설비마스터";
            // 
            // Form02_broken_management
            // 
            this.Form02_broken_management.Name = "Form02_broken_management";
            this.Form02_broken_management.Size = new System.Drawing.Size(180, 22);
            this.Form02_broken_management.Text = "고장이력관리";
            // 
            // Form04_Chart
            // 
            this.Form04_Chart.Name = "Form04_Chart";
            this.Form04_Chart.Size = new System.Drawing.Size(180, 22);
            this.Form04_Chart.Text = "MTTR,MTBF";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Dolnquire,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 49);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 72);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Dolnquire
            // 
            this.Dolnquire.Image = ((System.Drawing.Image)(resources.GetObject("Dolnquire.Image")));
            this.Dolnquire.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.Dolnquire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Dolnquire.Name = "Dolnquire";
            this.Dolnquire.Size = new System.Drawing.Size(54, 69);
            this.Dolnquire.Text = "조회";
            this.Dolnquire.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Dolnquire.Click += new System.EventHandler(this.btnFunction_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(54, 69);
            this.toolStripButton2.Text = "저장";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.btnFunction_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(54, 69);
            this.toolStripButton3.Text = "닫기";
            this.toolStripButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(54, 69);
            this.toolStripButton4.Text = "종료";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // myTabControl1
            // 
            this.myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myTabControl1.Location = new System.Drawing.Point(0, 121);
            this.myTabControl1.Name = "myTabControl1";
            this.myTabControl1.SelectedIndex = 0;
            this.myTabControl1.Size = new System.Drawing.Size(800, 329);
            this.myTabControl1.TabIndex = 4;
            // 
            // M01_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.myTabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "M01_Main";
            this.Text = "M01_Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.M01_Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 사용자프로그램ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Form03_WorkSystem;
        private System.Windows.Forms.ToolStripMenuItem 관리자프로그램ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Form01_FCS_Master;
        private System.Windows.Forms.ToolStripMenuItem Form02_broken_management;
        private System.Windows.Forms.ToolStripMenuItem Form04_Chart;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton Dolnquire;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private Assemble.MyTabControl myTabControl1;
    }
}