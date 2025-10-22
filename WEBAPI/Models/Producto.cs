namespace WEBAPI.Models
{
    public class Producto
    {
        public int Id { get; set; }  // Autoincrementable (PK)

        public string Codigo { get; set; } = null!;  // Ej. código de barras, hasta 13 chars

        public string Descripcion { get; set; } = null!;

        public decimal Precio { get; set; }

        // Relación con detalle de venta
        public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
    }
}
