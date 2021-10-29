using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mensaje : MonoBehaviour
{
    /// <summary> Desactiva la caja de dialogo - Autor: Tobias Malbos
    /// </summary>
    public void CerrarMensaje()
    {
        gameObject.SetActive(false);
    }
}
