using System;

namespace Domain
{
    public class IM252Producto
    {
        public Guid Id { get; set; }
        public string? Descripcion { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }
        public string? Foto { get; set; }
        
    }
}