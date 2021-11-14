using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class AddActionSlot : MonoBehaviour, IDropHandler
{
    [FormerlySerializedAs("Gameobject padre de la lista")] public GameObject actionList; // Layout horizontal que contiene la lista de Acciones
    private int childCount = 1;
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Recibi drop");
        if (eventData.pointerDrag == null) return;
        GameObject _copia = Instantiate(eventData.pointerDrag, actionList.transform, false);
        // _copia.transform.parent = actionList.transform;
        childCount = actionList.transform.childCount;
        _copia.transform.SetSiblingIndex(childCount - 2);
        // eventData.pointerDrag.transform.SetAsFirstSibling();
    }
}
