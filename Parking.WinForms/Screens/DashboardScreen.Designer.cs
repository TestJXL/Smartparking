namespace Parking.WinForms.Screens
{
    partial class DashboardScreen
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            layoutRoot = new TableLayoutPanel();
            layoutBody = new TableLayoutPanel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            statsLayout = new TableLayoutPanel();
            lblAvailableCount = new Label();
            lblEnabledCount = new Label();
            lblTotalCount = new Label();
            lblAvailableCaption = new Label();
            lblEnabledCaption = new Label();
            lblTotalCaption = new Label();
            occupancyBar = new Parking.WinForms.Controls.OccupancyBar();
            lblLegend = new Label();
            buttonRow = new FlowLayoutPanel();
            btnArrival = new Button();
            btnDeparture = new Button();
            btnRefresh = new Button();
            lblMessage = new Label();
            layoutRoot.SuspendLayout();
            layoutBody.SuspendLayout();
            statsLayout.SuspendLayout();
            buttonRow.SuspendLayout();
            SuspendLayout();
            //
            // layoutRoot
            //
            layoutRoot.AutoScroll = true;
            layoutRoot.ColumnCount = 3;
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            layoutRoot.Controls.Add(layoutBody, 1, 0);
            layoutRoot.Dock = DockStyle.Fill;
            layoutRoot.Location = new Point(0, 0);
            layoutRoot.Name = "layoutRoot";
            layoutRoot.RowCount = 1;
            layoutRoot.RowStyles.Add(new RowStyle());
            layoutRoot.Size = new Size(900, 560);
            layoutRoot.TabIndex = 0;
            //
            // layoutBody
            //
            layoutBody.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            layoutBody.AutoSize = true;
            layoutBody.ColumnCount = 1;
            layoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBody.Controls.Add(lblTitle, 0, 0);
            layoutBody.Controls.Add(lblSubtitle, 0, 1);
            layoutBody.Controls.Add(statsLayout, 0, 2);
            layoutBody.Controls.Add(occupancyBar, 0, 3);
            layoutBody.Controls.Add(lblLegend, 0, 4);
            layoutBody.Controls.Add(buttonRow, 0, 5);
            layoutBody.Controls.Add(lblMessage, 0, 6);
            layoutBody.Location = new Point(138, 3);
            layoutBody.Name = "layoutBody";
            layoutBody.RowCount = 7;
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.Size = new Size(624, 420);
            layoutBody.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(3, 24);
            lblTitle.Margin = new Padding(3, 24, 3, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(280, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Car Park Overview";
            //
            // lblSubtitle
            //
            lblSubtitle.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblSubtitle.AutoSize = true;
            lblSubtitle.ForeColor = Color.DimGray;
            lblSubtitle.Location = new Point(3, 71);
            lblSubtitle.Margin = new Padding(3, 0, 3, 16);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(618, 20);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Live space availability. When a car reaches the gate, press Vehicle Arrival or Vehicle Departure.";
            //
            // statsLayout
            //
            statsLayout.AutoSize = true;
            statsLayout.ColumnCount = 3;
            statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            statsLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            statsLayout.Controls.Add(lblAvailableCount, 0, 0);
            statsLayout.Controls.Add(lblEnabledCount, 1, 0);
            statsLayout.Controls.Add(lblTotalCount, 2, 0);
            statsLayout.Controls.Add(lblAvailableCaption, 0, 1);
            statsLayout.Controls.Add(lblEnabledCaption, 1, 1);
            statsLayout.Controls.Add(lblTotalCaption, 2, 1);
            statsLayout.Dock = DockStyle.Fill;
            statsLayout.Location = new Point(3, 110);
            statsLayout.Name = "statsLayout";
            statsLayout.RowCount = 2;
            statsLayout.RowStyles.Add(new RowStyle());
            statsLayout.RowStyles.Add(new RowStyle());
            statsLayout.Size = new Size(618, 100);
            statsLayout.TabIndex = 2;
            //
            // lblAvailableCount
            //
            lblAvailableCount.AutoSize = true;
            lblAvailableCount.Dock = DockStyle.Fill;
            lblAvailableCount.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblAvailableCount.ForeColor = Color.FromArgb(46, 139, 87);
            lblAvailableCount.Name = "lblAvailableCount";
            lblAvailableCount.TabIndex = 0;
            lblAvailableCount.Text = "–";
            lblAvailableCount.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblEnabledCount
            //
            lblEnabledCount.AutoSize = true;
            lblEnabledCount.Dock = DockStyle.Fill;
            lblEnabledCount.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblEnabledCount.Name = "lblEnabledCount";
            lblEnabledCount.TabIndex = 1;
            lblEnabledCount.Text = "–";
            lblEnabledCount.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblTotalCount
            //
            lblTotalCount.AutoSize = true;
            lblTotalCount.Dock = DockStyle.Fill;
            lblTotalCount.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblTotalCount.ForeColor = Color.DimGray;
            lblTotalCount.Name = "lblTotalCount";
            lblTotalCount.TabIndex = 2;
            lblTotalCount.Text = "–";
            lblTotalCount.TextAlign = ContentAlignment.MiddleCenter;
            //
            // lblAvailableCaption
            //
            lblAvailableCaption.AutoSize = true;
            lblAvailableCaption.Dock = DockStyle.Fill;
            lblAvailableCaption.ForeColor = Color.DimGray;
            lblAvailableCaption.Name = "lblAvailableCaption";
            lblAvailableCaption.TabIndex = 3;
            lblAvailableCaption.Text = "Available now";
            lblAvailableCaption.TextAlign = ContentAlignment.TopCenter;
            //
            // lblEnabledCaption
            //
            lblEnabledCaption.AutoSize = true;
            lblEnabledCaption.Dock = DockStyle.Fill;
            lblEnabledCaption.ForeColor = Color.DimGray;
            lblEnabledCaption.Name = "lblEnabledCaption";
            lblEnabledCaption.TabIndex = 4;
            lblEnabledCaption.Text = "Spaces in service";
            lblEnabledCaption.TextAlign = ContentAlignment.TopCenter;
            //
            // lblTotalCaption
            //
            lblTotalCaption.AutoSize = true;
            lblTotalCaption.Dock = DockStyle.Fill;
            lblTotalCaption.ForeColor = Color.DimGray;
            lblTotalCaption.Name = "lblTotalCaption";
            lblTotalCaption.TabIndex = 5;
            lblTotalCaption.Text = "All spaces";
            lblTotalCaption.TextAlign = ContentAlignment.TopCenter;
            //
            // occupancyBar
            //
            occupancyBar.Dock = DockStyle.Fill;
            occupancyBar.Margin = new Padding(3, 12, 3, 4);
            occupancyBar.Name = "occupancyBar";
            occupancyBar.TabIndex = 3;
            //
            // lblLegend
            //
            lblLegend.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblLegend.AutoSize = true;
            lblLegend.ForeColor = Color.DimGray;
            lblLegend.Name = "lblLegend";
            lblLegend.TabIndex = 4;
            lblLegend.Text = "Red = in use or leased · Green = available · Grey = out of service";
            //
            // buttonRow
            //
            buttonRow.AutoSize = true;
            buttonRow.Controls.Add(btnArrival);
            buttonRow.Controls.Add(btnDeparture);
            buttonRow.Controls.Add(btnRefresh);
            buttonRow.Dock = DockStyle.Fill;
            buttonRow.Margin = new Padding(0, 20, 0, 3);
            buttonRow.Name = "buttonRow";
            buttonRow.TabIndex = 5;
            //
            // btnArrival
            //
            btnArrival.AutoSize = true;
            btnArrival.BackColor = Color.FromArgb(33, 53, 85);
            btnArrival.FlatStyle = FlatStyle.Flat;
            btnArrival.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnArrival.ForeColor = Color.White;
            btnArrival.MinimumSize = new Size(220, 56);
            btnArrival.Name = "btnArrival";
            btnArrival.TabIndex = 0;
            btnArrival.Text = "Vehicle Arrival";
            btnArrival.UseVisualStyleBackColor = false;
            btnArrival.Click += btnArrival_Click;
            //
            // btnDeparture
            //
            btnDeparture.AutoSize = true;
            btnDeparture.BackColor = Color.FromArgb(33, 53, 85);
            btnDeparture.FlatStyle = FlatStyle.Flat;
            btnDeparture.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDeparture.ForeColor = Color.White;
            btnDeparture.Margin = new Padding(12, 3, 3, 3);
            btnDeparture.MinimumSize = new Size(220, 56);
            btnDeparture.Name = "btnDeparture";
            btnDeparture.TabIndex = 1;
            btnDeparture.Text = "Vehicle Departure";
            btnDeparture.UseVisualStyleBackColor = false;
            btnDeparture.Click += btnDeparture_Click;
            //
            // btnRefresh
            //
            btnRefresh.AutoSize = true;
            btnRefresh.Margin = new Padding(12, 3, 3, 3);
            btnRefresh.MinimumSize = new Size(120, 56);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            //
            // lblMessage
            //
            lblMessage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 11F);
            lblMessage.Margin = new Padding(3, 12, 3, 3);
            lblMessage.Name = "lblMessage";
            lblMessage.TabIndex = 6;
            //
            // DashboardScreen
            //
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layoutRoot);
            Name = "DashboardScreen";
            Size = new Size(900, 560);
            VisibleChanged += DashboardScreen_VisibleChanged;
            layoutRoot.ResumeLayout(false);
            layoutRoot.PerformLayout();
            layoutBody.ResumeLayout(false);
            layoutBody.PerformLayout();
            statsLayout.ResumeLayout(false);
            statsLayout.PerformLayout();
            buttonRow.ResumeLayout(false);
            buttonRow.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutRoot;
        private TableLayoutPanel layoutBody;
        private Label lblTitle;
        private Label lblSubtitle;
        private TableLayoutPanel statsLayout;
        private Label lblAvailableCount;
        private Label lblEnabledCount;
        private Label lblTotalCount;
        private Label lblAvailableCaption;
        private Label lblEnabledCaption;
        private Label lblTotalCaption;
        private Parking.WinForms.Controls.OccupancyBar occupancyBar;
        private Label lblLegend;
        private FlowLayoutPanel buttonRow;
        private Button btnArrival;
        private Button btnDeparture;
        private Button btnRefresh;
        private Label lblMessage;
    }
}
