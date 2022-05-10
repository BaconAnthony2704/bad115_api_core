using System;

namespace bad115_api_core.Models
{
	public class PaisModel
	{
		public bool Activo { get; set; }
		public string Codigo { get; set; }
		public int Id_pais { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Nombre { get; set; }
	}

}
