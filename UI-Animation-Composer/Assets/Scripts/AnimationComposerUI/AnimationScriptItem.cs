using System.Globalization;
using AnimationCreator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class AnimationScriptItem : MonoBehaviour
    {
        public TextMeshProUGUI tmpTitle;
        public TextMeshProUGUI tmpIntensidad;
        public Button botonCapa;
        
        private Animacion _animacion;

        private static GameObject _avatar;
        private static GameObject _animationComposerUI;
        
        public Animacion Animacion
        {
            set
            {
                if (value != null)
                {
                    _animacion = value;
                    tmpTitle.text = value.Nombre;
                    tmpIntensidad.text = value.Intensidad.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    Debug.Log("La animacion es invalida en AnimationScriptItem");
                }
            }
        }
        
        public void AddAnimToBlockQueue() => _animationComposerUI.GetComponent<AnimationComposerUI>().AddAnimToBlockQueue(Trigger, Layer);
        public void UpdateLayerClearButton() => botonCapa.transform.Find("Clear").gameObject.SetActive(true);
        public void PreviewAnimacion() => _avatar.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(_animacion.Trigger);
        
        private string Trigger => _animacion.Trigger;
        private string Layer => _animacion.Layer;

        private void Start()
        {
            _animationComposerUI = GameObject.FindWithTag("AnimationEditor");
            _avatar = GameObject.FindWithTag("Avatar");
        }
    }
}
