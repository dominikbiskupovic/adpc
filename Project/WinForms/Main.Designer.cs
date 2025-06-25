// Main.Designer.cs
namespace WinForms
{
    partial class Main
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            scrapeGeneDataButton = new System.Windows.Forms.Button();
            scrapeClinicalDataButton = new System.Windows.Forms.Button();
            mergeButton = new System.Windows.Forms.Button();
            patientIdInputField = new System.Windows.Forms.TextBox();
            patientCountLabel = new System.Windows.Forms.Label();
            viewPatientButton = new System.Windows.Forms.Button();
            labelPatientInfo = new System.Windows.Forms.Label();
            exportCsvButton = new System.Windows.Forms.Button();
            patientListComboBox = new System.Windows.Forms.ComboBox();
            geneChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)geneChart).BeginInit();
            SuspendLayout();
            // 
            // scrapeGeneDataButton
            // 
            scrapeGeneDataButton.Location = new System.Drawing.Point(20, 23);
            scrapeGeneDataButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            scrapeGeneDataButton.Name = "scrapeGeneDataButton";
            scrapeGeneDataButton.Size = new System.Drawing.Size(178, 66);
            scrapeGeneDataButton.TabIndex = 0;
            scrapeGeneDataButton.Text = "SCRAPE GENE DATA";
            scrapeGeneDataButton.UseVisualStyleBackColor = true;
            scrapeGeneDataButton.Click += ScrapeGeneDataClick;
            // 
            // scrapeClinicalDataButton
            // 
            scrapeClinicalDataButton.Location = new System.Drawing.Point(20, 101);
            scrapeClinicalDataButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            scrapeClinicalDataButton.Name = "scrapeClinicalDataButton";
            scrapeClinicalDataButton.Size = new System.Drawing.Size(178, 66);
            scrapeClinicalDataButton.TabIndex = 1;
            scrapeClinicalDataButton.Text = "SCRAPE CLINICAL DATA";
            scrapeClinicalDataButton.UseVisualStyleBackColor = true;
            scrapeClinicalDataButton.Click += ScrapeClinicalDataClick;
            // 
            // mergeButton
            // 
            mergeButton.Location = new System.Drawing.Point(20, 179);
            mergeButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            mergeButton.Name = "mergeButton";
            mergeButton.Size = new System.Drawing.Size(178, 44);
            mergeButton.TabIndex = 2;
            mergeButton.Text = "MERGE";
            mergeButton.UseVisualStyleBackColor = true;
            mergeButton.Click += MergeDataClick;
            // 
            // patientIdInputField
            // 
            patientIdInputField.Location = new System.Drawing.Point(940, 15);
            patientIdInputField.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            patientIdInputField.Name = "patientIdInputField";
            patientIdInputField.Size = new System.Drawing.Size(139, 31);
            patientIdInputField.TabIndex = 4;
            // 
            // patientCountLabel
            // 
            patientCountLabel.AutoSize = true;
            patientCountLabel.Location = new System.Drawing.Point(20, 817);
            patientCountLabel.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            patientCountLabel.Name = "patientCountLabel";
            patientCountLabel.Size = new System.Drawing.Size(77, 25);
            patientCountLabel.TabIndex = 6;
            patientCountLabel.Text = "Patients:";
            // 
            // viewPatientButton
            // 
            viewPatientButton.Location = new System.Drawing.Point(805, 15);
            viewPatientButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            viewPatientButton.Name = "viewPatientButton";
            viewPatientButton.Size = new System.Drawing.Size(125, 31);
            viewPatientButton.TabIndex = 7;
            viewPatientButton.Text = "View patient";
            viewPatientButton.UseVisualStyleBackColor = true;
            viewPatientButton.Click += ViewPatientClick;
            // 
            // labelPatientInfo
            // 
            labelPatientInfo.AutoSize = true;
            labelPatientInfo.Location = new System.Drawing.Point(633, 96);
            labelPatientInfo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            labelPatientInfo.Name = "labelPatientInfo";
            labelPatientInfo.Size = new System.Drawing.Size(0, 25);
            labelPatientInfo.TabIndex = 8;
            // 
            // exportCsvButton
            // 
            exportCsvButton.Location = new System.Drawing.Point(1175, 798);
            exportCsvButton.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            exportCsvButton.Name = "exportCsvButton";
            exportCsvButton.Size = new System.Drawing.Size(125, 44);
            exportCsvButton.TabIndex = 9;
            exportCsvButton.Text = "Export";
            exportCsvButton.UseVisualStyleBackColor = true;
            exportCsvButton.Click += ExportCsvClick;
            // 
            // patientListComboBox
            // 
            patientListComboBox.FormattingEnabled = true;
            patientListComboBox.Location = new System.Drawing.Point(1101, 15);
            patientListComboBox.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            patientListComboBox.Name = "patientListComboBox";
            patientListComboBox.Size = new System.Drawing.Size(199, 33);
            patientListComboBox.TabIndex = 10;
            // 
            // geneChart
            // 
            chartArea1.Name = "ChartArea1";
            geneChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            geneChart.Legends.Add(legend1);
            geneChart.Location = new System.Drawing.Point(208, 78);
            geneChart.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            geneChart.Name = "geneChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            geneChart.Series.Add(series1);
            geneChart.Size = new System.Drawing.Size(957, 764);
            geneChart.TabIndex = 11;
            geneChart.Text = "chart1";
            geneChart.Visible = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1333, 865);
            Controls.Add(geneChart);
            Controls.Add(patientListComboBox);
            Controls.Add(exportCsvButton);
            Controls.Add(labelPatientInfo);
            Controls.Add(viewPatientButton);
            Controls.Add(patientCountLabel);
            Controls.Add(patientIdInputField);
            Controls.Add(mergeButton);
            Controls.Add(scrapeClinicalDataButton);
            Controls.Add(scrapeGeneDataButton);
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            Text = "Main";
            ((System.ComponentModel.ISupportInitialize)geneChart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button scrapeGeneDataButton;
        private System.Windows.Forms.Button scrapeClinicalDataButton;
        private System.Windows.Forms.Button mergeButton;
        private System.Windows.Forms.TextBox patientIdInputField;
        private System.Windows.Forms.Label patientCountLabel;
        private System.Windows.Forms.Button viewPatientButton;
        private System.Windows.Forms.Label labelPatientInfo;
        private System.Windows.Forms.Button exportCsvButton;
        private System.Windows.Forms.ComboBox patientListComboBox;
        private System.Windows.Forms.DataVisualization.Charting.Chart geneChart;
    }
}
