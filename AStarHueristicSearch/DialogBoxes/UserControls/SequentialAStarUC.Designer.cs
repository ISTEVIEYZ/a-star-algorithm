namespace HeuristicSearch.DialogBoxes.UserControls
{
    partial class SequentialAStarUC
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboAnchorHeuristic = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numW2 = new System.Windows.Forms.NumericUpDown();
            this.numW1 = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.chkChebyshev = new System.Windows.Forms.CheckBox();
            this.chkDiagonal = new System.Windows.Forms.CheckBox();
            this.chkEuclidean = new System.Windows.Forms.CheckBox();
            this.chkManhattan = new System.Windows.Forms.CheckBox();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numW2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numW1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.54098F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.45901F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(725, 295);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboAnchorHeuristic);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 60);
            this.panel1.TabIndex = 0;
            // 
            // cboAnchorHeuristic
            // 
            this.cboAnchorHeuristic.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAnchorHeuristic.FormattingEnabled = true;
            this.cboAnchorHeuristic.Items.AddRange(new object[] {
            "Manhattan",
            "Euclidean"});
            this.cboAnchorHeuristic.Location = new System.Drawing.Point(6, 16);
            this.cboAnchorHeuristic.Name = "cboAnchorHeuristic";
            this.cboAnchorHeuristic.Size = new System.Drawing.Size(239, 21);
            this.cboAnchorHeuristic.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select an anchor heuristic:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.numW2);
            this.panel2.Controls.Add(this.numW1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(365, 3);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            this.panel2.Size = new System.Drawing.Size(357, 289);
            this.panel2.TabIndex = 1;
            // 
            // numW2
            // 
            this.numW2.DecimalPlaces = 2;
            this.numW2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numW2.Location = new System.Drawing.Point(6, 60);
            this.numW2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numW2.Name = "numW2";
            this.numW2.Size = new System.Drawing.Size(63, 20);
            this.numW2.TabIndex = 3;
            this.numW2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numW1
            // 
            this.numW1.DecimalPlaces = 2;
            this.numW1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numW1.Location = new System.Drawing.Point(6, 21);
            this.numW1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numW1.Name = "numW1";
            this.numW1.Size = new System.Drawing.Size(63, 20);
            this.numW1.TabIndex = 2;
            this.numW1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(278, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Enter weight to prioritize the inadmissable search process:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Enter weight for heuristic inflation:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.chkChebyshev);
            this.panel3.Controls.Add(this.chkDiagonal);
            this.panel3.Controls.Add(this.chkEuclidean);
            this.panel3.Controls.Add(this.chkManhattan);
            this.panel3.Controls.Add(this.chkDefault);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(356, 223);
            this.panel3.TabIndex = 2;
            // 
            // chkChebyshev
            // 
            this.chkChebyshev.AutoSize = true;
            this.chkChebyshev.Location = new System.Drawing.Point(6, 108);
            this.chkChebyshev.Name = "chkChebyshev";
            this.chkChebyshev.Size = new System.Drawing.Size(79, 17);
            this.chkChebyshev.TabIndex = 5;
            this.chkChebyshev.Text = "Chebyshev";
            this.chkChebyshev.UseVisualStyleBackColor = true;
            // 
            // chkDiagonal
            // 
            this.chkDiagonal.AutoSize = true;
            this.chkDiagonal.Location = new System.Drawing.Point(6, 85);
            this.chkDiagonal.Name = "chkDiagonal";
            this.chkDiagonal.Size = new System.Drawing.Size(68, 17);
            this.chkDiagonal.TabIndex = 4;
            this.chkDiagonal.Text = "Diagonal";
            this.chkDiagonal.UseVisualStyleBackColor = true;
            // 
            // chkEuclidean
            // 
            this.chkEuclidean.AutoSize = true;
            this.chkEuclidean.Location = new System.Drawing.Point(6, 62);
            this.chkEuclidean.Name = "chkEuclidean";
            this.chkEuclidean.Size = new System.Drawing.Size(73, 17);
            this.chkEuclidean.TabIndex = 3;
            this.chkEuclidean.Text = "Euclidean";
            this.chkEuclidean.UseVisualStyleBackColor = true;
            // 
            // chkManhattan
            // 
            this.chkManhattan.AutoSize = true;
            this.chkManhattan.Location = new System.Drawing.Point(6, 39);
            this.chkManhattan.Name = "chkManhattan";
            this.chkManhattan.Size = new System.Drawing.Size(77, 17);
            this.chkManhattan.TabIndex = 2;
            this.chkManhattan.Text = "Manhattan";
            this.chkManhattan.UseVisualStyleBackColor = true;
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Location = new System.Drawing.Point(6, 16);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(60, 17);
            this.chkDefault.TabIndex = 1;
            this.chkDefault.Text = "Default";
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Select additional heuristics:";
            // 
            // SequentialAStarUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SequentialAStarUC";
            this.Size = new System.Drawing.Size(725, 295);
            this.Load += new System.EventHandler(this.SequentialAStarUC_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numW2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numW1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboAnchorHeuristic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown numW2;
        private System.Windows.Forms.NumericUpDown numW1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkChebyshev;
        private System.Windows.Forms.CheckBox chkDiagonal;
        private System.Windows.Forms.CheckBox chkEuclidean;
        private System.Windows.Forms.CheckBox chkManhattan;
        private System.Windows.Forms.CheckBox chkDefault;
        private System.Windows.Forms.Label label4;
    }
}
