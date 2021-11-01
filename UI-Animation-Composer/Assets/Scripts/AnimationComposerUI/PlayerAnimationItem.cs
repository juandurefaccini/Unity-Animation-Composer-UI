using TMPro;
using UnityEngine;

public class PlayerAnimationItem : MonoBehaviour
{
    public TextMeshProUGUI title;

    internal void UpdateAnimName(string animation_name)
    {
        title.GetComponent<TextMeshProUGUI>().text = animation_name;
    }
}
