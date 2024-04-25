using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiNotas.Models
{
    public class Nota
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReg { get; set; }
        public string? CategoriaId { get; set; }
        //public Categoria? Categoria { get; set; }
    }
}
