using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeuristicSearch.DialogBoxes
{
    public static class LoadDialogBox
    {
        public static string ShowDialog()
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.DefaultExt = "txt";
            openDialog.Filter = "Grid text files (*.txt)|*.txt";
            openDialog.ShowDialog();

            return openDialog.FileName;
        }
    }
}
