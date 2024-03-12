namespace ApiNotas.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaReg { get; set; } = DateTime.Now;

        public virtual List<Nota>? Notas { get; set; }
    }
}
