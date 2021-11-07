using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Boton : MonoBehaviour, IDropHandler
{
    public GameObject columna;
    public int posicion;


    /// <summary> Agrega el callback al metodo cuando ocurre el evento de apretar por codigo - Autores : Dure Juan
    /// </summary>
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CambiarColor);
    }

    /// <summary>Cambia el color del boton y setea el boton anterior al color original - Autores : Camila Garcia Petiet
    /// </summary>
    /// ACTUALIZACION 31/10/21 Facundo Mozo : Arreglado el metodo para tener en cuenta el caso en que se seleccione dos veces el mismo boton
    public void CambiarColor()
    {   
        //  Pregunto si el boton esta pintado o no
        if ( GetComponent<Image>().color == Color.white)
        {   //Si no esta pintado, vuelvo blanco el ultimo y lo pinto verde
            columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
            GetComponent<Image>().color = Color.green;
        }
        else
        {   //Si esta pintado, vuelvo blaco el ultimo, osea este 
            columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("SOLTO");
        if (eventData.pointerDrag != null){
            Debug.Log("Tiene objeto");
            GameObject capturado = eventData.pointerDrag;
            TMP_Text texto = capturado.GetComponentInChildren<TMP_Text>();
            if (texto != null)
            {
                Debug.Log("no es nulo");
                gameObject.GetComponentInChildren<TMP_Text>().text = texto.text;
                CambiarColor(); 
            }
            columna.GetComponent<AnimationSequencializer>().AddAnimToSequencializer(posicion, capturado);
        }else
        {
            Debug.Log("no tiene objeto");
        }
    }

}
