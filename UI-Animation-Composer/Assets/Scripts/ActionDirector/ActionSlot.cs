using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace ActionDirector
{
    public class ActionSlot : MonoBehaviour, IDropHandler
    {
        [FormerlySerializedAs("Gameobject padre de la lista")] public GameObject actionList; // Layout horizontal que contiene la lista de Acciones
        private int _childCount = 1;
    
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            
            GameObject copia = Instantiate(eventData.pointerDrag, actionList.transform, false);
            ShowInputFields(copia);
            _childCount = actionList.transform.childCount;
            copia.transform.SetSiblingIndex(_childCount - 2);
        }

        private static void ShowInputFields(GameObject instance)
        {
            foreach (TMP_InputField campo in instance.GetComponentsInChildren<TMP_InputField>(true))
            {
                campo.gameObject.SetActive(true);
            }
        }
    }
}
