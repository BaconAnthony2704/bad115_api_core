using System;

namespace bad115_api_core.Models
{
	public class Detalle_orden_compraModel
	{
		public bool Activo { get; set; }
		public decimal Cantidad { get; set; }
		public DateTime Fecha_creacion { get; set; }
		public int Id_detalle_compra { get; set; }
		public int Id_divisas { get; set; }
		public int Id_orden_compra { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Numero_detalle { get; set; }
		public decimal Precio_unitario { get; set; }
		public decimal Valor_total { get; set; }
	}
}
