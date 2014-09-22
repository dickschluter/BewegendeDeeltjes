namespace BewegendeDeeltjes
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
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonBevriezen = new System.Windows.Forms.Button();
            this.numericUpDownAantal = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownRadius = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxKleur = new System.Windows.Forms.ComboBox();
            this.numericUpDownAantal2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRadius2 = new System.Windows.Forms.NumericUpDown();
            this.comboBoxKleur2 = new System.Windows.Forms.ComboBox();
            this.panelControls = new System.Windows.Forms.Panel();
            this.panelParameters = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxSlowMotion = new System.Windows.Forms.CheckBox();
            this.panelAnimatie = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAantal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAantal2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius2)).BeginInit();
            this.panelControls.SuspendLayout();
            this.panelParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(80, 360);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(80, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(80, 420);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(80, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonBevriezen
            // 
            this.buttonBevriezen.Location = new System.Drawing.Point(80, 390);
            this.buttonBevriezen.Name = "buttonBevriezen";
            this.buttonBevriezen.Size = new System.Drawing.Size(80, 23);
            this.buttonBevriezen.TabIndex = 2;
            this.buttonBevriezen.Text = "Bevriezen";
            this.buttonBevriezen.UseVisualStyleBackColor = true;
            this.buttonBevriezen.Click += new System.EventHandler(this.buttonBevriezen_Click);
            // 
            // numericUpDownAantal
            // 
            this.numericUpDownAantal.Location = new System.Drawing.Point(80, 0);
            this.numericUpDownAantal.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownAantal.Name = "numericUpDownAantal";
            this.numericUpDownAantal.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownAantal.TabIndex = 3;
            this.numericUpDownAantal.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Aantal";
            // 
            // numericUpDownRadius
            // 
            this.numericUpDownRadius.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownRadius.Location = new System.Drawing.Point(80, 30);
            this.numericUpDownRadius.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownRadius.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownRadius.Name = "numericUpDownRadius";
            this.numericUpDownRadius.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownRadius.TabIndex = 7;
            this.numericUpDownRadius.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Radius";
            // 
            // comboBoxKleur
            // 
            this.comboBoxKleur.FormattingEnabled = true;
            this.comboBoxKleur.Items.AddRange(new object[] {
            "Zwart",
            "Rood",
            "Groen",
            "Blauw",
            "Vier kleuren"});
            this.comboBoxKleur.Location = new System.Drawing.Point(80, 60);
            this.comboBoxKleur.Name = "comboBoxKleur";
            this.comboBoxKleur.Size = new System.Drawing.Size(80, 21);
            this.comboBoxKleur.TabIndex = 10;
            this.comboBoxKleur.Text = "Vier kleuren";
            // 
            // numericUpDownAantal2
            // 
            this.numericUpDownAantal2.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownAantal2.Location = new System.Drawing.Point(180, 0);
            this.numericUpDownAantal2.Name = "numericUpDownAantal2";
            this.numericUpDownAantal2.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownAantal2.TabIndex = 11;
            // 
            // numericUpDownRadius2
            // 
            this.numericUpDownRadius2.Increment = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownRadius2.Location = new System.Drawing.Point(180, 30);
            this.numericUpDownRadius2.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownRadius2.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownRadius2.Name = "numericUpDownRadius2";
            this.numericUpDownRadius2.Size = new System.Drawing.Size(80, 20);
            this.numericUpDownRadius2.TabIndex = 12;
            this.numericUpDownRadius2.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // comboBoxKleur2
            // 
            this.comboBoxKleur2.FormattingEnabled = true;
            this.comboBoxKleur2.Items.AddRange(new object[] {
            "Zwart",
            "Rood",
            "Groen",
            "Blauw",
            "Vier kleuren"});
            this.comboBoxKleur2.Location = new System.Drawing.Point(180, 60);
            this.comboBoxKleur2.Name = "comboBoxKleur2";
            this.comboBoxKleur2.Size = new System.Drawing.Size(80, 21);
            this.comboBoxKleur2.TabIndex = 13;
            this.comboBoxKleur2.Text = "Vier kleuren";
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.panelParameters);
            this.panelControls.Controls.Add(this.checkBoxSlowMotion);
            this.panelControls.Controls.Add(this.buttonBevriezen);
            this.panelControls.Controls.Add(this.buttonStop);
            this.panelControls.Controls.Add(this.buttonStart);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelControls.Location = new System.Drawing.Point(1000, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(300, 680);
            this.panelControls.TabIndex = 15;
            // 
            // panelParameters
            // 
            this.panelParameters.Controls.Add(this.label4);
            this.panelParameters.Controls.Add(this.comboBoxKleur);
            this.panelParameters.Controls.Add(this.numericUpDownRadius);
            this.panelParameters.Controls.Add(this.numericUpDownAantal);
            this.panelParameters.Controls.Add(this.numericUpDownAantal2);
            this.panelParameters.Controls.Add(this.numericUpDownRadius2);
            this.panelParameters.Controls.Add(this.comboBoxKleur2);
            this.panelParameters.Controls.Add(this.label3);
            this.panelParameters.Controls.Add(this.label1);
            this.panelParameters.Location = new System.Drawing.Point(0, 60);
            this.panelParameters.Name = "panelParameters";
            this.panelParameters.Size = new System.Drawing.Size(300, 90);
            this.panelParameters.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Kleur";
            // 
            // checkBoxSlowMotion
            // 
            this.checkBoxSlowMotion.AutoSize = true;
            this.checkBoxSlowMotion.Location = new System.Drawing.Point(180, 364);
            this.checkBoxSlowMotion.Name = "checkBoxSlowMotion";
            this.checkBoxSlowMotion.Size = new System.Drawing.Size(83, 17);
            this.checkBoxSlowMotion.TabIndex = 16;
            this.checkBoxSlowMotion.Text = "Slow motion";
            this.checkBoxSlowMotion.UseVisualStyleBackColor = true;
            this.checkBoxSlowMotion.CheckedChanged += new System.EventHandler(this.checkBoxSlowMotion_CheckedChanged);
            // 
            // panelAnimatie
            // 
            this.panelAnimatie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAnimatie.Location = new System.Drawing.Point(0, 0);
            this.panelAnimatie.Name = "panelAnimatie";
            this.panelAnimatie.Size = new System.Drawing.Size(1000, 680);
            this.panelAnimatie.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 680);
            this.Controls.Add(this.panelAnimatie);
            this.Controls.Add(this.panelControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "0";
            this.Text = "BewegendeDeeltjes";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAantal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAantal2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRadius2)).EndInit();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.panelParameters.ResumeLayout(false);
            this.panelParameters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonBevriezen;
        private System.Windows.Forms.NumericUpDown numericUpDownAantal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxKleur;
        private System.Windows.Forms.NumericUpDown numericUpDownAantal2;
        private System.Windows.Forms.NumericUpDown numericUpDownRadius2;
        private System.Windows.Forms.ComboBox comboBoxKleur2;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel panelAnimatie;
        private System.Windows.Forms.CheckBox checkBoxSlowMotion;
        private System.Windows.Forms.Panel panelParameters;
        private System.Windows.Forms.Label label4;
    }
}

