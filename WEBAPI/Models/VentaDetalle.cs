namespace WEBAPI.Models
{
    public class VentaDetalle
    {
        public int Id { get; set; }

        // FK hacia Header
        public int IdVentaHeader { get; set; }
        public virtual VentaHeader? VentaHeader { get; set; }

        // FK hacia Producto
        public int IdProducto { get; set; }
        public virtual Producto? Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }

        // Total por línea (calculado, opcional si quieres guardarlo)
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
