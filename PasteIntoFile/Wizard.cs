﻿using System;
using System.Windows.Forms;
using PasteIntoFile.Properties;

namespace PasteIntoFile
{
    public partial class Wizard : MasterForm
    {
        public Wizard()
        {
            InitializeComponent();
            
            foreach (Control element in GetAllChild(this))
            {
                // ReSharper disable once UnusedVariable (to convince IDE that these resource strings are actually used)
                string[] usedResourceStrings = { Resources.str_wizard_title, Resources.str_wizard_contextentry_title, Resources.str_wizard_contextentry_info, Resources.str_wizard_contextentry_button, Resources.str_wizard_autosave_title, Resources.str_wizard_autosave_info, Resources.str_wizard_autosave_button, Resources.str_wizard_finish };
                element.Text = Resources.ResourceManager.GetString(element.Text) ?? element.Text;
            }
            
            Icon = Resources.icon;
            Text = Resources.str_main_window_title;

            version.Text = string.Format(Resources.str_version, ProductVersion);

            chkAutoSave.Checked = Settings.Default.autoSave;
            chkContextEntry.Checked = Program.IsAppRegistered();
        }

        private void ChkAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.autoSave = chkAutoSave.Checked;
            Settings.Default.Save();

        }

        private void ChkContextEntry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkContextEntry.Checked && !Program.IsAppRegistered())
            {
                Program.RegisterApp();
            }
            else if (!chkContextEntry.Checked && Program.IsAppRegistered())
            {
                Program.UnRegisterApp();
            }
        }

        private void finish_Click(object sender, EventArgs e)
        {
            Settings.Default.firstLaunch = false;
            Settings.Default.Save();
            Close();
        }

        private void Wizard_Shown(object sender, EventArgs e)
        {
            // Auto size dialog height
            // All tableLayout rows are set to 'autosize' except for the last -> it's height is a measure for leftover space 
            Height -= tableLayoutPanel1.GetRowHeights()[tableLayoutPanel1.RowCount - 1];
            MinimumSize = Size;
        }
    }
}
