using AnimationDataScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimationLoader : MonoBehaviour
{
    public GameObject avatar;
    public GameObject prefabItem;
    public GameObject canvasListaAnimaciones;

    // Start is called before the first frame update
    void Start()
    {
        //LoadAtomicAnimations();
        LoadCustomAnimations();
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
    private void LoadCustomAnimations()
    {
        foreach (var animation in BibliotecaPersonalizadas.CustomAnimations)
        {
            CreateItemPrefab(animation.Key);
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
                { avatar.GetComponent<AnimationPlayer>().playAnimation(name); });
        }
    }
}
