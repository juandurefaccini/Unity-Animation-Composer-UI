using TMPro;
using UnityEngine;

public class AnimationScriptItem : MonoBehaviour
{
    public TextMeshProUGUI title;
    private string parteDelCuerpo { get; set; }
    private GameObject animationEditor;

    private void Start()
    {
        animationEditor = GameObject.FindWithTag("AnimationEditor");
    }

    internal void UpdateAnimName(string trigger)
    {
        title.GetComponent<TextMeshProUGUI>().text = trigger;
    }

    internal void UpdateParteDelCuerpo(string parteDelCuerpo)
    {
        this.parteDelCuerpo = parteDelCuerpo;
    }

    public void AddAnimToBlockQueue()
    {
        // Debug.Log(title.GetComponent<TextMeshProUGUI>().text);
        animationEditor.GetComponent<AnimationEditor>().AddAnimToBlockQueue(title.GetComponent<TextMeshProUGUI>().text, parteDelCuerpo);
    }
}
