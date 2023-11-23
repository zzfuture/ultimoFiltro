# ULTIMO FILTRO

Este proyecto es un WebApi basado en el modelo 4 capas (no implementa Autenticacion).

## ENDPOINTS

### http://localhost:5099/api/Pago/pagos2008

REPOSITORY

```csharp
public async Task<List<Pago>> GetPagos2008()
        {
            var query = await (from pago in _context.Pagos
                        where pago.FechaPago.Year == 2008
                        where pago.FormaPago == "Paypal"
                        orderby pago.Total
                        select pago).Reverse().ToListAsync();
            return query;
        }
```

CONTROLLER

```csharp
public async Task<ActionResult<IEnumerable<PagoDto>>> GetPagos2008()
        {
            var result = await _unitOfWork.Pagos.GetPagos2008();
            return _mapper.Map<List<PagoDto>>(result);
        }
```

Devuelve un listado con todos los pagos que se realizaron en el año 2008 mediante Paypal. De mayor a menor



## http://localhost:5099/api/Pago/formasPago

REPOSITORY

```csharp
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
```

CONTROLLER

```csharp
public async Task<ActionResult<IEnumerable<string>>> GetFormasPago()
        {
            var result = await _unitOfWork.Pagos.GetFormasPago();
            return result;
        }
```

Devuelve un listado con todas las formas de pago que aparecen en la tabla pago.







## http://localhost:5099/api/Cliente/clientesPagosRepresentanteCiudad

REPOSITORY

```csharp
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
```

CONTROLLER

```csharp
public async Task<ActionResult<IEnumerable<object>>> GetClientesPagosRepresentanteCiudad()
        {
            var results = await _unitOfWork.Clientes.GetClientesPagosRepresentanteCiudad();
            return results;
        }
```

Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus representantes junto conla ciudad de la oficina a la que pertenece el representante



## http://localhost:5099/api/Empleado/empleadoJefeJefe

REPOSITORY

```csharp
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
```

CONTROLLER

```csharp
public async Task<ActionResult<IEnumerable<object>>> GetEmpleadoJefeJefe()
        {
            var results = await _unitOfWork.Empleados.GetEmpleadoJefeJefe();
            return results;
        }
```

Devuelve el nombre de los empleados, de su jefe y jefe de jefe





## http://localhost:5099/api/Producto/productosSinPedido

REPOSITORY

```csharp
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
```

CONTROLLER

```csharp
public async Task<ActionResult<IEnumerable<object>>> GetProductosSinPedido()
        {
            var results = await _unitOfWork.Productos.GetProductosSinPedido();
            return results;
        }
```

Devuelve un listado de los productos que nunca han aparecido en un pedido. El resultado debe mostrar el nombre, la descripcion y la imagen del producto.





## http://localhost:5099/api/Pedido/PedidosEstados

REPOSITORY

```csharp
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
```

CONTROLLER

```csharp
[HttpGet("PedidosEstados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<object>> GetPedidosEstado()
        {
            var results = await _unitOfWork.Pedidos.GetPedidosEstado();
            return results;
        }
```

Devuelve cuantos pedidos hay en cada estado





## http://localhost:5099/api/Cliente/noPago

REPOSITORY

```csharp
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
```

CONTROLLER

```csharp
[HttpGet("PedidosEstados")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<object>> GetPedidosEstado()
        {
            var results = await _unitOfWork.Pedidos.GetPedidosEstado();
            return results;
        }
```

Devuelve cuantos pedidos hay en cada estado



http://localhost:5099/api/Cliente/clientesRepresentanteCiudad

http://localhost:5099/api/Cliente/clientesNoPagoRepresentanteOficinaTelefono










