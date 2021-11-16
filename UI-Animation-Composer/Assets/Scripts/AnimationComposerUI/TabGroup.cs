using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class TabGroup : MonoBehaviour
    {
        public List<TabButton> tabButtons;
        public List<GameObject> objectsToSwap;
        private TabButton _selectedTab;
        public GameObject targetAvatar;

        public void Subscribe(TabButton button)
        {
            tabButtons ??= new List<TabButton>();
            tabButtons.Add(button);
        }

        public void OnTabExit() => ResetTabs();

        public void OnTabSelected(TabButton button)
        {
            int index = button.transform.GetSiblingIndex();
            _selectedTab = button;
            ResetTabs();
            
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(i == index);
            }
            
            targetAvatar.GetComponent<AnimationComposer.AnimationComposer>().ClearAnims();
        }
        public void OnTabSelected(GameObject button)
        {
            int index = button.transform.GetSiblingIndex();
            
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(i == index);
            }
            
            targetAvatar.GetComponent<AnimationComposer.AnimationComposer>().ClearAnims();
        }

        private void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                if (_selectedTab != null && button == _selectedTab) continue;

                button.GetComponent<Image>().color = Color.grey;
            }
        }
    }
}
