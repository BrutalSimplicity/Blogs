namespace Mandelbrot
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbWidth = new System.Windows.Forms.TextBox();
            this.tbHeight = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbRealMin = new System.Windows.Forms.TextBox();
            this.tbRealMax = new System.Windows.Forms.TextBox();
            this.tbImagineMin = new System.Windows.Forms.TextBox();
            this.tbImagineMax = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LabelMax = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbIterations = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbWidth
            // 
            this.tbWidth.Location = new System.Drawing.Point(56, 9);
            this.tbWidth.Name = "tbWidth";
            this.tbWidth.Size = new System.Drawing.Size(66, 20);
            this.tbWidth.TabIndex = 0;
            this.tbWidth.Text = "600";
            // 
            // tbHeight
            // 
            this.tbHeight.Location = new System.Drawing.Point(56, 35);
            this.tbHeight.Name = "tbHeight";
            this.tbHeight.Size = new System.Drawing.Size(66, 20);
            this.tbHeight.TabIndex = 1;
            this.tbHeight.Text = "600";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(18, 195);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(106, 23);
            this.btnGenerate.TabIndex = 6;
            this.btnGenerate.Text = "Generate Sets";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Height";
            // 
            // tbRealMin
            // 
            this.tbRealMin.Location = new System.Drawing.Point(56, 61);
            this.tbRealMin.Name = "tbRealMin";
            this.tbRealMin.Size = new System.Drawing.Size(66, 20);
            this.tbRealMin.TabIndex = 2;
            this.tbRealMin.Text = "-2.5";
            // 
            // tbRealMax
            // 
            this.tbRealMax.Location = new System.Drawing.Point(56, 87);
            this.tbRealMax.Name = "tbRealMax";
            this.tbRealMax.Size = new System.Drawing.Size(66, 20);
            this.tbRealMax.TabIndex = 3;
            this.tbRealMax.Text = "1.0";
            // 
            // tbImagineMin
            // 
            this.tbImagineMin.Location = new System.Drawing.Point(56, 113);
            this.tbImagineMin.Name = "tbImagineMin";
            this.tbImagineMin.Size = new System.Drawing.Size(66, 20);
            this.tbImagineMin.TabIndex = 4;
            this.tbImagineMin.Text = "-1.0";
            // 
            // tbImagineMax
            // 
            this.tbImagineMax.Location = new System.Drawing.Point(56, 139);
            this.tbImagineMax.Name = "tbImagineMax";
            this.tbImagineMax.Size = new System.Drawing.Size(66, 20);
            this.tbImagineMax.TabIndex = 5;
            this.tbImagineMax.Text = "1.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Real Min";
            // 
            // LabelMax
            // 
            this.LabelMax.AutoSize = true;
            this.LabelMax.Location = new System.Drawing.Point(1, 90);
            this.LabelMax.Name = "LabelMax";
            this.LabelMax.Size = new System.Drawing.Size(52, 13);
            this.LabelMax.TabIndex = 11;
            this.LabelMax.Text = "Real Max";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Imag Min";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 142);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Imag Max";
            // 
            // tbIterations
            // 
            this.tbIterations.Location = new System.Drawing.Point(55, 165);
            this.tbIterations.Name = "tbIterations";
            this.tbIterations.Size = new System.Drawing.Size(66, 20);
            this.tbIterations.TabIndex = 14;
            this.tbIterations.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Iterations";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(133, 230);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbIterations);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.LabelMax);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.tbImagineMax);
            this.Controls.Add(this.tbImagineMin);
            this.Controls.Add(this.tbRealMax);
            this.Controls.Add(this.tbRealMin);
            this.Controls.Add(this.tbHeight);
            this.Controls.Add(this.tbWidth);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Mandelbrot Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbWidth;
        private System.Windows.Forms.TextBox tbHeight;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbRealMin;
        private System.Windows.Forms.TextBox tbRealMax;
        private System.Windows.Forms.TextBox tbImagineMin;
        private System.Windows.Forms.TextBox tbImagineMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LabelMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbIterations;
        private System.Windows.Forms.Label label4;

    }
}

