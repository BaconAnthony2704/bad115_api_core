using System;

namespace bad115_api_core.Models.DTO
{
    public class DTOrdenCompraModel:Orden_compraModel
    {
        public string Nombre_proveedor { get; set; }
        public string NRC { get; set; }
        public string Nombre_empresa { get; set; }
        public DateTime FechaLimite { get; set; }
    }
}
