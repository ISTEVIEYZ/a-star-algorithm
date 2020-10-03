using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeuristicSearch.DialogBoxes
{
    public static class SaveDialogBox
    {
        public static string ShowDialog()
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "txt";
            saveDialog.Filter = "Grid text files (*.txt)|.txt";
            saveDialog.ShowDialog();

            return saveDialog.FileName;
        }
    }
}
