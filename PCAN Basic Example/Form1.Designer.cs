namespace ICDIBasic
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbCanFD = new System.Windows.Forms.CheckBox();
            this.cbbHwType = new System.Windows.Forms.ComboBox();
            this.cbbInterrupt = new System.Windows.Forms.ComboBox();
            this.laInterrupt = new System.Windows.Forms.Label();
            this.cbbIO = new System.Windows.Forms.ComboBox();
            this.laIOPort = new System.Windows.Forms.Label();
            this.laHwType = new System.Windows.Forms.Label();
            this.cbbBaudrates = new System.Windows.Forms.ComboBox();
            this.laBaudrate = new System.Windows.Forms.Label();
            this.txtBitrate = new System.Windows.Forms.TextBox();
            this.laBitrate = new System.Windows.Forms.Label();
            this.btnHwRefresh = new System.Windows.Forms.Button();
            this.cbbChannel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnRelease = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnGetVersions = new System.Windows.Forms.Button();
            this.lbxInfo = new System.Windows.Forms.ListBox();
            this.btnInfoClear = new System.Windows.Forms.Button();
            this.listMinMax = new System.Windows.Forms.ListView();
            this.clmType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMin = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMax = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnMsgClear = new System.Windows.Forms.Button();
            this.tmrRead = new System.Windows.Forms.Timer(this.components);
            this.tmrCalculateDelay = new System.Windows.Forms.Timer(this.components);
            this.tmrDisplay = new System.Windows.Forms.Timer(this.components);
            this.listTrace = new System.Windows.Forms.ListView();
            this.clmnData = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnTrace = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chbCanFD);
            this.groupBox1.Controls.Add(this.cbbHwType);
            this.groupBox1.Controls.Add(this.cbbInterrupt);
            this.groupBox1.Controls.Add(this.laInterrupt);
            this.groupBox1.Controls.Add(this.cbbIO);
            this.groupBox1.Controls.Add(this.laIOPort);
            this.groupBox1.Controls.Add(this.laHwType);
            this.groupBox1.Controls.Add(this.cbbBaudrates);
            this.groupBox1.Controls.Add(this.laBaudrate);
            this.groupBox1.Controls.Add(this.txtBitrate);
            this.groupBox1.Controls.Add(this.laBitrate);
            this.groupBox1.Controls.Add(this.btnHwRefresh);
            this.groupBox1.Controls.Add(this.cbbChannel);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(637, 65);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Connection ";
            // 
            // chbCanFD
            // 
            this.chbCanFD.AutoSize = true;
            this.chbCanFD.Location = new System.Drawing.Point(574, 34);
            this.chbCanFD.Name = "chbCanFD";
            this.chbCanFD.Size = new System.Drawing.Size(65, 17);
            this.chbCanFD.TabIndex = 59;
            this.chbCanFD.Text = "CAN-FD";
            this.chbCanFD.UseVisualStyleBackColor = true;
            this.chbCanFD.CheckedChanged += new System.EventHandler(this.chbCanFD_CheckedChanged);
            // 
            // cbbHwType
            // 
            this.cbbHwType.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbbHwType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbHwType.Items.AddRange(new object[] {
            "ISA-82C200",
            "ISA-SJA1000",
            "ISA-PHYTEC",
            "DNG-82C200",
            "DNG-82C200 EPP",
            "DNG-SJA1000",
            "DNG-SJA1000 EPP"});
            this.cbbHwType.Location = new System.Drawing.Point(326, 31);
            this.cbbHwType.Name = "cbbHwType";
            this.cbbHwType.Size = new System.Drawing.Size(120, 21);
            this.cbbHwType.TabIndex = 50;
            this.cbbHwType.SelectedIndexChanged += new System.EventHandler(this.cbbHwType_SelectedIndexChanged);
            // 
            // cbbInterrupt
            // 
            this.cbbInterrupt.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbbInterrupt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbInterrupt.Items.AddRange(new object[] {
            "3",
            "4",
            "5",
            "7",
            "9",
            "10",
            "11",
            "12",
            "15"});
            this.cbbInterrupt.Location = new System.Drawing.Point(513, 31);
            this.cbbInterrupt.Name = "cbbInterrupt";
            this.cbbInterrupt.Size = new System.Drawing.Size(55, 21);
            this.cbbInterrupt.TabIndex = 52;
            // 
            // laInterrupt
            // 
            this.laInterrupt.Location = new System.Drawing.Point(515, 15);
            this.laInterrupt.Name = "laInterrupt";
            this.laInterrupt.Size = new System.Drawing.Size(53, 23);
            this.laInterrupt.TabIndex = 56;
            this.laInterrupt.Text = "Interrupt:";
            // 
            // cbbIO
            // 
            this.cbbIO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbbIO.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbIO.Items.AddRange(new object[] {
            "0100",
            "0120",
            "0140",
            "0200",
            "0220",
            "0240",
            "0260",
            "0278",
            "0280",
            "02A0",
            "02C0",
            "02E0",
            "02E8",
            "02F8",
            "0300",
            "0320",
            "0340",
            "0360",
            "0378",
            "0380",
            "03BC",
            "03E0",
            "03E8",
            "03F8"});
            this.cbbIO.Location = new System.Drawing.Point(452, 31);
            this.cbbIO.Name = "cbbIO";
            this.cbbIO.Size = new System.Drawing.Size(55, 21);
            this.cbbIO.TabIndex = 51;
            // 
            // laIOPort
            // 
            this.laIOPort.Location = new System.Drawing.Point(452, 15);
            this.laIOPort.Name = "laIOPort";
            this.laIOPort.Size = new System.Drawing.Size(55, 23);
            this.laIOPort.TabIndex = 55;
            this.laIOPort.Text = "I/O Port:";
            // 
            // laHwType
            // 
            this.laHwType.Location = new System.Drawing.Point(327, 15);
            this.laHwType.Name = "laHwType";
            this.laHwType.Size = new System.Drawing.Size(90, 23);
            this.laHwType.TabIndex = 54;
            this.laHwType.Text = "Hardware Type:";
            // 
            // cbbBaudrates
            // 
            this.cbbBaudrates.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbbBaudrates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBaudrates.Items.AddRange(new object[] {
            "1 MBit/sec",
            "800 kBit/s",
            "500 kBit/sec",
            "250 kBit/sec",
            "125 kBit/sec",
            "100 kBit/sec",
            "95,238 kBit/s",
            "83,333 kBit/s",
            "50 kBit/sec",
            "47,619 kBit/s",
            "33,333 kBit/s",
            "20 kBit/sec",
            "10 kBit/sec",
            "5 kBit/sec"});
            this.cbbBaudrates.Location = new System.Drawing.Point(204, 31);
            this.cbbBaudrates.Name = "cbbBaudrates";
            this.cbbBaudrates.Size = new System.Drawing.Size(116, 21);
            this.cbbBaudrates.TabIndex = 49;
            this.cbbBaudrates.SelectedIndexChanged += new System.EventHandler(this.cbbBaudrates_SelectedIndexChanged);
            // 
            // laBaudrate
            // 
            this.laBaudrate.Location = new System.Drawing.Point(204, 15);
            this.laBaudrate.Name = "laBaudrate";
            this.laBaudrate.Size = new System.Drawing.Size(56, 23);
            this.laBaudrate.TabIndex = 53;
            this.laBaudrate.Text = "Baudrate:";
            // 
            // txtBitrate
            // 
            this.txtBitrate.Location = new System.Drawing.Point(204, 24);
            this.txtBitrate.Multiline = true;
            this.txtBitrate.Name = "txtBitrate";
            this.txtBitrate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBitrate.Size = new System.Drawing.Size(364, 34);
            this.txtBitrate.TabIndex = 48;
            this.txtBitrate.Visible = false;
            this.txtBitrate.TextChanged += new System.EventHandler(this.txtBitrate_TextChanged);
            // 
            // laBitrate
            // 
            this.laBitrate.AutoSize = true;
            this.laBitrate.Location = new System.Drawing.Point(204, 11);
            this.laBitrate.Name = "laBitrate";
            this.laBitrate.Size = new System.Drawing.Size(43, 13);
            this.laBitrate.TabIndex = 46;
            this.laBitrate.Text = "Bit rate:";
            this.laBitrate.Visible = false;
            // 
            // btnHwRefresh
            // 
            this.btnHwRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHwRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnHwRefresh.Location = new System.Drawing.Point(141, 30);
            this.btnHwRefresh.Name = "btnHwRefresh";
            this.btnHwRefresh.Size = new System.Drawing.Size(57, 23);
            this.btnHwRefresh.TabIndex = 45;
            this.btnHwRefresh.Text = "Refresh";
            this.btnHwRefresh.Click += new System.EventHandler(this.btnHwRefresh_Click);
            // 
            // cbbChannel
            // 
            this.cbbChannel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbbChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbChannel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbbChannel.Items.AddRange(new object[] {
            "None",
            "DNG-Channel 1",
            "ISA-Channel 1",
            "ISA-Channel 2",
            "ISA-Channel 3",
            "ISA-Channel 4",
            "ISA-Channel 5",
            "ISA-Channel 6",
            "ISA-Channel 7",
            "ISA-Channel 8",
            "PCC-Channel 1",
            "PCC-Channel 2",
            "PCI-Channel 1",
            "PCI-Channel 2",
            "PCI-Channel 3",
            "PCI-Channel 4",
            "PCI-Channel 5",
            "PCI-Channel 6",
            "PCI-Channel 7",
            "PCI-Channel 8",
            "USB-Channel 1",
            "USB-Channel 2",
            "USB-Channel 3",
            "USB-Channel 4",
            "USB-Channel 5",
            "USB-Channel 6",
            "USB-Channel 7",
            "USB-Channel 8"});
            this.cbbChannel.Location = new System.Drawing.Point(8, 31);
            this.cbbChannel.Name = "cbbChannel";
            this.cbbChannel.Size = new System.Drawing.Size(127, 21);
            this.cbbChannel.TabIndex = 32;
            this.cbbChannel.SelectedIndexChanged += new System.EventHandler(this.cbbChannel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(7, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 40;
            this.label1.Text = "Hardware:";
            // 
            // btnInit
            // 
            this.btnInit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInit.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInit.Location = new System.Drawing.Point(651, 14);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(65, 23);
            this.btnInit.TabIndex = 34;
            this.btnInit.Text = "Initialize";
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnRelease
            // 
            this.btnRelease.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRelease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRelease.Enabled = false;
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnRelease.Location = new System.Drawing.Point(651, 41);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(65, 23);
            this.btnRelease.TabIndex = 35;
            this.btnRelease.Text = "Release";
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnReset);
            this.groupBox4.Controls.Add(this.btnStatus);
            this.groupBox4.Controls.Add(this.btnGetVersions);
            this.groupBox4.Controls.Add(this.lbxInfo);
            this.groupBox4.Controls.Add(this.btnInfoClear);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox4.Location = new System.Drawing.Point(8, 1083);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(714, 85);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Information";
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Enabled = false;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReset.Location = new System.Drawing.Point(643, 48);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(65, 23);
            this.btnReset.TabIndex = 58;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnStatus
            // 
            this.btnStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatus.Enabled = false;
            this.btnStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStatus.Location = new System.Drawing.Point(572, 48);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(65, 23);
            this.btnStatus.TabIndex = 57;
            this.btnStatus.Text = "Status";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.btnStatus_Click);
            // 
            // btnGetVersions
            // 
            this.btnGetVersions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetVersions.Enabled = false;
            this.btnGetVersions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnGetVersions.Location = new System.Drawing.Point(572, 19);
            this.btnGetVersions.Name = "btnGetVersions";
            this.btnGetVersions.Size = new System.Drawing.Size(65, 23);
            this.btnGetVersions.TabIndex = 53;
            this.btnGetVersions.Text = "Versions";
            this.btnGetVersions.UseVisualStyleBackColor = true;
            // 
            // lbxInfo
            // 
            this.lbxInfo.FormattingEnabled = true;
            this.lbxInfo.Items.AddRange(new object[] {
            "Select a Hardware and a configuration for it. Then click \"Initialize\" button",
            "When activated, the Debug-Log file will be found in the same directory as this ap" +
                "plication",
            "When activated, the PCAN-Trace file will be found in the same directory as this a" +
                "pplication"});
            this.lbxInfo.Location = new System.Drawing.Point(10, 19);
            this.lbxInfo.Name = "lbxInfo";
            this.lbxInfo.ScrollAlwaysVisible = true;
            this.lbxInfo.Size = new System.Drawing.Size(558, 56);
            this.lbxInfo.TabIndex = 56;
            this.lbxInfo.DoubleClick += new System.EventHandler(this.lbxInfo_DoubleClick);
            // 
            // btnInfoClear
            // 
            this.btnInfoClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInfoClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnInfoClear.Location = new System.Drawing.Point(643, 19);
            this.btnInfoClear.Name = "btnInfoClear";
            this.btnInfoClear.Size = new System.Drawing.Size(65, 23);
            this.btnInfoClear.TabIndex = 52;
            this.btnInfoClear.Text = "Clear";
            this.btnInfoClear.UseVisualStyleBackColor = true;
            this.btnInfoClear.Click += new System.EventHandler(this.btnInfoClear_Click);
            // 
            // listMinMax
            // 
            this.listMinMax.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmType,
            this.clmId,
            this.clmMin,
            this.clmMax,
            this.clmData,
            this.clmCount});
            this.listMinMax.FullRowSelect = true;
            this.listMinMax.HideSelection = false;
            this.listMinMax.Location = new System.Drawing.Point(8, 72);
            this.listMinMax.MultiSelect = false;
            this.listMinMax.Name = "listMinMax";
            this.listMinMax.Size = new System.Drawing.Size(645, 609);
            this.listMinMax.TabIndex = 52;
            this.listMinMax.UseCompatibleStateImageBehavior = false;
            this.listMinMax.View = System.Windows.Forms.View.Details;
            this.listMinMax.DoubleClick += new System.EventHandler(this.btnMsgClear_Click);
            this.listMinMax.GetType()
            .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .SetValue(listMinMax, true, null);
            // 
            // clmType
            // 
            this.clmType.Text = "Type";
            this.clmType.Width = 80;
            // 
            // clmId
            // 
            this.clmId.Text = "ID";
            this.clmId.Width = 90;
            // 
            // clmMin
            // 
            this.clmMin.Text = "Min";
            this.clmMin.Width = 100;
            // 
            // clmMax
            // 
            this.clmMax.Text = "Max";
            this.clmMax.Width = 101;
            // 
            // clmData
            // 
            this.clmData.Text = "Count";
            this.clmData.Width = 110;
            // 
            // clmCount
            // 
            this.clmCount.Text = "Data";
            this.clmCount.Width = 150;
            // 
            // btnMsgClear
            // 
            this.btnMsgClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMsgClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMsgClear.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnMsgClear.Location = new System.Drawing.Point(651, 72);
            this.btnMsgClear.Name = "btnMsgClear";
            this.btnMsgClear.Size = new System.Drawing.Size(65, 293);
            this.btnMsgClear.TabIndex = 50;
            this.btnMsgClear.Text = "Clear";
            this.btnMsgClear.UseVisualStyleBackColor = true;
            this.btnMsgClear.Click += new System.EventHandler(this.btnMsgClear_Click);
            // 
            // tmrRead
            // 
            this.tmrRead.Interval = 1;
            this.tmrRead.Tick += new System.EventHandler(this.tmrRead_Tick);
            // 
            // tmrCalculateDelay
            // 
            this.tmrCalculateDelay.Interval = 1;
            this.tmrCalculateDelay.Tick += new System.EventHandler(this.tmrCalculateDelay_Tick);
            // 
            // tmrDisplay
            // 
            this.tmrDisplay.Interval = 50;
            this.tmrDisplay.Tick += new System.EventHandler(this.tmrDisplay_Tick);
            // 
            // listTrace
            // 
            this.listTrace.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listTrace.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnData,
            this.clmnTime});
            this.listTrace.HideSelection = false;
            this.listTrace.HoverSelection = true;
            this.listTrace.Location = new System.Drawing.Point(8, 72);
            this.listTrace.Name = "listTrace";
            this.listTrace.Size = new System.Drawing.Size(645, 609);
            this.listTrace.TabIndex = 53;
            this.listTrace.UseCompatibleStateImageBehavior = false;
            this.listTrace.View = System.Windows.Forms.View.Details;
            this.listTrace.Visible = false;
            this.listTrace.SelectedIndexChanged += new System.EventHandler(this.listTrace_SelectedIndexChanged);
            this.listTrace.GetType()
            .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            .SetValue(listTrace, true, null);
            // 
            // clmnData
            // 
            this.clmnData.Text = "Data";
            this.clmnData.Width = 313;
            // 
            // clmnTime
            // 
            this.clmnTime.Text = "Time";
            this.clmnTime.Width = 171;
            // 
            // btnTrace
            // 
            this.btnTrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTrace.Location = new System.Drawing.Point(651, 369);
            this.btnTrace.Name = "btnTrace";
            this.btnTrace.Size = new System.Drawing.Size(65, 312);
            this.btnTrace.TabIndex = 54;
            this.btnTrace.Text = "Trace";
            this.btnTrace.UseVisualStyleBackColor = true;
            this.btnTrace.Click += new System.EventHandler(this.btnTrace_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 693);
            this.Controls.Add(this.btnTrace);
            this.Controls.Add(this.btnMsgClear);
            this.Controls.Add(this.listTrace);
            this.Controls.Add(this.listMinMax);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.btnRelease);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PCAN-Basic Sample";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbbChannel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnMsgClear;
        private System.Windows.Forms.Button btnInfoClear;
        private System.Windows.Forms.Button btnGetVersions;
        private System.Windows.Forms.ListBox lbxInfo;
        private System.Windows.Forms.Timer tmrRead;
        private System.Windows.Forms.Button btnHwRefresh;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnStatus;
        private System.Windows.Forms.Timer tmrDisplay;
        private System.Windows.Forms.Label laBitrate;
        private System.Windows.Forms.TextBox txtBitrate;
        private System.Windows.Forms.ComboBox cbbHwType;
        private System.Windows.Forms.ComboBox cbbInterrupt;
        private System.Windows.Forms.Label laInterrupt;
        private System.Windows.Forms.ComboBox cbbIO;
        private System.Windows.Forms.Label laIOPort;
        private System.Windows.Forms.Label laHwType;
        private System.Windows.Forms.ComboBox cbbBaudrates;
        private System.Windows.Forms.Label laBaudrate;
        private System.Windows.Forms.CheckBox chbCanFD;
        private System.Windows.Forms.ListView listMinMax;
        private System.Windows.Forms.ColumnHeader clmType;
        private System.Windows.Forms.ColumnHeader clmId;
        private System.Windows.Forms.ColumnHeader clmMin;
        private System.Windows.Forms.ColumnHeader clmMax;
        private System.Windows.Forms.ColumnHeader clmData;
        private System.Windows.Forms.Timer tmrCalculateDelay;
        //private System.Windows.Forms.Timer tmrDisplayMinMax_Tick;
        private System.Windows.Forms.ListView listTrace;
        private System.Windows.Forms.ColumnHeader clmnData;
        private System.Windows.Forms.ColumnHeader clmnTime;
        private System.Windows.Forms.Button btnTrace;
        private System.Windows.Forms.ColumnHeader clmCount;
    }
}

