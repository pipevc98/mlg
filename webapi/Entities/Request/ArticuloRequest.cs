namespace webapi.Entities.Request
{
    public class ArticuloRequest
    {
        public long id { get; set; }
        public string? codigo { get; set; }
        public string? imagen { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public string? descripcion { get; set; }
        public string? nombre { get; set; }

        public string? clienteId { get; set; }

    }
}
