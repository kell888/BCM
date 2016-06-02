using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BCM
{
    public partial class NewSettingForm : Form
    {
        public NewSettingForm(Dictionary<string, ObjectReference> currentSetting, string primaryKeySetting)
        {
            InitializeComponent();
            NewSetting(currentSetting, primaryKeySetting);
        }

        public Dictionary<string, ObjectReference> Record
        {
            get
            {
                return settingEditor1.Result;
            }
        }

        private void NewSetting(Dictionary<string, ObjectReference> currentSetting, string primaryKeySetting)
        {
            settingEditor1.NewSetting(currentSetting, primaryKeySetting);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
