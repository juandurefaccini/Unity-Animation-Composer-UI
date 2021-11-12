using AnimationComposer;
using UnityEngine;
using Utils;

namespace AnimationPlayer
{
    public class AnimationPlayer : MonoBehaviour
    {
        private Vector3 _posicionInicialAvatar;
        private Quaternion _rotacionInicialAvatar;

        private void Start()
        {
            Transform avatarTransform = transform;
            _posicionInicialAvatar = avatarTransform.localPosition;
            _rotacionInicialAvatar = avatarTransform.localRotation;
        }

        /// <summary> Ejecuta una animacion dado el nombre de la misma. El nombre de la animacion puede que se encuentre
        /// dentro de la BibliotecaAtomicas o BibliotecaPersonalizadas. Si en ambas bibliotecas existen animaciones con
        /// igual nombre, se antepone la que se encuentre en BibliotecaAtomicas - Autor: Tobias Malbos
        /// </summary>
        /// <param name="nombre"> Identificador de la animacion a reproducir </param>
        public void PlayAnimation(string nombre)
        {
            // Primero buscamos dentro de las animaciones atomicas
            // Si no la encontramos dentro de las atomicas, buscamos sobre la biblioteca de las personalizadas
            BlockQueue animacion = BibliotecaAtomicas.GETInstance().GETAnimation(nombre) ?? BibliotecaPersonalizadas.GETInstance().GETAnimation(nombre);

            // Si no encontramos en alguna de las bibliotecas una animacion con el nombre provisto no ejecutamos nada
            if (animacion == null) return;
            
            // Encola por defecto un bloque de limpieza al finalizar la animacion
            animacion.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
            PlayAnimation(animacion);
        }

        /// <summary> Ejecuta una animacion dada su BlockQueue. Resetea, ademas la posicion y rotacion del avatar
        /// Autores : Facundo Mozo y Tobias Malbos
        /// </summary>
        /// <param name="animacion"></param>
        /// ACTUALIZACION 29/10/21 Facundo Mozo : Ahora se paran las animaciones previas y reproduce la actual
        public void PlayAnimation(BlockQueue animacion)
        {
            Transform avatarTransform = transform;
            var composer = gameObject.GetComponent<AnimationComposer.AnimationComposer>();
            composer.ClearAnims();
            avatarTransform.localPosition = _posicionInicialAvatar;
            avatarTransform.localRotation = _rotacionInicialAvatar;
            composer.AddBlockQueue(animacion);
        }
    }
}
