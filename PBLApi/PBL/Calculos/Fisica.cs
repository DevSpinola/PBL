using PBL.Data.Dtos;
using PBL.Models;
using System.Diagnostics.Metrics;

namespace PBL.Calculos
{
    public class Fisica
    {
        public static double CalcularValores(double angulo, double vm, double x, double h, out double? t1, out double? t2, out string? mov1,
            out string? mov2, out double? y1, out double? y2, out double? vo1, out double? vo2)
        {
            double g = 9.8;

            double a, b, c;
            double? tSubida1, tSubida2;

            a = g / 2;
            b = -vm;
            c = (-x * CalculoAvancado.Tan(angulo)) + h;

            double delta = Math.Pow(b, 2) - (4 * a * c);
            t1 = (-b + Math.Sqrt(delta)) / (2 * a);

            t2 = (-b - Math.Sqrt(delta)) / (2 * a);

            vo1 = x / (CalculoAvancado.Cos(angulo) * t1);

            vo2 = x / (CalculoAvancado.Cos(angulo) * t2);

            y1 = h - (vm * t1);

            y2 = h - (vm * t2);
            tSubida1 = vo1 * CalculoAvancado.Sen(angulo) / g;
            tSubida2 = vo2 * CalculoAvancado.Sen(angulo) / g;

            if (delta < 0)
            {
                t1 = null; t2 = null;
                vo1 = null; vo2 = null;
                y1 = null; y2 = null;
                mov1 = null; mov2 = null;
            }
            else if ((t1 < 0 || vo1 < 0 || y1 < 0) && (t2 < 0 || vo2 < 0 || y2 < 0))// Altura Tempo ou Velocidade menores que zero, impossibilidade de atingir
            {
                t1 = null; t2 = null;
                vo1 = null; vo2 = null;
                y1 = null; y2 = null;
                mov1 = null; mov2 = null;
            }
            else
            {
                if (t1 < tSubida1) mov1 = "Ascendente";
                else if (t1 == tSubida1) mov1 = "Ponto de Máximo";
                else mov1 = "Descendente";
                if (t2 < tSubida2) mov2 = "Ascendente";
                else if (t2 == tSubida2) mov2 = "Ponto de Máximo";
                else mov2 = "Descendente";
            }
            return delta;
        }
    }
}
