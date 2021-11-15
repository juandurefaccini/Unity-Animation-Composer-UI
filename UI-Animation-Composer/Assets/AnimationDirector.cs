using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationDirector : MonoBehaviour
{
    public GameObject ListaAcciones;
    public GameObject TextoJson;

    public void GuardarAccionCompuesta()
    {
        String text = "";
        foreach (Accion child in ListaAcciones.transform.GetComponentsInChildren<Accion>()) // por cada elemento del listado del listadoi
        {
            text += "\n";
            text += "\n";
            text += "\n";
            text += child.GetJson();
        }
        Debug.Log(text);
        TextoJson.GetComponent<TextMeshProUGUI>().text = text;
    }
    
    
}
