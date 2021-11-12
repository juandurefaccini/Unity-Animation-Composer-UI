using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimationComposerUI
{
    public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public GameObject prefabItem;
        public TMP_Text nombre;

        private Canvas _canvas;
        private RectTransform _rectTransform;
        private GameObject _copia;
        private CanvasGroup _canvasGroup;

        private void Awake() => _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();

        public void OnPointerDown(PointerEventData eventData) => GetComponent<CanvasGroup>().alpha = 0.4f;
        public void OnPointerUp(PointerEventData eventData) => GetComponent<CanvasGroup>().alpha = 1f;
        public void OnDrag(PointerEventData eventData) => _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    
        public void OnBeginDrag(PointerEventData eventData)
        {
            _copia = Instantiate(prefabItem, _canvas.transform, false);
            _copia.transform.Find("PlayAnimButton").gameObject.SetActive(false);
            _copia.GetComponentInChildren<TMP_Text>().text = nombre.text;
            _copia.transform.position = gameObject.transform.position;
            _copia.GetComponent<CanvasGroup>().alpha = 1f;
            _rectTransform = _copia.GetComponent<RectTransform>();
            _canvasGroup = _copia.GetComponent<CanvasGroup>();
            _canvasGroup.blocksRaycasts = false;
        }
    
        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(_copia);
            GetComponent<CanvasGroup>().alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
