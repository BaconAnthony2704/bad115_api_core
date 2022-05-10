using System;

namespace bad115_api_core.Models
{
	public class SucursalModel
	{
		public bool Activo { get; set; }
		public string Direccion { get; set; }
		public int Id_pais { get; set; }
		public int Id_sucursal { get; set; }
		public string Latitud { get; set; }
		public string Longitud { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Nombre { get; set; }
	}

}
