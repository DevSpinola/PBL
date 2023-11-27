async function getColisaoPorId(id) {
    try {
        const resultado = await fazerRequisicao('GET', `Colisao/${id}`, null);
        console.log(resultado);
        return resultado;
    } catch (error) {
        console.error('Erro na requisi��o GET por ID:', error);
        return null; // Adicionado para garantir um retorno padr�o em caso de erro
    }
}

async function fazerRequisicao(tipo, endpoint, dados) {
    const response = await $.ajax({
        url: '/' + endpoint,
        type: tipo,
        contentType: 'application/json',
        data: tipo === 'GET' ? null : JSON.stringify(dados),
    });    

    return response;
}


