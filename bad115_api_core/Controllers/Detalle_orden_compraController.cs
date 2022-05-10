using bad115_api_core.Models;
using bad115_api_core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bad115_api_core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Detalle_orden_compraController : ControllerBase
	{
		private readonly RepositoryDetalle_orden_compraValue _repo;
		public Detalle_orden_compraController(RepositoryDetalle_orden_compraValue repository)
		{
			_repo = repository;
		}
		[HttpPost("[action]")]
		public async Task<List<Detalle_orden_compraModel>> Obtener([FromBody] IdentificadorModel model = default)
		{
			return await _repo.Obtener(model);
		}
		[HttpPost("[action]")]
		public async Task Guardar([FromBody] Detalle_orden_compraModel model)
		{
			await _repo.Guardar(model);
		}

	}
}
