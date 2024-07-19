namespace webapi.Entities.Request
{
    public class ClienteRequest
    {
        public long Id { get; set; }  
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Direccion { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public ICollection<Articulo>? Articulo { get; set; }

    }
}
