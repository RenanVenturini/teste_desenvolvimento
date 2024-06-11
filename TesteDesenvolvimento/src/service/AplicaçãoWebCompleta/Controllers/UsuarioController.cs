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
        private readonly IConsultaCep _consultaCep;

        public UsuarioController(IUsuarioService usuarioService, IConsultaCep consultaCep)
        {
            _usuarioService = usuarioService;
            _consultaCep = consultaCep;
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

        [HttpGet("consulta-cep/{cep}")]
        public async Task<IActionResult> ConsultarCepAsync(string cep)
        {
            var endereco = await _usuarioService.ConsultarCepAsync(cep);
            return Ok(endereco);
        }
    }
}
