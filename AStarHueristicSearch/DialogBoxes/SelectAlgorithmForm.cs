using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HeuristicSearch.Algorithms.Abstract;
using HeuristicSearch.Algorithms;
using HeuristicSearch.GridContent;

namespace HeuristicSearch.DialogBoxes
{
    public partial class SelectAlgorithmForm : Form
    {
        public IPathAlgorithm PathAlgorithm { get; private set; }

        private Cell[,] cellGrid;
        private Cell start;
        private Cell goal;

        private UserControl activeUc;

        public SelectAlgorithmForm(Cell[,] cellGrid, Cell start, Cell goal)
        {
            InitializeComponent();

            this.cellGrid = cellGrid;
            this.start = start;
            this.goal = goal;

            cboAlgo.SelectedIndex = 0;

            this.DialogResult = DialogResult.Cancel;
        }

        private void cboAlgo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAlgo.SelectedIndex > -1)
            {
                switch (cboAlgo.SelectedItem.ToString())
                {
                    case "A*":
                        activeUc = new UserControls.AStarUC();
                        activeUc.Dock = DockStyle.Top;
                        groupBox1.Controls.Clear();
                        groupBox1.Controls.Add(activeUc);
                        break;
                    case "Weighted A*":
                        activeUc = new UserControls.WeightedAStarUC();
                        activeUc.Dock = DockStyle.Top;
                        groupBox1.Controls.Clear();
                        groupBox1.Controls.Add(activeUc);
                        break;
                    case "Uniform Cost":
                        groupBox1.Controls.Clear();
                        break;
                    case "Sequential A*":
                        activeUc = new UserControls.SequentialAStarUC();
                        activeUc.Dock = DockStyle.Fill;
                        groupBox1.Controls.Clear();
                        groupBox1.Controls.Add(activeUc);
                        break;
                    case "Integrated A*":
                        activeUc = new UserControls.SequentialAStarUC();
                        activeUc.Dock = DockStyle.Fill;
                        groupBox1.Controls.Clear();
                        groupBox1.Controls.Add(activeUc);
                        break;
                    default:
                        MessageBox.Show("Invalid selection");
                        break;
                }
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if (cboAlgo.SelectedIndex > -1)
            {
                switch (cboAlgo.SelectedItem.ToString())
                {
                    case "A*":
                        if (activeUc is UserControls.AStarUC && (activeUc as UserControls.AStarUC).FormValid)
                        {
                            AStarPathAlgorithm aStar = new AStarPathAlgorithm(cellGrid, start, goal);
                            aStar.H = (activeUc as UserControls.AStarUC).Heuristic;
                            PathAlgorithm = aStar;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Missing data");
                        break;
                    case "Weighted A*":
                        if (activeUc is UserControls.WeightedAStarUC && (activeUc as UserControls.WeightedAStarUC).FormValid)
                        {
                            decimal w = (activeUc as UserControls.WeightedAStarUC).Weight;
                            WeightedAStarPathAlgorithm waStar = new WeightedAStarPathAlgorithm(cellGrid, start, goal, w);
                            waStar.H = (activeUc as UserControls.WeightedAStarUC).Heuristic;
                            PathAlgorithm = waStar;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Missing data");
                        break;
                    case "Uniform Cost":
                        UniformCostSearchPathAlgorithm ucs = new UniformCostSearchPathAlgorithm(cellGrid, start, goal);
                        PathAlgorithm = ucs;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case "Sequential A*":
                        if (activeUc is UserControls.SequentialAStarUC && (activeUc as UserControls.SequentialAStarUC).FormValid)
                        {
                            UserControls.SequentialAStarUC ucseq = activeUc as UserControls.SequentialAStarUC;
                            Delegates.HDelegate anchor = ucseq.AnchorHeuristic;
                            List<Delegates.HDelegate> h = ucseq.AdditionalHeuristics;
                            h.Insert(0, anchor);
                            decimal w1 = ucseq.Weight1;
                            decimal w2 = ucseq.Weight2;

                            AStarSequentialHeuristic astarseq = new AStarSequentialHeuristic(cellGrid, start, goal, h.Count, w1, w2);
                            astarseq.Heuristics = h.ToArray();

                            PathAlgorithm = astarseq;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Missing data");
                        break;
                    case "Integrated A*":
                        if (activeUc is UserControls.SequentialAStarUC && (activeUc as UserControls.SequentialAStarUC).FormValid)
                        {
                            UserControls.SequentialAStarUC ucseq = activeUc as UserControls.SequentialAStarUC;
                            Delegates.HDelegate anchor = ucseq.AnchorHeuristic;
                            List<Delegates.HDelegate> h = ucseq.AdditionalHeuristics;
                            h.Insert(0, anchor);
                            decimal w1 = ucseq.Weight1;
                            decimal w2 = ucseq.Weight2;

                            AStarIntegratedAlgorithm astarin = new AStarIntegratedAlgorithm(cellGrid, start, goal, h.Count, w1, w2);
                            astarin.Heuristics = h.ToArray();

                            PathAlgorithm = astarin;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show("Missing data");
                        break;
                    default:
                        MessageBox.Show("Invalid selection");
                        break;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
