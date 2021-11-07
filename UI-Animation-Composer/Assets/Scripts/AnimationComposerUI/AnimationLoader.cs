using AnimationDataScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationLoader : MonoBehaviour
{
    public enum LoadOptions
    {
        LoadAll = 0,
        OnlyCustom = 1,
        OnlyAtomics = 2
    };

    public LoadOptions loadOptions;
    public int maxBlocks = 0;
    public GameObject avatar;
    public GameObject prefabItem;
    public GameObject canvasListaAnimaciones;

    // Start is called before the first frame update
    /// <summary>
    /// </summary>
    /// ACTUALIZACiON 7/11/21 Tobias Malbos: Actualizado para que utilice un enumerador
    void Start()
    {
        if (loadOptions == LoadOptions.LoadAll || loadOptions == LoadOptions.OnlyCustom)
        {
            LoadCustomAnimations();
        }
        
        if (loadOptions == LoadOptions.LoadAll || loadOptions == LoadOptions.OnlyAtomics)
        {
            LoadAtomicAnimations();
        }
    }

    /// <summary> Instancia ItemPrefabs por cada animacion atomica cargada - Autor: Tobias Malbos
    /// </summary>
    private void LoadAtomicAnimations()
    {
        foreach (var layer in BibliotecaAtomicas.AtomicAnimations)
        {
            foreach (var emotion in layer.Value)
            {
                foreach (AnimationData animationData in emotion.Value)
                {
                    CreateItemPrefab(animationData.Nombre);
                }
            }
        }
    }

    /// <summary> Instancia ItemPrefabs por cada animacion personalizada cargada - Autor: Tobias Malbos
    /// </summary>
    /// ACTUALIZACION 7/11/21 Tobias Malbos : Actualizado para que solo se puedan cargar animaciones que esten por debajo de un numero maximo de bloques especificado
    private void LoadCustomAnimations()
    {
        foreach (var animacion in BibliotecaPersonalizadas.CustomAnimations)
        {
            if (maxBlocks > 0 && animacion.Value.GetBlocks().Count <= maxBlocks)
            {
                CreateItemPrefab(animacion.Key);
            }
        }
    }

    /// <summary> Instancia y configura el Prefab que representa la vista de la animacion - Autor: Tobias Malbos
    /// </summary>
    /// <param name="name"> Nombre de la animacion </param>
    private void CreateItemPrefab(string name)
    {
        GameObject itemAgregado = Instantiate(prefabItem, canvasListaAnimaciones.transform, false);
        Transform transNombre = itemAgregado.transform.Find("Name");

        if (transNombre != null)
        {
            transNombre.gameObject.GetComponent<TMP_Text>().text = name;
        }

        Transform transBoton = itemAgregado.transform.Find("PlayAnimButton");

        if (transBoton != null)
        {
            transBoton.GetComponent<Button>().onClick.AddListener(() => 
                { avatar.GetComponent<AnimationPlayer>().PlayAnimation(name); });
        }
    }
}
