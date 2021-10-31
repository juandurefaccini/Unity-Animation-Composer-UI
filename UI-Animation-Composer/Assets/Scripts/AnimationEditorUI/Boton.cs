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
    public void CambiarColor()
    {
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
        columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
    }
}
