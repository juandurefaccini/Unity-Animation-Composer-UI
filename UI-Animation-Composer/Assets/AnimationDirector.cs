using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDirector : MonoBehaviour
{
    public GameObject ListaAcciones;

    public void GuardarAccionCompuesta()
    {
        foreach (Transform child in ListaAcciones.transform) // por cada elemento del listado del listadoi
        {
            Debug.Log(child.name);
            Debug.Log(child.gameObject.GetComponent<IGetJson>().GetJson());
        }
    }
}
