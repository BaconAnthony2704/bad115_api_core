using System;

namespace bad115_api_core.Models
{
	public class ProveedorModel
	{
		public bool Activo { get; set; }
		public string Contacto_ventas { get; set; }
		public string Correo { get; set; }
		public string Direccion { get; set; }
		public int Id_proveedor { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Nombre { get; set; }
		public string Nrc { get; set; }
		public string Telefono { get; set; }
	}

}
