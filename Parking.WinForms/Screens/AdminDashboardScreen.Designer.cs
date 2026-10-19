namespace Parking.WinForms.Screens
{
    partial class AdminDashboardScreen
    {
        /// Required designer variables.
        private System.ComponentModel.IContainer components = null;

        
        /// Clean up all resources currently in use.
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// Methods supported by the designer—do not modify
        /// Use a code editor to modify the contents of this method.
        /// </summary>
        private void InitializeComponent()
        {
            layoutRoot = new TableLayoutPanel();
            headerRow = new TableLayoutPanel();
            lblTitle = new Label();
            lblMessage = new Label();
            btnLogout = new Button();
            tabs = new TabControl();
            tabHistory = new TabPage();
            historyLayout = new TableLayoutPanel();
            historyFilter = new FlowLayoutPanel();
            lblPlateFilter = new Label();
            txtPlateFilter = new TextBox();
            btnRefreshHistory = new Button();
            dgvHistory = new DataGridView();
            lblHistorySummary = new Label();
            tabLeases = new TabPage();
            leasesLayout = new TableLayoutPanel();
            leasesFilter = new FlowLayoutPanel();
            chkActiveOnly = new CheckBox();
            btnRefreshLeases = new Button();
            dgvLeases = new DataGridView();
            lblLeaseSummary = new Label();
            tabCancel = new TabPage();
            cancelLayout = new TableLayoutPanel();
            lblActiveLeases = new Label();
            lstActiveLeases = new ListBox();
            grpLeaseDetails = new GroupBox();
            lblLeaseDetails = new Label();
            cancelButtons = new FlowLayoutPanel();
            btnCancelLease = new Button();
            btnRefreshActive = new Button();
            lblCancelNote = new Label();
            layoutRoot.SuspendLayout();
            headerRow.SuspendLayout();
            tabs.SuspendLayout();
            tabHistory.SuspendLayout();
            historyLayout.SuspendLayout();
            historyFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).BeginInit();
            tabLeases.SuspendLayout();
            leasesLayout.SuspendLayout();
            leasesFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLeases).BeginInit();
            tabCancel.SuspendLayout();
            cancelLayout.SuspendLayout();
            grpLeaseDetails.SuspendLayout();
            cancelButtons.SuspendLayout();
            SuspendLayout();
            //
            // layoutRoot
            //
            layoutRoot.ColumnCount = 1;
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutRoot.Controls.Add(headerRow, 0, 0);
            layoutRoot.Controls.Add(tabs, 0, 1);
            layoutRoot.Dock = DockStyle.Fill;
            layoutRoot.Location = new Point(0, 0);
            layoutRoot.Name = "layoutRoot";
            layoutRoot.Padding = new Padding(12, 6, 12, 12);
            layoutRoot.RowCount = 2;
            layoutRoot.RowStyles.Add(new RowStyle());
            layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRoot.Size = new Size(1060, 620);
            layoutRoot.TabIndex = 0;
            //
            // headerRow
            //
            headerRow.AutoSize = true;
            headerRow.ColumnCount = 3;
            headerRow.ColumnStyles.Add(new ColumnStyle());
            headerRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            headerRow.ColumnStyles.Add(new ColumnStyle());
            headerRow.Controls.Add(lblTitle, 0, 0);
            headerRow.Controls.Add(lblMessage, 1, 0);
            headerRow.Controls.Add(btnLogout, 2, 0);
            headerRow.Dock = DockStyle.Fill;
            headerRow.Name = "headerRow";
            headerRow.RowCount = 1;
            headerRow.RowStyles.Add(new RowStyle());
            headerRow.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.Anchor = AnchorStyles.Left;
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Administration";
            //
            // lblMessage
            //
            lblMessage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 10.5F);
            lblMessage.Margin = new Padding(20, 0, 3, 0);
            lblMessage.Name = "lblMessage";
            lblMessage.TabIndex = 1;
            //
            // btnLogout
            //
            btnLogout.Anchor = AnchorStyles.Right;
            btnLogout.AutoSize = true;
            btnLogout.MinimumSize = new Size(120, 40);
            btnLogout.Name = "btnLogout";
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Log Out";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            //
            // tabs
            //
            tabs.Controls.Add(tabHistory);
            tabs.Controls.Add(tabLeases);
            tabs.Controls.Add(tabCancel);
            tabs.Dock = DockStyle.Fill;
            tabs.Margin = new Padding(3, 8, 3, 3);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.TabIndex = 1;
            tabs.SelectedIndexChanged += tabs_SelectedIndexChanged;
            //
            // tabHistory
            //
            tabHistory.Controls.Add(historyLayout);
            tabHistory.Name = "tabHistory";
            tabHistory.Padding = new Padding(6);
            tabHistory.TabIndex = 0;
            tabHistory.Text = "Parking History";
            tabHistory.UseVisualStyleBackColor = true;
            //
            // historyLayout
            //
            historyLayout.ColumnCount = 1;
            historyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            historyLayout.Controls.Add(historyFilter, 0, 0);
            historyLayout.Controls.Add(dgvHistory, 0, 1);
            historyLayout.Controls.Add(lblHistorySummary, 0, 2);
            historyLayout.Dock = DockStyle.Fill;
            historyLayout.Name = "historyLayout";
            historyLayout.RowCount = 3;
            historyLayout.RowStyles.Add(new RowStyle());
            historyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyLayout.RowStyles.Add(new RowStyle());
            historyLayout.TabIndex = 0;
            //
            // historyFilter
            //
            historyFilter.AutoSize = true;
            historyFilter.Controls.Add(lblPlateFilter);
            historyFilter.Controls.Add(txtPlateFilter);
            historyFilter.Controls.Add(btnRefreshHistory);
            historyFilter.Dock = DockStyle.Fill;
            historyFilter.Name = "historyFilter";
            historyFilter.TabIndex = 0;
            //
            // lblPlateFilter
            //
            lblPlateFilter.AutoSize = true;
            lblPlateFilter.Margin = new Padding(3, 9, 3, 3);
            lblPlateFilter.Name = "lblPlateFilter";
            lblPlateFilter.TabIndex = 0;
            lblPlateFilter.Text = "Plate contains:";
            //
            // txtPlateFilter
            //
            txtPlateFilter.CharacterCasing = CharacterCasing.Upper;
            txtPlateFilter.Margin = new Padding(3, 5, 12, 3);
            txtPlateFilter.MaxLength = 16;
            txtPlateFilter.Name = "txtPlateFilter";
            txtPlateFilter.Size = new Size(140, 27);
            txtPlateFilter.TabIndex = 1;
            txtPlateFilter.TextChanged += txtPlateFilter_TextChanged;
            //
            // btnRefreshHistory
            //
            btnRefreshHistory.AutoSize = true;
            btnRefreshHistory.MinimumSize = new Size(100, 34);
            btnRefreshHistory.Name = "btnRefreshHistory";
            btnRefreshHistory.TabIndex = 2;
            btnRefreshHistory.Text = "Refresh";
            btnRefreshHistory.UseVisualStyleBackColor = true;
            btnRefreshHistory.Click += btnRefreshHistory_Click;
            //
            // dgvHistory
            //
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.Name = "dgvHistory";
            dgvHistory.TabIndex = 1;
            //
            // lblHistorySummary
            //
            lblHistorySummary.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblHistorySummary.AutoSize = true;
            lblHistorySummary.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblHistorySummary.Margin = new Padding(3, 6, 3, 3);
            lblHistorySummary.Name = "lblHistorySummary";
            lblHistorySummary.TabIndex = 2;
            //
            // tabLeases
            //
            tabLeases.Controls.Add(leasesLayout);
            tabLeases.Name = "tabLeases";
            tabLeases.Padding = new Padding(6);
            tabLeases.TabIndex = 1;
            tabLeases.Text = "Leases";
            tabLeases.UseVisualStyleBackColor = true;
            //
            // leasesLayout
            //
            leasesLayout.ColumnCount = 1;
            leasesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            leasesLayout.Controls.Add(leasesFilter, 0, 0);
            leasesLayout.Controls.Add(dgvLeases, 0, 1);
            leasesLayout.Controls.Add(lblLeaseSummary, 0, 2);
            leasesLayout.Dock = DockStyle.Fill;
            leasesLayout.Name = "leasesLayout";
            leasesLayout.RowCount = 3;
            leasesLayout.RowStyles.Add(new RowStyle());
            leasesLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            leasesLayout.RowStyles.Add(new RowStyle());
            leasesLayout.TabIndex = 0;
            //
            // leasesFilter
            //
            leasesFilter.AutoSize = true;
            leasesFilter.Controls.Add(chkActiveOnly);
            leasesFilter.Controls.Add(btnRefreshLeases);
            leasesFilter.Dock = DockStyle.Fill;
            leasesFilter.Name = "leasesFilter";
            leasesFilter.TabIndex = 0;
            //
            // chkActiveOnly
            //
            chkActiveOnly.AutoSize = true;
            chkActiveOnly.Margin = new Padding(3, 8, 12, 3);
            chkActiveOnly.Name = "chkActiveOnly";
            chkActiveOnly.TabIndex = 0;
            chkActiveOnly.Text = "Show active leases only";
            chkActiveOnly.UseVisualStyleBackColor = true;
            chkActiveOnly.CheckedChanged += chkActiveOnly_CheckedChanged;
            //
            // btnRefreshLeases
            //
            btnRefreshLeases.AutoSize = true;
            btnRefreshLeases.MinimumSize = new Size(100, 34);
            btnRefreshLeases.Name = "btnRefreshLeases";
            btnRefreshLeases.TabIndex = 1;
            btnRefreshLeases.Text = "Refresh";
            btnRefreshLeases.UseVisualStyleBackColor = true;
            btnRefreshLeases.Click += btnRefreshLeases_Click;
            //
            // dgvLeases
            //
            dgvLeases.Dock = DockStyle.Fill;
            dgvLeases.Name = "dgvLeases";
            dgvLeases.TabIndex = 1;
            //
            // lblLeaseSummary
            //
            lblLeaseSummary.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblLeaseSummary.AutoSize = true;
            lblLeaseSummary.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLeaseSummary.Margin = new Padding(3, 6, 3, 3);
            lblLeaseSummary.Name = "lblLeaseSummary";
            lblLeaseSummary.TabIndex = 2;
            //
            // tabCancel
            //
            tabCancel.Controls.Add(cancelLayout);
            tabCancel.Name = "tabCancel";
            tabCancel.Padding = new Padding(6);
            tabCancel.TabIndex = 2;
            tabCancel.Text = "Cancel a Lease";
            tabCancel.UseVisualStyleBackColor = true;
            //
            // cancelLayout
            //
            cancelLayout.ColumnCount = 2;
            cancelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            cancelLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            cancelLayout.Controls.Add(lblActiveLeases, 0, 0);
            cancelLayout.Controls.Add(lstActiveLeases, 0, 1);
            cancelLayout.Controls.Add(grpLeaseDetails, 1, 1);
            cancelLayout.Dock = DockStyle.Fill;
            cancelLayout.Name = "cancelLayout";
            cancelLayout.RowCount = 2;
            cancelLayout.RowStyles.Add(new RowStyle());
            cancelLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            cancelLayout.TabIndex = 0;
            //
            // lblActiveLeases
            //
            lblActiveLeases.AutoSize = true;
            lblActiveLeases.Margin = new Padding(3, 6, 3, 6);
            lblActiveLeases.Name = "lblActiveLeases";
            lblActiveLeases.TabIndex = 0;
            lblActiveLeases.Text = "Active leases:";
            //
            // lstActiveLeases
            //
            lstActiveLeases.Dock = DockStyle.Fill;
            lstActiveLeases.IntegralHeight = false;
            lstActiveLeases.Name = "lstActiveLeases";
            lstActiveLeases.TabIndex = 1;
            lstActiveLeases.SelectedIndexChanged += lstActiveLeases_SelectedIndexChanged;
            //
            // grpLeaseDetails
            //
            grpLeaseDetails.Controls.Add(lblLeaseDetails);
            grpLeaseDetails.Controls.Add(lblCancelNote);
            grpLeaseDetails.Controls.Add(cancelButtons);
            grpLeaseDetails.Dock = DockStyle.Fill;
            grpLeaseDetails.Margin = new Padding(12, 3, 3, 3);
            grpLeaseDetails.Name = "grpLeaseDetails";
            grpLeaseDetails.Padding = new Padding(10);
            grpLeaseDetails.TabIndex = 2;
            grpLeaseDetails.TabStop = false;
            grpLeaseDetails.Text = "Selected lease";
            //
            // lblLeaseDetails
            //
            lblLeaseDetails.Dock = DockStyle.Fill;
            lblLeaseDetails.Font = new Font("Consolas", 10F);
            lblLeaseDetails.Name = "lblLeaseDetails";
            lblLeaseDetails.TabIndex = 0;
            lblLeaseDetails.Text = "Select a lease on the left.";
            //
            // lblCancelNote
            //
            lblCancelNote.AutoSize = true;
            lblCancelNote.Dock = DockStyle.Bottom;
            lblCancelNote.ForeColor = Color.DimGray;
            lblCancelNote.MaximumSize = new Size(420, 0);
            lblCancelNote.Name = "lblCancelNote";
            lblCancelNote.Padding = new Padding(0, 6, 0, 6);
            lblCancelNote.TabIndex = 1;
            lblCancelNote.Text = "Cancelling a lease releases its space. If the tenant's car is parked, it is checked out free of charge.";
            //
            // cancelButtons
            //
            cancelButtons.AutoSize = true;
            cancelButtons.Controls.Add(btnCancelLease);
            cancelButtons.Controls.Add(btnRefreshActive);
            cancelButtons.Dock = DockStyle.Bottom;
            cancelButtons.Name = "cancelButtons";
            cancelButtons.TabIndex = 2;
            //
            // btnCancelLease
            //
            btnCancelLease.AutoSize = true;
            btnCancelLease.Enabled = false;
            btnCancelLease.ForeColor = Color.Firebrick;
            btnCancelLease.MinimumSize = new Size(150, 40);
            btnCancelLease.Name = "btnCancelLease";
            btnCancelLease.TabIndex = 0;
            btnCancelLease.Text = "Cancel Lease";
            btnCancelLease.UseVisualStyleBackColor = true;
            btnCancelLease.Click += btnCancelLease_Click;
            //
            // btnRefreshActive
            //
            btnRefreshActive.AutoSize = true;
            btnRefreshActive.Margin = new Padding(12, 3, 3, 3);
            btnRefreshActive.MinimumSize = new Size(100, 40);
            btnRefreshActive.Name = "btnRefreshActive";
            btnRefreshActive.TabIndex = 1;
            btnRefreshActive.Text = "Refresh";
            btnRefreshActive.UseVisualStyleBackColor = true;
            btnRefreshActive.Click += btnRefreshActive_Click;
            //
            // AdminDashboardScreen
            //
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layoutRoot);
            Name = "AdminDashboardScreen";
            Size = new Size(1060, 620);
            VisibleChanged += AdminDashboardScreen_VisibleChanged;
            layoutRoot.ResumeLayout(false);
            layoutRoot.PerformLayout();
            headerRow.ResumeLayout(false);
            headerRow.PerformLayout();
            tabs.ResumeLayout(false);
            tabHistory.ResumeLayout(false);
            historyLayout.ResumeLayout(false);
            historyLayout.PerformLayout();
            historyFilter.ResumeLayout(false);
            historyFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistory).EndInit();
            tabLeases.ResumeLayout(false);
            leasesLayout.ResumeLayout(false);
            leasesLayout.PerformLayout();
            leasesFilter.ResumeLayout(false);
            leasesFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLeases).EndInit();
            tabCancel.ResumeLayout(false);
            cancelLayout.ResumeLayout(false);
            cancelLayout.PerformLayout();
            grpLeaseDetails.ResumeLayout(false);
            grpLeaseDetails.PerformLayout();
            cancelButtons.ResumeLayout(false);
            cancelButtons.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutRoot;
        private TableLayoutPanel headerRow;
        private Label lblTitle;
        private Label lblMessage;
        private Button btnLogout;
        private TabControl tabs;
        private TabPage tabHistory;
        private TableLayoutPanel historyLayout;
        private FlowLayoutPanel historyFilter;
        private Label lblPlateFilter;
        private TextBox txtPlateFilter;
        private Button btnRefreshHistory;
        private DataGridView dgvHistory;
        private Label lblHistorySummary;
        private TabPage tabLeases;
        private TableLayoutPanel leasesLayout;
        private FlowLayoutPanel leasesFilter;
        private CheckBox chkActiveOnly;
        private Button btnRefreshLeases;
        private DataGridView dgvLeases;
        private Label lblLeaseSummary;
        private TabPage tabCancel;
        private TableLayoutPanel cancelLayout;
        private Label lblActiveLeases;
        private ListBox lstActiveLeases;
        private GroupBox grpLeaseDetails;
        private Label lblLeaseDetails;
        private FlowLayoutPanel cancelButtons;
        private Button btnCancelLease;
        private Button btnRefreshActive;
        private Label lblCancelNote;
    }
}
