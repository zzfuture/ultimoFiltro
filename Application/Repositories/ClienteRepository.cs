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
    public class ClienteRepository : GenericRepositoryINT<Cliente>, ICliente
    {
        private readonly FiltroContext _context;

        public ClienteRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<object>> GetClientesPagosRepresentanteCiudad()
        {
            var query = await (from pago in _context.Pagos 
                        join cliente in _context.Clientes on pago.IdClienteFk equals cliente.Id
                        join representante in _context.Empleados on cliente.IdEmpleadoRepresentanteVentasFk equals representante.Id
                        join oficina in _context.Oficinas on representante.IdOficinaFk equals oficina.Id
                        select new {
                            NombreCliente = cliente.NombreCliente,
                            NombreRepresentante = representante.Nombre,
                            CiudadOficina = oficina.Ciudad
                        }).ToListAsync<object>();
            return query;
        }
        public async Task<List<Cliente>> GetClientesNoPago()
        {
            var query = await (from cliente in _context.Clientes
                        where! _context.Pagos.Any(x => x.IdClienteFk == cliente.Id)
                        select cliente
                        ).ToListAsync<Cliente>();
            return query;
        }

        public async Task<List<object>> GetClientesRepresentanteCiudad()
        {
            var query = await (from cliente in _context.Clientes 
                        join representante in _context.Empleados on cliente.IdEmpleadoRepresentanteVentasFk equals representante.Id
                        join oficina in _context.Oficinas on representante.IdOficinaFk equals oficina.Id
                        select new {
                            NombreCliente = cliente.NombreCliente,
                            NombreRepresentante = representante.Nombre,
                            ApellidoRepresentante = representante.Apellido1,
                            CiudadOficina = oficina.Ciudad
                        }).ToListAsync<object>();
            return query;
        }
        public async Task<List<object>> GetClientesNoPagoRepresentanteOficinaTelefono()
        {
            var query = await (from cliente in _context.Clientes
                        join representante in _context.Empleados on cliente.IdEmpleadoRepresentanteVentasFk equals representante.Id
                        join oficina in _context.Oficinas on representante.IdOficinaFk equals oficina.Id
                        where! _context.Pagos.Any(x => x.IdClienteFk == cliente.Id)
                        select new {
                            NombreCliente = cliente.NombreCliente,
                            NombreRepresentante = representante.Nombre,
                            ApellidoRepresentante = representante.Apellido1,
                            TelefonoRepresentante = oficina.Telefono
                        }
                        ).ToListAsync<object>();
            return query;
        }
    }
}
