using System;
using System.Collections;
using System.Collections.Generic;
using AnimationCreator;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Accion : MonoBehaviour
{
    public string nombre;
    public Dictionary<string, string> parametros;

    public string GetJson()
    {
        parametros = new Dictionary<string, string>();
        
        foreach (TMP_InputField campo in GetComponentsInChildren<TMP_InputField>())
        {
            parametros.Add(campo.name, campo.text);
            Debug.Log("Nombre: " + campo.name);
            Debug.Log("Valor: " + campo.text);
        }
        
        return JsonHelper.ToJson(nombre, parametros);
    }
}
