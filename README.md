# PBL Trabalho Interdisciplinar - Faculdade Engenheiro Salvador Arena
## LEIA ANTES DE TENTAR MEXER
- Para executar corretamente primeiro crie o banco com o script 'ScriptCriaBancoPBL' pré montado em seu SQL Server
- Faça a correta manutenção da connectionString dentro do arquivo appSettings.json
- Verifique se todas as bibliotecas em python estão instaladas em seu ambiente
- Após verificar os passos anteriores execute o programa principal no Visual Studio
  - Atenção: O Programa não funcionará corretamente se a Api não estiver rodando e estiver conectada corretamente ao Banco!!!!
 ### Este trabalho utiliza uma aplicação ASPNET Core 6.0, Arquitetura MVC, EntityFramework C#, Python, SQLServer, JavaScript, HTML/CSS
 ## Informações Gerais: 
- Objetivo do programa é calcular as possibilidades de um projétil acertar um meteoro caindo em velocidade constante, segundo teorias da cinemática
- Os cálculos trigonométricos do Programa são feitos utilizando os conceitos de Série de Taylor
- Esse programa possui uma Api e é moldado com 3 clasess: Projetil, Meteoro e Colisão
- A Api possui apenas os Métodos POST e GET específicos de acordo com as Models
- Toda a comunicação entre os arquivos é feito por meio da API em C#
- Todos os cálculos e devolutivas são comandados pela API
- A Geração das Animações foram feitas em python, utilizando MatplotLibAnimation
- Ao executar o script 'main', este se comunicará com a API e lerá o banco de Dados gerando todas as animações da tabela Colisão 
