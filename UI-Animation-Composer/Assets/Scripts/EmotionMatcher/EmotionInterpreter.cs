using System;
using System.IO; // Para directory
using System.Linq; // Para parsear el nombre de la carpeta de un path
using System.Collections.Generic;
using UnityEngine;
using AnimationDataScriptableObject; //para usar animationData
using AnimationBlockQueue; //para poder usar blockqueue
//using AnimatorComposerStructures;

namespace AnimationEmotionInterpreter{
    public class EmotionInterpreter : MonoBehaviour
    {
        private void Start()
        {
        }
       
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
    /// <param name="intensidad1"> Primer intensidad </param>
    /// <param name="intensidad2"> Segunda intensidad </param>
    /// <returns> Valor de la diferencia </returns>
        public double diferencia(double izq, double der)
        {
            double aux = 0;
            aux=System.Math.Abs(izq-der);
            return aux;
        }

    /// <summary> Dada una lista de emociones, una intensidad y una emocion se matchea con la animacion compuesta que mas coincida - Autor : Camila Garcia Petiet
    /// </summary>
    /// <param name="intensidad"> Intensidad </param>
    /// <param name="emocion"> Emocion </param>
    /// <returns> Nombre de la animacion </returns>
        public BlockQueue MatchingAnimation(double intensidad, string emocion)
        {
            AnimacionCompuesta sol1 = new AnimacionCompuesta(null,4,null);
            foreach (KeyValuePair<string, AnimacionCompuesta> t in BibliotecaPersonalizadas.CustomAnimations)
            {
                if (emocion==t.Value.Emocion && diferencia(intensidad, t.Value.Intensidad) < diferencia(intensidad, sol1.Intensidad))
                {
                    sol1 = t.Value;
                }
            }
            
            if (sol1.Emocion != null && sol1.Animacion!= null)
            {
                return sol1.Animacion;
            }
            return null;
        }
    }
}
