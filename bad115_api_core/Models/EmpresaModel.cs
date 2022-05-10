using System;

namespace bad115_api_core.Models
{
	public class EmpresaModel
	{
		public bool Activo { get; set; }
		public string Direccion_fiscal { get; set; }
		public string Email { get; set; }
		public int Id_empresa { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Nit { get; set; }
		public string Nombre { get; set; }
		public string Telefono { get; set; }
	}

}
