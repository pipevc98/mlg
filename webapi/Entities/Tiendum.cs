using System;
using System.Collections.Generic;

namespace webapi.Entities
{
    public partial class Tiendum
    {
        public string? Sucursal { get; set; }
        public string? Direccion { get; set; }
        public long Id { get; set; }

        public ICollection<Articulo> Articulo { get; set; }
    }
}
