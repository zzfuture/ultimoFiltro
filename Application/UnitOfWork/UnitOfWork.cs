using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repositories;
using Domain.Interfaces;
using Persistence.Data;

namespace Application.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FiltroContext _context;

        public UnitOfWork(FiltroContext context) // 2611
        {
            _context = context;
        }
        private ICliente _Clientes;
        public ICliente Clientes
        {
            get
            {
                if (_Clientes == null)
                {
                    _Clientes = new ClienteRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Clientes;
            }
        }
        private IDetalle _Detalles;
        public IDetalle Detalles
        {
            get
            {
                if (_Detalles == null)
                {
                    _Detalles = new DetalleRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Detalles;
            }
        }
        private IEmpleado _Empleados;
        public IEmpleado Empleados
        {
            get
            {
                if (_Empleados == null)
                {
                    _Empleados = new EmpleadoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Empleados;
            }
        }
        private IGamaProducto _GamaProductos;
        public IGamaProducto GamaProductos
        {
            get
            {
                if (_GamaProductos == null)
                {
                    _GamaProductos = new GamaProductoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _GamaProductos;
            }
        }
        private IOficina _Oficinas;
        public IOficina Oficinas
        {
            get
            {
                if (_Oficinas == null)
                {
                    _Oficinas = new OficinaRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Oficinas;
            }
        }
        private IPago _Pagos;
        public IPago Pagos
        {
            get
            {
                if (_Pagos == null)
                {
                    _Pagos = new PagoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Pagos;
            }
        }
        private IPedido _Pedidos;
        public IPedido Pedidos
        {
            get
            {
                if (_Pedidos == null)
                {
                    _Pedidos = new PedidoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Pedidos;
            }
        }
        private IProducto _Productos;
        public IProducto Productos
        {
            get
            {
                if (_Productos == null)
                {
                    _Productos = new ProductoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _Productos;
            }
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

    }
}