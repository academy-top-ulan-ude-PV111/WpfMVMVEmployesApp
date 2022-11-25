using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace WpfMVMVEmployesApp
{
    public interface IDialogService
    {
        void ShowMsg(string message);
        string? Path { get; set; }
        bool OpenFileDialog();
        bool SaveFileDialog();
    }

    public class AppDialogService : IDialogService
    {
        public string? Path { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog dialog = new();
            if(dialog.ShowDialog() == true)
            {
                Path = dialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog dialog = new();
            if(dialog.ShowDialog() == true)
            {
                Path = dialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMsg(string message)
        {
            MessageBox.Show(message);
        }
    }
}
