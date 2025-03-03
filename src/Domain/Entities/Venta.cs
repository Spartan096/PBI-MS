using System;

namespace Domain
{
    public class IM252Venta
    {
        private byte someByteColumn;

        public Guid Id { get; set; }  // Primary Key
        public Guid ClienteId { get; set; }  // Foreign Key a IM252Cliente
        public Guid ProductoId { get; set; }  // Foreign Key a IM252Producto
        public DateTime Fecha { get; set; }  // smalldatetime
       // public byte SomeByteColumn { get => someByteColumn; set => someByteColumn = value; }
        // Propiedades de navegación
        public required IM252Cliente Cliente { get; set; }
        public required IM252Producto Producto { get; set; }
    }
}
