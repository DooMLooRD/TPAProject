using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Win32;
using ViewModels;

namespace WPFPathLoader
{
    [Export(typeof(IPathLoader))]
    public class WPFPathLoader : IPathLoader
    {
        public string LoadPath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library File(*.dll)| *.dll|"+
                         "XML File(*.xml)| *.xml",
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

