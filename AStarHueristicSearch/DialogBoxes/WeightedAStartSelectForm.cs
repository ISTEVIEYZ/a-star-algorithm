using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeuristicSearch.DialogBoxes
{
    public partial class WeightedAStartSelectForm : Form
    {
        public WeightedAStartSelectForm()
        {
            InitializeComponent();
            cboHeuristics.SelectedIndex = 0;
        }

        public decimal Weight
        {
            get { return numWeight.Value; }
        }

        public string Heuristic
        {
            get { return cboHeuristics.SelectedItem.ToString(); }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (cboHeuristics.SelectedIndex > -1)
                this.DialogResult = DialogResult.OK;
            else
                this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
