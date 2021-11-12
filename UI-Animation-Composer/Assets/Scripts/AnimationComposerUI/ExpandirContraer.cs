using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class ExpandirContraer : MonoBehaviour
    {
        public Sprite expandir;
        public Sprite contraer;
        public Button boton;
        public GameObject rawImage;
        public GameObject rawImageExpandida;
        
        public void ActualizarIcono()
        {   
            if (boton.image.sprite == expandir)
            {
                boton.image.sprite = contraer;
                rawImage.SetActive(false);
                rawImageExpandida.SetActive(true);
            }
            else
            {
                boton.image.sprite = expandir;
                rawImageExpandida.SetActive(false);
                rawImage.SetActive(true);
            }
        }
    }
}
