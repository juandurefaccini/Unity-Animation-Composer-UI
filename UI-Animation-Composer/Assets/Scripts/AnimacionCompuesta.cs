using System;
using AnimationBlockQueue;

/// <summary> Clase utilizada para mantener una lista de las animaciones compuestas personalizadas almacenadas
/// Autor : Tobias Malbos
/// </summary>
public class AnimacionCompuesta
{
    public AnimacionCompuesta(String emocion, double intensidad, BlockQueue animacion)
    {
        Emocion = emocion;
        Intensidad = intensidad;
        Animacion = animacion;
    }

    public string Emocion { get; protected set; } 
    public double Intensidad { get; protected set; }
    public BlockQueue Animacion { get; protected set; }
}
