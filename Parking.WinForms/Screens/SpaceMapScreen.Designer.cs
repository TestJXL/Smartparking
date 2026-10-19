namespace Parking.WinForms.Screens
{
    partial class SpaceMapScreen
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
            lblTitle = new Label();
            legendFlow = new FlowLayoutPanel();
            lblLegendAvailable = new Label();
            lblLegendLeased = new Label();
            lblLegendOccupied = new Label();
            lblLegendNotSuitable = new Label();
            lblLegendInactive = new Label();
            lblLegendRecommended = new Label();
            lblLegendSelected = new Label();
            mapPanel = new Panel();
            rightLayout = new TableLayoutPanel();
            grpRecommend = new GroupBox();
            lblRecommendation = new Label();
            btnUseRecommended = new Button();
            grpSelected = new GroupBox();
            lblSelected = new Label();
            bottomBar = new TableLayoutPanel();
            lblMessage = new Label();
            btnCancel = new Button();
            btnRefresh = new Button();
            btnConfirm = new Button();
            layoutRoot.SuspendLayout();
            legendFlow.SuspendLayout();
            rightLayout.SuspendLayout();
            grpRecommend.SuspendLayout();
            grpSelected.SuspendLayout();
            bottomBar.SuspendLayout();
            SuspendLayout();
            //
            // layoutRoot
            //
            layoutRoot.ColumnCount = 2;
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
            layoutRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            layoutRoot.Controls.Add(lblTitle, 0, 0);
            layoutRoot.Controls.Add(legendFlow, 0, 1);
            layoutRoot.Controls.Add(mapPanel, 0, 2);
            layoutRoot.Controls.Add(rightLayout, 1, 2);
            layoutRoot.Controls.Add(bottomBar, 0, 3);
            layoutRoot.Dock = DockStyle.Fill;
            layoutRoot.Location = new Point(0, 0);
            layoutRoot.Name = "layoutRoot";
            layoutRoot.Padding = new Padding(12);
            layoutRoot.RowCount = 4;
            layoutRoot.RowStyles.Add(new RowStyle());
            layoutRoot.RowStyles.Add(new RowStyle());
            layoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutRoot.RowStyles.Add(new RowStyle());
            layoutRoot.Size = new Size(1000, 600);
            layoutRoot.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            layoutRoot.SetColumnSpan(lblTitle, 2);
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Choose a Parking Space";
            //
            // legendFlow
            //
            legendFlow.AutoSize = true;
            layoutRoot.SetColumnSpan(legendFlow, 2);
            legendFlow.Controls.Add(lblLegendAvailable);
            legendFlow.Controls.Add(lblLegendLeased);
            legendFlow.Controls.Add(lblLegendOccupied);
            legendFlow.Controls.Add(lblLegendNotSuitable);
            legendFlow.Controls.Add(lblLegendInactive);
            legendFlow.Controls.Add(lblLegendRecommended);
            legendFlow.Controls.Add(lblLegendSelected);
            legendFlow.Dock = DockStyle.Fill;
            legendFlow.Margin = new Padding(3, 3, 3, 8);
            legendFlow.Name = "legendFlow";
            legendFlow.TabIndex = 1;
            //
            // lblLegendAvailable
            //
            lblLegendAvailable.AutoSize = true;
            lblLegendAvailable.BorderStyle = BorderStyle.FixedSingle;
            lblLegendAvailable.Name = "lblLegendAvailable";
            lblLegendAvailable.Padding = new Padding(6, 3, 6, 3);
            lblLegendAvailable.TabIndex = 0;
            lblLegendAvailable.Text = "Available";
            //
            // lblLegendLeased
            //
            lblLegendLeased.AutoSize = true;
            lblLegendLeased.BorderStyle = BorderStyle.FixedSingle;
            lblLegendLeased.Name = "lblLegendLeased";
            lblLegendLeased.Padding = new Padding(6, 3, 6, 3);
            lblLegendLeased.TabIndex = 1;
            lblLegendLeased.Text = "Leased";
            //
            // lblLegendOccupied
            //
            lblLegendOccupied.AutoSize = true;
            lblLegendOccupied.BorderStyle = BorderStyle.FixedSingle;
            lblLegendOccupied.Name = "lblLegendOccupied";
            lblLegendOccupied.Padding = new Padding(6, 3, 6, 3);
            lblLegendOccupied.TabIndex = 2;
            lblLegendOccupied.Text = "Occupied";
            //
            // lblLegendNotSuitable
            //
            lblLegendNotSuitable.AutoSize = true;
            lblLegendNotSuitable.BorderStyle = BorderStyle.FixedSingle;
            lblLegendNotSuitable.ForeColor = Color.Gray;
            lblLegendNotSuitable.Name = "lblLegendNotSuitable";
            lblLegendNotSuitable.Padding = new Padding(6, 3, 6, 3);
            lblLegendNotSuitable.TabIndex = 3;
            lblLegendNotSuitable.Text = "Not suitable for this vehicle";
            //
            // lblLegendInactive
            //
            lblLegendInactive.AutoSize = true;
            lblLegendInactive.BackColor = Color.FromArgb(225, 225, 225);
            lblLegendInactive.BorderStyle = BorderStyle.FixedSingle;
            lblLegendInactive.ForeColor = Color.DimGray;
            lblLegendInactive.Name = "lblLegendInactive";
            lblLegendInactive.Padding = new Padding(6, 3, 6, 3);
            lblLegendInactive.TabIndex = 4;
            lblLegendInactive.Text = "Inactive";
            //
            // lblLegendRecommended
            //
            lblLegendRecommended.AutoSize = true;
            lblLegendRecommended.BorderStyle = BorderStyle.FixedSingle;
            lblLegendRecommended.ForeColor = Color.FromArgb(30, 110, 220);
            lblLegendRecommended.Name = "lblLegendRecommended";
            lblLegendRecommended.Padding = new Padding(6, 3, 6, 3);
            lblLegendRecommended.TabIndex = 5;
            lblLegendRecommended.Text = "★ Recommended";
            //
            // lblLegendSelected
            //
            lblLegendSelected.AutoSize = true;
            lblLegendSelected.BackColor = Color.FromArgb(20, 40, 90);
            lblLegendSelected.BorderStyle = BorderStyle.FixedSingle;
            lblLegendSelected.ForeColor = Color.White;
            lblLegendSelected.Name = "lblLegendSelected";
            lblLegendSelected.Padding = new Padding(6, 3, 6, 3);
            lblLegendSelected.TabIndex = 6;
            lblLegendSelected.Text = "Selected";
            //
            // mapPanel
            //
            mapPanel.AutoScroll = true;
            mapPanel.BackColor = Color.FromArgb(236, 238, 241);
            mapPanel.BorderStyle = BorderStyle.FixedSingle;
            mapPanel.Dock = DockStyle.Fill;
            mapPanel.Name = "mapPanel";
            mapPanel.TabIndex = 2;
            //
            // rightLayout
            //
            rightLayout.ColumnCount = 1;
            rightLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightLayout.Controls.Add(grpRecommend, 0, 0);
            rightLayout.Controls.Add(grpSelected, 0, 1);
            rightLayout.Dock = DockStyle.Fill;
            rightLayout.Margin = new Padding(6, 3, 0, 3);
            rightLayout.Name = "rightLayout";
            rightLayout.RowCount = 3;
            rightLayout.RowStyles.Add(new RowStyle());
            rightLayout.RowStyles.Add(new RowStyle());
            rightLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            rightLayout.TabIndex = 3;
            //
            // grpRecommend
            //
            grpRecommend.Controls.Add(lblRecommendation);
            grpRecommend.Controls.Add(btnUseRecommended);
            grpRecommend.Dock = DockStyle.Fill;
            grpRecommend.Name = "grpRecommend";
            grpRecommend.Padding = new Padding(8);
            grpRecommend.Size = new Size(320, 170);
            grpRecommend.TabIndex = 0;
            grpRecommend.TabStop = false;
            grpRecommend.Text = "★ Recommended space";
            //
            // lblRecommendation
            //
            lblRecommendation.Dock = DockStyle.Fill;
            lblRecommendation.Name = "lblRecommendation";
            lblRecommendation.TabIndex = 0;
            //
            // btnUseRecommended
            //
            btnUseRecommended.Dock = DockStyle.Bottom;
            btnUseRecommended.Enabled = false;
            btnUseRecommended.Name = "btnUseRecommended";
            btnUseRecommended.Size = new Size(304, 40);
            btnUseRecommended.TabIndex = 1;
            btnUseRecommended.Text = "Use Recommended Space";
            btnUseRecommended.UseVisualStyleBackColor = true;
            btnUseRecommended.Click += btnUseRecommended_Click;
            //
            // grpSelected
            //
            grpSelected.Controls.Add(lblSelected);
            grpSelected.Dock = DockStyle.Fill;
            grpSelected.Name = "grpSelected";
            grpSelected.Padding = new Padding(8);
            grpSelected.Size = new Size(320, 110);
            grpSelected.TabIndex = 1;
            grpSelected.TabStop = false;
            grpSelected.Text = "Selected space";
            //
            // lblSelected
            //
            lblSelected.Dock = DockStyle.Fill;
            lblSelected.Name = "lblSelected";
            lblSelected.TabIndex = 0;
            lblSelected.Text = "No space selected.";
            //
            // bottomBar
            //
            bottomBar.AutoSize = true;
            bottomBar.ColumnCount = 4;
            layoutRoot.SetColumnSpan(bottomBar, 2);
            bottomBar.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            bottomBar.ColumnStyles.Add(new ColumnStyle());
            bottomBar.ColumnStyles.Add(new ColumnStyle());
            bottomBar.ColumnStyles.Add(new ColumnStyle());
            bottomBar.Controls.Add(lblMessage, 0, 0);
            bottomBar.Controls.Add(btnCancel, 1, 0);
            bottomBar.Controls.Add(btnRefresh, 2, 0);
            bottomBar.Controls.Add(btnConfirm, 3, 0);
            bottomBar.Dock = DockStyle.Fill;
            bottomBar.Name = "bottomBar";
            bottomBar.RowCount = 1;
            bottomBar.RowStyles.Add(new RowStyle());
            bottomBar.TabIndex = 4;
            //
            // lblMessage
            //
            lblMessage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 10.5F);
            lblMessage.Name = "lblMessage";
            lblMessage.TabIndex = 0;
            //
            // btnCancel
            //
            btnCancel.Anchor = AnchorStyles.Right;
            btnCancel.AutoSize = true;
            btnCancel.MinimumSize = new Size(110, 44);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            //
            // btnRefresh
            //
            btnRefresh.Anchor = AnchorStyles.Right;
            btnRefresh.AutoSize = true;
            btnRefresh.MinimumSize = new Size(110, 44);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            //
            // btnConfirm
            //
            btnConfirm.Anchor = AnchorStyles.Right;
            btnConfirm.AutoSize = true;
            btnConfirm.Enabled = false;
            btnConfirm.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnConfirm.MinimumSize = new Size(170, 44);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.TabIndex = 3;
            btnConfirm.Text = "Confirm Space";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            //
            // SpaceMapScreen
            //
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layoutRoot);
            Name = "SpaceMapScreen";
            Size = new Size(1000, 600);
            VisibleChanged += SpaceMapScreen_VisibleChanged;
            layoutRoot.ResumeLayout(false);
            layoutRoot.PerformLayout();
            legendFlow.ResumeLayout(false);
            legendFlow.PerformLayout();
            rightLayout.ResumeLayout(false);
            grpRecommend.ResumeLayout(false);
            grpSelected.ResumeLayout(false);
            bottomBar.ResumeLayout(false);
            bottomBar.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutRoot;
        private Label lblTitle;
        private FlowLayoutPanel legendFlow;
        private Label lblLegendAvailable;
        private Label lblLegendLeased;
        private Label lblLegendOccupied;
        private Label lblLegendNotSuitable;
        private Label lblLegendInactive;
        private Label lblLegendRecommended;
        private Label lblLegendSelected;
        private Panel mapPanel;
        private TableLayoutPanel rightLayout;
        private GroupBox grpRecommend;
        private Label lblRecommendation;
        private Button btnUseRecommended;
        private GroupBox grpSelected;
        private Label lblSelected;
        private TableLayoutPanel bottomBar;
        private Label lblMessage;
        private Button btnCancel;
        private Button btnRefresh;
        private Button btnConfirm;
    }
}
