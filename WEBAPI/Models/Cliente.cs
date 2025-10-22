namespace WEBAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Apellido { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<VentaHeader> Ventas { get; set; } = new List<VentaHeader>();
    }
}
