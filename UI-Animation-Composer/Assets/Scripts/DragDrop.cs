using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour,IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{//IDropHandler
    private RectTransform rectTransform;
    private Vector3 posInicial;

    public GameObject playButton;
    private GameObject canvas;

    private Transform parent;

    private GameObject copia;
    public GameObject prefabItem;
    public TMP_Text Name;
    

    private void Awake() {
        //rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    public void OnEndDrag( PointerEventData eventData)
    {
        //gameObject.transform.position = posInicial;
        //gameObject.transform.parent = parent;
        //playButton.SetActive(true);
    }
    
    public void OnDrag ( PointerEventData eventData)
    {
        //Debug.Log("On Drag");
        rectTransform.anchoredPosition += eventData.delta;

    }
    public void OnBeginDrag ( PointerEventData eventData)
    {
        Debug.Log("On Begin Drag");
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
        copia.transform.parent = canvas.transform;
        copia.transform.position = gameObject.transform.position;
        rectTransform = copia.GetComponent<RectTransform>();
        //playButton.SetActive(false); 
        //posInicial = gameObject.transform.position;
    }
    //public void OnDrop(PointerEventData eventData){
        
    //}
}
