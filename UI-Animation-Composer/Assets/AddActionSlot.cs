using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AddActionSlot : MonoBehaviour, IDropHandler
{
    [FormerlySerializedAs("Gameobject padre de la lista")] public GameObject actionList; // Layout horizontal que contiene la lista de Acciones
    private int _childCount = 1;
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Recibi drop");
        if (eventData.pointerDrag == null) return;
        GameObject copia = Instantiate(eventData.pointerDrag, actionList.transform, false);
        ShowInputFields(copia);
        // copia.transform.parent = actionList.transform;
        _childCount = actionList.transform.childCount;
        copia.transform.SetSiblingIndex(_childCount - 2);
        // eventData.pointerDrag.transform.SetAsFirstSibling();
    }

    private static void ShowInputFields(GameObject instancia)
    {
        foreach (TMP_InputField campo in instancia.GetComponentsInChildren<TMP_InputField>(true))
        {
            campo.gameObject.SetActive(true);
        }
    }
}
