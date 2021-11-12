using System.Collections.Generic;
using AnimationComposer;
using AnimationPlayer;
using UnityEngine; // Para directory

namespace EmotionMatcher {
    public class EmotionInterpreter : MonoBehaviour
    {
        /// <summary> Obtiene la animacion que hace matching con lso parametros y la devuelve - Autor : Camila Garcia Petiet
        /// </summary>
        /// <param name="intensidad"> Valor de la intensidad a hacer matching </param>
        /// <param name="emocion"> Emocion con la que hay que hacer matching </param>
        /// <returns> Nombre de la animacion </returns>
        public BlockQueue GetMatch(double intensidad, string emocion){
            BlockQueue ganador = MatchingAnimation(intensidad, emocion);
            return ganador; //si es null es porque no hay animaciones
        }
        
        /// <summary> Calcula la diferencia entre dos intensidades - Autor : Camila Garcia Petiet
        /// </summary>
        /// <param name="izquierda"></param>
        /// <param name="derecha"></param>
        /// <returns> Valor de la diferencia </returns>
        private double Diferencia(double izquierda, double derecha) => System.Math.Abs(izquierda - derecha);

        /// <summary> Dada una lista de emociones, una intensidad y una emocion se matchea con la animacion compuesta que mas coincida
        /// Autora : Camila Garcia Petiet
        /// </summary>
        /// <param name="intensidad"> Intensidad </param>
        /// <param name="emocion"> Emocion </param>
        /// <returns> Nombre de la animacion </returns>
        private BlockQueue MatchingAnimation(double intensidad, string emocion)
        {
            AnimacionCompuesta sol1 = new AnimacionCompuesta(null,4,null);
            
            foreach (KeyValuePair<string, AnimacionCompuesta> t in BibliotecaPersonalizadas.CustomAnimations)
            {
                if (emocion == t.Value.Emocion && Diferencia(intensidad, t.Value.Intensidad) < Diferencia(intensidad, sol1.Intensidad))
                {
                    sol1 = t.Value;
                }
            }
            
            if (sol1.Emocion != null && sol1.Animacion != null)
            {
                return sol1.Animacion;
            }
            
            return null;
        }
    }
}
