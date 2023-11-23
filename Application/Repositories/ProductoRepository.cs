using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Repositories
{
    public class ProductoRepository : GenericRepositorySTRING<Producto>, IProducto
    {
        private readonly FiltroContext _context;

        public ProductoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<object>> GetProductosSinPedido()
        {
            var query = await (from producto in _context.Productos
                        where! _context.DetallePedidos.Any(x => x.IdProductoFk == producto.Id)
                        join gama in _context.GamaProductos on producto.IdGamaFk equals gama.Id
                        select new{
                            NombreProducto = producto.Nombre,
                            Descripcion = producto.Descripcion,
                            Imagen = gama.Imagen
                        }).ToListAsync<object>();
            return query;
        }
    }
}