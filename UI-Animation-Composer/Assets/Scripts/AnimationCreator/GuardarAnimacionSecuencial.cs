using System.IO;
using System.Linq;
using AnimationComposer;
using AnimationComposerUI;
using AnimationPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace AnimationCreator
{
    public class GuardarAnimacionSecuencial : MonoBehaviour, IGuardarAnimacion
    {
        public TMP_Text nombreAnimacion;
        public AnimationSequencializer secuencializador;
        public Slider sliderIntensidad;
        public TMP_Dropdown emocionDropbox;
        public AnimationLoader loaderSecuencial;

        private const string PathCustomAnims = "/Resources/CustomAnimations/";
    
        public void GuardarAnimacion()
        {
            BlockQueue animacion = secuencializador.GenerateBlockQueue();
            AnimacionCompuesta compuesta = new AnimacionCompuesta(emocionDropbox.captionText.text, sliderIntensidad.value, animacion);
            Debug.Log("CANTIDAD DE BLOQUES " + animacion.GetBlocks().Count);
            BibliotecaPersonalizadas.CustomAnimations.Add(nombreAnimacion.text, compuesta);
            string json = JsonHelper.ToJson(compuesta);
            File.WriteAllText(Application.dataPath + PathCustomAnims + nombreAnimacion.text + ".json", json);
            loaderSecuencial.UpdateAnimations();
            secuencializador.BorrarAnimacionesSeleccionadas();
        }

        public int CantidadComponentes() => secuencializador.animacionesSeleccionadas.Count(g => g != null);
    }
}
