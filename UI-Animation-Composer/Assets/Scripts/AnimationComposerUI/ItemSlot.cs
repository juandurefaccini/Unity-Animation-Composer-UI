using AnimationCreator;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimationComposerUI
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        public AnimationSequencializer sequencializer;
    
        /// <summary> Implementacion del IDropHandler. Esta funcion se ejecuta cuando un objeto arrastrable 'cae' sobre
        /// el objeto que contiene este script - Autor : Facundo Mozo
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null){
                Debug.Log("Tiene objeto");
                GameObject capturado = eventData.pointerDrag;
                int posicion = int.Parse(name.Substring(name.Length - 1));
                sequencializer.AddAnimToSequencializer(posicion, capturado);
            }
            else
            {
                Debug.Log("no tiene objeto");
            }
        }
    }
}
