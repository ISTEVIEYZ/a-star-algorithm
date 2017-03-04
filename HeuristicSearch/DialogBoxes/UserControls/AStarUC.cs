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
    public partial class AStarUC : UserControl
    {
        public bool FormValid
        {
            get { return this.Heuristic != null; }
        }

        public Algorithms.Abstract.Delegates.HDelegate Heuristic
        {
            get
            {
                switch (cboHeuristic.SelectedItem.ToString())
                {
                    case "Default":
                        return Algorithms.Formulas.Heuristics.DefaultHeuristic;
                    case "Manhattan":
                        return Algorithms.Formulas.Heuristics.Manhattan;
                    case "Euclidean":
                        return Algorithms.Formulas.Heuristics.Manhattan;
                    case "Diagonal":
                        return Algorithms.Formulas.Heuristics.DiagonalDistance;
                    case "Chebyshev":
                        return Algorithms.Formulas.Heuristics.Chebyshev;
                    default:
                        return null;
                }
            }
        }

        public AStarUC()
        {
            InitializeComponent();

            cboHeuristic.SelectedIndex = 0;
        }
    }
}
