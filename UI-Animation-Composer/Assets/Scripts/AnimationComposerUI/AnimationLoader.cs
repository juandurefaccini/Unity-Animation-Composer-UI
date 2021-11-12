using AnimationPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class AnimationLoader : MonoBehaviour
    {
        public enum LoadOptions
        {
            LoadAll = 0,
            OnlyCustom = 1,
            OnlyAtomics = 2
        }

        public LoadOptions loadOptions;
        public GameObject avatar;
        public GameObject prefabItem;
        public GameObject canvasListaAnimaciones;

        /// <summary> Actualiza el listado de animaciones dependiendo de las opciones de carga - Autor : Tobias Malbos
        /// </summary>
        public void UpdateAnimations()
        {
            BorrarAnimaciones();
        
            if (loadOptions == LoadOptions.LoadAll || loadOptions == LoadOptions.OnlyCustom)
            {
                LoadCustomAnimations();
            }
        
            if (loadOptions == LoadOptions.LoadAll || loadOptions == LoadOptions.OnlyAtomics)
            {
                LoadAtomicAnimations();
            }
        }

        // Start is called before the first frame update
        /// <summary>
        /// </summary>
        /// ACTUALIZACiON 7/11/21 Tobias Malbos: Actualizado para que utilice un enumerador
        private void Start() => UpdateAnimations();
        
        private void BorrarAnimaciones()
        {
            foreach (Transform child in canvasListaAnimaciones.transform) {
                Destroy(child.gameObject);
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
                        CreateItemPrefab(animationData.nombre);
                    }
                }
            }
        }

        /// <summary> Instancia ItemPrefabs por cada animacion personalizada cargada - Autor: Tobias Malbos
        /// </summary>
        private void LoadCustomAnimations()
        {
            foreach (var animacion in BibliotecaPersonalizadas.CustomAnimations)
            {
                CreateItemPrefab(animacion.Key);
            }
        }

        /// <summary> Instancia y configura el Prefab que representa la vista de la animacion - Autor: Tobias Malbos
        /// </summary>
        /// <param name="nombre"> Nombre de la animacion </param>
        private void CreateItemPrefab(string nombre)
        {
            GameObject itemAgregado = Instantiate(prefabItem, canvasListaAnimaciones.transform, false);
            TMP_Text tmpNombre = itemAgregado.GetComponentInChildren<TMP_Text>();

            if (tmpNombre != null)
            {
                tmpNombre.text = nombre;
            }

            Transform transBoton = itemAgregado.transform.Find("PlayAnimButton");

            if (transBoton != null)
            {
                transBoton.GetComponent<Button>().onClick.AddListener(() => 
                    { avatar.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(nombre); });
            }
        }
    }
}
