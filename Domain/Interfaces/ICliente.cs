using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICliente : IGenericRepositoryINT<Cliente>
    {
        Task<List<object>> GetClientesPagosRepresentanteCiudad();
        Task<List<Cliente>> GetClientesNoPago();
        Task<List<object>> GetClientesRepresentanteCiudad();
        Task<List<object>> GetClientesNoPagoRepresentanteOficinaTelefono();
    }
}