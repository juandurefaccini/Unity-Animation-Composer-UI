using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AnimationBlockQueue;
using TMPro;
using System.IO;
using AnimationComposerUI;



public class GuardarAnimacionSecuencial : MonoBehaviour, IGuardarAnimacion
{
    public TMP_Text nombreAnimacion;
    public GameObject editorAnimacioneSecuenciales;
    public Slider sliderIntensidad;
    public TMP_Dropdown emocionDropbox;
    public AnimationLoader loaderSecuencial;
    public AnimationSequencializer secuencializador;

    private const string PATH_CUSTOM_ANIMS = "/Resources/CustomAnimations/";
    
    public void GuardarAnimacion()
    {
        AnimationSequencializer secuencializador = editorAnimacioneSecuenciales.GetComponent<AnimationSequencializer>();
        /*GameObject[] animacionesSeleccionadas = secuencializador.animacionesSeleccionadas;
        
        List<string> nombresAnimaciones = ExtraerNombresAnimaciones(animacionesSeleccionadas);*/
        
        BlockQueue animacion = secuencializador.GenerateBlockQueue();
        AnimacionCompuesta compuesta = new AnimacionCompuesta(emocionDropbox.captionText.text, sliderIntensidad.value, animacion);
        Debug.Log("CANTIDAD DE BLOQUES" + animacion.GetBlocks().Count);
        BibliotecaPersonalizadas.CustomAnimations.Add(nombreAnimacion.text, compuesta);
        string json = JsonHelper.ToJson(/*nombresAnimaciones, */compuesta);
        File.WriteAllText(Application.dataPath + PATH_CUSTOM_ANIMS + nombreAnimacion.text + ".json", json);
        loaderSecuencial.UpdateAnimations();
        secuencializador.BorrarAnimacionesSeleccionadas();
    }

    public int CantidadComponentes()
    {
        AnimationSequencializer secuencializador = editorAnimacioneSecuenciales.GetComponent<AnimationSequencializer>();
        int contador = 0;
        
        foreach (GameObject g in secuencializador.animacionesSeleccionadas)
        {
            if (g != null)
            {
                ++contador;
            }
        }

        return contador;
    }

    /*private List<string> ExtraerNombresAnimaciones(GameObject[] animacionesSeleccionadas)
    {
        List<string> retorno = new List<string>();
        foreach (GameObject g in animacionesSeleccionadas)
        {
            if (g != null)
            {
                string texto = g.GetComponentInChildren<TMP_Text>().text;
                retorno.Add(texto);
            }
        }
        return retorno;
    }*/
}
