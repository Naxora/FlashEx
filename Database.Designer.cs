namespace FlashEx
{
    partial class Database
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Database));
            comboBoxSelectBoard = new ComboBox();
            groupBox1 = new GroupBox();
            pictureBoxInfo = new PictureBox();
            listBoxDatasheetList = new ListBox();
            linkLabelEspProdSelector = new LinkLabel();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxInfo).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxSelectBoard
            // 
            comboBoxSelectBoard.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSelectBoard.FormattingEnabled = true;
            comboBoxSelectBoard.Location = new Point(6, 22);
            comboBoxSelectBoard.Name = "comboBoxSelectBoard";
            comboBoxSelectBoard.Size = new Size(284, 23);
            comboBoxSelectBoard.TabIndex = 0;
            comboBoxSelectBoard.SelectedIndexChanged += comboBoxSelectBoard_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(comboBoxSelectBoard);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(296, 58);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Select Board";
            // 
            // pictureBoxInfo
            // 
            pictureBoxInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxInfo.BackColor = Color.FromArgb(38, 44, 66);
            pictureBoxInfo.Location = new Point(319, -1);
            pictureBoxInfo.Name = "pictureBoxInfo";
            pictureBoxInfo.Size = new Size(737, 638);
            pictureBoxInfo.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxInfo.TabIndex = 2;
            pictureBoxInfo.TabStop = false;
            // 
            // listBoxDatasheetList
            // 
            listBoxDatasheetList.FormattingEnabled = true;
            listBoxDatasheetList.ItemHeight = 15;
            listBoxDatasheetList.Location = new Point(6, 22);
            listBoxDatasheetList.Name = "listBoxDatasheetList";
            listBoxDatasheetList.Size = new Size(284, 409);
            listBoxDatasheetList.TabIndex = 4;
            listBoxDatasheetList.DoubleClick += listBoxDatasheetList_DoubleClick;
            // 
            // linkLabelEspProdSelector
            // 
            linkLabelEspProdSelector.AutoSize = true;
            linkLabelEspProdSelector.Location = new Point(12, 517);
            linkLabelEspProdSelector.Name = "linkLabelEspProdSelector";
            linkLabelEspProdSelector.Size = new Size(116, 15);
            linkLabelEspProdSelector.TabIndex = 5;
            linkLabelEspProdSelector.TabStop = true;
            linkLabelEspProdSelector.Text = "ESP Product Selector";
            linkLabelEspProdSelector.LinkClicked += linkLabelEspProdSelector_LinkClicked;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(listBoxDatasheetList);
            groupBox2.Location = new Point(12, 76);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(296, 438);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Docs";
            // 
            // Database
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1053, 637);
            Controls.Add(groupBox2);
            Controls.Add(linkLabelEspProdSelector);
            Controls.Add(pictureBoxInfo);
            Controls.Add(groupBox1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Database";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Database";
            Load += Database_Load;
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxInfo).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxSelectBoard;
        private GroupBox groupBox1;
        private PictureBox pictureBoxInfo;
        private ListBox listBoxDatasheetList;
        private LinkLabel linkLabelEspProdSelector;
        private GroupBox groupBox2;
    }
}