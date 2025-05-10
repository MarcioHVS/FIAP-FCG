using FCG.Application.DTOs;
using FCG.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogosController : MainController
    {
        private readonly IJogoService _jogo;

        public JogosController(IJogoService jogo)
        {
            _jogo = jogo;
        }

        [HttpGet("ObterJogo")]
        public async Task<IActionResult> ObterJogo(Guid jogoId)
        {
            var jogo = await _jogo.ObterJogoAsync(jogoId);

            return CustomResponse(jogo);
        }

        [HttpGet("ObterJogos")]
        public async Task<IActionResult> ObterJogos()
        {
            var jogos = await _jogo.ObterJogosAsync();

            return jogos.Count() > 0
                ? CustomResponse(jogos)
                : CustomResponse("Nenhum jogo encontrado", StatusCodes.Status404NotFound);
        }

        [HttpPost("AdicionarJogo")]
        public async Task<IActionResult> AdicionarJogo(JogoAdicionarDto jogo)
        {
            if (!ValidarModelo())
                return CustomResponse();

            await _jogo.AdicionarJogo(jogo);
            
            return CustomResponse("Jogo adicionado com sucesso");
        }

        [HttpPut("AlterarJogo")]
        public async Task<IActionResult> AlterarJogo(JogoAlterarDto jogo)
        {
            if (!ValidarModelo())
                return CustomResponse();

            await _jogo.AlterarJogo(jogo);

            return CustomResponse("Jogo alterado com sucesso");
        }

        [HttpPut("AtivarJogo")]
        public async Task<IActionResult> AtivarJogo(Guid jogoId)
        {
            await _jogo.AtivarJogo(jogoId);

            return CustomResponse("Jogo ativado com sucesso");
        }

        [HttpPut("DesativarJogo")]
        public async Task<IActionResult> DesativarJogo(Guid jogoId)
        {
            await _jogo.DesativarJogo(jogoId);

            return CustomResponse("Jogo desativado com sucesso");
        }
    }
}
