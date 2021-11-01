using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using AnimationDataScriptableObject;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class AnimationScriptItem : MonoBehaviour
    {
        public TextMeshProUGUI TMP_title;
        public TextMeshProUGUI TMP_intensidad;
        public Button botonCapa;
        private GameObject _animationComposerUI;
        private Animacion _animacion;
        public Animacion Animacion
        {
            private get { return _animacion; }
            set
            {
                if (value != null)
                {
                    _animacion = value;
                    UpdateAnimNameText(value.Nombre);
                    UpdateIntensidadText(value.Intensidad.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    Debug.Log("La animacion es invalida en AnimationScriptItem");
                }
            }
        }

        private string Trigger => _animacion.Trigger;
        private string Layer => _animacion.Layer;
        private void Start() => _animationComposerUI = GameObject.FindWithTag("AnimationEditor");
        private void UpdateAnimNameText(string nombre) => TMP_title.GetComponent<TextMeshProUGUI>().text = nombre;
        private void UpdateIntensidadText(string intensidad) => TMP_intensidad.GetComponent<TextMeshProUGUI>().text = intensidad;
        public void AddAnimToBlockQueue() => _animationComposerUI.GetComponent<AnimationComposerUI>().AddAnimToBlockQueue(Trigger, Layer);
        public void UpdateLayerClearButton() => botonCapa.transform.Find("Clear").gameObject.SetActive(true);
        public void PreviewAnimacion() => _animationComposerUI.GetComponent<AnimationComposerUI>().PreviewAnimacion(Trigger);
    }
}
