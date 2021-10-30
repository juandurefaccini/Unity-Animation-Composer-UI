using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Columna : MonoBehaviour
{
    GameObject ultimoBoton;
    // Start is called before the first frame update
    /// <summary>inicializa ultimoBoton - Autores : Camila Garcia Petiet
    /// </summary>
    void Start()
    {
        ultimoBoton=null; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>Vuelve al color original al ultimo boton clickeado y actualiza el valor del ultim boton clikeado - Autores : Camila Garcia Petiet
    /// </summary>
    /// <param name="next">nuevo valor del ultimo boton clikeado</param>
    public void SetBlanco(GameObject next)
    {
       if(ultimoBoton!=null)
        {
            ultimoBoton.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            ultimoBoton=next;
        }else{
            ultimoBoton=next;
        }
    }
}
