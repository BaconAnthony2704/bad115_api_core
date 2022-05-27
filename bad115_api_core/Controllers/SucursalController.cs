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
	public class SucursalController : ControllerBase
	{
		private readonly RepositorySucursalValue _repo;
		public SucursalController(RepositorySucursalValue repository)
		{
			_repo = repository;
		}
		[HttpPost("[action]")]

		//obtener para mostrar los datos
		public async Task<List<SucursalModel>> Obtener([FromBody] IdentificadorModel model = default)
		{
			return await _repo.Obtener(model);
		}
		[HttpPost("[action]")]
		//guardar los datos
		public async Task Guardar([FromBody] SucursalModel model)
		{
			await _repo.Guardar(model);
		}

	}
}
