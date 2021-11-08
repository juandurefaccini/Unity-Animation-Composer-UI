using System.Collections;
using System.Collections.Generic;
using TMPro;
using AnimationBlockQueue;
using UnityEngine;
using UnityEngine.Serialization;

public class AnimationSequencializer : MonoBehaviour
{
    public const int MaximoAnimaciones = 4;
    public GameObject[] animacionesSeleccionadas;
    public GameObject targetAvatar;

    void Start()
    {
        animacionesSeleccionadas = new GameObject[MaximoAnimaciones];
    }

    /// <summary> Agrega a la lista de animaciones seleccionadas el objeto de la animacion en la posicion adecuadda
    /// Autor : Tobias Malbos
    /// </summary>
    internal void AddAnimToSequencializer(int posicion, GameObject animacion)
    {
        animacionesSeleccionadas[posicion] = animacion;
        AddAnimVisual(posicion, animacion);
    }

    internal void AddAnimVisual(int posicion, GameObject animacion)
    {
        TMP_Text texto = animacion.GetComponentInChildren<TMP_Text>();
        
        if (texto != null)
        {
            Debug.Log("no es nulo");
            Transform transAnimacion = FindBotonAnimacion(posicion);
            transAnimacion.GetComponentInChildren<TMP_Text>().text = texto.text;
            transAnimacion.GetComponent<Boton>().CambiarColor();
        }
        
        SetActiveCloseButton(true, posicion);
    }

    /// <summary> Borrar emocion seleccionada en cierta posicion - Autores : Tobias Malbos
    /// </summary>
    public void BorrarAnimacion(int posicion)
    {
        animacionesSeleccionadas[posicion] = null;
        BorrarAnimacionVisual(posicion);
    }

    private void BorrarAnimacionVisual(int posicion)
    {
        Transform transAnimacion = FindBotonAnimacion(posicion);
        SetActiveCloseButton(false, posicion);
        transAnimacion.GetComponentInChildren<TMP_Text>().text = "Animacion " + (posicion + 1);
    }
    
    /// <summary> Borra las animaciones seleccionadas - Autor : Tobias Malbos
    /// </summary>
    public void BorrarAnimacionesSeleccionadas()
    {
        for (int i = 0; i < MaximoAnimaciones; ++i)
        {
            BorrarAnimacion(i);
        }
    }

    /// <summary> Se reproduce la animacion creada hasta el momento - Autor : Tobias Malbos
    /// </summary>
    public void PreviewAnimacion()
    {
        BlockQueue blockQueue = GenerateBlockQueue();
        blockQueue.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
        targetAvatar.GetComponent<AnimationPlayer>().PlayAnimation(blockQueue);
    }

    public BlockQueue GenerateBlockQueue()
    {
        BlockQueue retorno = new BlockQueue();
        foreach (GameObject g in animacionesSeleccionadas)
        {
            if (g != null){
                string texto = g.GetComponentInChildren<TMP_Text>().text;
                Queue<Block> bloques = BibliotecaPersonalizadas.getInstance().getAnimation(texto).GetBlocks();
                foreach (Block b in bloques)
                {
                    retorno.Enqueue(b);
                }
            }
        }

        return retorno;
    }

    private void SetActiveCloseButton(bool active, int posicion)
    {
        Transform transAnimacion = FindBotonAnimacion(posicion);
        transAnimacion.Find("Clear").gameObject.SetActive(active);
    }
    
    private Transform FindBotonAnimacion(int posicion) => transform.Find("AnimationSlot" + posicion);
}
