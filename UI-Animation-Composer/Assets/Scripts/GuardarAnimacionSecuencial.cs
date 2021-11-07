using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationBlockQueue;


public class GuardarAnimacionSecuencial : MonoBehaviour
{
    public GameObject editorAnimacioneSecuenciales;
    public void GuardarAnimacion(string nombreAnimacion)
    {
        AnimationSequencializer secuencializador = editorAnimacioneSecuenciales.GetComponent<AnimationSequencializer>();
        GameObject[] animacionesSeleccionadas = secuencializador.animacionesSeleccionadas;
        BlockQueue animacion = secuencializador.GenerateBlockQueue();
        BibliotecaPersonalizadas.CustomAnimations.Add(nombreAnimacion, animacion);
        //string json = JsonHelper.ToJson(nombreAnimacion, AnimationSequencializer.Select(q => q.Trigger).ToList());
        //File.WriteAllText(Application.dataPath + PATH_CUSTOM_ANIMS + nombreAnimacion + ".json", json);
        //CancelarIngresoNombre();
    }
}
