using AplicaçãoWebCompleta.Models.Request;
using AplicaçãoWebCompleta.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AplicaçãoWebCompleta.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
       private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> CriarUsuarioAsync(UsuarioRequest usuarioRequest)
        {
            await _usuarioService.CriarUsuarioAsync(usuarioRequest);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> AtualizarUsuarioAsync(AtualizarUsuarioRequest atualizarUsuarioRequest)
        {
            await _usuarioService.AtualizarUsuarioAsync(atualizarUsuarioRequest);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("listar")]
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

        [HttpDelete("deletar")]
        public async Task<IActionResult> RemoverUsuarioAsync(int id)
        {
            await _usuarioService.RemoverUsuarioAsync(id);
            return NoContent();
        }
    }
}
