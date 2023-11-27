// Use as funções conforme necessário
async function EnviaDados(formId, event) {
    event.preventDefault();
    let projetil = null;
    let meteoro = null;

    try {
        // Obtenha os valores dos campos de entrada usando o ID do formulário
        const angulo = parseFloat(document.getElementById(`${formId}`).querySelector('#anguloInput').value);
        const AlturaMeteoro = parseFloat(document.getElementById(`${formId}`).querySelector('#alturaInput').value);
        const DistanciaHorizontal = parseFloat(document.getElementById(`${formId}`).querySelector('#distanciaInput').value);
        const VelocidadeMeteoro = parseFloat(document.getElementById(`${formId}`).querySelector('#velocidadeInput').value);

        // Verifique se a conversão foi bem-sucedida antes de prosseguir
        if (isNaN(angulo) || isNaN(AlturaMeteoro) || isNaN(DistanciaHorizontal) || isNaN(VelocidadeMeteoro)) {
            console.error("Valores inválidos. Certifique-se de inserir números válidos nos campos.");
            return false; // Retorna false para evitar a submissão padrão do formulário
        }


        // Reorganize o código para incluir a lógica do projetil e meteoro dentro do mesmo bloco try
        try {
            // Realize a lógica desejada com os valores obtidos
            console.log(`Valores obtidos - Angulo: ${angulo}, Altura: ${AlturaMeteoro}, Distancia: ${DistanciaHorizontal}, Velocidade: ${VelocidadeMeteoro}`);

            // Realiza uma requisição GET para obter os projéteis da API
            while (true) {
                const Projeteis = await getProjetil()

                // Verifica se há um projétil com o ângulo correspondente
                projetil = Projeteis.find(proj => proj.anguloGraus === angulo);

                if (projetil) {
                    // Se encontrou um projétil com o mesmo ângulo, exibe no console
                    console.log("Projétil encontrado:", projetil);
                    break;
                } else {
                    // Caso não tenha encontrado, faz uma requisição POST com o ângulo recebido
                    const novoProj = { anguloGraus: angulo, /* outras propriedades necessárias */ };
                    await postProjetil(novoProj);
                    console.log("Novo projétil adicionado com ângulo:", angulo);
                }
            }

            // Realiza uma requisição GET para obter os meteoros da API
            while (true) {
                const Meteoros = await getMeteoro();

                // Verifica se há um meteoro com os mesmos parâmetros
                meteoro = Meteoros.find(met =>
                    met.alturaInicial === AlturaMeteoro &&
                    met.velocidadeMeteoro === VelocidadeMeteoro &&
                    met.distanciaHorizontal === DistanciaHorizontal
                );

                if (meteoro) {
                    // Se encontrou um meteoro com os mesmos parâmetros, exibe no console
                    console.log("Meteoro encontrado:", meteoro);
                    break;
                } else {
                    // Caso não tenha encontrado, faz uma requisição POST com os parâmetros recebidos
                    const novoMeteoro = {
                        AlturaInicial: AlturaMeteoro,
                        VelocidadeMeteoro: VelocidadeMeteoro,
                        distanciaHorizontal: DistanciaHorizontal
                    };

                    await postMeteoro(novoMeteoro);
                    console.log("Novo meteoro adicionado com parâmetros:", novoMeteoro);
                }
            }
        } catch (erro) {
            console.error("Erro ao realizar lógica do projetil e meteoro:", erro);
        }

        // Realiza a lógica de colisão após as requisições GET
        try {
            const CreateColisao = { projetilId: projetil.projetilId, meteoroId: meteoro.meteoroId }
            const resultado = await postColisao(CreateColisao);
            debugger
            if (resultado.colisoes[0].voParaColidir === null) alert(resultado.mensagem)
            else {
                if (resultado.colisoes.length > 1) { return window.location.href = '/Home/Grafico?resultado1=' + encodeURIComponent(resultado.colisoes[0].colisaoId) + '&resultado2=' + encodeURIComponent(resultado.colisoes[1].colisaoId); }
                else return window.location.href = '/Home/Grafico?resultado1=' + encodeURIComponent(resultado.colisoes[0].colisaoId);
            }

        } catch (erro) {
            console.error("Erro ao enviar dados:", erro);
        }
    } catch (erro) {
        console.error("Erro ao enviar dados:", erro);
    }

    // Retorna false para garantir que a submissão padrão do formulário não ocorra
    return false;
}
// arquivo Requisicoes.js

async function fazerRequisicao(tipo, endpoint, dados) {
    try {
        const response = await $.ajax({
            url: '/' + endpoint,
            type: tipo,
            contentType: 'application/json',
            data: tipo === 'GET' ? null : JSON.stringify(dados),
        });

        console.log(`Dados da API (${tipo}):`, response);
        return response;
        // Adicione aqui o código para manipular a resposta da API
    } catch (erro) {
        console.error(`Erro na requisição ${tipo} para a API: `, erro);
    }
}

// Exemplos de uso
async function getProjetil() {
    return await fazerRequisicao('GET', 'Projetil', null);
    // Código a ser executado após a requisição GET
}

async function postProjetil(dados) {
    return await fazerRequisicao('POST', 'Projetil', dados);
    // Código a ser executado após a requisição POST
}

async function getMeteoro() {
    return await fazerRequisicao('GET', 'Meteoro', null);
    // Código a ser executado após a requisição GET
}

async function postMeteoro(dados) {
    return await fazerRequisicao('POST', 'Meteoro', dados);
    // Código a ser executado após a requisição POST
}
async function postColisao(dados) {
    return await fazerRequisicao('POST', 'Colisao', dados);
    // Código a ser executado após a requisição POST
}

