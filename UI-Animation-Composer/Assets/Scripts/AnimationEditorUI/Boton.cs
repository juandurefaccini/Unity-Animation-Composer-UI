using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton : MonoBehaviour
{
    public GameObject columna; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>Cambia el color del boton y setea el boton anterior al color original - Autores : Camila Garcia Petiet
    /// </summary>
    public void CambiarColor(){
        GetComponent<UnityEngine.UI.Image>().color = Color.green;
        columna.GetComponent<Columna>().SetBlanco(transform.gameObject);
    }
}
