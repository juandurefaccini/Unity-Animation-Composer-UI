using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AnimationComposerUI;
using AnimationComposer;
using AnimationPlayer;



namespace AnimationCreator
{
    public class AnimatorCoordinatorUI : MonoBehaviour
    {
        public GameObject avatar1;
        public GameObject avatar2;
        public GameObject slider1;
        public GameObject slider2;
        public GameObject columna1;
        public GameObject columna2;
        public PreviewCoordinada previewCoordinada;

        /// <summary> Metodo utilizado para obtener la informacion seleccionada por el usuario y reproducir un preview de la animacion coordinada con los valores actuales
        /// Autor : Facundo Mozo
        /// </summary>
        public void previewAnimacionCordinada()
        {
            float off1 = GetDesfase1();
            float off2 = GetDesfase2();
            GameObject colum1 = columna1.GetComponent<Columna>()._ultimoBoton;
            string anim1 = "";
            if (colum1 != null)
                anim1 = colum1.GetComponentInChildren<TextMeshProUGUI>().text;  
            GameObject colum2 = columna2.GetComponent<Columna>()._ultimoBoton;
            string anim2 = "";
            if (colum2 != null)
                anim2 = colum2.GetComponentInChildren<TextMeshProUGUI>().text;  
            previewCoordinada.reproducirAnimacionCordinada(avatar1, avatar2, off1, off2, anim1, anim2);
        }
        /// <summary> Metodo utilizado para obtener una lista de BlockQueue de las animaciones compuestas personalizadas seleccionadas para el guardado en un json 
        /// /// Autor : Facundo Mozo
        /// </summary>
        public List<BlockQueue> GenerateBlockQueues()
        {
            List<BlockQueue> retorno = new List<BlockQueue>();
            BlockQueue blockQueue = new BlockQueue();
            Queue<Block> bloques = BibliotecaPersonalizadas.GETInstance().GETAnimation(columna1.GetComponent<Columna>()._ultimoBoton.GetComponentInChildren<TextMeshProUGUI>().text).GetBlocks();
            foreach (Block block in bloques)
            {
                blockQueue.Enqueue(block);
            }
            retorno.Add(blockQueue);
            bloques = BibliotecaPersonalizadas.GETInstance().GETAnimation(columna2.GetComponent<Columna>()._ultimoBoton.GetComponentInChildren<TextMeshProUGUI>().text).GetBlocks();
            foreach (Block block in bloques)
            {
                blockQueue.Enqueue(block);
            }
            retorno.Add(blockQueue);
            return retorno;
        }
        public float GetDesfase1()
        {
            return slider1.transform.GetComponent<Slider>().value;
        }
        public float GetDesfase2()
        {
            return slider2.transform.GetComponent<Slider>().value;
        }
        /// <summary> Metodo utilizado para borrar las animaciones seleccionadas hasta el momento, se utiliza para reiniciar la seleccion una vez que se guarda la animacion cordinada 
        /// Autor : Facundo Mozo
        /// </summary>
        public void BorrarAnimacionesSeleccionadas()
        {
            if (columna1.GetComponent<Columna>()._ultimoBoton != null)
                columna1.GetComponent<Columna>().UpdateButtonPrefab(columna1.GetComponent<Columna>()._ultimoBoton);
            if (columna2.GetComponent<Columna>()._ultimoBoton != null)
                columna2.GetComponent<Columna>().UpdateButtonPrefab(columna2.GetComponent<Columna>()._ultimoBoton);
        }

        public bool AreAnimacionesSeleccionadas()
        {   
            if (columna1.GetComponent<Columna>()._ultimoBoton == null || columna2.GetComponent<Columna>()._ultimoBoton == null)
                return false;
            else
                return true;
        }
    }
}
