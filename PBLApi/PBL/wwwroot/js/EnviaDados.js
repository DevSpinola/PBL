// Use as fun��es conforme necess�rio
async function EnviaDados(formId, event) {
    event.preventDefault();
    let projetil = null;
    let meteoro = null;

    try {
        // Obtenha os valores dos campos de entrada usando o ID do formul�rio
        const angulo = parseFloat(document.getElementById(`${formId}`).querySelector('#anguloInput').value);
        const AlturaMeteoro = parseFloat(document.getElementById(`${formId}`).querySelector('#alturaInput').value);
        const DistanciaHorizontal = parseFloat(document.getElementById(`${formId}`).querySelector('#distanciaInput').value);
        const VelocidadeMeteoro = parseFloat(document.getElementById(`${formId}`).querySelector('#velocidadeInput').value);

        // Verifique se a convers�o foi bem-sucedida antes de prosseguir
        if (isNaN(angulo) || isNaN(AlturaMeteoro) || isNaN(DistanciaHorizontal) || isNaN(VelocidadeMeteoro)) {
            console.error("Valores inv�lidos. Certifique-se de inserir n�meros v�lidos nos campos.");
            return false; // Retorna false para evitar a submiss�o padr�o do formul�rio
        }


        // Reorganize o c�digo para incluir a l�gica do projetil e meteoro dentro do mesmo bloco try
        try {
            // Realize a l�gica desejada com os valores obtidos
            console.log(`Valores obtidos - Angulo: ${angulo}, Altura: ${AlturaMeteoro}, Distancia: ${DistanciaHorizontal}, Velocidade: ${VelocidadeMeteoro}`);

            // Realiza uma requisi��o GET para obter os proj�teis da API
            while (true) {
                const Projeteis = await getProjetil()

                // Verifica se h� um proj�til com o �ngulo correspondente
                projetil = Projeteis.find(proj => proj.anguloGraus === angulo);

                if (projetil) {
                    // Se encontrou um proj�til com o mesmo �ngulo, exibe no console
                    console.log("Proj�til encontrado:", projetil);
                    break;
                } else {
                    // Caso n�o tenha encontrado, faz uma requisi��o POST com o �ngulo recebido
                    const novoProj = { anguloGraus: angulo, /* outras propriedades necess�rias */ };
                    await postProjetil(novoProj);
                    console.log("Novo proj�til adicionado com �ngulo:", angulo);
                }
            }

            // Realiza uma requisi��o GET para obter os meteoros da API
            while (true) {
                const Meteoros = await getMeteoro();

                // Verifica se h� um meteoro com os mesmos par�metros
                meteoro = Meteoros.find(met =>
                    met.alturaInicial === AlturaMeteoro &&
                    met.velocidadeMeteoro === VelocidadeMeteoro &&
                    met.distanciaHorizontal === DistanciaHorizontal
                );

                if (meteoro) {
                    // Se encontrou um meteoro com os mesmos par�metros, exibe no console
                    console.log("Meteoro encontrado:", meteoro);
                    break;
                } else {
                    // Caso n�o tenha encontrado, faz uma requisi��o POST com os par�metros recebidos
                    const novoMeteoro = {
                        AlturaInicial: AlturaMeteoro,
                        VelocidadeMeteoro: VelocidadeMeteoro,
                        distanciaHorizontal: DistanciaHorizontal
                    };

                    await postMeteoro(novoMeteoro);
                    console.log("Novo meteoro adicionado com par�metros:", novoMeteoro);
                }
            }
        } catch (erro) {
            console.error("Erro ao realizar l�gica do projetil e meteoro:", erro);
        }

        // Realiza a l�gica de colis�o ap�s as requisi��es GET
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

    // Retorna false para garantir que a submiss�o padr�o do formul�rio n�o ocorra
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
        // Adicione aqui o c�digo para manipular a resposta da API
    } catch (erro) {
        console.error(`Erro na requisi��o ${tipo} para a API: `, erro);
    }
}

// Exemplos de uso
async function getProjetil() {
    return await fazerRequisicao('GET', 'Projetil', null);
    // C�digo a ser executado ap�s a requisi��o GET
}

async function postProjetil(dados) {
    return await fazerRequisicao('POST', 'Projetil', dados);
    // C�digo a ser executado ap�s a requisi��o POST
}

async function getMeteoro() {
    return await fazerRequisicao('GET', 'Meteoro', null);
    // C�digo a ser executado ap�s a requisi��o GET
}

async function postMeteoro(dados) {
    return await fazerRequisicao('POST', 'Meteoro', dados);
    // C�digo a ser executado ap�s a requisi��o POST
}
async function postColisao(dados) {
    return await fazerRequisicao('POST', 'Colisao', dados);
    // C�digo a ser executado ap�s a requisi��o POST
}

