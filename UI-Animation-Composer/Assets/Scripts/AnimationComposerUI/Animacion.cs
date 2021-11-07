using AnimationDataScriptableObject;

namespace AnimationComposerUI
{
    /// <summary> Clase utilizada para mantener una lista de las animaciones atomicas almacenadas  - 
    /// Autor : Camila Garcia Petiet y Juan Dure
    /// </summary>
    /// Modificacion 31/10/2021 Juan Dure : Renaming y borrado de vector, ya que no se usa porque ahora usamos intesidad. 
    /// Borrada funcion de carga que simulaba constructor, generacion de constructor , renombrada la clase y proteccion de datos usando propiedades
    public class Animacion
    {
        public Animacion(AnimationData animacion, string layer, string emocion)
        {
            AnimacionData = animacion;
            Layer = layer;
            Emocion = emocion;
        }

        public AnimationData AnimacionData { get; protected set; }
        public string Nombre => AnimacionData.Nombre;
        public double Intensidad => AnimacionData.Intensidad;
        public string Trigger => AnimacionData.Trigger;
        public string Layer { get; protected set; }
        public string Emocion { get; protected set; }
        
    }
}


