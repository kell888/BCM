namespace BCM
{
    partial class EditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.settingEditor1 = new BCM.SettingEditor();
            this.settingList = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.settingList)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Image = global::BCM.Properties.Resources.update;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(497, 283);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 40);
            this.button1.TabIndex = 1;
            this.button1.Text = "保存";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Image = global::BCM.Properties.Resources.delete;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(602, 283);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 40);
            this.button2.TabIndex = 2;
            this.button2.Text = "退出";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // settingEditor1
            // 
            this.settingEditor1.AutoScroll = true;
            this.settingEditor1.Location = new System.Drawing.Point(423, 12);
            this.settingEditor1.MinimumSize = new System.Drawing.Size(180, 55);
            this.settingEditor1.Name = "settingEditor1";
            this.settingEditor1.Size = new System.Drawing.Size(334, 260);
            this.settingEditor1.TabIndex = 3;
            // 
            // settingList
            // 
            this.settingList.AllowUserToAddRows = false;
            this.settingList.AllowUserToDeleteRows = false;
            this.settingList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.settingList.Location = new System.Drawing.Point(12, 12);
            this.settingList.Name = "settingList";
            this.settingList.ReadOnly = true;
            this.settingList.RowTemplate.Height = 23;
            this.settingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.settingList.Size = new System.Drawing.Size(395, 311);
            this.settingList.TabIndex = 4;
            this.settingList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.settingList_CellClick);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 335);
            this.Controls.Add(this.settingList);
            this.Controls.Add(this.settingEditor1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑对象";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.settingList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private SettingEditor settingEditor1;
        private System.Windows.Forms.DataGridView settingList;

    }
}