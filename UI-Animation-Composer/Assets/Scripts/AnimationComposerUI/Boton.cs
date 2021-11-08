using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Boton : MonoBehaviour, IDropHandler
{
    // 0.1960784f
    public GameObject columna;
    
    /// <summary> Agrega el callback al metodo cuando ocurre el evento de apretar por codigo - Autores : Dure Juan
    /// </summary>
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(CambiarColor);
    }

    /// <summary>Cambia el color del boton y setea el boton anterior al color original - Autores : Camila Garcia Petiet
    /// </summary>
    /// ACTUALIZACION 31/10/21 Facundo Mozo : Arreglado el metodo para tener en cuenta el caso en que se seleccione dos veces el mismo boton
    /// ACTUALIZACION 6/11/21 Tobias Malbos : Cambiado los colores al seleccionar el boton
    /// ACTUALIZACION 8/11/21 Tobias Malbos : Cambiado para que utilice los colores adecuados de la columna asignada
    public void CambiarColor()
    {
        Columna col = columna.GetComponent<Columna>();
        
        //  Pregunto si el boton esta pintado o no
        if ( GetComponent<Image>().color == Color.white)
        {   //Si no esta pintado, vuelvo blanco el ultimo y lo pinto verde
            col.SetBlanco(gameObject);
            transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = col.textSelectedColor;
            GetComponent<Image>().color = col.buttonSelectedColor;
        }
        else
        {   //Si esta pintado, vuelvo blaco el ultimo, osea este
            col.SetBlanco(gameObject);
        }
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("SOLTO");
        if (eventData.pointerDrag != null){
            Debug.Log("Tiene objeto");
            GameObject capturado = eventData.pointerDrag;
            int posicion = int.Parse(name.Substring(name.Length - 1));
            columna.GetComponent<AnimationSequencializer>().AddAnimToSequencializer(posicion, capturado);
        }
        else
        {
            Debug.Log("no tiene objeto");
        }
    }
}
