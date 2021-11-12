using AnimationComposer;

namespace AnimationPlayer
{
    /// <summary> Clase utilizada para mantener una lista de las animaciones compuestas personalizadas almacenadas
    /// Autor : Tobias Malbos
    /// </summary>
    public class AnimacionCompuesta
    {
        public AnimacionCompuesta(string emocion, double intensidad, BlockQueue animacion)
        {
            Emocion = emocion;
            Intensidad = intensidad;
            Animacion = animacion;
        }

        public string Emocion { get; } 
        public double Intensidad { get; }
        public BlockQueue Animacion { get; }
    }
}
