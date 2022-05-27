namespace bad115_api_core.Models
{
	public class DetallerequisicionModel
	{
        public int Iddetallerequisicion { get; set; }
        public decimal Cantidad { get; set; }
		public bool Estado { get; set; }
		public int Id_producto { get; set; }
		public int Idrequisicion { get; set; }
	}

}
