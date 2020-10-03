using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeuristicSearch.DialogBoxes.UserControls
{
    public partial class SequentialAStarUC : UserControl
    {
        public Algorithms.Abstract.Delegates.HDelegate AnchorHeuristic
        {
            get
            {
                return stringToHeuristic(cboAnchorHeuristic.SelectedItem.ToString());
            }
        }

        public List<Algorithms.Abstract.Delegates.HDelegate> AdditionalHeuristics
        {
            get
            {
                List<Algorithms.Abstract.Delegates.HDelegate> h = new List<Algorithms.Abstract.Delegates.HDelegate>();

                if (chkDefault.Checked)
                    h.Add(stringToHeuristic("Default"));
                if (chkManhattan.Checked)
                    h.Add(stringToHeuristic("Manhattan"));
                if (chkEuclidean.Checked)
                    h.Add(stringToHeuristic("Euclidean"));
                if (chkDiagonal.Checked)
                    h.Add(stringToHeuristic("Diagonal"));
                if (chkChebyshev.Checked)
                    h.Add(stringToHeuristic("Chebyshev"));

                return h;
            }
        } 

        public decimal Weight1 { get { return numW1.Value; } }

        public decimal Weight2 { get { return numW2.Value; } }

        public SequentialAStarUC()
        {
            InitializeComponent();
        }

        public bool FormValid
        {
            get
            {
                int numAdditional = 0;

                if (chkDefault.Checked)
                    numAdditional++;
                if (chkManhattan.Checked)
                    numAdditional++;
                if (chkEuclidean.Checked)
                    numAdditional++;
                if (chkDiagonal.Checked)
                    numAdditional++;
                if (chkChebyshev.Checked)
                    numAdditional++;

                return numAdditional > 0;
            }
        }

        private void EnsureAnchorIsNotSelected(object sender, EventArgs e)
        {
            switch (cboAnchorHeuristic.SelectedItem.ToString())
            {
                case "Euclidean":
                    chkEuclidean.Checked = false;
                    chkEuclidean.Enabled = false;
                    chkManhattan.Enabled = true;
                    break;
                case "Manhattan":
                    chkManhattan.Checked = false;
                    chkManhattan.Enabled = false;
                    chkEuclidean.Enabled = true;
                    break;
                default:
                    MessageBox.Show("Invalid selection");
                    break;
            }
        }

        private Algorithms.Abstract.Delegates.HDelegate stringToHeuristic(string s)
        {
            switch (s)
            {
                case "Default":
                    return Algorithms.Formulas.Heuristics.DefaultHeuristic;
                case "Manhattan":
                    return Algorithms.Formulas.Heuristics.Manhattan;
                case "Euclidean":
                    return Algorithms.Formulas.Heuristics.Euclidean;
                case "Diagonal":
                    return Algorithms.Formulas.Heuristics.DiagonalDistance;
                case "Chebyshev":
                    return Algorithms.Formulas.Heuristics.Chebyshev;
            }
            return null;
        }

        private void SequentialAStarUC_Load(object sender, EventArgs e)
        {
            cboAnchorHeuristic.SelectedIndex = 0;
            chkManhattan.Enabled = false;

            cboAnchorHeuristic.SelectedIndexChanged += EnsureAnchorIsNotSelected;
        }
    }
}
