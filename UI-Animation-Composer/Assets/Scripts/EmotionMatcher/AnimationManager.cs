using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationBlockQueue;
using AnimationEmotionInterpreter;

public class AnimationManager : MonoBehaviour
{
    // El chat se comunica conmigo para que me pueda hacer las animaciones, una vez obtenida la respuesta del network manager
    private EmotionInterpreter emotionInterpreter;
    private AnimationPlayer player;
    public string emocion;
    public double intensidad;

    private void Start() {
        emotionInterpreter = GetComponent<EmotionInterpreter>(); // Obtiene el componente hermano
        player = GetComponent<AnimationPlayer>(); // Obtiene el componente hermano
        //procesar json para sacarle la intencion y la emocion
        BlockQueue animacion= new BlockQueue();
        //string em="Enojo";
        //double inten=0.2;
        animacion = emotionInterpreter.GetMatch(intensidad, emocion);
        animacion.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
        if(animacion!=null){
            player.PlayAnimation(animacion);
        }else{
            Debug.Log("No hay animaciones compuestas cargadas");
        }
    }

    /// <summary> Dado un json con el cual hay que hacer matching, ejecuta la animacion ganadora
    /// - Autor : Camila Garcia Petiet
    /// </summary>
    /// <param name="em"> emocion </param>
    /// <param name="inten"> intension </param>
   public void AnimateCharacter(string em, double inten)
    {
        emotionInterpreter = GetComponent<EmotionInterpreter>(); // Obtiene el componente hermano
        player = GetComponent<AnimationPlayer>(); // Obtiene el componente hermano
        BlockQueue animacion= new BlockQueue();
        animacion = emotionInterpreter.GetMatch(inten, em);
        if(animacion!=null){
            player.PlayAnimation(animacion);
        }else{
            Debug.Log("No hay animaciones compuestas cargadas");
        }
    }
}