namespace WEBAPI.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Apellido { get; set; }

        public string? Telefono { get; set; }

        public string? Email { get; set; }
    }
}
