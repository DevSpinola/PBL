from time import sleep
import requests

url = 'https://localhost:7116/Projetil'



for x in range(1,90,1):
    dados = {'AnguloGraus': x}
    try:
        response = requests.post(url, json=dados, verify=False, headers={'Content-Type': 'application/json'})

        print (f'Codigo da Resposta: {response.status_code}\n Texto da Resposta: {response.text}' )
    except requests.exceptions.RequestException as e:
        print(e)    
    except Exception as e:
        print(e)  
          