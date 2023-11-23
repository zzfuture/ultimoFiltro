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
    public class PedidoRepository : GenericRepositoryINT<Pedido>, IPedido
    {
        private readonly FiltroContext _context;

        public PedidoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }

        public async Task<object> GetPedidosEstado()
        {
            var query = await (from pedido in _context.Pedidos
                                select pedido).ToListAsync<Pedido>();
            int entregado = 0;
            int pendiente = 0;
            int rechazado = 0;
            foreach(Pedido p in query)
            {
                if (p.Estado == "Entregado")
                {
                    entregado += 1;
                }
                else if (p.Estado == "Pendiente")
                {
                    pendiente += 1;
                }
                else {
                    rechazado += 1;
                }
            }
            var o = new {
                Entregado = entregado,
                Pendiente = pendiente,
                Rechazado = rechazado
            };
            return o;
        }
    }
}