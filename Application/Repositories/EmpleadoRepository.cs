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
    public class EmpleadoRepository : GenericRepositoryINT<Empleado>, IEmpleado
    {
        private readonly FiltroContext _context;

        public EmpleadoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<object>> GetEmpleadoJefeJefe()
        {
            var query = await (from empleado in _context.Empleados
                        join jefe in _context.Empleados on empleado.IdEmpleadoJefeFk equals jefe.Id
                        join jefejefe in _context.Empleados on jefe.IdEmpleadoJefeFk equals jefejefe.Id
                        select new {
                            NombreEmpleado = empleado.Nombre,
                            NombreJefe = jefe.Nombre,
                            NombreJefeJefe = jefejefe.Nombre
                        }).ToListAsync<object>();
            return query;
        }
    }
}