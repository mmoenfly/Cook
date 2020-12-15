namespace TechAdmin
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsMenuMain = new System.Windows.Forms.ToolStripMenuItem();
            this.pullCustomerFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DwnCustFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Custset = new System.Windows.Forms.TabControl();
            this.tbpCust = new System.Windows.Forms.TabPage();
            this.chkAll0 = new System.Windows.Forms.CheckBox();
            this.dgUnid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkBox1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SchoolNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dept = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xMLParmsDtlBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.cC2DataSetUnid1 = new TechAdmin.CC2DataSetUnid();
            this.dgCustomers = new System.Windows.Forms.DataGridView();
            this.customerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChkBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.xMLParmsDtlBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cCIDataSet = new TechAdmin.CCIDataSet();
            this.tbpShare = new System.Windows.Forms.TabPage();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbOffice = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkXML = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLetter = new System.Windows.Forms.TextBox();
            this.chkNet = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSheets = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkShare = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.lsUnids = new System.Windows.Forms.ListBox();
            this.txtShare = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.customerIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.xMLParmsDtlTableAdapter = new TechAdmin.CCIDataSetTableAdapters.XMLParmsDtlTableAdapter();
            this.xMLParmsDtlTableAdapter1 = new TechAdmin.CC2DataSetUnidTableAdapters.XMLParmsDtlTableAdapter();
            this.lblShareErr = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.Custset.SuspendLayout();
            this.tbpCust.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUnid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMLParmsDtlBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cC2DataSetUnid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMLParmsDtlBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cCIDataSet)).BeginInit();
            this.tbpShare.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuMain});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(757, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsMenuMain
            // 
            this.tsMenuMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pullCustomerFilesToolStripMenuItem,
            this.resetFormToolStripMenuItem});
            this.tsMenuMain.Name = "tsMenuMain";
            this.tsMenuMain.Size = new System.Drawing.Size(59, 24);
            this.tsMenuMain.Text = "Setup";
            // 
            // pullCustomerFilesToolStripMenuItem
            // 
            this.pullCustomerFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DwnCustFiles});
            this.pullCustomerFilesToolStripMenuItem.Name = "pullCustomerFilesToolStripMenuItem";
            this.pullCustomerFilesToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.pullCustomerFilesToolStripMenuItem.Text = "Customer Files";
            this.pullCustomerFilesToolStripMenuItem.Click += new System.EventHandler(this.pullCustomerFilesToolStripMenuItem_Click);
            // 
            // DwnCustFiles
            // 
            this.DwnCustFiles.Name = "DwnCustFiles";
            this.DwnCustFiles.Size = new System.Drawing.Size(247, 24);
            this.DwnCustFiles.Text = "Download Customer Files";
            this.DwnCustFiles.Click += new System.EventHandler(this.DwnCustFiles_Click);
            // 
            // resetFormToolStripMenuItem
            // 
            this.resetFormToolStripMenuItem.Name = "resetFormToolStripMenuItem";
            this.resetFormToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.resetFormToolStripMenuItem.Text = "Reset Form";
            this.resetFormToolStripMenuItem.Click += new System.EventHandler(this.resetFormToolStripMenuItem_Click);
            // 
            // Custset
            // 
            this.Custset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Custset.Controls.Add(this.tbpCust);
            this.Custset.Controls.Add(this.tbpShare);
            this.Custset.Location = new System.Drawing.Point(12, 31);
            this.Custset.Name = "Custset";
            this.Custset.SelectedIndex = 0;
            this.Custset.Size = new System.Drawing.Size(745, 399);
            this.Custset.TabIndex = 1;
            // 
            // tbpCust
            // 
            this.tbpCust.BackColor = System.Drawing.Color.LightBlue;
            this.tbpCust.Controls.Add(this.chkAll0);
            this.tbpCust.Controls.Add(this.dgUnid);
            this.tbpCust.Controls.Add(this.dgCustomers);
            this.tbpCust.Location = new System.Drawing.Point(4, 25);
            this.tbpCust.Name = "tbpCust";
            this.tbpCust.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCust.Size = new System.Drawing.Size(737, 370);
            this.tbpCust.TabIndex = 0;
            this.tbpCust.Text = "Setup";
            this.tbpCust.Click += new System.EventHandler(this.tbpCust_Click);
            // 
            // chkAll0
            // 
            this.chkAll0.AutoSize = true;
            this.chkAll0.Location = new System.Drawing.Point(274, 3);
            this.chkAll0.Name = "chkAll0";
            this.chkAll0.Size = new System.Drawing.Size(45, 21);
            this.chkAll0.TabIndex = 2;
            this.chkAll0.Text = "All";
            this.chkAll0.UseVisualStyleBackColor = true;
            this.chkAll0.CheckedChanged += new System.EventHandler(this.chkAll0_CheckedChanged);
            // 
            // dgUnid
            // 
            this.dgUnid.AllowUserToAddRows = false;
            this.dgUnid.AllowUserToDeleteRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Cornsilk;
            this.dgUnid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgUnid.AutoGenerateColumns = false;
            this.dgUnid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgUnid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgUnid.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgUnid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgUnid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUnid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.UserType,
            this.chkBox1,
            this.SchoolNum,
            this.Dept});
            this.dgUnid.DataSource = this.xMLParmsDtlBindingSource1;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgUnid.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgUnid.Dock = System.Windows.Forms.DockStyle.Right;
            this.dgUnid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgUnid.GridColor = System.Drawing.SystemColors.InactiveBorder;
            this.dgUnid.Location = new System.Drawing.Point(367, 3);
            this.dgUnid.Name = "dgUnid";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgUnid.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgUnid.RowHeadersVisible = false;
            this.dgUnid.RowTemplate.Height = 24;
            this.dgUnid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgUnid.Size = new System.Drawing.Size(367, 364);
            this.dgUnid.TabIndex = 1;
            this.dgUnid.Visible = false;
            this.dgUnid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgUnid_CellContentClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Customer ID";
            this.dataGridViewTextBoxColumn1.HeaderText = "CustId";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "UserName";
            this.dataGridViewTextBoxColumn2.HeaderText = "Customer Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // UserType
            // 
            this.UserType.DataPropertyName = "UserType";
            this.UserType.HeaderText = "UserType";
            this.UserType.Name = "UserType";
            // 
            // chkBox1
            // 
            this.chkBox1.FalseValue = "0";
            this.chkBox1.HeaderText = "Chk";
            this.chkBox1.Name = "chkBox1";
            this.chkBox1.TrueValue = "1";
            // 
            // SchoolNum
            // 
            this.SchoolNum.DataPropertyName = "SchoolNum";
            this.SchoolNum.HeaderText = "SchoolNum";
            this.SchoolNum.Name = "SchoolNum";
            this.SchoolNum.ReadOnly = true;
            this.SchoolNum.Visible = false;
            // 
            // Dept
            // 
            this.Dept.DataPropertyName = "Dept";
            this.Dept.HeaderText = "Dept";
            this.Dept.Name = "Dept";
            this.Dept.ReadOnly = true;
            this.Dept.Visible = false;
            // 
            // xMLParmsDtlBindingSource1
            // 
            this.xMLParmsDtlBindingSource1.DataMember = "XMLParmsDtl";
            this.xMLParmsDtlBindingSource1.DataSource = this.cC2DataSetUnid1;
            this.xMLParmsDtlBindingSource1.CurrentChanged += new System.EventHandler(this.xMLParmsDtlBindingSource1_CurrentChanged);
            // 
            // cC2DataSetUnid1
            // 
            this.cC2DataSetUnid1.DataSetName = "CC2DataSetUnid";
            this.cC2DataSetUnid1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dgCustomers
            // 
            this.dgCustomers.AllowUserToAddRows = false;
            this.dgCustomers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Cornsilk;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Transparent;
            this.dgCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dgCustomers.AutoGenerateColumns = false;
            this.dgCustomers.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerDataGridViewTextBoxColumn,
            this.unid,
            this.ChkBox});
            this.dgCustomers.DataSource = this.xMLParmsDtlBindingSource;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCustomers.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgCustomers.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgCustomers.Location = new System.Drawing.Point(6, 6);
            this.dgCustomers.Name = "dgCustomers";
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.SeaShell;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCustomers.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            this.dgCustomers.RowHeadersVisible = false;
            this.dgCustomers.RowTemplate.Height = 24;
            this.dgCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgCustomers.ShowEditingIcon = false;
            this.dgCustomers.Size = new System.Drawing.Size(253, 314);
            this.dgCustomers.TabIndex = 0;
            this.dgCustomers.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgCustomers_CurrentCellChanged);
            this.dgCustomers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCustomers_CellContentClick);
            // 
            // customerDataGridViewTextBoxColumn
            // 
            this.customerDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.customerDataGridViewTextBoxColumn.DataPropertyName = "Customer";
            this.customerDataGridViewTextBoxColumn.HeaderText = "Customer";
            this.customerDataGridViewTextBoxColumn.Name = "customerDataGridViewTextBoxColumn";
            this.customerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // unid
            // 
            this.unid.DataPropertyName = "unid";
            this.unid.HeaderText = "unid";
            this.unid.Name = "unid";
            this.unid.Visible = false;
            // 
            // ChkBox
            // 
            this.ChkBox.DividerWidth = 5;
            this.ChkBox.FalseValue = "0";
            this.ChkBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ChkBox.HeaderText = "Chk";
            this.ChkBox.Name = "ChkBox";
            this.ChkBox.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.ChkBox.TrueValue = "1";
            this.ChkBox.Width = 50;
            // 
            // xMLParmsDtlBindingSource
            // 
            this.xMLParmsDtlBindingSource.DataMember = "XMLParmsDtl";
            this.xMLParmsDtlBindingSource.DataSource = this.cCIDataSet;
            this.xMLParmsDtlBindingSource.CurrentChanged += new System.EventHandler(this.xMLParmsDtlBindingSource_CurrentChanged);
            // 
            // cCIDataSet
            // 
            this.cCIDataSet.DataSetName = "CCIDataSet";
            this.cCIDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tbpShare
            // 
            this.tbpShare.BackColor = System.Drawing.Color.LightBlue;
            this.tbpShare.Controls.Add(this.lblShareErr);
            this.tbpShare.Controls.Add(this.txtEmail);
            this.tbpShare.Controls.Add(this.label6);
            this.tbpShare.Controls.Add(this.cmbOffice);
            this.tbpShare.Controls.Add(this.label7);
            this.tbpShare.Controls.Add(this.chkXML);
            this.tbpShare.Controls.Add(this.label5);
            this.tbpShare.Controls.Add(this.txtLetter);
            this.tbpShare.Controls.Add(this.chkNet);
            this.tbpShare.Controls.Add(this.label4);
            this.tbpShare.Controls.Add(this.chkSheets);
            this.tbpShare.Controls.Add(this.label3);
            this.tbpShare.Controls.Add(this.chkShare);
            this.tbpShare.Controls.Add(this.label2);
            this.tbpShare.Controls.Add(this.chkAll);
            this.tbpShare.Controls.Add(this.lsUnids);
            this.tbpShare.Controls.Add(this.txtShare);
            this.tbpShare.Controls.Add(this.label1);
            this.tbpShare.Location = new System.Drawing.Point(4, 25);
            this.tbpShare.Name = "tbpShare";
            this.tbpShare.Padding = new System.Windows.Forms.Padding(3);
            this.tbpShare.Size = new System.Drawing.Size(737, 370);
            this.tbpShare.TabIndex = 1;
            this.tbpShare.Text = "Deployment";
            this.tbpShare.Click += new System.EventHandler(this.tbpShare_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Location = new System.Drawing.Point(231, 139);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(489, 22);
            this.txtEmail.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(231, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Enter your email address  for install";
            // 
            // cmbOffice
            // 
            this.cmbOffice.FormattingEnabled = true;
            this.cmbOffice.Items.AddRange(new object[] {
            "2003",
            "2007",
            "2010"});
            this.cmbOffice.Location = new System.Drawing.Point(531, 38);
            this.cmbOffice.Name = "cmbOffice";
            this.cmbOffice.Size = new System.Drawing.Size(73, 24);
            this.cmbOffice.Sorted = true;
            this.cmbOffice.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(231, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Office Version?";
            // 
            // chkXML
            // 
            this.chkXML.AutoSize = true;
            this.chkXML.Location = new System.Drawing.Point(180, 41);
            this.chkXML.Name = "chkXML";
            this.chkXML.Size = new System.Drawing.Size(54, 21);
            this.chkXML.TabIndex = 11;
            this.chkXML.Text = "Yes";
            this.chkXML.UseVisualStyleBackColor = true;
            this.chkXML.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Use XML?";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtLetter
            // 
            this.txtLetter.Location = new System.Drawing.Point(571, 10);
            this.txtLetter.MaxLength = 1;
            this.txtLetter.Name = "txtLetter";
            this.txtLetter.ShortcutsEnabled = false;
            this.txtLetter.Size = new System.Drawing.Size(33, 22);
            this.txtLetter.TabIndex = 3;
            this.txtLetter.TextChanged += new System.EventHandler(this.txtLetter_TextChanged);
            // 
            // chkNet
            // 
            this.chkNet.AutoSize = true;
            this.chkNet.Location = new System.Drawing.Point(472, 12);
            this.chkNet.Name = "chkNet";
            this.chkNet.Size = new System.Drawing.Size(54, 21);
            this.chkNet.TabIndex = 2;
            this.chkNet.Text = "Yes";
            this.chkNet.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(231, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Use Network Mapping?";
            // 
            // chkSheets
            // 
            this.chkSheets.AutoSize = true;
            this.chkSheets.Location = new System.Drawing.Point(180, 12);
            this.chkSheets.Name = "chkSheets";
            this.chkSheets.Size = new System.Drawing.Size(54, 21);
            this.chkSheets.TabIndex = 1;
            this.chkSheets.Text = "Yes";
            this.chkSheets.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Download Sheets?";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // chkShare
            // 
            this.chkShare.AutoSize = true;
            this.chkShare.Location = new System.Drawing.Point(180, 81);
            this.chkShare.Name = "chkShare";
            this.chkShare.Size = new System.Drawing.Size(18, 17);
            this.chkShare.TabIndex = 6;
            this.chkShare.UseVisualStyleBackColor = true;
            this.chkShare.CheckedChanged += new System.EventHandler(this.chkShare_CheckedChanged);
            this.chkShare.MouseHover += new System.EventHandler(this.chkShare_MouseHover);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(231, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter the UNC Path to build the installer directory";
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(10, 81);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(45, 21);
            this.chkAll.TabIndex = 4;
            this.chkAll.Text = "All";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // lsUnids
            // 
            this.lsUnids.FormattingEnabled = true;
            this.lsUnids.ItemHeight = 16;
            this.lsUnids.Location = new System.Drawing.Point(10, 108);
            this.lsUnids.Name = "lsUnids";
            this.lsUnids.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lsUnids.Size = new System.Drawing.Size(194, 212);
            this.lsUnids.TabIndex = 2;
            // 
            // txtShare
            // 
            this.txtShare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtShare.Location = new System.Drawing.Point(231, 94);
            this.txtShare.Name = "txtShare";
            this.txtShare.Size = new System.Drawing.Size(489, 22);
            this.txtShare.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select your Unids";
            // 
            // customerIDDataGridViewTextBoxColumn
            // 
            this.customerIDDataGridViewTextBoxColumn.DataPropertyName = "Customer ID";
            this.customerIDDataGridViewTextBoxColumn.HeaderText = "Customer ID";
            this.customerIDDataGridViewTextBoxColumn.Name = "customerIDDataGridViewTextBoxColumn";
            this.customerIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(678, 432);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 15;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblStatus.CausesValidation = false;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblStatus.Location = new System.Drawing.Point(-2, 487);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(759, 20);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Visible = false;
            // 
            // xMLParmsDtlTableAdapter
            // 
            this.xMLParmsDtlTableAdapter.ClearBeforeFill = true;
            // 
            // xMLParmsDtlTableAdapter1
            // 
            this.xMLParmsDtlTableAdapter1.ClearBeforeFill = true;
            // 
            // lblShareErr
            // 
            this.lblShareErr.AutoSize = true;
            this.lblShareErr.Location = new System.Drawing.Point(231, 57);
            this.lblShareErr.Name = "lblShareErr";
            this.lblShareErr.Size = new System.Drawing.Size(0, 17);
            this.lblShareErr.TabIndex = 18;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(757, 506);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.Custset);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = " ";
            this.Load += new System.EventHandler(this.primary_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.Custset.ResumeLayout(false);
            this.tbpCust.ResumeLayout(false);
            this.tbpCust.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUnid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMLParmsDtlBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cC2DataSetUnid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xMLParmsDtlBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cCIDataSet)).EndInit();
            this.tbpShare.ResumeLayout(false);
            this.tbpShare.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

   
        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsMenuMain;
        private System.Windows.Forms.ToolStripMenuItem pullCustomerFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DwnCustFiles;
        private System.Windows.Forms.TabControl Custset;
        private System.Windows.Forms.TabPage tbpCust;
        private System.Windows.Forms.TabPage tbpShare;
        private CCIDataSet cCIDataSet;
        private System.Windows.Forms.DataGridView dgCustomers;
        private System.Windows.Forms.BindingSource xMLParmsDtlBindingSource;
        private TechAdmin.CCIDataSetTableAdapters.XMLParmsDtlTableAdapter xMLParmsDtlTableAdapter;
        private System.Windows.Forms.DataGridView dgUnid;
        private CC2DataSetUnid cC2DataSetUnid1;
        private System.Windows.Forms.BindingSource xMLParmsDtlBindingSource1;
        private TechAdmin.CC2DataSetUnidTableAdapters.XMLParmsDtlTableAdapter xMLParmsDtlTableAdapter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShare;
        private System.Windows.Forms.ListBox lsUnids;
        private System.Windows.Forms.CheckBox chkAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkShare;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.CheckBox chkAll0;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn unid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ChkBox;
        private System.Windows.Forms.CheckBox chkSheets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem resetFormToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SchoolNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dept;
        private System.Windows.Forms.CheckBox chkNet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLetter;
        private System.Windows.Forms.CheckBox chkXML;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbOffice;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblShareErr;
    }

}

