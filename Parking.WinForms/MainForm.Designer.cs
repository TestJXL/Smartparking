namespace Parking.WinForms
{
    partial class MainForm
    {
        ///  Required designer variable.
        private System.ComponentModel.IContainer components = null;

        ///  Clean up any resources being used.
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

        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        private void InitializeComponent()
        {
            headerPanel = new TableLayoutPanel();
            lblAppTitle = new Label();
            lblScreenTitle = new Label();
            navFlow = new FlowLayoutPanel();
            btnEntry = new Button();
            btnHome = new Button();
            contentPanel = new Panel();
            headerPanel.SuspendLayout();
            navFlow.SuspendLayout();
            SuspendLayout();
            //
            // headerPanel
            //
            headerPanel.AutoSize = true;
            headerPanel.BackColor = Color.FromArgb(33, 53, 85);
            headerPanel.ColumnCount = 3;
            headerPanel.ColumnStyles.Add(new ColumnStyle());
            headerPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerPanel.ColumnStyles.Add(new ColumnStyle());
            headerPanel.Controls.Add(lblAppTitle, 0, 0);
            headerPanel.Controls.Add(lblScreenTitle, 1, 0);
            headerPanel.Controls.Add(navFlow, 2, 0);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(12, 6, 12, 6);
            headerPanel.RowCount = 1;
            headerPanel.RowStyles.Add(new RowStyle());
            headerPanel.Size = new Size(1082, 62);
            headerPanel.TabIndex = 0;
            //
            // lblAppTitle
            //
            lblAppTitle.Anchor = AnchorStyles.Left;
            lblAppTitle.AutoSize = true;
            lblAppTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.White;
            lblAppTitle.Location = new Point(15, 13);
            lblAppTitle.Name = "lblAppTitle";
            lblAppTitle.Size = new Size(128, 35);
            lblAppTitle.TabIndex = 0;
            lblAppTitle.Text = "SmartPark";
            //
            // lblScreenTitle
            //
            lblScreenTitle.Anchor = AnchorStyles.Left;
            lblScreenTitle.AutoSize = true;
            lblScreenTitle.Font = new Font("Segoe UI", 11F);
            lblScreenTitle.ForeColor = Color.FromArgb(200, 214, 235);
            lblScreenTitle.Location = new Point(166, 18);
            lblScreenTitle.Margin = new Padding(20, 0, 3, 0);
            lblScreenTitle.Name = "lblScreenTitle";
            lblScreenTitle.Size = new Size(0, 25);
            lblScreenTitle.TabIndex = 1;
            //
            // navFlow
            //
            navFlow.Anchor = AnchorStyles.Right;
            navFlow.AutoSize = true;
            navFlow.Controls.Add(btnHome);
            navFlow.Controls.Add(btnEntry);
            navFlow.Location = new Point(783, 9);
            navFlow.Name = "navFlow";
            navFlow.Size = new Size(284, 44);
            navFlow.TabIndex = 2;
            navFlow.WrapContents = false;
            //
            // btnHome
            //
            btnHome.AutoSize = true;
            btnHome.Location = new Point(3, 3);
            btnHome.MinimumSize = new Size(130, 38);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(130, 38);
            btnHome.TabIndex = 0;
            btnHome.Text = "Home";
            btnHome.UseVisualStyleBackColor = true;
            btnHome.Click += btnHome_Click;
            //
            // btnEntry
            //
            btnEntry.AutoSize = true;
            btnEntry.Location = new Point(139, 3);
            btnEntry.MinimumSize = new Size(130, 38);
            btnEntry.Name = "btnEntry";
            btnEntry.Size = new Size(130, 38);
            btnEntry.TabIndex = 1;
            btnEntry.Text = "Vehicle Gate";
            btnEntry.UseVisualStyleBackColor = true;
            btnEntry.Click += btnEntry_Click;
            //
            // contentPanel
            //
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 62);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1082, 591);
            contentPanel.TabIndex = 1;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1082, 653);
            Controls.Add(contentPanel);
            Controls.Add(headerPanel);
            MinimumSize = new Size(900, 600);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SmartPark";
            Load += MainForm_Load;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            navFlow.ResumeLayout(false);
            navFlow.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel headerPanel;
        private Label lblAppTitle;
        private Label lblScreenTitle;
        private FlowLayoutPanel navFlow;
        private Button btnHome;
        private Button btnEntry;
        private Panel contentPanel;
    }
}
