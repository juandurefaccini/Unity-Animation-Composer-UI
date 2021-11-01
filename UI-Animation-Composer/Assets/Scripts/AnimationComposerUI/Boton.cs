using System.Collections;
using System.Collections.Generic;
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
    public void CambiarColor()
    {   
        //  Pregunto si el boton esta pintado o no
        if ( GetComponent<UnityEngine.UI.Image>().color == Color.white)
        {   //Si no esta pintado, vuelvo blanco el ultimo y lo pinto verde
            columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
            GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        else
        {   //Si esta pintado, vuelvo blaco el ultimo, osea este 
            columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
        }
    }
}
