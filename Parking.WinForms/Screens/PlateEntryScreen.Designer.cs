namespace Parking.WinForms.Screens
{
    partial class PlateEntryScreen
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
            plateRow = new TableLayoutPanel();
            txtPlateOrToken = new TextBox();
            btnContinue = new Button();
            grpNewCar = new GroupBox();
            newCarLayout = new TableLayoutPanel();
            lblSize = new Label();
            rbSmall = new RadioButton();
            rbBig = new RadioButton();
            chkEV = new CheckBox();
            chkLease = new CheckBox();
            grpLease = new GroupBox();
            leaseLayout = new TableLayoutPanel();
            lblName = new Label();
            txtName = new TextBox();
            lblContact = new Label();
            txtContact = new TextBox();
            lblAddress = new Label();
            txtAddress = new TextBox();
            detailsButtons = new FlowLayoutPanel();
            btnFindSpace = new Button();
            btnChangePlate = new Button();
            lblMessage = new Label();
            layoutRoot.SuspendLayout();
            layoutBody.SuspendLayout();
            plateRow.SuspendLayout();
            grpNewCar.SuspendLayout();
            newCarLayout.SuspendLayout();
            grpLease.SuspendLayout();
            leaseLayout.SuspendLayout();
            detailsButtons.SuspendLayout();
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
            layoutBody.AutoSize = true;
            layoutBody.ColumnCount = 1;
            layoutBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutBody.Controls.Add(lblTitle, 0, 0);
            layoutBody.Controls.Add(lblHint, 0, 1);
            layoutBody.Controls.Add(plateRow, 0, 2);
            layoutBody.Controls.Add(grpNewCar, 0, 3);
            layoutBody.Controls.Add(grpLease, 0, 4);
            layoutBody.Controls.Add(detailsButtons, 0, 5);
            layoutBody.Controls.Add(lblMessage, 0, 6);
            layoutBody.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            layoutBody.Location = new Point(138, 3);
            layoutBody.Name = "layoutBody";
            layoutBody.RowCount = 7;
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.RowStyles.Add(new RowStyle());
            layoutBody.Size = new Size(624, 530);
            layoutBody.TabIndex = 0;
            //
            // lblTitle
            //
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(3, 24);
            lblTitle.Margin = new Padding(3, 24, 3, 6);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(225, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Vehicle Gate";
            //
            // lblHint
            //
            lblHint.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblHint.AutoSize = true;
            lblHint.ForeColor = Color.DimGray;
            lblHint.Location = new Point(3, 71);
            lblHint.Margin = new Padding(3, 0, 3, 12);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(618, 40);
            lblHint.TabIndex = 1;
            lblHint.Text = "Enter or scan the number plate and press Continue. New vehicles then choose a space; vehicles already parked go to checkout. Staff can enter their admin token (it starts with @).";
            //
            // plateRow
            //
            plateRow.AutoSize = true;
            plateRow.ColumnCount = 2;
            plateRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            plateRow.ColumnStyles.Add(new ColumnStyle());
            plateRow.Controls.Add(txtPlateOrToken, 0, 0);
            plateRow.Controls.Add(btnContinue, 1, 0);
            plateRow.Dock = DockStyle.Fill;
            plateRow.Location = new Point(3, 126);
            plateRow.Name = "plateRow";
            plateRow.RowCount = 1;
            plateRow.RowStyles.Add(new RowStyle());
            plateRow.Size = new Size(618, 50);
            plateRow.TabIndex = 2;
            //
            // txtPlateOrToken
            //
            txtPlateOrToken.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtPlateOrToken.Font = new Font("Segoe UI", 14F);
            txtPlateOrToken.Location = new Point(3, 5);
            txtPlateOrToken.MaxLength = 32;
            txtPlateOrToken.Name = "txtPlateOrToken";
            txtPlateOrToken.PlaceholderText = "e.g. ABC123";
            txtPlateOrToken.Size = new Size(466, 39);
            txtPlateOrToken.TabIndex = 0;
            txtPlateOrToken.KeyDown += txtPlateOrToken_KeyDown;
            //
            // btnContinue
            //
            btnContinue.Anchor = AnchorStyles.Left;
            btnContinue.AutoSize = true;
            btnContinue.Location = new Point(475, 3);
            btnContinue.MinimumSize = new Size(140, 44);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(140, 44);
            btnContinue.TabIndex = 1;
            btnContinue.Text = "Continue";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            //
            // grpNewCar
            //
            grpNewCar.AutoSize = true;
            grpNewCar.Controls.Add(newCarLayout);
            grpNewCar.Dock = DockStyle.Fill;
            grpNewCar.Location = new Point(3, 182);
            grpNewCar.Margin = new Padding(3, 12, 3, 3);
            grpNewCar.Name = "grpNewCar";
            grpNewCar.Padding = new Padding(10);
            grpNewCar.Size = new Size(618, 140);
            grpNewCar.TabIndex = 3;
            grpNewCar.TabStop = false;
            grpNewCar.Text = "New vehicle";
            grpNewCar.Visible = false;
            //
            // newCarLayout
            //
            newCarLayout.AutoSize = true;
            newCarLayout.ColumnCount = 3;
            newCarLayout.ColumnStyles.Add(new ColumnStyle());
            newCarLayout.ColumnStyles.Add(new ColumnStyle());
            newCarLayout.ColumnStyles.Add(new ColumnStyle());
            newCarLayout.Controls.Add(lblSize, 0, 0);
            newCarLayout.Controls.Add(rbSmall, 1, 0);
            newCarLayout.Controls.Add(rbBig, 2, 0);
            newCarLayout.Controls.Add(chkEV, 0, 1);
            newCarLayout.Controls.Add(chkLease, 0, 2);
            newCarLayout.Dock = DockStyle.Fill;
            newCarLayout.Location = new Point(10, 30);
            newCarLayout.Name = "newCarLayout";
            newCarLayout.RowCount = 3;
            newCarLayout.RowStyles.Add(new RowStyle());
            newCarLayout.RowStyles.Add(new RowStyle());
            newCarLayout.RowStyles.Add(new RowStyle());
            newCarLayout.Size = new Size(598, 100);
            newCarLayout.TabIndex = 0;
            //
            // lblSize
            //
            lblSize.Anchor = AnchorStyles.Left;
            lblSize.AutoSize = true;
            lblSize.Location = new Point(3, 5);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(97, 20);
            lblSize.TabIndex = 0;
            lblSize.Text = "Vehicle size:";
            //
            // rbSmall
            //
            rbSmall.AutoSize = true;
            rbSmall.Checked = true;
            rbSmall.Location = new Point(106, 3);
            rbSmall.Name = "rbSmall";
            rbSmall.Size = new Size(108, 24);
            rbSmall.TabIndex = 1;
            rbSmall.TabStop = true;
            rbSmall.Text = "Small (car)";
            rbSmall.UseVisualStyleBackColor = true;
            //
            // rbBig
            //
            rbBig.AutoSize = true;
            rbBig.Location = new Point(220, 3);
            rbBig.Name = "rbBig";
            rbBig.Size = new Size(148, 24);
            rbBig.TabIndex = 2;
            rbBig.Text = "Big (SUV / van)";
            rbBig.UseVisualStyleBackColor = true;
            //
            // chkEV
            //
            chkEV.AutoSize = true;
            newCarLayout.SetColumnSpan(chkEV, 3);
            chkEV.Location = new Point(3, 36);
            chkEV.Margin = new Padding(3, 6, 3, 3);
            chkEV.Name = "chkEV";
            chkEV.Size = new Size(184, 24);
            chkEV.TabIndex = 3;
            chkEV.Text = "Electric vehicle (EV)";
            chkEV.UseVisualStyleBackColor = true;
            //
            // chkLease
            //
            chkLease.AutoSize = true;
            newCarLayout.SetColumnSpan(chkLease, 3);
            chkLease.Location = new Point(3, 69);
            chkLease.Margin = new Padding(3, 6, 3, 3);
            chkLease.Name = "chkLease";
            chkLease.Size = new Size(324, 24);
            chkLease.TabIndex = 4;
            chkLease.Text = "Sign a long-term lease for a fixed space";
            chkLease.UseVisualStyleBackColor = true;
            chkLease.CheckedChanged += chkLease_CheckedChanged;
            //
            // grpLease
            //
            grpLease.AutoSize = true;
            grpLease.Controls.Add(leaseLayout);
            grpLease.Dock = DockStyle.Fill;
            grpLease.Location = new Point(3, 337);
            grpLease.Margin = new Padding(3, 12, 3, 3);
            grpLease.Name = "grpLease";
            grpLease.Padding = new Padding(10);
            grpLease.Size = new Size(618, 150);
            grpLease.TabIndex = 4;
            grpLease.TabStop = false;
            grpLease.Text = "Lease details";
            grpLease.Visible = false;
            //
            // leaseLayout
            //
            leaseLayout.AutoSize = true;
            leaseLayout.ColumnCount = 2;
            leaseLayout.ColumnStyles.Add(new ColumnStyle());
            leaseLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            leaseLayout.Controls.Add(lblName, 0, 0);
            leaseLayout.Controls.Add(txtName, 1, 0);
            leaseLayout.Controls.Add(lblContact, 0, 1);
            leaseLayout.Controls.Add(txtContact, 1, 1);
            leaseLayout.Controls.Add(lblAddress, 0, 2);
            leaseLayout.Controls.Add(txtAddress, 1, 2);
            leaseLayout.Dock = DockStyle.Fill;
            leaseLayout.Location = new Point(10, 30);
            leaseLayout.Name = "leaseLayout";
            leaseLayout.RowCount = 3;
            leaseLayout.RowStyles.Add(new RowStyle());
            leaseLayout.RowStyles.Add(new RowStyle());
            leaseLayout.RowStyles.Add(new RowStyle());
            leaseLayout.Size = new Size(598, 110);
            leaseLayout.TabIndex = 0;
            //
            // lblName
            //
            lblName.Anchor = AnchorStyles.Left;
            lblName.AutoSize = true;
            lblName.Location = new Point(3, 8);
            lblName.Name = "lblName";
            lblName.Size = new Size(52, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            //
            // txtName
            //
            txtName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new Point(80, 4);
            txtName.MaxLength = 100;
            txtName.Name = "txtName";
            txtName.Size = new Size(515, 27);
            txtName.TabIndex = 1;
            //
            // lblContact
            //
            lblContact.Anchor = AnchorStyles.Left;
            lblContact.AutoSize = true;
            lblContact.Location = new Point(3, 44);
            lblContact.Name = "lblContact";
            lblContact.Size = new Size(64, 20);
            lblContact.TabIndex = 2;
            lblContact.Text = "Contact:";
            //
            // txtContact
            //
            txtContact.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtContact.Location = new Point(80, 40);
            txtContact.MaxLength = 100;
            txtContact.Name = "txtContact";
            txtContact.PlaceholderText = "Phone or email";
            txtContact.Size = new Size(515, 27);
            txtContact.TabIndex = 3;
            //
            // lblAddress
            //
            lblAddress.Anchor = AnchorStyles.Left;
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(3, 80);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(65, 20);
            lblAddress.TabIndex = 4;
            lblAddress.Text = "Address:";
            //
            // txtAddress
            //
            txtAddress.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtAddress.Location = new Point(80, 76);
            txtAddress.MaxLength = 200;
            txtAddress.Name = "txtAddress";
            txtAddress.Size = new Size(515, 27);
            txtAddress.TabIndex = 5;
            //
            // lblMessage
            //
            lblMessage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            lblMessage.AutoSize = true;
            lblMessage.Font = new Font("Segoe UI", 11F);
            lblMessage.Location = new Point(3, 502);
            lblMessage.Margin = new Padding(3, 12, 3, 3);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(618, 25);
            lblMessage.TabIndex = 5;
            //
            // detailsButtons
            //
            detailsButtons.AutoSize = true;
            detailsButtons.Controls.Add(btnFindSpace);
            detailsButtons.Controls.Add(btnChangePlate);
            detailsButtons.Dock = DockStyle.Fill;
            detailsButtons.Margin = new Padding(0, 12, 0, 3);
            detailsButtons.Name = "detailsButtons";
            detailsButtons.TabIndex = 6;
            detailsButtons.Visible = false;
            //
            // btnFindSpace
            //
            btnFindSpace.AutoSize = true;
            btnFindSpace.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFindSpace.MinimumSize = new Size(170, 44);
            btnFindSpace.Name = "btnFindSpace";
            btnFindSpace.TabIndex = 0;
            btnFindSpace.Text = "Choose a Space";
            btnFindSpace.UseVisualStyleBackColor = true;
            btnFindSpace.Click += btnFindSpace_Click;
            //
            // btnChangePlate
            //
            btnChangePlate.AutoSize = true;
            btnChangePlate.Margin = new Padding(12, 3, 3, 3);
            btnChangePlate.MinimumSize = new Size(140, 44);
            btnChangePlate.Name = "btnChangePlate";
            btnChangePlate.TabIndex = 1;
            btnChangePlate.Text = "Change Plate";
            btnChangePlate.UseVisualStyleBackColor = true;
            btnChangePlate.Click += btnChangePlate_Click;
            //
            // PlateEntryScreen
            //
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(layoutRoot);
            Name = "PlateEntryScreen";
            Size = new Size(900, 560);
            VisibleChanged += PlateEntryScreen_VisibleChanged;
            layoutRoot.ResumeLayout(false);
            layoutRoot.PerformLayout();
            layoutBody.ResumeLayout(false);
            layoutBody.PerformLayout();
            plateRow.ResumeLayout(false);
            plateRow.PerformLayout();
            grpNewCar.ResumeLayout(false);
            grpNewCar.PerformLayout();
            newCarLayout.ResumeLayout(false);
            newCarLayout.PerformLayout();
            grpLease.ResumeLayout(false);
            grpLease.PerformLayout();
            leaseLayout.ResumeLayout(false);
            leaseLayout.PerformLayout();
            detailsButtons.ResumeLayout(false);
            detailsButtons.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel layoutRoot;
        private TableLayoutPanel layoutBody;
        private Label lblTitle;
        private Label lblHint;
        private TableLayoutPanel plateRow;
        private TextBox txtPlateOrToken;
        private Button btnContinue;
        private GroupBox grpNewCar;
        private TableLayoutPanel newCarLayout;
        private Label lblSize;
        private RadioButton rbSmall;
        private RadioButton rbBig;
        private CheckBox chkEV;
        private CheckBox chkLease;
        private GroupBox grpLease;
        private TableLayoutPanel leaseLayout;
        private Label lblName;
        private TextBox txtName;
        private Label lblContact;
        private TextBox txtContact;
        private Label lblAddress;
        private TextBox txtAddress;
        private FlowLayoutPanel detailsButtons;
        private Button btnFindSpace;
        private Button btnChangePlate;
        private Label lblMessage;
    }
}
