using System;

namespace bad115_api_core.Models
{
	public class DivisasModel
	{
		public bool Activo { get; set; }
		public decimal Factor_moneda { get; set; }
		public int Id_divisas { get; set; }
		public int Id_pais { get; set; }
		public int Modificado_por { get; set; }
		public DateTime Modificador_en { get; set; }
		public string Nombre { get; set; }
		public decimal Valor { get; set; }
	}

}
