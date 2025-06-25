namespace WinForms.Models
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    
    public class GeneExpression
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id            { get; set; }

        [BsonElement("tcga_barcode")]
        public string TcgaBarcode   { get; set; }

        [BsonElement("cohort")]
        public string Cohort        { get; set; }
        
        [BsonElement("patient_clinical_record")]
        public PatientClinicalRecord PatientClinicalRecord { get; set; }
        
        [BsonElement("pathway_genes")]
        public Dictionary<string, double> PathwayGenes { get; set; } = new();
    }
}