using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiNotas.Models
{
    public class Categoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaReg { get; set; } = DateTime.Now;

        public virtual List<Nota>? Notas { get; set; }
    }
}
