using System;

namespace bad115_api_core.Models
{
	public class RequisicionModel
	{
		public int? Estado { get; set; }
		public DateTime? Fechaingresada { get; set; }
		public DateTime Fechalimite { get; set; }
		public int Id_sucursal { get; set; }
		public int Id_usuario { get; set; }
		public int Idrequisicion { get; set; }
		public string Usuarioencargado { get; set; }
	}

}
