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
    public partial class StartGoalPairSelectForm : Form
    {
        private RadioButton[] radioButtons;
        private GridContent.StartGoalPair[] startGoalPairs;

        public int StartGoalPairIndex
        {
            get
            {
                for (int i = 0; i < radioButtons.Length; i++)
                    if (radioButtons[i].Checked)
                        return i;

                return -1;
            }
        }

        public StartGoalPairSelectForm(int startIndex, GridContent.StartGoalPair[] startGoalPairs)
        {
            InitializeComponent();

            this.startGoalPairs = startGoalPairs;
            radioButtons = new RadioButton[]
            {
                 pair0
                ,pair1
                ,pair2
                ,pair3
                ,pair4
                ,pair5
                ,pair6
                ,pair7
                ,pair8
                ,pair9
            };
            radioButtons[startIndex].Checked = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.radioButtons.Any(aa => aa.Checked))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Select a start/goal pair");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void StartGoalPairSelectForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < radioButtons.Length; i++)
                radioButtons[i].Text = startGoalPairs[i].ToString();
        }
    }
}
