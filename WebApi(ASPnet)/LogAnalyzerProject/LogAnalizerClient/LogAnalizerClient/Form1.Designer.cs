namespace LogAnalizerClient;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
        textBoxFilePath = new System.Windows.Forms.TextBox();
        buttonBrowse = new System.Windows.Forms.Button();
        comboBoxWeekType = new System.Windows.Forms.ComboBox();
        buttonImport = new System.Windows.Forms.Button();
        buttonCompare = new System.Windows.Forms.Button();
        comboBoxWeekType2 = new System.Windows.Forms.ComboBox();
        buttonShowResults = new System.Windows.Forms.Button();
        dataGridView1 = new System.Windows.Forms.DataGridView();
        ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
        SuspendLayout();
        // 
        // textBoxFilePath
        // 
        textBoxFilePath.Location = new System.Drawing.Point(16, 26);
        textBoxFilePath.Name = "textBoxFilePath";
        textBoxFilePath.Size = new System.Drawing.Size(175, 23);
        textBoxFilePath.TabIndex = 0;
        // 
        // buttonBrowse
        // 
        buttonBrowse.Location = new System.Drawing.Point(16, 366);
        buttonBrowse.Name = "buttonBrowse";
        buttonBrowse.Size = new System.Drawing.Size(60, 27);
        buttonBrowse.TabIndex = 1;
        buttonBrowse.Text = "Browse";
        buttonBrowse.UseVisualStyleBackColor = true;
        buttonBrowse.Click += buttonBrowse_Click;
        // 
        // comboBoxWeekType
        // 
        comboBoxWeekType.FormattingEnabled = true;
        comboBoxWeekType.Location = new System.Drawing.Point(131, 366);
        comboBoxWeekType.Name = "comboBoxWeekType";
        comboBoxWeekType.Size = new System.Drawing.Size(102, 23);
        comboBoxWeekType.TabIndex = 2;
        // 
        // buttonImport
        // 
        buttonImport.Location = new System.Drawing.Point(292, 366);
        buttonImport.Name = "buttonImport";
        buttonImport.Size = new System.Drawing.Size(88, 23);
        buttonImport.TabIndex = 3;
        buttonImport.Text = "Import";
        buttonImport.UseVisualStyleBackColor = true;
        buttonImport.Click += buttonImport_Click;
        // 
        // buttonCompare
        // 
        buttonCompare.Location = new System.Drawing.Point(455, 366);
        buttonCompare.Name = "buttonCompare";
        buttonCompare.Size = new System.Drawing.Size(100, 22);
        buttonCompare.TabIndex = 4;
        buttonCompare.Text = "Compare";
        buttonCompare.UseVisualStyleBackColor = true;
        buttonCompare.Click += buttonCompare_Click;
        // 
        // comboBoxWeekType2
        // 
        comboBoxWeekType2.FormattingEnabled = true;
        comboBoxWeekType2.Location = new System.Drawing.Point(131, 395);
        comboBoxWeekType2.Name = "comboBoxWeekType2";
        comboBoxWeekType2.Size = new System.Drawing.Size(102, 23);
        comboBoxWeekType2.TabIndex = 5;
        comboBoxWeekType2.SelectedIndexChanged += comboBoxWeekType2_SelectedIndexChanged;
        // 
        // buttonShowResults
        // 
        buttonShowResults.Location = new System.Drawing.Point(612, 365);
        buttonShowResults.Name = "buttonShowResults";
        buttonShowResults.Size = new System.Drawing.Size(84, 23);
        buttonShowResults.TabIndex = 8;
        buttonShowResults.Text = "ShowResults";
        buttonShowResults.Click += buttonShowResults_Click;
        // 
        // dataGridView1
        // 
        dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dataGridView1.Location = new System.Drawing.Point(272, 395);
        dataGridView1.Name = "dataGridView1";
        dataGridView1.Size = new System.Drawing.Size(491, 167);
        dataGridView1.TabIndex = 7;
        dataGridView1.Text = "dataGridView1";
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 578);
        Controls.Add(dataGridView1);
        Controls.Add(buttonShowResults);
        Controls.Add(comboBoxWeekType2);
        Controls.Add(buttonCompare);
        Controls.Add(buttonImport);
        Controls.Add(comboBoxWeekType);
        Controls.Add(buttonBrowse);
        Controls.Add(textBoxFilePath);
        Text = "Form1";
        ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.DataGridView dataGridView1;

    private System.Windows.Forms.Button buttonShowResults;

    private System.Windows.Forms.ComboBox comboBoxWeekType2;

    private System.Windows.Forms.Button buttonCompare;

    private System.Windows.Forms.Button buttonImport;

    private System.Windows.Forms.ComboBox comboBoxWeekType;

    private System.Windows.Forms.Button buttonBrowse;

    private System.Windows.Forms.TextBox textBoxFilePath;

    #endregion
}