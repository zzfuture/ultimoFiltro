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
    public class PagoRepository : GenericRepositorySTRING<Pago>, IPago
    {
        private readonly FiltroContext _context;

        public PagoRepository(FiltroContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<Pago>> GetPagos2008()
        {
            var query = await (from pago in _context.Pagos
                        where pago.FechaPago.Year == 2008
                        where pago.FormaPago == "Paypal"
                        orderby pago.Total
                        select pago).Reverse().ToListAsync();
            return query;
        }

        public async Task<List<String>> GetFormasPago()
        {
            var query = await(
                        from pago in _context.Pagos
                        select pago.FormaPago
                        ).ToListAsync();
            var queryFixed = new List<string>();
            foreach (var x in query)
            {
                if (queryFixed.Contains(x)){
                    continue;
                }
                else{
                    queryFixed.Add(x);
                }
            }
            return queryFixed;  // NO ME ACUERDO COMO SE HACIA EL DISTINCT
        }

    }
}