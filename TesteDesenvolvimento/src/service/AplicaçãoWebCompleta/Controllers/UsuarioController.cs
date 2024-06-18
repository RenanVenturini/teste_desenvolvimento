using AplicaçãoWebCompleta.Models.Request;
using AplicaçãoWebCompleta.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AplicaçãoWebCompleta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMemoryCache _memoryCache;
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

        public UsuarioController(IUsuarioService usuarioService, IMemoryCache memoryCache)
        {
            _usuarioService = usuarioService;
            _memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuarioAsync(UsuarioRequest usuarioRequest)
        {
            await _usuarioService.CriarUsuarioAsync(usuarioRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarUsuarioAsync(AtualizarUsuarioRequest atualizarUsuarioRequest)
        {
            await _usuarioService.AtualizarUsuarioAsync(atualizarUsuarioRequest);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarioAsync()
        {
            var usuarios = await _usuarioService.ListarUsuarioAsync();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarUsuarioPorIdAsync(int id)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverUsuarioAsync(int id)
        {
            await _usuarioService.RemoverUsuarioAsync(id);
            return NoContent();
        }

        [HttpGet("consulta-cep/{cep}")]
        public async Task<IActionResult> ConsultarCepAsync(string cep)
        {
            if (!_memoryCache.TryGetValue(cep, out var endereco))
            {
                endereco = await _usuarioService.ConsultarCepAsync(cep);

                _memoryCache.Set(cep, endereco, CacheDuration);
            }

            return Ok(endereco);
        }
    }
}
