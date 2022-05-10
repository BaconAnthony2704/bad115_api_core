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
	public class DivisasController : ControllerBase
	{
		private readonly RepositoryDivisasValue _repo;
		public DivisasController(RepositoryDivisasValue repository)
		{
			_repo = repository;
		}
		[HttpPost("[action]")]
		public async Task<List<DivisasModel>> Obtener([FromBody] IdentificadorModel model = default)
		{
			return await _repo.Obtener(model);
		}
		[HttpPost("[action]")]
		public async Task Guardar([FromBody] DivisasModel model)
		{
			await _repo.Guardar(model);
		}

	}
}
