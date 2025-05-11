Feature: Autenticacao de Usuario
    Como usuario da plataforma FCG
    Quero poder realizar login
    Para acessar minha conta e meus jogos adquiridos

Scenario: Login com credenciais validas
    Given um usuario cadastrado com email "teste@fcg.com" e senha "Senha@123"
    When ele tenta realizar login com esses dados
    Then ele recebe um token de acesso valido

Scenario: Login com credenciais invalidas
    Given um usuario cadastrado com email "teste@fcg.com" e senha "Senha@123"
    When ele tenta realizar login com "teste@fcg.com" e senha "Senha@321"
    Then ele recebe um erro de autenticacao