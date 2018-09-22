namespace WhereIsMyPhoto
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.imageInformationTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.filesListBox = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.skipWinDirectoryCheckBox = new System.Windows.Forms.CheckBox();
            this.allDrivesCheckBox = new System.Windows.Forms.CheckBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.settingsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.endDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.startDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.datesCheckBox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.orientationComboBox = new System.Windows.Forms.ComboBox();
            this.orientationСheckBox = new System.Windows.Forms.CheckBox();
            this.exposureProgramComboBox = new System.Windows.Forms.ComboBox();
            this.ExposureProgramCheckBox = new System.Windows.Forms.CheckBox();
            this.flashOnCheckBox = new System.Windows.Forms.CheckBox();
            this.WhiteBalanceСheckBox = new System.Windows.Forms.CheckBox();
            this.gpsCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.maxISOTextBox = new System.Windows.Forms.TextBox();
            this.minISOTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ISOCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.maxExposureTimeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.minExposureTimeTextBox = new System.Windows.Forms.TextBox();
            this.ExposureTimeCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.CameraTextBox = new System.Windows.Forms.TextBox();
            this.cameraCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.EditCheckBox = new System.Windows.Forms.CheckBox();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.showHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.infoPanel, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.settingsPanel, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(891, 485);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // statusStrip
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.statusStrip, 2);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status});
            this.statusStrip.Location = new System.Drawing.Point(0, 465);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(891, 20);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(16, 15);
            this.status.Text = "...";
            this.status.Click += new System.EventHandler(this.status_Click);
            // 
            // infoPanel
            // 
            this.infoPanel.AutoScroll = true;
            this.infoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.infoPanel.Controls.Add(this.imageInformationTextBox);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPanel.Location = new System.Drawing.Point(573, 263);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Size = new System.Drawing.Size(315, 199);
            this.infoPanel.TabIndex = 1;
            // 
            // imageInformationTextBox
            // 
            this.imageInformationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.imageInformationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageInformationTextBox.Location = new System.Drawing.Point(0, 0);
            this.imageInformationTextBox.Multiline = true;
            this.imageInformationTextBox.Name = "imageInformationTextBox";
            this.imageInformationTextBox.ReadOnly = true;
            this.imageInformationTextBox.Size = new System.Drawing.Size(313, 197);
            this.imageInformationTextBox.TabIndex = 0;
            this.imageInformationTextBox.TabStop = false;
            this.imageInformationTextBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageInformation_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.filesListBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 263);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(564, 199);
            this.panel1.TabIndex = 5;
            // 
            // filesListBox
            // 
            this.filesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.filesListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesListBox.FormattingEnabled = true;
            this.filesListBox.HorizontalScrollbar = true;
            this.filesListBox.Location = new System.Drawing.Point(0, 0);
            this.filesListBox.Name = "filesListBox";
            this.filesListBox.Size = new System.Drawing.Size(562, 197);
            this.filesListBox.TabIndex = 7;
            this.filesListBox.SelectedIndexChanged += new System.EventHandler(this.files_SelectedIndexChanged);
            this.filesListBox.DoubleClick += new System.EventHandler(this.files_DoubleClick);
            // 
            // panel2
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel2, 2);
            this.panel2.Controls.Add(this.skipWinDirectoryCheckBox);
            this.panel2.Controls.Add(this.allDrivesCheckBox);
            this.panel2.Controls.Add(this.pathTextBox);
            this.panel2.Controls.Add(this.browseButton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(885, 54);
            this.panel2.TabIndex = 0;
            // 
            // skipWinDirectoryCheckBox
            // 
            this.skipWinDirectoryCheckBox.AutoSize = true;
            this.skipWinDirectoryCheckBox.Location = new System.Drawing.Point(125, 31);
            this.skipWinDirectoryCheckBox.Name = "skipWinDirectoryCheckBox";
            this.skipWinDirectoryCheckBox.Size = new System.Drawing.Size(188, 17);
            this.skipWinDirectoryCheckBox.TabIndex = 4;
            this.skipWinDirectoryCheckBox.Text = "Не сканировать папку системы";
            this.skipWinDirectoryCheckBox.UseVisualStyleBackColor = true;
            // 
            // allDrivesCheckBox
            // 
            this.allDrivesCheckBox.AutoSize = true;
            this.allDrivesCheckBox.Location = new System.Drawing.Point(7, 31);
            this.allDrivesCheckBox.Name = "allDrivesCheckBox";
            this.allDrivesCheckBox.Size = new System.Drawing.Size(111, 17);
            this.allDrivesCheckBox.TabIndex = 3;
            this.allDrivesCheckBox.Text = "Весь компьютер";
            this.allDrivesCheckBox.UseVisualStyleBackColor = true;
            this.allDrivesCheckBox.CheckedChanged += new System.EventHandler(this.allDrivesCheckBox_CheckedChanged);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pathTextBox.Location = new System.Drawing.Point(7, 5);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(786, 20);
            this.pathTextBox.TabIndex = 0;
            // 
            // browseButton
            // 
            this.browseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseButton.Location = new System.Drawing.Point(797, 3);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(79, 23);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Обзор...";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // settingsPanel
            // 
            this.settingsPanel.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.settingsPanel, 2);
            this.settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.settingsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.settingsPanel.Controls.Add(this.groupBox1, 0, 0);
            this.settingsPanel.Controls.Add(this.startButton, 3, 1);
            this.settingsPanel.Controls.Add(this.groupBox2, 3, 0);
            this.settingsPanel.Controls.Add(this.groupBox3, 1, 0);
            this.settingsPanel.Controls.Add(this.groupBox4, 2, 0);
            this.settingsPanel.Controls.Add(this.groupBox5, 0, 1);
            this.settingsPanel.Controls.Add(this.groupBox6, 1, 1);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(3, 63);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.RowCount = 2;
            this.settingsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.settingsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.settingsPanel.Size = new System.Drawing.Size(885, 194);
            this.settingsPanel.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.endDateTimePicker);
            this.groupBox1.Controls.Add(this.startDateTimePicker);
            this.groupBox1.Controls.Add(this.datesCheckBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 139);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дата фотосъемки";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "По:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "С:";
            // 
            // endDateTimePicker
            // 
            this.endDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endDateTimePicker.Location = new System.Drawing.Point(27, 70);
            this.endDateTimePicker.MaxDate = new System.DateTime(4059, 11, 28, 0, 0, 0, 0);
            this.endDateTimePicker.Name = "endDateTimePicker";
            this.endDateTimePicker.Size = new System.Drawing.Size(223, 20);
            this.endDateTimePicker.TabIndex = 2;
            this.endDateTimePicker.Value = new System.DateTime(2018, 8, 26, 0, 0, 0, 0);
            // 
            // startDateTimePicker
            // 
            this.startDateTimePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.startDateTimePicker.Location = new System.Drawing.Point(27, 43);
            this.startDateTimePicker.Name = "startDateTimePicker";
            this.startDateTimePicker.Size = new System.Drawing.Size(223, 20);
            this.startDateTimePicker.TabIndex = 1;
            // 
            // datesCheckBox
            // 
            this.datesCheckBox.AutoSize = true;
            this.datesCheckBox.Location = new System.Drawing.Point(5, 20);
            this.datesCheckBox.Name = "datesCheckBox";
            this.datesCheckBox.Size = new System.Drawing.Size(133, 17);
            this.datesCheckBox.TabIndex = 0;
            this.datesCheckBox.Text = "По дате фотосъемки";
            this.datesCheckBox.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startButton.Location = new System.Drawing.Point(568, 152);
            this.startButton.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(314, 39);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Начать поиск";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartSearch);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.orientationComboBox);
            this.groupBox2.Controls.Add(this.orientationСheckBox);
            this.groupBox2.Controls.Add(this.exposureProgramComboBox);
            this.groupBox2.Controls.Add(this.ExposureProgramCheckBox);
            this.groupBox2.Controls.Add(this.flashOnCheckBox);
            this.groupBox2.Controls.Add(this.WhiteBalanceСheckBox);
            this.groupBox2.Controls.Add(this.gpsCheckBox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(568, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(314, 139);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Параметры съемки";
            // 
            // orientationComboBox
            // 
            this.orientationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.orientationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.orientationComboBox.FormattingEnabled = true;
            this.orientationComboBox.Items.AddRange(new object[] {
            "Горизонтальная",
            "Вертикальная"});
            this.orientationComboBox.Location = new System.Drawing.Point(167, 109);
            this.orientationComboBox.Name = "orientationComboBox";
            this.orientationComboBox.Size = new System.Drawing.Size(141, 21);
            this.orientationComboBox.TabIndex = 6;
            // 
            // orientationСheckBox
            // 
            this.orientationСheckBox.AutoSize = true;
            this.orientationСheckBox.Location = new System.Drawing.Point(5, 108);
            this.orientationСheckBox.Name = "orientationСheckBox";
            this.orientationСheckBox.Size = new System.Drawing.Size(90, 17);
            this.orientationСheckBox.TabIndex = 5;
            this.orientationСheckBox.Text = "Ориентация:";
            this.orientationСheckBox.UseVisualStyleBackColor = true;
            // 
            // exposureProgramComboBox
            // 
            this.exposureProgramComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.exposureProgramComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.exposureProgramComboBox.FormattingEnabled = true;
            this.exposureProgramComboBox.Items.AddRange(new object[] {
            "Ручной",
            "Программный",
            "Приоритет диафрагмы",
            "Приоритет выдержки",
            "Творческий режим",
            "Спорт/Скорость",
            "Портретный режим",
            "Пейзажный режим"});
            this.exposureProgramComboBox.Location = new System.Drawing.Point(167, 83);
            this.exposureProgramComboBox.Name = "exposureProgramComboBox";
            this.exposureProgramComboBox.Size = new System.Drawing.Size(141, 21);
            this.exposureProgramComboBox.TabIndex = 4;
            // 
            // ExposureProgramCheckBox
            // 
            this.ExposureProgramCheckBox.AutoSize = true;
            this.ExposureProgramCheckBox.Location = new System.Drawing.Point(5, 86);
            this.ExposureProgramCheckBox.Name = "ExposureProgramCheckBox";
            this.ExposureProgramCheckBox.Size = new System.Drawing.Size(160, 17);
            this.ExposureProgramCheckBox.TabIndex = 3;
            this.ExposureProgramCheckBox.Text = "Управление экспозицией:";
            this.ExposureProgramCheckBox.UseVisualStyleBackColor = true;
            // 
            // flashOnCheckBox
            // 
            this.flashOnCheckBox.AutoSize = true;
            this.flashOnCheckBox.Location = new System.Drawing.Point(5, 42);
            this.flashOnCheckBox.Name = "flashOnCheckBox";
            this.flashOnCheckBox.Size = new System.Drawing.Size(94, 17);
            this.flashOnCheckBox.TabIndex = 1;
            this.flashOnCheckBox.Text = "Со вспышкой";
            this.flashOnCheckBox.UseVisualStyleBackColor = true;
            // 
            // WhiteBalanceСheckBox
            // 
            this.WhiteBalanceСheckBox.AutoSize = true;
            this.WhiteBalanceСheckBox.Location = new System.Drawing.Point(5, 20);
            this.WhiteBalanceСheckBox.Name = "WhiteBalanceСheckBox";
            this.WhiteBalanceСheckBox.Size = new System.Drawing.Size(138, 17);
            this.WhiteBalanceСheckBox.TabIndex = 0;
            this.WhiteBalanceСheckBox.Text = "Ручной баланс белого";
            this.WhiteBalanceСheckBox.UseVisualStyleBackColor = true;
            // 
            // gpsCheckBox
            // 
            this.gpsCheckBox.AutoSize = true;
            this.gpsCheckBox.Location = new System.Drawing.Point(5, 64);
            this.gpsCheckBox.Name = "gpsCheckBox";
            this.gpsCheckBox.Size = new System.Drawing.Size(107, 17);
            this.gpsCheckBox.TabIndex = 2;
            this.gpsCheckBox.Text = "С данными GPS";
            this.gpsCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.maxISOTextBox);
            this.groupBox3.Controls.Add(this.minISOTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.ISOCheckBox);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(268, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(144, 139);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ISO";
            // 
            // maxISOTextBox
            // 
            this.maxISOTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maxISOTextBox.Location = new System.Drawing.Point(27, 70);
            this.maxISOTextBox.Name = "maxISOTextBox";
            this.maxISOTextBox.Size = new System.Drawing.Size(107, 20);
            this.maxISOTextBox.TabIndex = 2;
            this.maxISOTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ISOVerify);
            // 
            // minISOTextBox
            // 
            this.minISOTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minISOTextBox.Location = new System.Drawing.Point(27, 43);
            this.minISOTextBox.Name = "minISOTextBox";
            this.minISOTextBox.Size = new System.Drawing.Size(107, 20);
            this.minISOTextBox.TabIndex = 1;
            this.minISOTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ISOVerify);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "По:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "С:";
            // 
            // ISOCheckBox
            // 
            this.ISOCheckBox.AutoSize = true;
            this.ISOCheckBox.Location = new System.Drawing.Point(5, 20);
            this.ISOCheckBox.Name = "ISOCheckBox";
            this.ISOCheckBox.Size = new System.Drawing.Size(61, 17);
            this.ISOCheckBox.TabIndex = 0;
            this.ISOCheckBox.Text = "По ISO";
            this.ISOCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.maxExposureTimeTextBox);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.minExposureTimeTextBox);
            this.groupBox4.Controls.Add(this.ExposureTimeCheckBox);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(418, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 139);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Выдержка";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(24, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "По:";
            // 
            // maxExposureTimeTextBox
            // 
            this.maxExposureTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maxExposureTimeTextBox.Location = new System.Drawing.Point(27, 70);
            this.maxExposureTimeTextBox.Name = "maxExposureTimeTextBox";
            this.maxExposureTimeTextBox.Size = new System.Drawing.Size(107, 20);
            this.maxExposureTimeTextBox.TabIndex = 2;
            this.maxExposureTimeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ExposureTimeVerify);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "С:";
            // 
            // minExposureTimeTextBox
            // 
            this.minExposureTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.minExposureTimeTextBox.Location = new System.Drawing.Point(27, 43);
            this.minExposureTimeTextBox.Name = "minExposureTimeTextBox";
            this.minExposureTimeTextBox.Size = new System.Drawing.Size(107, 20);
            this.minExposureTimeTextBox.TabIndex = 1;
            this.minExposureTimeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ExposureTimeVerify);
            // 
            // ExposureTimeCheckBox
            // 
            this.ExposureTimeCheckBox.AutoSize = true;
            this.ExposureTimeCheckBox.Location = new System.Drawing.Point(5, 20);
            this.ExposureTimeCheckBox.Name = "ExposureTimeCheckBox";
            this.ExposureTimeCheckBox.Size = new System.Drawing.Size(98, 17);
            this.ExposureTimeCheckBox.TabIndex = 0;
            this.ExposureTimeCheckBox.Text = "По выдержке:";
            this.ExposureTimeCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.CameraTextBox);
            this.groupBox5.Controls.Add(this.cameraCheckBox);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 148);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(259, 43);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Камера";
            // 
            // CameraTextBox
            // 
            this.CameraTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CameraTextBox.Location = new System.Drawing.Point(161, 18);
            this.CameraTextBox.Name = "CameraTextBox";
            this.CameraTextBox.Size = new System.Drawing.Size(92, 20);
            this.CameraTextBox.TabIndex = 1;
            // 
            // cameraCheckBox
            // 
            this.cameraCheckBox.AutoSize = true;
            this.cameraCheckBox.Location = new System.Drawing.Point(5, 21);
            this.cameraCheckBox.Name = "cameraCheckBox";
            this.cameraCheckBox.Size = new System.Drawing.Size(151, 17);
            this.cameraCheckBox.TabIndex = 0;
            this.cameraCheckBox.Text = "Производитель камеры:";
            this.cameraCheckBox.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.settingsPanel.SetColumnSpan(this.groupBox6, 2);
            this.groupBox6.Controls.Add(this.EditCheckBox);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(268, 148);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(294, 43);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Обработка изображения";
            // 
            // EditCheckBox
            // 
            this.EditCheckBox.AutoSize = true;
            this.EditCheckBox.Location = new System.Drawing.Point(5, 21);
            this.EditCheckBox.Name = "EditCheckBox";
            this.EditCheckBox.Size = new System.Drawing.Size(205, 17);
            this.EditCheckBox.TabIndex = 0;
            this.EditCheckBox.Text = "Изображение было редактировано";
            this.EditCheckBox.UseVisualStyleBackColor = true;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(891, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.saveResultsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.searchToolStripMenuItem.Text = "Начать поиск";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.StartSearch);
            // 
            // saveResultsToolStripMenuItem
            // 
            this.saveResultsToolStripMenuItem.Name = "saveResultsToolStripMenuItem";
            this.saveResultsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.saveResultsToolStripMenuItem.Text = "Сохранить результаты";
            this.saveResultsToolStripMenuItem.Click += new System.EventHandler(this.saveResultsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(65, 20);
            this.helpToolStripMenuItem1.Text = "Справка";
            // 
            // showHelpToolStripMenuItem
            // 
            this.showHelpToolStripMenuItem.Name = "showHelpToolStripMenuItem";
            this.showHelpToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.showHelpToolStripMenuItem.Text = "Посмотреть справку";
            this.showHelpToolStripMenuItem.Click += new System.EventHandler(this.showHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 509);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mainMenuStrip);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.settingsPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel infoPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TableLayoutPanel settingsPanel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker endDateTimePicker;
        private System.Windows.Forms.DateTimePicker startDateTimePicker;
        private System.Windows.Forms.CheckBox datesCheckBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox WhiteBalanceСheckBox;
        private System.Windows.Forms.CheckBox flashOnCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox ISOCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox maxExposureTimeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox minExposureTimeTextBox;
        private System.Windows.Forms.CheckBox ExposureTimeCheckBox;
        private System.Windows.Forms.TextBox imageInformationTextBox;
        private System.Windows.Forms.TextBox maxISOTextBox;
        private System.Windows.Forms.TextBox minISOTextBox;
        private System.Windows.Forms.CheckBox gpsCheckBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox cameraCheckBox;
        private System.Windows.Forms.TextBox CameraTextBox;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox EditCheckBox;
        private System.Windows.Forms.CheckBox allDrivesCheckBox;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem showHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ComboBox exposureProgramComboBox;
        private System.Windows.Forms.CheckBox ExposureProgramCheckBox;
        private System.Windows.Forms.CheckBox skipWinDirectoryCheckBox;
        private System.Windows.Forms.ListBox filesListBox;
        private System.Windows.Forms.ComboBox orientationComboBox;
        private System.Windows.Forms.CheckBox orientationСheckBox;
    }
}

