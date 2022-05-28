using System;

namespace bad115_api_core.Models.DTO
{
    public class DTORequisicionProducto
    {
        public int Idrequisicion { get; set; }
        public string UsuarioEncargado { get; set; }
        public DateTime FechaIngresada { get; set; }
        public bool Estado_requisicion { get; set; }
        public string Nombre_sucursal { get; set; }
        public string Pais { get; set; }
        public int Id_producto { get; set; }
        public string Descripcion_producto { get; set; }
        public string Nombre_producto { get; set; }
        public double Precio_minimo { get; set; }

        public DateTime Fecha_vencimiento { get; set; }
        public bool Es_exportado { get; set; }

        public int Modificado_por { get; set; }

        public DateTime Modificador_en { get; set; }

        public bool Activo { get; set; }

        public int Cantidad { get; set; }



    }
}
