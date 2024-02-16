using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;

using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace FlashEx
{
    public partial class Database : Form
    {

        // Folder Path Defs
        private static readonly string AppExecPath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";
        private static readonly string DatabasePath = AppExecPath + @"database\";



        public Database()
        {
            InitializeComponent();
        }

        private void Database_Load(object sender, EventArgs e)
        {
            // Get all subdirectories (folders) within the specified directory
            string[] folders = Directory.GetDirectories(DatabasePath);

            foreach (string folder in folders)
            {
                // Get the folder name from the full directory path
                string folderName = new DirectoryInfo(folder).Name;
                comboBoxSelectBoard.Items.Add(folderName);
            }

        }


        string datasheet = "";
        private void comboBoxSelectBoard_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBoxInfo.Image = System.Drawing.Image.FromFile($"{DatabasePath}{comboBoxSelectBoard.Text}\\info.png");
                datasheet = $"{DatabasePath}{comboBoxSelectBoard.Text}\\datasheet.pdf";
            }
            catch (Exception)
            {

                //throw;
            }


            listBoxDatasheetList.Items.Clear();
            string[] pdfFiles = Directory.GetFiles(DatabasePath + comboBoxSelectBoard.Text, "*.pdf");

            // Output all PDF file names
            foreach (string pdfFile in pdfFiles)
            {
                listBoxDatasheetList.Items.Add(Path.GetFileName(pdfFile));
            }



        }


        // Open URL with browser
        private void OpenLinBrowser(string url)
        {
            Process.Start(new ProcessStartInfo("cmd", $"/c start {url}")
            {
                CreateNoWindow = true
            });
        }



        private void listBoxDatasheetList_DoubleClick(object sender, EventArgs e)
        {
            //($"{DatabasePath}{comboBoxSelectBoard.Text}\\info.png"
            string selectedText = listBoxDatasheetList.SelectedItem.ToString();
            OpenLinBrowser($"{DatabasePath}{comboBoxSelectBoard.Text}\\{selectedText}");
        }

        private void linkLabelEspProdSelector_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenLinBrowser("https://products.espressif.com/#/");
        }
    }
}
