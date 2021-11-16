using UnityEngine;
using AnimationComposerUI;

public class BotonPrefab : MonoBehaviour
{
    public GameObject boton;
    
    private Transform _padreBoton;
    
   public void encontrarPadre() {
       _padreBoton = transform.parent;
       _padreBoton.GetComponent<Columna>().UpdateButtonPrefab(boton);
   }
}