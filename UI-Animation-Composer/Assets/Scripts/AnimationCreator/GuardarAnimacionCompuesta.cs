using System.Collections.Generic;
using System.IO;
using AnimationComposer;
using AnimationPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace AnimationCreator
{
    public class GuardarAnimacionCompuesta : MonoBehaviour, IGuardarAnimacion
    {
        public TMP_Text nombreAnimacion;
        public Slider sliderIntensidad;
        public TMP_Dropdown emocionDropbox;
    
        public GameObject editorAnimaciones;
    
        private const string PathCustomAnims = "/Resources/CustomAnimations/";
    
        /// <summary> Ejecuta el guardado de la animacion en un archivo json - Autor: Tobias Malbos
        /// </summary>
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion con nueva lista
        /// ACTUALIZACION 2/11/21 Tobias Malbos : Actualizado para que agregue la nueva animacion dentro de BibliotecaPersonalizadas
        /// ACTUALIZACION 6/11/21 Tobias Malbos : Actualizado para que lea la intensidad y la emocion de los componentes correspondientes
        public void GuardarAnimacion()
        {
            List<Animacion> triggersSeleccionados = editorAnimaciones.GetComponent<AnimationComposerUI.AnimationComposerUI>().TriggersSeleccionados;
            BlockQueue animacion = GetBlockQueue(triggersSeleccionados);
            AnimacionCompuesta compuesta = new AnimacionCompuesta(emocionDropbox.captionText.text, sliderIntensidad.value, animacion);
            BibliotecaPersonalizadas.CustomAnimations.Add(nombreAnimacion.text, compuesta);
            string json = JsonHelper.ToJson(compuesta);
            File.WriteAllText(Application.dataPath + PathCustomAnims + nombreAnimacion.text + ".json", json);
        }

        public int CantidadComponentes() => editorAnimaciones.GetComponent<AnimationComposerUI.AnimationComposerUI>().TriggersSeleccionados.Count;

        /// <summary> Genera una BlockQueue en funcion de una lista de animaciones (No agrega un bloque de clear al final)
        /// Autor : Tobias Malbos
        /// </summary>
        /// <param name="animaciones"></param>
        /// <returns></returns>
        private static BlockQueue GetBlockQueue(List<Animacion> animaciones)
        {
            List<Block> blocks = new List<Block> { new Block() };

            foreach (Animacion animacion in animaciones)
            {
                blocks[0].AddLayerInfo(new LayerInfo(animacion.Trigger));
            }
        
            return new BlockQueue(blocks);
        }
    }
}
