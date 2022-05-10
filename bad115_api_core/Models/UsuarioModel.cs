using System;

namespace bad115_api_core.Models
{
	public class UsuarioModel
	{
		public bool Activo { get; set; }
		public string Email { get; set; }
		public int Id_usuario { get; set; }
		public DateTime Modificado_en { get; set; }
		public int Modificado_por { get; set; }
		public string Nombre { get; set; }
		public string Password { get; set; }
		public string Usuario { get; set; }
	}

}
