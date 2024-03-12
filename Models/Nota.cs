namespace ApiNotas.Models
{
    public class Nota
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaReg { get; set; }
        public int CategoriaId { get; set; }
        //public Categoria? Categoria { get; set; }
    }
}
