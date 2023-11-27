import requests

urlProjetil = 'https://localhost:7116/Projetil'
urlMeteoro = 'https://localhost:7116/Meteoro'
urlColisao = 'https://localhost:7116/Colisao'
def checaProjetil(anguloGraus):
    # Pegando todos os projeteis
    response = requests.get(urlProjetil, verify=False)
    if response.status_code == 200:
        lista_projeteis = response.json()

        for projetil in lista_projeteis:            
            if projetil.get('anguloGraus') == anguloGraus:
                print(f"Encontrado no banco projétil com ângulo {anguloGraus}!")               
                return projetil
       
        dados = {'AnguloGraus': anguloGraus}             
        projetil = requests.post(urlProjetil, json=dados, verify=False, headers={'Content-Type': 'application/json'}).json()  
        print(f"Inserindo no banco projétil om ângulo {anguloGraus}")
        return projetil
    else:
        # Se a solicitação não foi bem-sucedida, imprime o código de status
        print(f"Falha na solicitação. Código de status: {response.status_code}") 
def checaMeteoro(meteoroAltura,meteorodistancia, meteoroVelocidade ):
    response = requests.get(urlMeteoro, verify=False)
    if response.status_code == 200:
        lista_Meteoros = response.json()

        for Meteoro in lista_Meteoros:
            if (Meteoro.get('alturaInicial') == meteoroAltura and
                Meteoro.get('velocidadeMeteoro') == meteoroVelocidade and
                Meteoro.get('distanciaHorizontal') == meteorodistancia ):
                print(f"Encontrado no banco meteoro com altura inicial {meteoroAltura}, distância horizontal {meteorodistancia} e velocidade {meteoroVelocidade}!")
                return Meteoro
            
        dados = {'alturaInicial': meteoroAltura, 'distanciaHorizontal': meteorodistancia, 'velocidadeMeteoro': meteoroVelocidade}
        meteoro = requests.post(urlMeteoro, json=dados, verify=False, headers={'Content-Type': 'application/json'}).json()
        print(f"Inserindo no banco meteoro com altura inicial {meteoroAltura}, distância horizontal {meteorodistancia} e velocidade {meteoroVelocidade}")
        return meteoro
    else:
        # Se a solicitação não foi bem-sucedida, imprime o código de status
        print(f"Falha na solicitação. Código de status: {response.status_code}")
def checaColisao(projetilId, meteoroId):    
    
    dados = {'projetilId': projetilId, 'meteoroId': meteoroId}
    colisao = requests.post(urlColisao, json=dados, verify=False, headers={'Content-Type': 'application/json'}).json()
    # print(f"Inserindo no banco colisao com projetilId:{projetilId} e meteoroId: {meteoroId}")
    return colisao    
def getColisoes():
    response = requests.get(urlColisao, verify=False)
    if response.status_code == 200:
        lista_colisoes = response.json()
        return lista_colisoes
    else:
        # Se a solicitação não foi bem-sucedida, imprime o código de status
        print(f"Falha na solicitação. Código de status: {response.status_code}")
def getMeteoros():
    response = requests.get(urlMeteoro, verify=False)
    if response.status_code == 200:
        lista_meteoros = response.json()
        return lista_meteoros
    else:
        # Se a solicitação não foi bem-sucedida, imprime o código de status
        print(f"Falha na solicitação. Código de status: {response.status_code}")        
def getProjeteis():
    response = requests.get(urlProjetil, verify=False)
    if response.status_code == 200:
        lista_projeteis = response.json()
        return lista_projeteis
    else:
        # Se a solicitação não foi bem-sucedida, imprime o código de status
        print(f"Falha na solicitação. Código de status: {response.status_code}")  
         