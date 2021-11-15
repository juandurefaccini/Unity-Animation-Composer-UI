using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropAction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject action_prefab;
    public TMP_Text nombre;

    private Canvas _canvas;
    private RectTransform _rectTransform;
    private GameObject _copia;
    private CanvasGroup _canvasGroup;

    /// <summary> Se obtiene el componente canvas. Esto se utilizara para establecer el parent de la instancia a
    /// crear cuando se comienza a hacer el drag - Autor : Juan Dure
    /// </summary>
    private void Awake() => _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

    /// <summary> Al hacer click sobre un boton de la lista de animaciones se setea el alpha para que se vea que 
    /// esta siendo seleccionado - Autor : Juan Dure
    /// </summary>
    public void OnPointerDown(PointerEventData eventData) => GetComponent<CanvasGroup>().alpha = 0.4f;
    
    /// <summary> Al dejar de hacer click se reinicia el alpha al valor original - Autor : Juan Dure
    /// </summary>
    public void OnPointerUp(PointerEventData eventData) => GetComponent<CanvasGroup>().alpha = 1f;
    
    /// <summary> Actualiza la posicion de la instancia creada haciendola coincidir con la del mouse. Debido a la
    /// diferencia de escalas se tiene en cuenta la del canvas para que no se genere error - Autor : Juan Dure
    /// </summary>
    public void OnDrag(PointerEventData eventData) => _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

    /// <summary> Al iniciar "Drag" o arrastre de boton se crea una instancia la cual sera la arrastrada 
    /// Los datos son tomados de la lista de animaciones - Autor : Facundo Mozo
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        _copia = Instantiate(action_prefab, _canvas.transform, false);
        _copia.GetComponentInChildren<TMP_Text>().text = nombre.text;
        _copia.transform.position = gameObject.transform.position;
        _copia.GetComponent<CanvasGroup>().alpha = 1f;
        _rectTransform = _copia.GetComponent<RectTransform>();
        _canvasGroup = _copia.GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = false;
    }

    /// <summary> Al finalizar "Drag" o arrastre de boton destruye la instancia creada al inicio y retorna el 
    /// color normal al boton original - Autor : Facundo Mozo
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(_copia);
        GetComponent<CanvasGroup>().alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }
}
