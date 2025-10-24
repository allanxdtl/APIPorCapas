using WEBAPI.Models;

namespace WEBAPI.DTOs
{
    public class VentaDto
    {
        public int Id { get; set; }  // Autoincrementable, PK

        public string Folio { get; set; } = null!;  // Ejemplo: "VTA-2025-0001"

        public DateTime Fecha { get; set; }

        public decimal Total { get; set; }

        public int IdUsuario { get; set; }

        public int IdCliente { get; set; }

        // Relación con detalle
        public virtual ICollection<RenglonesDto> Renglones { get; set; } = new List<RenglonesDto>();
    }
}
