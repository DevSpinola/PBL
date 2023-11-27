from time import sleep
import requests

url = 'https://localhost:7116/Meteoro'




dados = {'AlturaInicial': 150, 'VelocidadeMeteoro': 20, 'DistanciaHorizontal': 100}
try:
    response = requests.post(url, json=dados, verify=False, headers={'Content-Type': 'application/json'})
    print (f'Codigo da Resposta: {response.status_code}\n Texto da Resposta: {response.text}' )
except requests.exceptions.RequestException as e:
    print(e)    
except Exception as e:
    print(e)  
sleep(1)      