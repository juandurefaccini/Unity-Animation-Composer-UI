using AnimationBlockQueue;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    /// <summary> Ejecuta una animacion dado el nombre de la misma. El nombre de la animacion puede que se encuentre
    /// dentro de la BibliotecaAtomicas o BibliotecaPersonalizadas. Si en ambas bibliotecas existen animaciones con
    /// igual nombre, se antepone la que se encuentre en BibliotecaAtomicas - Autor: Tobias Malbos
    /// </summary>
    /// <param name="name"> Identificador de la animacion a reproducir </param>
    public void playAnimation(string name)
    {
        // Primero buscamos dentro de las animaciones atomicas
        BlockQueue animacion = BibliotecaAtomicas.getInstance().getAnimation(name);

        // Si no la encontramos dentro de las atomicas, buscamos sobre la biblioteca de las personalizadas
        if (animacion == null)
        {
            animacion = BibliotecaPersonalizadas.getInstance().getAnimation(name);
        }

        // Si encontramos en alguna de las bibliotecas una animacion con el nombre provisto la ejecutamos
        if (animacion != null)
        {
            gameObject.GetComponent<AnimationComposer>().AddBlockQueue(animacion);
        }  
    }
}
