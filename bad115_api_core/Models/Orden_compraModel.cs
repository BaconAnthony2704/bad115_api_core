using System;

namespace bad115_api_core.Models
{
	public class Orden_compraModel
	{
		public bool Activo { get; set; }
		public int Aprobada_por { get; set; }
		public DateTime Fecha_aprobacion { get; set; }
		public DateTime Fecha_entrega { get; set; }
		public DateTime Fecha_orden { get; set; }
		public DateTime Fecha_revision { get; set; }
		public int Id_empresa { get; set; }
		public int Id_orden_compra { get; set; }
		public int Id_proveedor { get; set; }
		public int Idrequisicion { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Numero_orden { get; set; }
		public string Observaciones { get; set; }
		public int Revisado_por { get; set; }
	}

}
