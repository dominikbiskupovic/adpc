namespace WinForms.Services
{
    using MongoDB.Driver;
    using Configurations;
    using Models;
    
    public class MongoDbService
    {
        private readonly IMongoCollection<GeneExpression> _geneExpressions;
        private readonly MongoClient _mongoClient;
        private readonly MongoConfig _mongoConfig;

        public MongoDbService(MongoConfig mongoConfig)
        {
            _mongoClient = new MongoClient(mongoConfig.ConnectionString);
            _mongoConfig = mongoConfig;
            
            var database     = _mongoClient.GetDatabase(mongoConfig.Database);
            _geneExpressions = database.GetCollection<GeneExpression>(mongoConfig.Collection);
        }

        public async Task<List<GeneExpression>> GetGeneExpressionsAsync()
        {
            return await _geneExpressions.Find(_ => true).ToListAsync();
        }

        public async Task<GeneExpression> GetExpressionByPatientIdAsync(string patientId)
        {
            var filter = Builders<GeneExpression>.Filter.Eq(g => g.TcgaBarcode, patientId);
            return await _geneExpressions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateClinicalDataAsync(List<GeneExpression> expressions)
        {
            var bulkOps = new List<WriteModel<GeneExpression>>();

            foreach (var expr in expressions)
            {
                var filter = Builders<GeneExpression>.Filter.Eq(g => g.TcgaBarcode, expr.TcgaBarcode);
                var update = Builders<GeneExpression>.Update.Set(g => g.PatientClinicalRecord, expr.PatientClinicalRecord);
                bulkOps.Add(new UpdateOneModel<GeneExpression>(filter, update) { IsUpsert = true });
            }

            if (bulkOps.Count > 0)
            {
                await _geneExpressions.BulkWriteAsync(bulkOps);
                Console.WriteLine($"Updated clinical data for {bulkOps.Count} patients");
            }
        }

        public async Task ClearCollectionAsync()
        {
            await _geneExpressions.DeleteManyAsync(FilterDefinition<GeneExpression>.Empty);
            Console.WriteLine("Cleared gene expression collection");
        }

        public async Task SaveGeneExpressionsToMongo(List<GeneExpression> expressions)
        {
            var database    = _mongoClient.GetDatabase(_mongoConfig.Database);
            var collection  = database.GetCollection<GeneExpression>(_mongoConfig.Collection);
            
            await collection.InsertManyAsync(expressions);
        }
    }
}