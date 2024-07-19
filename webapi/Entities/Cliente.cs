using System;
using System.Collections.Generic;

namespace webapi.Entities
{
    public partial class Cliente
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Direccion { get; set; }
        public long Id { get; set; }

        public string? User { get; set; }
        
        public string? Password { get; set; }

        public ICollection<Articulo> Articulo { get; set; } 
    }
}
