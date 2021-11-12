using System.Collections;
using System.Collections.Generic;
using AnimationBlockQueue;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject prefabItem;
    public TMP_Text Name;
    private RectTransform rectTransform;
    private Canvas canvas;
    public BlockQueue colaBloques;
    private Transform parent;
    private GameObject copia;
    private CanvasGroup canvasGroup;

    /// <summary> Se obtiene el componente canvas. Esto se utilizara para establecer el parent de la instancia a crear cuando se comienza a hacer el drag  - Autores : Facundo Mozo
    /// </summary>
    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }
    /// <summary> Al hacer click sobre un boton de la lista de animaciones se setea el alpha para que se vea que 
    /// esta siendo seleccionado - Autores : Facundo Mozo
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().alpha = 0.4f;
    }
    /// <summary> Al dejar de hacer click se reinicia el alpha al valor original - Autores : Facundo Mozo
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().alpha = 1f;
    }
    /// <summary> Al iniciar "Drag" o arrastre de boton se crea una instancia la cual sera la arrastrada 
    ///  Los datos son tomados de la lista de animaciones - Autores : Facundo Mozo
    /// </summary>
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
        canvasGroup.blocksRaycasts = false;
    }
    /// <summary> Actualiza la posision de la instancia creada haciendola coindir con la del mouse. debido a la diferencia de scalas se tiene en cuenta la del canvas para que no se genere error 
    ///- Autores : Facundo Mozo
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    /// <summary> Al finalizar "Drag" o arrastre de boton destruye la instancia creada al inicio y retorna el 
    ///color normal al boton original - Autores : Facundo Mozo
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(copia);
        GetComponent<CanvasGroup>().alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
