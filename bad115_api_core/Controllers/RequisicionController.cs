using bad115_api_core.Models;
using bad115_api_core.Models.DTO;
using bad115_api_core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bad115_api_core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RequisicionController : ControllerBase
	{
		private readonly RepositoryRequisicionValue _repo;
		public RequisicionController(RepositoryRequisicionValue repository)
		{
			_repo = repository;
		}
		[HttpPost("[action]")]
		public async Task<List<RequisicionModel>> Obtener([FromBody] IdentificadorModel model = default)
		{
			return await _repo.Obtener(model);
		}
		[HttpPost("[action]")]
		public async Task<List<RequisicionModel>> Guardar([FromBody] RequisicionModel model)
		{
			return await _repo.Guardar(model);
		}


		[HttpPost("[action]")]
		public async Task GuardarRequesicionUDetalle([FromBody] RequisicionUDetalleModel model)
		{
			 await _repo.GuardarRequisicionUDetalle(model);
		}

		[HttpPost("[action]")]
		public async Task<List<DTORequisicionProducto>> ObtenerRequisicionProducto([FromBody] IdentificadorModel model = default)
		{
			return await _repo.ObtenerRequisicionProducto(model);
		}






	}
}
