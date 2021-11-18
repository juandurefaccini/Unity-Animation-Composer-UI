using AnimationComposer;

namespace AnimationPlayer
{
    /// <summary> Clase utilizada para las animaciones coordinadas
    /// Autor : Facundo Mozo
    /// </summary>
    public class AnimacionCoordinada
    {
        public AnimacionCoordinada(float desfase, char avatar , BlockQueue animacion)
        {
            Desfase = desfase;
            Avatar = avatar;
            Animacion = animacion;
        }


        public float Desfase { get; }
        public char Avatar { get; }
        public BlockQueue Animacion { get; }
    }
}
