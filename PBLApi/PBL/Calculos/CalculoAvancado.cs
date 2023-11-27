using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBL.Calculos
{
    internal class CalculoAvancado
    {
        public static double Cos(double x, double precisaoDesejada = 1e-20)// Função que calcula valor de cos(x) baseado na serie de CalculoAvancado
        {
            double f = 0;
            int k = 0;
            int cont = 0;
            double Resto;            
            while (true)
            {
                if (k % 2 == 0)
                {
                    f += (Math.Pow(-1, cont) / CalculoAvancado.Fatorial(2 * cont)) * Math.Pow(x, 2 * cont); // Valor genérico da função cos(x) em MacLaurin                   
                    cont += 1;
                } // As potências ímpares não aparecem, logo se considera apenas as pares          
                Resto = (1 / CalculoAvancado.Fatorial(k + 1)) * Math.Pow(Math.Abs(x), k + 1);
                if (Resto <= precisaoDesejada)
                {
                    break;
                }
                k++;
            }
            return f;
        }
        public static double Sen(double x, double precisaoDesejada = 1e-20)
        {
            double f = 0;
            int k = 0;
            int cont = 0;
            double Resto;            
            while (true)
            {
                if (k % 2 != 0)
                {
                    f += (Math.Pow(-1, cont) / CalculoAvancado.Fatorial((2 * cont) + 1)) * Math.Pow(x, (2 * cont) + 1); // Valor genérico da função cos(x) em MacLaurin                   
                    cont += 1;
                } // As potências ímpares  não aparecem, logo se considera apenas as pares              
                Resto = (1 / CalculoAvancado.Fatorial(k + 1)) * Math.Pow(Math.Abs(x), k + 1);
                if (Resto <= precisaoDesejada)
                {
                    break;
                }
                k++;
            }
            return f;
        }
        public static double Tan(double x, double precisaoDesejada = 1e-20)// Função que Calcula o valor da Tangente = Sen/Cos
        {
            return Sen(x, precisaoDesejada) / Cos(x, precisaoDesejada);
        }
        public static double Fatorial(int x) // Calcula Fatorial de um número
        {
            if (x == 0) return 1;
            int fatorial = x;
            for (int i = x; i > 1; i--)
            {
                fatorial *= i - 1;
            }
            return fatorial;
        }
        public static double ConverteAngulo(double AnguloEmGraus) // Função que converte Angulo de Graus para Radianos
        {
            double AnguloEmRad = AnguloEmGraus * Math.PI / 180;
            return AnguloEmRad;
        }
    }
}

