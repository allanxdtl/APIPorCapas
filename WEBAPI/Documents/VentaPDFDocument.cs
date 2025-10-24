using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WEBAPI.Models;

namespace WEBAPI.Documents
{
    public class VentaPDFDocument : IDocument
    {
        //Campo encapsultado del modelo VentaHeader
        private readonly VentaHeader _venta;

        //Constructor con parametro con el modelo de la venta
        public VentaPDFDocument(VentaHeader venta)
        {
            _venta = venta;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header()
                    .Column(column =>
                    {
                        column.Item().Text($"Nota de Venta #{_venta.Id}").FontSize(22).Bold().AlignLeft().FontColor(Colors.Blue.Lighten2);
                        //column.Item().Image()
                    });
                // Encabezado
                //page.Header()
                //    .Text($"Factura #{_venta.Id}")
                //    .FontSize(20)
                //    .Bold()
                //    .AlignCenter();

                // Cuerpo
                page.Content().PaddingVertical(10).Column(column =>
                {
                    column.Item().Text($"Fecha: {_venta.Fecha:dd/MM/yyyy}");
                    column.Item().Text($"Cliente: {_venta.Cliente?.Nombre ?? "N/A"}");
                    column.Item().Text($"Vendedor: {_venta.Usuario?.Nombre ?? "N/A"}");

                    column.Item().PaddingTop(10).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(4);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Descripción").Bold();
                            header.Cell().Text("Cantidad").Bold();
                            header.Cell().Text("Precio").Bold();
                            header.Cell().Text("Importe").Bold();
                        });

                        foreach (var item in _venta.Detalles)
                        {
                            table.Cell().Text(item.Producto.Descripcion);
                            table.Cell().Text(item.Cantidad.ToString());
                            table.Cell().Text(item.PrecioUnitario.ToString("C"));
                            table.Cell().Text((item.Cantidad * item.PrecioUnitario).ToString("C"));
                        }
                    });

                    column.Item().AlignRight().PaddingTop(10)
                        .Text($"Total: {_venta.Total:C}")
                        .FontSize(14)
                        .Bold();
                });

                // Pie de página
                page.Footer()
                    .AlignCenter()
                    .Text("Gracias por su compra — Mercado de Frutas y Verduras");
            });
        }
    }
}
