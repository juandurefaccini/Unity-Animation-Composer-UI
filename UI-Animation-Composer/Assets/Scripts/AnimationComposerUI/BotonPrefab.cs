using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationComposerUI;

public class BotonPrefab : MonoBehaviour
{
    private Transform padreBoton;
    public GameObject boton;
   public void encontrarPadre(){
       padreBoton= transform.parent;
       Debug.Log(padreBoton.name);
       padreBoton.GetComponent<Columna>().UpdateButtonCoordinator(boton);
   }
}
