namespace WEBAPI.DTOs
{
    public class ProductoDto
    {
        public int Id { get; set; } 

        public string Codigo { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public decimal Precio { get; set; }
    }
}
