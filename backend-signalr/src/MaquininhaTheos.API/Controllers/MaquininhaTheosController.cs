using MaquininhaTheos.Domain.Entidades;
using MaquininhaTheos.Domain.Interfaces.Repository;
using MaquininhaTheos.Domain.TheosHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaquininhaTheos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaquininhaTheosController : ControllerBase
    {
        private readonly IHubContext<TheosMaquininhaHub> _hub;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public MaquininhaTheosController(
            IUsuarioRepositorio usuarioRepositorio,
            IHubContext<TheosMaquininhaHub> hub
            )
        {
            _hub = hub;
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // var usuarios = await _usuarioRepositorio.ListarAsync();
            var lista = new List<string>();
            lista.Add("Nome 1");
            lista.Add("Nome 2");
            lista.Add("Nome 3");
            await _hub.Clients.All.SendAsync("transferdata", lista);
            return Ok(new { Message = "Request Completed" });
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario usuario)
        {
            await _usuarioRepositorio.AdicionarAsync(usuario);
            await _hub.Clients.All.SendAsync("postdata", usuario);
            return Ok(new { Message = "Request Completed" });
        }

    }
}
