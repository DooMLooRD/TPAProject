using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BusinessLogic.ViewModel;
using Microsoft.Win32;

namespace WPFApplication.Helper
{
    public class WPFPathLoader : IPathLoader
    {
        public string LoadPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library File(*.dll)| *.dll",
                RestoreDirectory = true
            };
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
                return null;
            }

            return openFileDialog.FileName;

        }
    }
}

