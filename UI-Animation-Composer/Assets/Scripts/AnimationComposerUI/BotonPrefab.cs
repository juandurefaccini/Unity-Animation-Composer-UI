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
       padreBoton.GetComponent<Columna>().UpdateButtonCoordinator(boton);
   }
}
