using System.Collections.Generic;
using AnimationComposer;
using AnimationPlayer;
using TMPro;
using UnityEngine;
using Utils;

namespace AnimationCreator
{
    public class AnimationSequencializer : MonoBehaviour
    {
        private const int MaximoAnimaciones = 4;
        
        public GameObject[] animacionesSeleccionadas;
        public GameObject targetAvatar;

        /// <summary> Agrega a la lista de animaciones seleccionadas el objeto de la animacion en la posicion adecuada y muestra
        /// estos cambios en pantalla - Autor : Tobias Malbos
        /// </summary>
        /// <param name="posicion"> Posicion donde se agrega la animacion </param>
        /// <param name="animacion"></param>
        internal void AddAnimToSequencializer(int posicion, GameObject animacion)
        {
            animacionesSeleccionadas[posicion] = animacion;
            AddAnimVisual(posicion, animacion);
        }

        /// <summary> Borrar emocion seleccionada en cierta posicion y actualizar vista acordemente - Autor : Tobias Malbos
        /// </summary>
        /// <param name="posicion"> Posicion donde se elimina la animacion </param>
        public void BorrarAnimacion(int posicion)
        {
            animacionesSeleccionadas[posicion] = null;
            BorrarAnimacionVisual(posicion);
        }

        /// <summary> Actualiza la vista cuando se elimina una animacion - Autor : Tobias Malbos
        /// </summary>
        /// <param name="posicion"> Posicion donde se elimina la animacion </param>
        private void BorrarAnimacionVisual(int posicion)
        {
            Transform transAnimacion = FindBotonAnimacion(posicion);
            SetActiveCloseButton(false, posicion);
            transAnimacion.GetComponentInChildren<TMP_Text>().text = "Animacion " + (posicion + 1);
        }
    
        /// <summary> Borra todas las animaciones seleccionadas - Autor : Tobias Malbos
        /// </summary>
        public void BorrarAnimacionesSeleccionadas()
        {
            for (int i = 0; i < MaximoAnimaciones; ++i)
            {
                BorrarAnimacion(i);
            }
        }

        /// <summary> Se reproduce la animacion creada hasta el momento - Autor : Tobias Malbos
        /// </summary>
        public void PreviewAnimacion()
        {
            BlockQueue blockQueue = GenerateBlockQueue();
            blockQueue.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
            targetAvatar.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueue);
        }

        /// <summary> Genera una BlockQueue con las animaciones compuestas seleccionadas - Autor : Tobias Malbos
        /// </summary>
        /// <returns></returns>
        public BlockQueue GenerateBlockQueue()
        {
            BlockQueue blockQueue = new BlockQueue();
        
            foreach (GameObject animacion in animacionesSeleccionadas)
            {
                if (animacion == null) continue;
                
                string texto = animacion.GetComponentInChildren<TMP_Text>().text;
                Queue<Block> bloques = BibliotecaPersonalizadas.GETInstance().GETAnimation(texto).GetBlocks();
                
                foreach (Block block in bloques)
                {
                    blockQueue.Enqueue(block);
                }
            }

            return blockQueue;
        }

        private void Start() => animacionesSeleccionadas = new GameObject[MaximoAnimaciones];
        
        /// <summary> Cambia el texto y color del boton cuando una animacion es agregada - Autor : Tobias Malbos
        /// </summary>
        /// <param name="posicion"> Posicion donde se agrega la animacion </param>
        /// <param name="animacion"></param>
        private void AddAnimVisual(int posicion, GameObject animacion)
        {
            TMP_Text texto = animacion.GetComponentInChildren<TMP_Text>();
        
            if (texto != null)
            {
                Transform transAnimacion = FindBotonAnimacion(posicion);
                transAnimacion.GetComponentInChildren<TMP_Text>().text = texto.text;
            }
        
            SetActiveCloseButton(true, posicion);
        }
        
        /// <summary> Setea el activado del boton de borrar animacion - Autor : Tobias Malbos
        /// </summary>
        /// <param name="active"></param>
        /// <param name="posicion"> Posicion donde se activa el boton </param>
        private void SetActiveCloseButton(bool active, int posicion)
        {
            Transform transAnimacion = FindBotonAnimacion(posicion);
            transAnimacion.Find("Clear").gameObject.SetActive(active);
        }
    
        /// <summary> Busca el boton asociado a cierta posicion - Autor : Tobias Malbos
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns></returns>
        private Transform FindBotonAnimacion(int posicion) => transform.Find("AnimationSlot" + posicion);
    }
}
