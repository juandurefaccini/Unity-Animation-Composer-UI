using System.Collections;
using System.Collections.Generic;
using AnimationBlockQueue;
using JetBrains.Annotations;
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
    public BlockQueue colaBloques;

    private Transform parent;

    private GameObject copia;
    private CanvasGroup canvasGroup;

    private void Awake() {
        //rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        parent = gameObject.transform.parent;
        GetComponent<CanvasGroup>().alpha = 0.4f;
        //playButton.SetActive(false); 
        //posInicial = gameObject.transform.position;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        copia = Instantiate(prefabItem, canvas.transform, false);
        copia.transform.Find("PlayAnimButton").gameObject.SetActive(false);
        TMP_Text nombre = copia.GetComponentInChildren<TMP_Text>();
        if (nombre != null)
        {
            nombre.text = Name.text;
        }
        copia.transform.SetParent(canvas.transform);
        copia.transform.position = gameObject.transform.position;
        copia.GetComponent<CanvasGroup>().alpha = 1f;
        rectTransform = copia.GetComponent<RectTransform>();
        canvasGroup = copia.GetComponent<CanvasGroup>();
        /*Debug.Log("On Begin Drag");*/
        
        canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(copia);
        //Debug.Log("End Drag");
        GetComponent<CanvasGroup>().alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
