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
		public async Task Guardar([FromBody] RequisicionModel model)
		{
			await _repo.Guardar(model);
		}

	}
}
