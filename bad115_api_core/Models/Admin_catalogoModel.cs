using System;

namespace bad115_api_core.Models
{
	public class Admin_catalogoModel
	{
		public bool Activo { get; set; }
		public string Codigo { get; set; }
		public string Descripcion { get; set; }
		public int Id_admin_catalogo { get; set; }
		public int Idempleado { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Tipo { get; set; }
	}

}
