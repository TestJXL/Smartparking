namespace Parking.WinForms.Screens
{
    partial class CheckoutScreen
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
            lblHint = new Label();
            grpQuote = new GroupBox();
            quoteLayout = new TableLayoutPanel();
            lblPlateCaption = new Label();
            lblPlate = new Label();
            lblSpaceCaption = new Label();
            lblSpace = new Label();
            lblEntryCaption = new Label();
            lblEntry = new Label();
            lblCheckoutCaption = new Label();
            lblCheckout = new Label();
            lblDurationCaption = new Label();
            lblDuration = new Label();
            lblHoursCaption = new Label();
            lblHours = new Label();
            lblRateCaption = new Label();
            lblRate = new Label();
            lblAmountCaption = new Label();
            lblAmount = new Label();
            lblGraceNote = new Label();
            buttonRow = new FlowLayoutPanel();
            btnPay = new Button();
            btnCancel = new Button();
            grpReceipt = new GroupBox();
            lblReceipt = new Label();
            btnDone = new Button();
            lblMessage = new Label();
            layoutRoot.SuspendLayout();
            layoutBody.SuspendLayout();
            grpQuote.SuspendLayout();
            quoteLayout.SuspendLayout();
            buttonRow.SuspendLayout();
            grpReceipt.SuspendLayout();
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
            layoutBody.Controls.Add(lblHint, 0, 1);
            layoutBody.Controls.Add(grpQuote, 0, 2);
            layoutBody.Controls.Add(lblGraceNote, 0, 3);
            layoutBody.Controls.Add(buttonRow, 0, 4);
            layoutBody.Controls.Add(grpReceipt, 0, 5);
            layoutBody.Controls.Add(btnDone, 0, 6);
            layoutBody.Controls.Add(lblMessage, 0, 7);
            layoutBody.Location = new Point(138, 3);
            layoutBody.Name = "layoutBody";
            layoutBody.RowCount = 8;
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.Size = new Size(624, 540);
            layoutBody.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Margin = new Padding(3, 24, 3, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Checkout";
            //
            // lblHint
            //
            lblHint.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblHint.AutoSize = true;
            lblHint.ForeColor = Color.DimGray;
            lblHint.Margin = new Padding(3, 0, 3, 12);
            lblHint.Name = "lblHint";
            lblHint.TabIndex = 1;
            //
            // grpQuote
            //
            grpQuote.AutoSize = true;
            grpQuote.Controls.Add(quoteLayout);
            grpQuote.Dock = DockStyle.Fill;
            grpQuote.Name = "grpQuote";
            grpQuote.Padding = new Padding(10);
            grpQuote.TabIndex = 2;
            grpQuote.TabStop = false;
            grpQuote.Text = "Amount to pay";
            grpQuote.Visible = false;
            //
            // quoteLayout
            //
            quoteLayout.AutoSize = true;
            quoteLayout.ColumnCount = 2;
            quoteLayout.ColumnStyles.Add(new ColumnStyle());
            quoteLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            quoteLayout.Controls.Add(lblPlateCaption, 0, 0);
            quoteLayout.Controls.Add(lblPlate, 1, 0);
            quoteLayout.Controls.Add(lblSpaceCaption, 0, 1);
            quoteLayout.Controls.Add(lblSpace, 1, 1);
            quoteLayout.Controls.Add(lblEntryCaption, 0, 2);
            quoteLayout.Controls.Add(lblEntry, 1, 2);
            quoteLayout.Controls.Add(lblCheckoutCaption, 0, 3);
            quoteLayout.Controls.Add(lblCheckout, 1, 3);
            quoteLayout.Controls.Add(lblDurationCaption, 0, 4);
            quoteLayout.Controls.Add(lblDuration, 1, 4);
            quoteLayout.Controls.Add(lblHoursCaption, 0, 5);
            quoteLayout.Controls.Add(lblHours, 1, 5);
            quoteLayout.Controls.Add(lblRateCaption, 0, 6);
            quoteLayout.Controls.Add(lblRate, 1, 6);
            quoteLayout.Controls.Add(lblAmountCaption, 0, 7);
            quoteLayout.Controls.Add(lblAmount, 1, 7);
            quoteLayout.Dock = DockStyle.Fill;
            quoteLayout.Name = "quoteLayout";
            quoteLayout.RowCount = 8;
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.RowStyles.Add(new RowStyle());
            quoteLayout.TabIndex = 0;
            //
            // lblPlateCaption
            //
            lblPlateCaption.AutoSize = true;
            lblPlateCaption.Margin = new Padding(3, 3, 24, 3);
            lblPlateCaption.Name = "lblPlateCaption";
            lblPlateCaption.TabIndex = 0;
            lblPlateCaption.Text = "Plate";
            //
            // lblPlate
            //
            lblPlate.AutoSize = true;
            lblPlate.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPlate.Margin = new Padding(3);
            lblPlate.Name = "lblPlate";
            lblPlate.TabIndex = 1;
            //
            // lblSpaceCaption
            //
            lblSpaceCaption.AutoSize = true;
            lblSpaceCaption.Margin = new Padding(3, 3, 24, 3);
            lblSpaceCaption.Name = "lblSpaceCaption";
            lblSpaceCaption.TabIndex = 2;
            lblSpaceCaption.Text = "Space";
            //
            // lblSpace
            //
            lblSpace.AutoSize = true;
            lblSpace.Margin = new Padding(3);
            lblSpace.Name = "lblSpace";
            lblSpace.TabIndex = 3;
            //
            // lblEntryCaption
            //
            lblEntryCaption.AutoSize = true;
            lblEntryCaption.Margin = new Padding(3, 3, 24, 3);
            lblEntryCaption.Name = "lblEntryCaption";
            lblEntryCaption.TabIndex = 4;
            lblEntryCaption.Text = "Entry time";
            //
            // lblEntry
            //
            lblEntry.AutoSize = true;
            lblEntry.Margin = new Padding(3);
            lblEntry.Name = "lblEntry";
            lblEntry.TabIndex = 5;
            //
            // lblCheckoutCaption
            //
            lblCheckoutCaption.AutoSize = true;
            lblCheckoutCaption.Margin = new Padding(3, 3, 24, 3);
            lblCheckoutCaption.Name = "lblCheckoutCaption";
            lblCheckoutCaption.TabIndex = 6;
            lblCheckoutCaption.Text = "Checkout time";
            //
            // lblCheckout
            //
            lblCheckout.AutoSize = true;
            lblCheckout.Margin = new Padding(3);
            lblCheckout.Name = "lblCheckout";
            lblCheckout.TabIndex = 7;
            //
            // lblDurationCaption
            //
            lblDurationCaption.AutoSize = true;
            lblDurationCaption.Margin = new Padding(3, 3, 24, 3);
            lblDurationCaption.Name = "lblDurationCaption";
            lblDurationCaption.TabIndex = 8;
            lblDurationCaption.Text = "Duration";
            //
            // lblDuration
            //
            lblDuration.AutoSize = true;
            lblDuration.Margin = new Padding(3);
            lblDuration.Name = "lblDuration";
            lblDuration.TabIndex = 9;
            //
            // lblHoursCaption
            //
            lblHoursCaption.AutoSize = true;
            lblHoursCaption.Margin = new Padding(3, 3, 24, 3);
            lblHoursCaption.Name = "lblHoursCaption";
            lblHoursCaption.TabIndex = 10;
            lblHoursCaption.Text = "Chargeable hours";
            //
            // lblHours
            //
            lblHours.AutoSize = true;
            lblHours.Margin = new Padding(3);
            lblHours.Name = "lblHours";
            lblHours.TabIndex = 11;
            //
            // lblRateCaption
            //
            lblRateCaption.AutoSize = true;
            lblRateCaption.Margin = new Padding(3, 3, 24, 3);
            lblRateCaption.Name = "lblRateCaption";
            lblRateCaption.TabIndex = 12;
            lblRateCaption.Text = "Hourly rate";
            //
            // lblRate
            //
            lblRate.AutoSize = true;
            lblRate.Margin = new Padding(3);
            lblRate.Name = "lblRate";
            lblRate.TabIndex = 13;
            //
            // lblAmountCaption
            //
            lblAmountCaption.Anchor = AnchorStyles.Left;
            lblAmountCaption.AutoSize = true;
            lblAmountCaption.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAmountCaption.Margin = new Padding(3, 8, 24, 3);
            lblAmountCaption.Name = "lblAmountCaption";
            lblAmountCaption.TabIndex = 14;
            lblAmountCaption.Text = "Amount due";
            //
            // lblAmount
            //
            lblAmount.AutoSize = true;
            lblAmount.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblAmount.ForeColor = Color.FromArgb(33, 53, 85);
            lblAmount.Margin = new Padding(3, 5, 3, 3);
            lblAmount.Name = "lblAmount";
            lblAmount.TabIndex = 15;
            //
            // lblGraceNote
            //
            lblGraceNote.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblGraceNote.AutoSize = true;
            lblGraceNote.ForeColor = Color.DimGray;
            lblGraceNote.Margin = new Padding(3, 6, 3, 6);
            lblGraceNote.Name = "lblGraceNote";
            lblGraceNote.TabIndex = 3;
            lblGraceNote.Visible = false;
            //
            // buttonRow
            //
            buttonRow.AutoSize = true;
            buttonRow.Controls.Add(btnPay);
            buttonRow.Controls.Add(btnCancel);
            buttonRow.Dock = DockStyle.Fill;
            buttonRow.Margin = new Padding(0, 8, 0, 3);
            buttonRow.Name = "buttonRow";
            buttonRow.TabIndex = 4;
            //
            // btnPay
            //
            btnPay.AutoSize = true;
            btnPay.Enabled = false;
            btnPay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPay.MinimumSize = new Size(210, 44);
            btnPay.Name = "btnPay";
            btnPay.TabIndex = 0;
            btnPay.Text = "Pay && Exit";
            btnPay.UseVisualStyleBackColor = true;
            btnPay.Click += btnPay_Click;
            //
            // btnCancel
            //
            btnCancel.AutoSize = true;
            btnCancel.Margin = new Padding(12, 3, 3, 3);
            btnCancel.MinimumSize = new Size(120, 44);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            //
            // grpReceipt
            //
            grpReceipt.AutoSize = true;
            grpReceipt.BackColor = Color.FromArgb(232, 244, 236);
            grpReceipt.Controls.Add(lblReceipt);
            grpReceipt.Dock = DockStyle.Fill;
            grpReceipt.Margin = new Padding(3, 12, 3, 3);
            grpReceipt.Name = "grpReceipt";
            grpReceipt.Padding = new Padding(10);
            grpReceipt.TabIndex = 5;
            grpReceipt.TabStop = false;
            grpReceipt.Text = "Receipt";
            grpReceipt.Visible = false;
            //
            // lblReceipt
            //
            lblReceipt.AutoSize = true;
            lblReceipt.Dock = DockStyle.Fill;
            lblReceipt.Font = new Font("Consolas", 10F);
            lblReceipt.Name = "lblReceipt";
            lblReceipt.TabIndex = 0;
            //
            // btnDone
            //
            btnDone.AutoSize = true;
            btnDone.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDone.Margin = new Padding(3, 8, 3, 3);
            btnDone.MinimumSize = new Size(160, 44);
            btnDone.Name = "btnDone";
            btnDone.TabIndex = 6;
            btnDone.Text = "Done";
            btnDone.UseVisualStyleBackColor = true;
            btnDone.Visible = false;
            btnDone.Click += btnDone_Click;
            //
            // lblMessage
            //
            lblMessage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 11F);
            lblMessage.Margin = new Padding(3, 12, 3, 3);
            lblMessage.Name = "lblMessage";
            lblMessage.TabIndex = 7;
            //
            // CheckoutScreen
            //
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layoutRoot);
            Name = "CheckoutScreen";
            Size = new Size(900, 560);
            VisibleChanged += CheckoutScreen_VisibleChanged;
            layoutRoot.ResumeLayout(false);
            layoutRoot.PerformLayout();
            layoutBody.ResumeLayout(false);
            layoutBody.PerformLayout();
            grpQuote.ResumeLayout(false);
            grpQuote.PerformLayout();
            quoteLayout.ResumeLayout(false);
            quoteLayout.PerformLayout();
            buttonRow.ResumeLayout(false);
            buttonRow.PerformLayout();
            grpReceipt.ResumeLayout(false);
            grpReceipt.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutRoot;
        private TableLayoutPanel layoutBody;
        private Label lblTitle;
        private Label lblHint;
        private GroupBox grpQuote;
        private TableLayoutPanel quoteLayout;
        private Label lblPlateCaption;
        private Label lblPlate;
        private Label lblSpaceCaption;
        private Label lblSpace;
        private Label lblEntryCaption;
        private Label lblEntry;
        private Label lblCheckoutCaption;
        private Label lblCheckout;
        private Label lblDurationCaption;
        private Label lblDuration;
        private Label lblHoursCaption;
        private Label lblHours;
        private Label lblRateCaption;
        private Label lblRate;
        private Label lblAmountCaption;
        private Label lblAmount;
        private Label lblGraceNote;
        private FlowLayoutPanel buttonRow;
        private Button btnPay;
        private Button btnCancel;
        private GroupBox grpReceipt;
        private Label lblReceipt;
        private Button btnDone;
        private Label lblMessage;
    }
}
