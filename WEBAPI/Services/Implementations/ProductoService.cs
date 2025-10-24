using Microsoft.EntityFrameworkCore;
using WEBAPI.DTOs;
using WEBAPI.Models;
using WEBAPI.Repositories.Interfaces;
using WEBAPI.Services.Interfaces;

namespace WEBAPI.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerProductosAsync()
        {
            return await _repository.GetAllProductosAsync();
        }

        public async Task<bool> CrearProductoAsync(Producto producto)
        {
            try
            {
                //Validamos el codigo
                Producto? productoExistente = await _repository.VerifyBarcodeAsync(producto.Codigo);

                if (productoExistente != null)
                    throw new ArgumentException("El codigo de barra ya esta registrado en otro producto");

                return await _repository.CreateProductoAsync(producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ActualizarProductoAsync(Producto producto)
        {
            try
            {
                //Verificamos que el codigo no exista en la base de datos, y que el precio sea mayor a cero
                Producto? productoExistente = await _repository.VerifyBarcodeAsync(producto.Codigo);

                if (producto.Precio <= 0)
                    return false;

                if (productoExistente != null && producto.Id != productoExistente.Id)
                    return false;

                return await _repository.UpdateProductoAsync(producto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EliminarProductoAsync(string barcode)
        {
            try
            {
                //Primero localizamos el producto
                Producto? producto = await _repository.VerifyBarcodeAsync(barcode);

                return producto == null
                    ? throw new ArgumentException("El producto no fue encontrado")
                    : await _repository.DeleteProductoAsync(producto);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
