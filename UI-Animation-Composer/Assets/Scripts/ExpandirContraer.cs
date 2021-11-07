using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ExpandirContraer : MonoBehaviour
{
    public Sprite expandir;
    public Sprite contraer;
    public Button boton;
    public GameObject rawImage;
    public GameObject rawImageExpandida;


    // Update is called once per frame
    public void ActualizarIcono()
    {   
        if (boton.image.sprite == expandir )
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
