using UnityEngine;
using UnityEngine.EventSystems;

namespace AnimationComposerUI
{
    public class TabButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
    {
        public TabGroup tabGroup;
    
        public void OnPointerClick(PointerEventData eventData) => tabGroup.OnTabSelected(this);
        public void OnPointerExit(PointerEventData eventData) => tabGroup.OnTabExit();
        
        // Start is called before the first frame update
        private void Start() => tabGroup.Subscribe(this);
    }
}
