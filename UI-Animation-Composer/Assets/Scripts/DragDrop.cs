using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour,IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject prefabItem;
    public TMP_Text Name;
    private RectTransform rectTransform;
    private Canvas canvas;

    private Transform parent;

    private GameObject copia;
    private CanvasGroup canvasGroup;
    

    private void Awake() {
        //rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    public void OnPointerDown( PointerEventData eventData)
    {
        parent = gameObject.transform.parent;
        copia = Instantiate(prefabItem, canvas.transform, false);
        Transform transNombre = copia.transform.Find("Name");
        if (transNombre != null)
        {
            transNombre.gameObject.GetComponent<TMP_Text>().text = Name.text;
        }
        copia.transform.SetParent(canvas.transform);
        copia.transform.position = gameObject.transform.position;
        rectTransform = copia.GetComponent<RectTransform>();
        canvasGroup = copia.GetComponent<CanvasGroup>();
        //playButton.SetActive(false); 
        //posInicial = gameObject.transform.position;
    }
    
    public void OnBeginDrag ( PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
        GetComponent<CanvasGroup>().alpha = 0.4f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag ( PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }
    public void OnEndDrag( PointerEventData eventData)
    {
        Object.Destroy(copia);
        Debug.Log("End Drag");
        GetComponent<CanvasGroup>().alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    
}
