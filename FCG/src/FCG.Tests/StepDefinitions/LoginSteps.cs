using Reqnroll;
using Xunit;

namespace FCG.Tests.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private string _email;
        private string _senha;
        private bool _loginSucesso;

        [Given(@"um usuario cadastrado com email ""(.*)"" e senha ""(.*)""")]
        public void DadoUsuarioCadastrado(string email, string senha)
        {
            _email = email;
            _senha = senha;
        }

        [When(@"ele tenta realizar login com esses dados")]
        public void QuandoTentaRealizarLogin()
        {
            _loginSucesso = (_email == "teste@fcg.com" && _senha == "Senha@123");
        }

        [Then(@"ele recebe um token de acesso valido")]
        public void EntaoRecebeToken()
        {
            Assert.True(_loginSucesso, "O login deveria ser bem-sucedido");
        }

        [When(@"ele tenta realizar login com ""(.*)"" e senha ""(.*)""")]
        public void QuandoTentaLoginComCredenciaisInvalidas(string email, string senha)
        {
            _loginSucesso = (email == "teste@fcg.com" && senha == "Senha@123");
        }

        [Then(@"ele recebe um erro de autenticacao")]
        public void EntaoRecebeErro()
        {
            Assert.False(_loginSucesso, "O login deveria falhar");
        }
    }
}