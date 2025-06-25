namespace WinForms
{
    using System.Globalization;
    using System.Windows.Forms.DataVisualization.Charting;
    using Services;
    using Models;
    
    public partial class Main : Form
    {
        private readonly MongoDbService         _mongoDbService;
        private readonly XenaScraperService     _xenaScraperService;
        private readonly ClinicalParserService  _clinicalParserService;
        
        public Main(MongoDbService mongoDbService, XenaScraperService xenaScraperService, ClinicalParserService clinicalParserService)
        {
            _mongoDbService         = mongoDbService;
            _xenaScraperService     = xenaScraperService;
            _clinicalParserService  = clinicalParserService;

            _ = mongoDbService.ClearCollectionAsync();
            
            InitializeComponent();
        }

        [Obsolete("Obsolete")]
        private async void ScrapeGeneDataClick(object sender, EventArgs e)
        {
            try
            {
                scrapeGeneDataButton.Enabled = false;
                scrapeGeneDataButton.Text = "SCRAPING GENE DATA...";

                await _xenaScraperService?.ScrapeAndDownloadFilesAsync()!;
                var insertedPatients = await _mongoDbService?.GetGeneExpressionsAsync()!;
                patientCountLabel.Text = $"Patients: {insertedPatients.Count}";

                await _xenaScraperService.ProcessFiles();

                MessageBox.Show("Done scraping.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                scrapeGeneDataButton.Enabled = true;
                scrapeGeneDataButton.Text = "SCRAPE GENE DATA";
            }
        }

        [Obsolete("Obsolete")]
        private async void ScrapeClinicalDataClick(object sender, EventArgs e)
        {
            try
            {
                scrapeClinicalDataButton.Enabled = false;
                scrapeClinicalDataButton.Text = "SCRAPING CLINICAL DATA...";

                await _clinicalParserService?.ScrapeAndDownloadClinicalFilesAsync()!;
                
                MessageBox.Show("Done scraping.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                scrapeClinicalDataButton.Enabled = true;
                scrapeClinicalDataButton.Text = "SCRAPE CLINICAL DATA";
            }
        }

        private async void MergeDataClick(object sender, EventArgs e)
        {
            try
            {
                mergeButton.Enabled = false;
                mergeButton.Text = "MERGING...";

                await _clinicalParserService?.MergeClinicalWithGeneExpressionAsync()!;

                MessageBox.Show("Data has been merged.");
                await LoadPatientsIntoDropdown();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                mergeButton.Enabled = true;
                mergeButton.Text = "MERGE DATA";
            }
        }

        private async Task LoadPatientsIntoDropdown()
        {
            var patients = await _mongoDbService?.GetGeneExpressionsAsync()!;
            var ids = patients.Select(p => p.TcgaBarcode).Distinct().OrderBy(id => id).ToArray();

            patientListComboBox.Items.Clear();
            patientListComboBox.Items.AddRange(ids);

            if (patientListComboBox.Items.Count > 0)
                patientListComboBox.SelectedIndex = 0;
        }

        private void DisplayGeneChart(GeneExpression patient)
        {
            var targetGenes = new[]
            {
                "C6orf150", "CCL5", "CXCL10", "TMEM173", "CXCL9", "CXCL11", "NFKB1", "IKBKE", "IRF3", "TREX1", "ATM", "IL6", "IL8"
            };

            MessageBox.Show($"Patient is: {patient.TcgaBarcode}, and gene value count is: {patient.PathwayGenes.Count}.");
            
            geneChart.Series.Clear();
            geneChart.ChartAreas.Clear();

            var area = geneChart.ChartAreas.Add("MainArea");
            var series = geneChart.Series.Add("GeneExpression");
            series.ChartType = SeriesChartType.Column;

            foreach (var gene in targetGenes)
            {
                if (patient.PathwayGenes.TryGetValue(gene, out var value))
                {
                    var pointIndex = series.Points.AddXY(gene, value);
                    series.Points[pointIndex].ToolTip = $"{gene}: {value:F2}";
                }
            }

            geneChart.Visible = true;
        }
        
        private async void ViewPatientClick(object sender, EventArgs e)
        {
            var id = patientIdInputField.Text.Trim().ToUpper();

            if (string.IsNullOrEmpty(id) && patientListComboBox.SelectedItem != null)
            {
                id = patientListComboBox.SelectedItem.ToString()?.Trim().ToUpper();
            }

            if (string.IsNullOrEmpty(id)) return;

            var patient = await _mongoDbService?.GetExpressionByPatientIdAsync(id)!;
            labelPatientInfo.Text = $"Patient: {patient.TcgaBarcode}, Cohort: {patient.Cohort}, " +
                                    $"Genes: {patient.PathwayGenes.Count}, DSS: {patient.PatientClinicalRecord?.DiseaseSpecificSurvivalRate}";

            DisplayGeneChart(patient);
        }

        private async void ExportCsvClick(object sender, EventArgs e)
        {
            var allPatients = await _mongoDbService?.GetGeneExpressionsAsync()!;
            if (allPatients.Count == 0)
            {
                MessageBox.Show("No patient data available.");
                return;
            }

            var saveDialog = new SaveFileDialog
            {
                Filter = "CSV Files (*.csv)|*.csv",
                FileName = "GeneDataExport.csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                await using var writer = new StreamWriter(saveDialog.FileName);
                await using var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture);

                csv.WriteField("PatientId");
                csv.WriteField("CancerCohort");
                foreach (var gene in allPatients.SelectMany(p => p.PathwayGenes.Keys).Distinct())
                    csv.WriteField(gene);
                await csv.NextRecordAsync();

                foreach (var patient in allPatients)
                {
                    csv.WriteField(patient.TcgaBarcode);
                    csv.WriteField(patient.Cohort);
                    foreach (var gene in allPatients.SelectMany(p => p.PathwayGenes.Keys).Distinct())
                        csv.WriteField(patient.PathwayGenes.GetValueOrDefault(gene, 0));
                    await csv.NextRecordAsync();
                }

                MessageBox.Show("Exported to CSV.");
            }
        }
    }
}