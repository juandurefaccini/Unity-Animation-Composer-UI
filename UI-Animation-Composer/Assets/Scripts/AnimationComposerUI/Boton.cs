using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Boton : MonoBehaviour
{
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
    public void CambiarColor()
    {   
        //  Pregunto si el boton esta pintado o no
        if ( GetComponent<Image>().color == Color.white)
        {   //Si no esta pintado, vuelvo blanco el ultimo y lo pinto verde
            columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
            transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = Color.white;
            GetComponent<Image>().color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        else
        {   //Si esta pintado, vuelvo blaco el ultimo, osea este
            columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
        }
    }
}
