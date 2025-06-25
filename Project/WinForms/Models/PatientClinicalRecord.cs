namespace WinForms.Models
{
    using MongoDB.Bson.Serialization.Attributes;
    
    public class PatientClinicalRecord
    {
        [BsonElement("tcga_barcode")]
        public string TcgaBarcode               { get; set; }

        [BsonElement("disease_specific_survival_rate")]
        public int? DiseaseSpecificSurvivalRate { get; set; }

        [BsonElement("overall_survival_rate")]
        public int? OverallSurvivalRate         { get; set; }

        [BsonElement("clinical_stage")]
        public string? ClinicalStage            { get; set; }
    }
}