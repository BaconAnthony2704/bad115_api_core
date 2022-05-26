using System.Collections.Generic;

namespace bad115_api_core.Models
{
    public class RequisicionUDetalleModel
    {
        public RequisicionModel requisicion { get; set; }
        public List<DetallerequisicionModel> detalleRequisicion { get; set; }
    }
}
