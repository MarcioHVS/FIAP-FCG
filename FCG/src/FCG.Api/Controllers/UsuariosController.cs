using FCG.Application.DTOs;
using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : MainController
    {
        private readonly IUsuarioService _usuario;

        public UsuariosController(IUsuarioService usuario)
        {
            _usuario = usuario;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var token = await _usuario.LoginAsync(login);

            return CustomResponse(token);
        }

        [HttpGet("ObterUsuario")]
        public async Task<IActionResult> ObterUsuario(Guid usuarioId)
        {
            var usuario = await _usuario.ObterUsuarioAsync(usuarioId);

            return CustomResponse(usuario);
        }

        [HttpGet("ObterUsuarios")]
        public async Task<IActionResult> ObterUsuarios()
        {
            var usuarios = await _usuario.ObterUsuariosAsync();

            return usuarios.Count() > 0
                ? CustomResponse(usuarios)
                : CustomResponse("Nenhum usu�rio encontrado", StatusCodes.Status404NotFound);
        }

        [HttpPost("AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] UsuarioAdicionarDto usuario)
        {
            if (!ValidarModelo())
                return CustomResponse();

            await _usuario.AdicionarUsuario(usuario);
            
            return CustomResponse("Usu�rio adicionado com sucesso");
        }

        [HttpPut("AlterarUsuario")]
        public async Task<IActionResult> AlterarUsuario(UsuarioAlterarDto usuario)
        {
            if (!ValidarModelo())
                return CustomResponse();

            await _usuario.AlterarUsuario(usuario);

            return CustomResponse("Usuario alterado com sucesso");
        }

        [HttpPut("AtivarUsuario")]
        public async Task<IActionResult> AtivarUsuario(Guid usuarioId)
        {
            await _usuario.AtivarUsuario(usuarioId);

            return CustomResponse("Usuario ativado com sucesso");
        }

        [HttpPut("DesativarUsuario")]
        public async Task<IActionResult> DesativarUsuario(Guid usuarioId)
        {
            await _usuario.DesativarUsuario(usuarioId);

            return CustomResponse("Usuario desativado com sucesso");
        }
    }
}
