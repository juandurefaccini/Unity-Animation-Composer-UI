using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Columna : MonoBehaviour
{
    GameObject ultimoBoton;

    /// <summary> Inicializa ultimoBoton - Autores : Camila Garcia Petiet
    /// </summary>
    void Start()
    {
        ultimoBoton = null;
    }

    /// <summary>Vuelve al color original al ultimo boton clickeado y actualiza el valor del ultim boton clikeado - Autores : Camila Garcia Petiet
    /// </summary>
    /// <param name="next"> nuevo valor del ultimo boton clikeado</param>
    public void SetBlanco(GameObject next)
    {
        if (ultimoBoton != null)
        {
            ultimoBoton.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            ultimoBoton = next;
        }
        else
        {
            ultimoBoton = next;
        }
    }
    /// <summary> Habilitar los botones de filtrado por emociones cuando NO se selecciona el layer "Base"
    ///  - Autor : Facundo Mozo  
    public void HabilitarEmociones()
    {
        Button[] allChildren = gameObject.GetComponentsInChildren<Button>();
        foreach (Button child in allChildren)
        {
            child.interactable = true;
        }
    }
    /// <summary> Desahilitar los botones de filtrado por emociones cuando se selecciona el layer "Base"
    ///  - Autor : Facundo Mozo
    /// </summary>
    public void DeshabilitarEmociones()
    {
        Button[] allChildren = gameObject.GetComponentsInChildren<Button>();
        foreach (Button child in allChildren)
        {
            child.interactable = false;
        }
    }
    /// <summary> Deshabilitar boton base layer si se selecciona una emocion  - Autor : Facundo Mozo
    /// </summary>    
    public void DeshabilitarBaseLayer()
    {
        Button[] allChildren = gameObject.GetComponentsInChildren<Button>();
        foreach (Button child in allChildren)
        {
            TextMeshProUGUI aux = child.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (aux != null) {
                if (aux.text == "Base")
                {     
                    child.interactable =false;
                    return;
                }
            }
        }
    }
    /// <summary> Habilitar boton base layer si se deselecciona una emocion  - Autor : Facundo Mozo
    /// </summary>
    public void HabilitarBaseLayer()
    {
       Button[] allChildren = gameObject.GetComponentsInChildren<Button>();
        foreach (Button child in allChildren)
        {
            TextMeshProUGUI aux = child.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (aux != null) {
                if (aux.text == "Base")
                {     
                    child.interactable = true;
                    return;
                }
            }
        }
    }
}
