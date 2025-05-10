using FCG.Application.DTOs;
using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [Authorize(Roles = "Usuario,Administrador")]
        [HttpGet("ObterUsuario")]
        public async Task<IActionResult> ObterUsuario(Guid usuarioId)
        {
            if (!ValidarPermissao(usuarioId))
                return CustomResponse();

            var usuario = await _usuario.ObterUsuarioAsync(usuarioId);

            return CustomResponse(usuario);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("ObterUsuarios")]
        public async Task<IActionResult> ObterUsuarios()
        {
            var usuarios = await _usuario.ObterUsuariosAsync();

            return usuarios.Count() > 0
                ? CustomResponse(usuarios)
                : CustomResponse("Nenhum usu�rio encontrado", StatusCodes.Status404NotFound);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("AdicionarUsuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] UsuarioAdicionarDto usuario)
        {
            if (!ValidarModelo())
                return CustomResponse();

            await _usuario.AdicionarUsuario(usuario);

            return CustomResponse("Usu�rio adicionado com sucesso");
        }

        [Authorize(Roles = "Usuario,Administrador")]
        [HttpPut("AlterarUsuario")]
        public async Task<IActionResult> AlterarUsuario(UsuarioAlterarDto usuario)
        {
            if (!ValidarModelo())
                return CustomResponse();

            if (!ValidarPermissao(usuario.Id))
                return CustomResponse();

            await _usuario.AlterarUsuario(usuario);

            return CustomResponse("Usuario alterado com sucesso");
        }

        [Authorize(Roles = "Usuario,Administrador")]
        [HttpPut("AlterarSenha")]
        public async Task<IActionResult> AlterarSenha(string novaSenha)
        {
            if (!ValidarModelo())
                return CustomResponse();

            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _usuario.AlterarSenha(Guid.Parse(usuarioId), novaSenha);

            return CustomResponse("Senha alterada com sucesso");
        }

        [Authorize(Roles = "Usuario,Administrador")]
        [HttpPut("AtivarUsuario")]
        public async Task<IActionResult> AtivarUsuario(Guid usuarioId)
        {
            if (!ValidarPermissao(usuarioId))
                return CustomResponse();

            await _usuario.AtivarUsuario(usuarioId);

            return CustomResponse("Usuario ativado com sucesso");
        }

        [Authorize(Roles = "Usuario,Administrador")]
        [HttpPut("DesativarUsuario")]
        public async Task<IActionResult> DesativarUsuario(Guid usuarioId)
        {
            if (!ValidarPermissao(usuarioId))
                return CustomResponse();

            await _usuario.DesativarUsuario(usuarioId);

            return CustomResponse("Usuario desativado com sucesso");
        }
    }
}
