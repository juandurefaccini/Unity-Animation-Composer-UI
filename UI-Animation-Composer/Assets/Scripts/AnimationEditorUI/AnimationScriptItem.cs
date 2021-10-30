using TMPro;
using UnityEngine;

public class AnimationScriptItem : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI coeficiente;
    private string parteDelCuerpo { get; set; }
    private GameObject animationEditor;

    private string trigger;

    private void Start()
    {
        animationEditor = GameObject.FindWithTag("AnimationEditor");
    }

    internal void UpdateAnimName(string Nombre)
    {
        title.GetComponent<TextMeshProUGUI>().text = Nombre;
    }

    internal void UpdateAnimTrigger(string trigger)
    {
        this.trigger = trigger;
    }

    internal void UpdateParteDelCuerpo(string parteDelCuerpo)
    {
        this.parteDelCuerpo = parteDelCuerpo;
    }

    internal void UpdateCoeficiente(string coeficiente)
    {
        this.coeficiente.GetComponent<TextMeshProUGUI>().text = coeficiente;
    }

    public void AddAnimToBlockQueue()
    {
        // Debug.Log(title.GetComponent<TextMeshProUGUI>().text);
        animationEditor.GetComponent<AnimationEditor>().AddAnimToBlockQueue(trigger, parteDelCuerpo);
    }

    public void PreviewAnimacion()
    {
        animationEditor.GetComponent<AnimationEditor>().PreviewAnimacion(trigger);
    }
}
