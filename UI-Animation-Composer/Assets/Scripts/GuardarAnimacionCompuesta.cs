using System.Collections;
using System.Collections.Generic;
using System.IO;
using AnimationBlockQueue;
using AnimationComposerUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuardarAnimacionCompuesta : MonoBehaviour, IGuardarAnimacion
{
    public TMP_Text nombreAnimacion;
    public Slider sliderIntensidad;
    public TMP_Dropdown emocionDropbox;
    
    public GameObject editorAnimaciones;
    
    private const string PATH_CUSTOM_ANIMS = "/Resources/CustomAnimations/";
    
    /// <summary> Ejecuta el guardado de la animacion en un archivo json - Autor: Tobias Malbos
    /// </summary>
    /// <param name="nombreAnimacion"> Nombre que tendra el .json a generar </param>
    /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion con nueva lista
    /// ACTUALIZACION 2/11/21 Tobias Malbos : Actualizado para que agregue la nueva animacion dentro de BibliotecaPersonalizadas
    /// ACTUALIZACION 6/11/21 Tobias Malbos : Actualizado para que lea la intensidad y la emocion de los componentes correspondientes
    public void GuardarAnimacion()
    {
        List<Animacion> triggersSeleccionados = editorAnimaciones.GetComponent<AnimationComposerUI.AnimationComposerUI>().TriggersSeleccionados;
        BlockQueue animacion = BlockQueueGenerator.GetBlockQueue(triggersSeleccionados);
        AnimacionCompuesta compuesta = new AnimacionCompuesta(emocionDropbox.captionText.text, sliderIntensidad.value, animacion);
        BibliotecaPersonalizadas.CustomAnimations.Add(nombreAnimacion.text, compuesta);
        string json = JsonHelper.ToJson(/*nombreAnimacion, */compuesta);
        File.WriteAllText(Application.dataPath + PATH_CUSTOM_ANIMS + nombreAnimacion.text + ".json", json);
    }

    public int CantidadComponentes()
    {
        return editorAnimaciones.GetComponent<AnimationComposerUI.AnimationComposerUI>().TriggersSeleccionados.Count;
    }
}
