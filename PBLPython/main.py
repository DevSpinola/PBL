from api_client import getColisoes, getMeteoros, getProjeteis
from geraGrafico import simulacao_animada
projeteis = getProjeteis()
meteoros = getMeteoros()
colisoes = getColisoes()
for colisao in colisoes:
    try:
        velocidade_inicial = colisao.get('voParaColidir')
        projetilEncontrado = next((projetil for projetil in projeteis if projetil.get('projetilId') == colisao.get('projetilId')), None)
        anguloRad = projetilEncontrado.get('anguloRad')
        tempo_total = colisao.get('tempoColisao')
        intervalo_tempo = tempo_total/100
        meteoroEncontrado = next((meteoro for meteoro in meteoros if meteoro.get('meteoroId') == colisao.get('meteoroId')), None)
        distancia_meteoro = meteoroEncontrado.get('distanciaHorizontal')  # em metros
        altura_meteoro = meteoroEncontrado.get('alturaInicial')  # em metros
        velocidade_meteoro = meteoroEncontrado.get('velocidadeMeteoro')  # em metros por segundo
        nome_arquivo = 'colisao' + str(colisao.get('colisaoId'))
        print(f'Salvando animação...')
        simulacao_animada(velocidade_inicial, anguloRad, tempo_total, intervalo_tempo, distancia_meteoro, altura_meteoro, velocidade_meteoro,nome_arquivo )
    except Exception as e :
        print(e)
