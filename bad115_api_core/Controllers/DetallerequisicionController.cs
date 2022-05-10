using bad115_api_core.Models;
using bad115_api_core.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace bad115_api_core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DetallerequisicionController : ControllerBase
	{
		private readonly RepositoryDetallerequisicionValue _repo;
		public DetallerequisicionController(RepositoryDetallerequisicionValue repository)
		{
			_repo = repository;
		}
		[HttpPost("[action]")]
		public async Task<List<DetallerequisicionModel>> Obtener([FromBody] IdentificadorModel model = default)
		{
			return await _repo.Obtener(model);
		}
		[HttpPost("[action]")]
		public async Task Guardar([FromBody] DetallerequisicionModel model)
		{
			await _repo.Guardar(model);
		}

	}
}
