using System;

namespace bad115_api_core.Models
{
	public class ProductoModel
	{
		public bool Activo { get; set; }
		public bool Es_exportado { get; set; }
		public DateTime Fecha_vencimiento { get; set; }
		public int Id_admin_catalogo { get; set; }
		public int Id_producto { get; set; }
		public int Modificado_por { get; set; }
		public DateTime Modificador_en { get; set; }
		public string Nombre { get; set; }
		public decimal Precio_minimo { get; set; }
	}

}
