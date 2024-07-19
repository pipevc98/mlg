using System;
using System.Collections.Generic;

namespace webapi.Entities
{
    public partial class Articulo
    {
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Precio { get; set; }
        public string? Imagen { get; set; }
        public int? Stock { get; set; }
        public long Id { get; set; }

        public string? Nombre { get; set; }

        public long? ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
        public Tiendum? Tiendum { get; set; }
    }
}
