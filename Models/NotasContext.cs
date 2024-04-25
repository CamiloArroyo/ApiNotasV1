using MongoDB.Driver;

namespace ApiNotas.Models
{
    public class NotasContext
    {
        private readonly IMongoDatabase _database;

        public NotasContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Nota> Notas => _database.GetCollection<Nota>("Notas");
        public IMongoCollection<Categoria> Categorias => _database.GetCollection<Categoria>("Categorias");
    }
}
