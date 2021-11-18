using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextParser : MonoBehaviour
{

    public TMP_Text texto;
    public TMP_Text inputField;
    private string[] oraciones;

    public void getInputText(){
        texto.text = inputField.text;
        oraciones= texto.text.Split('.');
        imprimirResult();
    }

    public void imprimirResult(){
        string resultado="";
        foreach(string s in oraciones){
            Debug.Log(s);
            resultado += s +"\n";
        }
        texto.text = resultado;
    }
}