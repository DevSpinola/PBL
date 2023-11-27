import time
import matplotlib.pyplot as plt
import matplotlib.animation as animation
import numpy as np
import os

def atualizar(frame, linha_projetil, linha_meteoro, tempos, velocidade_inicial, angulo, distancia_meteoro, altura_meteoro, velocidade_meteoro):
    # Atualiza as posições do projetil em cada quadro (frame)
    posicao_x_projetil =  velocidade_inicial * np.cos(angulo) * tempos[:frame+1]
    posicao_y_projetil =  velocidade_inicial * np.sin(angulo) * tempos[:frame+1] - 0.5 * 9.8 * tempos[:frame+1]**2

    # Atualiza as posições do meteoro em cada quadro (frame)
    posicao_x_meteoro = distancia_meteoro + np.zeros_like(tempos[:frame+1])
    posicao_y_meteoro = altura_meteoro - velocidade_meteoro * tempos[:frame+1]  # O meteoro cai para baixo

    # Atualiza as posições das linhas
    linha_projetil.set_data(posicao_x_projetil, posicao_y_projetil)
    linha_meteoro.set_data(posicao_x_meteoro, posicao_y_meteoro)
    return linha_projetil, linha_meteoro

def simulacao_animada(velocidade_inicial, angulo, tempo_total, intervalo_tempo, distancia_meteoro, altura_meteoro, velocidade_meteoro, nomeArquivo):
    fig, ax = plt.subplots()
    tempos = np.arange(0, tempo_total, intervalo_tempo)

    # Inicializa as linhas
    linha_projetil, = ax.plot([], [], '-o', label='Projétil')
    linha_meteoro, = ax.plot([], [], '-s', label='Meteoro')

    ax.set_xlim(0, distancia_meteoro + velocidade_inicial * tempo_total * np.cos(angulo))
    ax.set_ylim(0,  altura_meteoro)

    ax.legend()

    # Configuração da animação
    animacao = animation.FuncAnimation(fig, atualizar, frames=len(tempos),
                                       fargs=(linha_projetil, linha_meteoro, tempos, velocidade_inicial, angulo, distancia_meteoro, altura_meteoro, velocidade_meteoro),
                                       interval=intervalo_tempo*1000, blit=True)

    plt.title('Simulação de Lançamento Oblíquo com Meteoro (Animação)')
    plt.xlabel('Posição X (metros)')
    plt.ylabel('Posição Y (metros)')
    plt.legend()
    script_dir = os.path.dirname(os.path.abspath(__file__))
    diretorio = os.path.join(script_dir, '..', 'PBLApi', 'PBL', 'wwwroot', 'animacoes', nomeArquivo + '.gif')    
    if not (os.path.exists(diretorio)): 
        try:
            animacao.save(diretorio, writer='pillow', fps=30)
            print('Animação Salva com sucesso!')
            time.sleep(1)
        except Exception as e:
            print(f"Erro ao salvar a animação: {e}")
    else:
        print(f'O arquivo {nomeArquivo}.gif já existe.')   
    


