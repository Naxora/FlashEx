namespace FlashEx
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            toolStripStatusLabelActionStatus = new ToolStripStatusLabel();
            groupBox1 = new GroupBox();
            comboBoxComPorts = new ComboBox();
            iconButtonComPortRefresh = new FontAwesome.Sharp.IconButton();
            buttonInitFlashProcess = new FontAwesome.Sharp.IconButton();
            comboBoxBaudRate = new ComboBox();
            iconButtonOpenBinFile = new FontAwesome.Sharp.IconButton();
            labelSelectedFWName = new Label();
            richTextBoxConsoleLog = new RichTextBox();
            iconButtonReadESPInfo = new FontAwesome.Sharp.IconButton();
            groupBox2 = new GroupBox();
            checkBoxCustomBaudRateBool = new CheckBox();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            checkBoxCustomPartitionTableBool = new CheckBox();
            comboBoxPartitionTable = new ComboBox();
            groupBox5 = new GroupBox();
            iconButtonImageInfo = new FontAwesome.Sharp.IconButton();
            groupBox6 = new GroupBox();
            iconButtonOpenEspDocsDatabase = new FontAwesome.Sharp.IconButton();
            groupBox7 = new GroupBox();
            iconButtonEraseFlash = new FontAwesome.Sharp.IconButton();
            iconButtonTasmotaSetWifi = new FontAwesome.Sharp.IconButton();
            iconButtonDisconnectFromSerial = new FontAwesome.Sharp.IconButton();
            textBoxTasmotaWifiSSID = new TextBox();
            textBoxTasmotaWifiPassword = new TextBox();
            label1 = new Label();
            label2 = new Label();
            groupBox8 = new GroupBox();
            textBox1 = new TextBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            iconButtonConnectToSerial = new FontAwesome.Sharp.IconButton();
            tabPage2 = new TabPage();
            groupBox9 = new GroupBox();
            linkLabelOpenTasmotaFirmwares = new LinkLabel();
            labelDownloadTasmotaBin = new Label();
            iconButtonDownloadTasmotaBin = new FontAwesome.Sharp.IconButton();
            comboBoxTasmotaSource = new ComboBox();
            statusStrip1.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox9.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, toolStripStatusLabelActionStatus });
            statusStrip1.Location = new Point(0, 614);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(906, 22);
            statusStrip1.TabIndex = 0;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(45, 17);
            toolStripStatusLabel1.Text = "Status:";
            // 
            // toolStripStatusLabelActionStatus
            // 
            toolStripStatusLabelActionStatus.Name = "toolStripStatusLabelActionStatus";
            toolStripStatusLabelActionStatus.Size = new Size(26, 17);
            toolStripStatusLabelActionStatus.Text = "idle";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBoxComPorts);
            groupBox1.Controls.Add(iconButtonComPortRefresh);
            groupBox1.Location = new Point(6, 79);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(309, 67);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Serial Ports";
            // 
            // comboBoxComPorts
            // 
            comboBoxComPorts.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxComPorts.FormattingEnabled = true;
            comboBoxComPorts.Location = new Point(6, 22);
            comboBoxComPorts.Name = "comboBoxComPorts";
            comboBoxComPorts.Size = new Size(257, 23);
            comboBoxComPorts.TabIndex = 8;
            comboBoxComPorts.SelectedValueChanged += comboBoxComPorts_SelectedValueChanged;
            // 
            // iconButtonComPortRefresh
            // 
            iconButtonComPortRefresh.IconChar = FontAwesome.Sharp.IconChar.Refresh;
            iconButtonComPortRefresh.IconColor = Color.MediumSeaGreen;
            iconButtonComPortRefresh.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonComPortRefresh.IconSize = 19;
            iconButtonComPortRefresh.Location = new Point(269, 21);
            iconButtonComPortRefresh.Name = "iconButtonComPortRefresh";
            iconButtonComPortRefresh.Size = new Size(33, 24);
            iconButtonComPortRefresh.TabIndex = 10;
            iconButtonComPortRefresh.UseVisualStyleBackColor = true;
            iconButtonComPortRefresh.Click += iconButtonComPortRefresh_Click;
            // 
            // buttonInitFlashProcess
            // 
            buttonInitFlashProcess.ForeColor = SystemColors.WindowText;
            buttonInitFlashProcess.IconChar = FontAwesome.Sharp.IconChar.Fire;
            buttonInitFlashProcess.IconColor = Color.Green;
            buttonInitFlashProcess.IconFont = FontAwesome.Sharp.IconFont.Auto;
            buttonInitFlashProcess.IconSize = 19;
            buttonInitFlashProcess.ImageAlign = ContentAlignment.MiddleLeft;
            buttonInitFlashProcess.Location = new Point(155, 22);
            buttonInitFlashProcess.Name = "buttonInitFlashProcess";
            buttonInitFlashProcess.Size = new Size(147, 39);
            buttonInitFlashProcess.TabIndex = 11;
            buttonInitFlashProcess.Text = "Flash";
            buttonInitFlashProcess.UseVisualStyleBackColor = true;
            buttonInitFlashProcess.Click += buttonInitFlashProcess_Click;
            // 
            // comboBoxBaudRate
            // 
            comboBoxBaudRate.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBaudRate.FormattingEnabled = true;
            comboBoxBaudRate.Items.AddRange(new object[] { "110", "300", "600", "1200", "2400", "4800", "9600", "14400", "19200", "38400", "57600", "115200", "128000", "256000" });
            comboBoxBaudRate.Location = new Point(6, 22);
            comboBoxBaudRate.Name = "comboBoxBaudRate";
            comboBoxBaudRate.Size = new Size(122, 23);
            comboBoxBaudRate.TabIndex = 12;
            // 
            // iconButtonOpenBinFile
            // 
            iconButtonOpenBinFile.BackColor = SystemColors.ControlLightLight;
            iconButtonOpenBinFile.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            iconButtonOpenBinFile.IconColor = Color.MediumSeaGreen;
            iconButtonOpenBinFile.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonOpenBinFile.IconSize = 19;
            iconButtonOpenBinFile.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonOpenBinFile.Location = new Point(11, 20);
            iconButtonOpenBinFile.Name = "iconButtonOpenBinFile";
            iconButtonOpenBinFile.Size = new Size(291, 24);
            iconButtonOpenBinFile.TabIndex = 13;
            iconButtonOpenBinFile.Text = "Open File";
            iconButtonOpenBinFile.UseVisualStyleBackColor = false;
            iconButtonOpenBinFile.Click += iconButtonOpenBinFile_Click;
            // 
            // labelSelectedFWName
            // 
            labelSelectedFWName.AutoSize = true;
            labelSelectedFWName.Location = new Point(11, 47);
            labelSelectedFWName.Name = "labelSelectedFWName";
            labelSelectedFWName.Size = new Size(26, 15);
            labelSelectedFWName.TabIndex = 14;
            labelSelectedFWName.Text = "BIN";
            // 
            // richTextBoxConsoleLog
            // 
            richTextBoxConsoleLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBoxConsoleLog.BackColor = Color.FromArgb(38, 44, 66);
            richTextBoxConsoleLog.BorderStyle = BorderStyle.None;
            richTextBoxConsoleLog.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBoxConsoleLog.ForeColor = SystemColors.ActiveCaption;
            richTextBoxConsoleLog.Location = new Point(0, 21);
            richTextBoxConsoleLog.Name = "richTextBoxConsoleLog";
            richTextBoxConsoleLog.ReadOnly = true;
            richTextBoxConsoleLog.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBoxConsoleLog.Size = new Size(922, 262);
            richTextBoxConsoleLog.TabIndex = 16;
            richTextBoxConsoleLog.Text = "";
            // 
            // iconButtonReadESPInfo
            // 
            iconButtonReadESPInfo.IconChar = FontAwesome.Sharp.IconChar.InfoCircle;
            iconButtonReadESPInfo.IconColor = Color.SteelBlue;
            iconButtonReadESPInfo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonReadESPInfo.IconSize = 19;
            iconButtonReadESPInfo.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonReadESPInfo.Location = new Point(132, 52);
            iconButtonReadESPInfo.Name = "iconButtonReadESPInfo";
            iconButtonReadESPInfo.Size = new Size(120, 24);
            iconButtonReadESPInfo.TabIndex = 18;
            iconButtonReadESPInfo.Text = "Read Info";
            iconButtonReadESPInfo.UseVisualStyleBackColor = true;
            iconButtonReadESPInfo.Click += iconButtonReadESPInfo_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(checkBoxCustomBaudRateBool);
            groupBox2.Controls.Add(comboBoxBaudRate);
            groupBox2.Location = new Point(6, 152);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(155, 67);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "Baudrate";
            // 
            // checkBoxCustomBaudRateBool
            // 
            checkBoxCustomBaudRateBool.AutoSize = true;
            checkBoxCustomBaudRateBool.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxCustomBaudRateBool.Location = new Point(134, 26);
            checkBoxCustomBaudRateBool.Name = "checkBoxCustomBaudRateBool";
            checkBoxCustomBaudRateBool.Size = new Size(15, 14);
            checkBoxCustomBaudRateBool.TabIndex = 13;
            checkBoxCustomBaudRateBool.UseVisualStyleBackColor = true;
            checkBoxCustomBaudRateBool.CheckStateChanged += checkBoxCustomBaudRateBool_CheckStateChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(iconButtonOpenBinFile);
            groupBox3.Controls.Add(labelSelectedFWName);
            groupBox3.Location = new Point(6, 6);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(309, 67);
            groupBox3.TabIndex = 21;
            groupBox3.TabStop = false;
            groupBox3.Text = "Open Firmware File";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(checkBoxCustomPartitionTableBool);
            groupBox4.Controls.Add(comboBoxPartitionTable);
            groupBox4.Location = new Point(167, 152);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(148, 67);
            groupBox4.TabIndex = 21;
            groupBox4.TabStop = false;
            groupBox4.Text = "Partition Table";
            // 
            // checkBoxCustomPartitionTableBool
            // 
            checkBoxCustomPartitionTableBool.AutoSize = true;
            checkBoxCustomPartitionTableBool.Location = new Point(126, 26);
            checkBoxCustomPartitionTableBool.Name = "checkBoxCustomPartitionTableBool";
            checkBoxCustomPartitionTableBool.Size = new Size(15, 14);
            checkBoxCustomPartitionTableBool.TabIndex = 14;
            checkBoxCustomPartitionTableBool.UseVisualStyleBackColor = true;
            checkBoxCustomPartitionTableBool.CheckStateChanged += checkBoxCustomPartitionTableBool_CheckStateChanged;
            // 
            // comboBoxPartitionTable
            // 
            comboBoxPartitionTable.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxPartitionTable.FormattingEnabled = true;
            comboBoxPartitionTable.Items.AddRange(new object[] { "0x0" });
            comboBoxPartitionTable.Location = new Point(6, 22);
            comboBoxPartitionTable.Name = "comboBoxPartitionTable";
            comboBoxPartitionTable.Size = new Size(114, 23);
            comboBoxPartitionTable.TabIndex = 12;
            // 
            // groupBox5
            // 
            groupBox5.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            groupBox5.Controls.Add(richTextBoxConsoleLog);
            groupBox5.Location = new Point(0, 328);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(906, 286);
            groupBox5.TabIndex = 22;
            groupBox5.TabStop = false;
            groupBox5.Text = "Console Log";
            // 
            // iconButtonImageInfo
            // 
            iconButtonImageInfo.IconChar = FontAwesome.Sharp.IconChar.InfoCircle;
            iconButtonImageInfo.IconColor = Color.SteelBlue;
            iconButtonImageInfo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonImageInfo.IconSize = 19;
            iconButtonImageInfo.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonImageInfo.Location = new Point(6, 52);
            iconButtonImageInfo.Name = "iconButtonImageInfo";
            iconButtonImageInfo.Size = new Size(120, 24);
            iconButtonImageInfo.TabIndex = 23;
            iconButtonImageInfo.Text = "Image Info";
            iconButtonImageInfo.UseVisualStyleBackColor = true;
            iconButtonImageInfo.Click += iconButtonImageInfo_Click;
            // 
            // groupBox6
            // 
            groupBox6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBox6.Controls.Add(iconButtonOpenEspDocsDatabase);
            groupBox6.Controls.Add(iconButtonImageInfo);
            groupBox6.Controls.Add(iconButtonReadESPInfo);
            groupBox6.Location = new Point(634, 6);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(260, 88);
            groupBox6.TabIndex = 24;
            groupBox6.TabStop = false;
            groupBox6.Text = "Tools";
            // 
            // iconButtonOpenEspDocsDatabase
            // 
            iconButtonOpenEspDocsDatabase.IconChar = FontAwesome.Sharp.IconChar.FileInvoice;
            iconButtonOpenEspDocsDatabase.IconColor = Color.SteelBlue;
            iconButtonOpenEspDocsDatabase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonOpenEspDocsDatabase.IconSize = 19;
            iconButtonOpenEspDocsDatabase.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonOpenEspDocsDatabase.Location = new Point(6, 22);
            iconButtonOpenEspDocsDatabase.Name = "iconButtonOpenEspDocsDatabase";
            iconButtonOpenEspDocsDatabase.Size = new Size(120, 24);
            iconButtonOpenEspDocsDatabase.TabIndex = 24;
            iconButtonOpenEspDocsDatabase.Text = "ESP Database";
            iconButtonOpenEspDocsDatabase.UseVisualStyleBackColor = true;
            iconButtonOpenEspDocsDatabase.Click += iconButtonOpenEspDocsDatabase_Click;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(iconButtonEraseFlash);
            groupBox7.Controls.Add(buttonInitFlashProcess);
            groupBox7.Location = new Point(6, 225);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(309, 67);
            groupBox7.TabIndex = 25;
            groupBox7.TabStop = false;
            groupBox7.Text = "Final Step";
            // 
            // iconButtonEraseFlash
            // 
            iconButtonEraseFlash.ForeColor = SystemColors.WindowText;
            iconButtonEraseFlash.IconChar = FontAwesome.Sharp.IconChar.Eraser;
            iconButtonEraseFlash.IconColor = Color.Crimson;
            iconButtonEraseFlash.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonEraseFlash.IconSize = 19;
            iconButtonEraseFlash.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonEraseFlash.Location = new Point(6, 22);
            iconButtonEraseFlash.Name = "iconButtonEraseFlash";
            iconButtonEraseFlash.Size = new Size(143, 39);
            iconButtonEraseFlash.TabIndex = 12;
            iconButtonEraseFlash.Text = "Erase Flash";
            iconButtonEraseFlash.UseVisualStyleBackColor = true;
            iconButtonEraseFlash.Click += iconButtonEraseFlash_Click;
            // 
            // iconButtonTasmotaSetWifi
            // 
            iconButtonTasmotaSetWifi.IconChar = FontAwesome.Sharp.IconChar.Wifi3;
            iconButtonTasmotaSetWifi.IconColor = Color.Black;
            iconButtonTasmotaSetWifi.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonTasmotaSetWifi.IconSize = 19;
            iconButtonTasmotaSetWifi.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonTasmotaSetWifi.Location = new Point(8, 75);
            iconButtonTasmotaSetWifi.Name = "iconButtonTasmotaSetWifi";
            iconButtonTasmotaSetWifi.Size = new Size(209, 24);
            iconButtonTasmotaSetWifi.TabIndex = 26;
            iconButtonTasmotaSetWifi.Text = "Set Wifi";
            iconButtonTasmotaSetWifi.UseVisualStyleBackColor = true;
            iconButtonTasmotaSetWifi.Click += iconButtonTasmotaSetWifi_Click;
            // 
            // iconButtonDisconnectFromSerial
            // 
            iconButtonDisconnectFromSerial.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            iconButtonDisconnectFromSerial.IconChar = FontAwesome.Sharp.IconChar.PlugCircleXmark;
            iconButtonDisconnectFromSerial.IconColor = Color.MediumPurple;
            iconButtonDisconnectFromSerial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonDisconnectFromSerial.IconSize = 19;
            iconButtonDisconnectFromSerial.ImageAlign = ContentAlignment.TopLeft;
            iconButtonDisconnectFromSerial.Location = new Point(755, 268);
            iconButtonDisconnectFromSerial.Name = "iconButtonDisconnectFromSerial";
            iconButtonDisconnectFromSerial.Size = new Size(139, 24);
            iconButtonDisconnectFromSerial.TabIndex = 27;
            iconButtonDisconnectFromSerial.Text = "Disconnect Serial";
            iconButtonDisconnectFromSerial.UseVisualStyleBackColor = true;
            iconButtonDisconnectFromSerial.Click += iconButtonDisconnectFromCOM_Click;
            // 
            // textBoxTasmotaWifiSSID
            // 
            textBoxTasmotaWifiSSID.Location = new Point(48, 16);
            textBoxTasmotaWifiSSID.Name = "textBoxTasmotaWifiSSID";
            textBoxTasmotaWifiSSID.Size = new Size(169, 23);
            textBoxTasmotaWifiSSID.TabIndex = 28;
            // 
            // textBoxTasmotaWifiPassword
            // 
            textBoxTasmotaWifiPassword.Location = new Point(48, 45);
            textBoxTasmotaWifiPassword.Name = "textBoxTasmotaWifiPassword";
            textBoxTasmotaWifiPassword.Size = new Size(169, 23);
            textBoxTasmotaWifiPassword.TabIndex = 29;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 30;
            label1.Text = "SSID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 48);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 31;
            label2.Text = "PWD:";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(textBox1);
            groupBox8.Controls.Add(textBoxTasmotaWifiSSID);
            groupBox8.Controls.Add(label2);
            groupBox8.Controls.Add(iconButtonTasmotaSetWifi);
            groupBox8.Controls.Add(textBoxTasmotaWifiPassword);
            groupBox8.Controls.Add(label1);
            groupBox8.Location = new Point(6, 6);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(225, 153);
            groupBox8.TabIndex = 32;
            groupBox8.TabStop = false;
            groupBox8.Text = "Set Wifi";
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new Point(6, 105);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(211, 39);
            textBox1.TabIndex = 33;
            textBox1.Text = "Reset device then after 2 seconds press 'Set Wifi' button.";
            textBox1.TextAlign = HorizontalAlignment.Center;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(-4, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(915, 326);
            tabControl1.TabIndex = 33;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(iconButtonConnectToSerial);
            tabPage1.Controls.Add(groupBox3);
            tabPage1.Controls.Add(iconButtonDisconnectFromSerial);
            tabPage1.Controls.Add(groupBox7);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox6);
            tabPage1.Controls.Add(groupBox4);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(907, 298);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Main";
            // 
            // iconButtonConnectToSerial
            // 
            iconButtonConnectToSerial.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            iconButtonConnectToSerial.IconChar = FontAwesome.Sharp.IconChar.PlayCircle;
            iconButtonConnectToSerial.IconColor = Color.MediumSeaGreen;
            iconButtonConnectToSerial.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonConnectToSerial.IconSize = 19;
            iconButtonConnectToSerial.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonConnectToSerial.Location = new Point(755, 238);
            iconButtonConnectToSerial.Name = "iconButtonConnectToSerial";
            iconButtonConnectToSerial.Size = new Size(139, 24);
            iconButtonConnectToSerial.TabIndex = 28;
            iconButtonConnectToSerial.Text = "Connect Serial";
            iconButtonConnectToSerial.UseVisualStyleBackColor = true;
            iconButtonConnectToSerial.Click += iconButtonConnectToSerial_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Controls.Add(groupBox9);
            tabPage2.Controls.Add(groupBox8);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(907, 298);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Tasmota";
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(linkLabelOpenTasmotaFirmwares);
            groupBox9.Controls.Add(labelDownloadTasmotaBin);
            groupBox9.Controls.Add(iconButtonDownloadTasmotaBin);
            groupBox9.Controls.Add(comboBoxTasmotaSource);
            groupBox9.Location = new Point(237, 6);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(347, 153);
            groupBox9.TabIndex = 34;
            groupBox9.TabStop = false;
            groupBox9.Text = "Download";
            // 
            // linkLabelOpenTasmotaFirmwares
            // 
            linkLabelOpenTasmotaFirmwares.AutoSize = true;
            linkLabelOpenTasmotaFirmwares.Location = new Point(6, 84);
            linkLabelOpenTasmotaFirmwares.Name = "linkLabelOpenTasmotaFirmwares";
            linkLabelOpenTasmotaFirmwares.Size = new Size(98, 15);
            linkLabelOpenTasmotaFirmwares.TabIndex = 36;
            linkLabelOpenTasmotaFirmwares.TabStop = true;
            linkLabelOpenTasmotaFirmwares.Text = "Tasmota Releases";
            linkLabelOpenTasmotaFirmwares.LinkClicked += linkLabelOpenTasmotaFirmwares_LinkClicked;
            // 
            // labelDownloadTasmotaBin
            // 
            labelDownloadTasmotaBin.AutoSize = true;
            labelDownloadTasmotaBin.Location = new Point(295, 55);
            labelDownloadTasmotaBin.Name = "labelDownloadTasmotaBin";
            labelDownloadTasmotaBin.Size = new Size(46, 15);
            labelDownloadTasmotaBin.TabIndex = 35;
            labelDownloadTasmotaBin.Text = "000 MB";
            // 
            // iconButtonDownloadTasmotaBin
            // 
            iconButtonDownloadTasmotaBin.IconChar = FontAwesome.Sharp.IconChar.Download;
            iconButtonDownloadTasmotaBin.IconColor = Color.Black;
            iconButtonDownloadTasmotaBin.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconButtonDownloadTasmotaBin.IconSize = 19;
            iconButtonDownloadTasmotaBin.ImageAlign = ContentAlignment.MiddleLeft;
            iconButtonDownloadTasmotaBin.Location = new Point(6, 51);
            iconButtonDownloadTasmotaBin.Name = "iconButtonDownloadTasmotaBin";
            iconButtonDownloadTasmotaBin.Size = new Size(283, 23);
            iconButtonDownloadTasmotaBin.TabIndex = 34;
            iconButtonDownloadTasmotaBin.Text = "Download";
            iconButtonDownloadTasmotaBin.UseVisualStyleBackColor = true;
            iconButtonDownloadTasmotaBin.Click += iconButtonDownloadTasmotaBin_Click;
            // 
            // comboBoxTasmotaSource
            // 
            comboBoxTasmotaSource.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTasmotaSource.FormattingEnabled = true;
            comboBoxTasmotaSource.Location = new Point(6, 22);
            comboBoxTasmotaSource.Name = "comboBoxTasmotaSource";
            comboBoxTasmotaSource.Size = new Size(335, 23);
            comboBoxTasmotaSource.TabIndex = 33;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(906, 636);
            Controls.Add(groupBox5);
            Controls.Add(statusStrip1);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FlashEx";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip statusStrip1;
        private GroupBox groupBox1;
        private ComboBox comboBoxComPorts;
        private FontAwesome.Sharp.IconButton iconButtonComPortRefresh;
        private FontAwesome.Sharp.IconButton buttonInitFlashProcess;
        private ComboBox comboBoxBaudRate;
        private FontAwesome.Sharp.IconButton iconButtonOpenBinFile;
        private Label labelSelectedFWName;
        private RichTextBox richTextBoxConsoleLog;
        private FontAwesome.Sharp.IconButton iconButtonReadESPInfo;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private ComboBox comboBoxPartitionTable;
        private GroupBox groupBox5;
        private FontAwesome.Sharp.IconButton iconButtonImageInfo;
        private CheckBox checkBoxCustomBaudRateBool;
        private CheckBox checkBoxCustomPartitionTableBool;
        private GroupBox groupBox6;
        private GroupBox groupBox7;
        private FontAwesome.Sharp.IconButton iconButtonTasmotaSetWifi;
        private FontAwesome.Sharp.IconButton iconButtonDisconnectFromSerial;
        private TextBox textBoxTasmotaWifiSSID;
        private TextBox textBoxTasmotaWifiPassword;
        private Label label1;
        private Label label2;
        private GroupBox groupBox8;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox textBox1;
        private FontAwesome.Sharp.IconButton iconButtonConnectToSerial;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabelActionStatus;
        private FontAwesome.Sharp.IconButton iconButtonEraseFlash;
        private GroupBox groupBox9;
        private ComboBox comboBoxTasmotaSource;
        private FontAwesome.Sharp.IconButton iconButtonDownloadTasmotaBin;
        private Label labelDownloadTasmotaBin;
        private LinkLabel linkLabelOpenTasmotaFirmwares;
        private FontAwesome.Sharp.IconButton iconButtonOpenEspDocsDatabase;
    }
}
