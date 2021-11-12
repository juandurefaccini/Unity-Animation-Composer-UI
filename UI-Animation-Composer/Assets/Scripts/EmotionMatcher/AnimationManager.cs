using AnimationComposer;
using UnityEngine;
using Utils;

namespace EmotionMatcher
{
    public class AnimationManager : MonoBehaviour
    {
        // El chat se comunica conmigo para que me pueda hacer las animaciones, una vez obtenida la respuesta del network manager
        public string emocion;
        public double intensidad;
        
        private EmotionInterpreter _emotionInterpreter;
        private AnimationPlayer.AnimationPlayer _player;

        /// <summary> Dado un json con el cual hay que hacer matching, ejecuta la animacion ganadora
        /// - Autora : Camila Garcia Petiet
        /// </summary>
        /// <param name="emotion"> emocion </param>
        /// <param name="intensity"> intension </param>
        public void AnimateCharacter(string emotion, double intensity)
        {
            _emotionInterpreter = GetComponent<EmotionInterpreter>(); // Obtiene el componente hermano
            _player = GetComponent<AnimationPlayer.AnimationPlayer>(); // Obtiene el componente hermano
            BlockQueue animacion = _emotionInterpreter.GetMatch(intensity, emotion);
            
            if(animacion != null) 
            {
                _player.PlayAnimation(animacion);
            }
            else
            {
                Debug.Log("No hay animaciones compuestas cargadas");
            }
        }
        
        private void Start() {
            _emotionInterpreter = GetComponent<EmotionInterpreter>(); // Obtiene el componente hermano
            _player = GetComponent<AnimationPlayer.AnimationPlayer>(); // Obtiene el componente hermano
            //procesar json para sacarle la intencion y la emocion
            BlockQueue animacion = _emotionInterpreter.GetMatch(intensidad, emocion);
            animacion.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
            _player.PlayAnimation(animacion);
        }
    }
}