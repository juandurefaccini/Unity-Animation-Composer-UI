using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class TabGroup : MonoBehaviour
    {
        public List<GameObject> objectsToSwap;
        public GameObject targetAvatar;

        public void OnTabSelected(GameObject button)
        {
            int index = button.transform.GetSiblingIndex();
            
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                objectsToSwap[i].SetActive(i == index);
            }
            
            targetAvatar.GetComponent<AnimationComposer.AnimationComposer>().ClearAnims();
        }
    }
}
