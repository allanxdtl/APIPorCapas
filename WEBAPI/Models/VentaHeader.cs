namespace WEBAPI.Models
{
    public class VentaHeader
    {
        public int Id { get; set; }  // Autoincrementable, PK

        public string Folio { get; set; } = null!;  // Ejemplo: "VTA-2025-0001"

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        // FK hacia Usuario (vendedor)
        public int IdUsuario { get; set; }
        public virtual Usuario? Usuario { get; set; }

        // FK hacia Cliente
        public int IdCliente { get; set; }
        public virtual Cliente? Cliente { get; set; }

        // Relación con detalle
        public virtual ICollection<VentaDetalle> Detalles { get; set; } = new List<VentaDetalle>();
    }
}
