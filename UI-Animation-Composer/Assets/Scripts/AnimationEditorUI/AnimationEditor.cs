using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationData;
using AnimationBlockQueue;
using System.Linq;
using TMPro;

public class AnimationEditor : MonoBehaviour
{
    class AnimacionItem
    {
        public string Trigger;
        public double[] Vector;
        public string Layer;
        public string Emocion;

        /// <summary> Carga valores en los atributos del objeto  - Autor : Camila Garcia Petiet
        /// </summary>
        /// <param name="t">Trigger</param>
        /// <param name="v">vector de emociones</param>
        /// <param name="l">Layer</param>
        /// <param name="e">Emocion</param>
        public void setAnim(string t, double[] v, string l, string e)
        {
            Trigger = t;
            Vector = v;
            Layer = l;
            Emocion = e;
        }
    }

    public static List<string> triggers_seleccionados { get; } = new List<string>() ;
    private List<AnimacionItem> animaciones = new List<AnimacionItem>();
    public GameObject targetAvatar;
    public GameObject Canvas_AnimationEditor;
    public GameObject Canvas_Lista_Resultados_NodoPadre;
    public GameObject Prefab_Lista_Resultados;
    public GameObject ingresoNombre;
    private string parteDelCuerpo;
    private string emocion;
    public TextMeshProUGUI Canvas_IndicadorCantAnimaciones;

    // Estas constantes se utilizan cuando no se ha seleccionado alguna parte del cuerpo o emocion 
    private const string PARTE_DEL_CUERPO_INDEFINIDA = "Parte del Cuerpo Indefinida";
    private const string EMOCION_INDEFINIDA = "Emocion Indefinida";

    internal void AddAnimToBlockQueue(string animName, string parteDelCuerpo)
    {
        List<string> triggersABorrar = animaciones.Where(q => q.Layer == parteDelCuerpo).Select(q => q.Trigger).ToList();
        triggers_seleccionados.RemoveAll(q => triggersABorrar.Contains(q));
        triggers_seleccionados.Add(animName);
    }

    public void BorrarAnimacionesSeleccionadas()
    {
        triggers_seleccionados.Clear();
    }

    /// <summary>carga la lista de animaciones convirtiendo los triggers en un elemento de tipo AnimacionItem  - Autor : Camila Garcia Petiet
    /// </summary>
    private void cargarAnimationItems()
    {
        animaciones = new List<AnimacionItem>();
        foreach (var parteDelCuerpo in BibliotecaAnimaciones.animations)
        {
            foreach (var emocion in parteDelCuerpo.Value)
            {
                foreach (var tupla in emocion.Value)
                {
                    AnimacionItem aux = new AnimacionItem();
                    aux.setAnim(tupla.Trigger, tupla.Vector, parteDelCuerpo.Key, emocion.Key);
                    animaciones.Add(aux);
                }
            }

        }
        SetAnimacionesCargadas(animaciones.Count);
    }

    /// <summary>Actualiza el indicador de cantidad de animaciones cargadas  - Autor : Juan Dure
    /// </summary>
    private void SetAnimacionesCargadas(int cantidad)
    {
        Canvas_IndicadorCantAnimaciones.GetComponent<TextMeshProUGUI>().text = cantidad.ToString() + " animaciones cargadas";
    }

    void Start()
    {
        BibliotecaAnimaciones.CargarAnimaciones(); //cargar la biblioteca de animaciones a la lista de BibliotecaAnimaciones
        cargarAnimationItems(); //cargar la lista de animaciones
        parteDelCuerpo = PARTE_DEL_CUERPO_INDEFINIDA;
        emocion = EMOCION_INDEFINIDA;
        ActualizarListadoAnimaciones();
    }

    /// <summary>Obtiene las animaciones filtradas por parte del cuerpo, si no se ha seleccionado alguna emocion,
    /// o por parte del cuerpo y emocion, si se ha seleccionado una emocion - Autores : Juan Dure y Tobias Malbos
    /// </summary>
    /// <param name="parteDelCuerpo">Parte del cuerpo</param>
    /// <param name="emocionSeleccionada">Emocion</param>
    /// <returns>Lista de strings con los nombres de las animaciones</returns>
    private List<AnimacionItem> getAnimacionesFiltradas()
    {
        // Retornar animaciones filtradas de la lista de animaciones
        List<AnimacionItem> filtroParteCuerpo = animaciones.Where(q => q.Layer == parteDelCuerpo).ToList();

        if (emocion != EMOCION_INDEFINIDA)
        {
            filtroParteCuerpo = filtroParteCuerpo.Where(q => q.Emocion == emocion).ToList();
        }

        return filtroParteCuerpo;
    }

    /// <summary> Actualizamos el listado de animaciones - Autor : Juan Dure
    /// </summary>
    private void ActualizarListadoAnimaciones()
    {
        List<AnimacionItem> listaAnimacionesFiltradas = getAnimacionesFiltradas();
        // Debug.Log("Cantidad de animaciones resultantes: " + listaAnimacionesFiltradas.Count);

        // Limpiar lista de resultados
        foreach (Transform child in Canvas_Lista_Resultados_NodoPadre.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // Crear lista de resultados
        listaAnimacionesFiltradas.ForEach(q =>
        {
            // Debug.Log(q);
            GameObject itemAgregado = Instantiate(Prefab_Lista_Resultados);
            itemAgregado.transform.SetParent(Canvas_Lista_Resultados_NodoPadre.transform, false);
            itemAgregado.GetComponent<AnimationScriptItem>().UpdateAnimName(q.Trigger);
            itemAgregado.GetComponent<AnimationScriptItem>().UpdateParteDelCuerpo(q.Layer);
        });
    }

    /// <summary>Busca los triggers seleccionados en el panel y los añade a una lista de tipo TuplaScriptableObject  - Autor : Camila Garcia Petiet
    /// </summary>
    ///<returns>Lista de TuplaScriptableObject con las animaciones</returns>
    private List<TuplaScriptableObject> getTriggers()
    {
        List<TuplaScriptableObject> aux = new List<TuplaScriptableObject>();
        foreach (var tr in triggers_seleccionados)
        {
            AnimacionItem anim = animaciones.Find(x => x.Trigger == tr); //buscar el trigger 
            TuplaScriptableObject tuplaAux = new TuplaScriptableObject
            {
                Trigger = anim.Trigger,
                Vector = anim.Vector
            };
            aux.Add(tuplaAux);
        }
        return aux;
    }

    /// <summary>se asignan las animaciones al avatar para que las ejecute  - Autor : Camila Garcia Petiet. Actualizacion, ahora se paran las animaciones previas y reproduce la actual - Modificacion : Facundo Mozo - 29-10-2021
    /// </summary>
    public void ReproducirAnimacion()
    {
        List<TuplaScriptableObject> triggersElegidos = this.getTriggers();
        // Debug.Log("Cantidad de animaciones seleccionadas: " + triggersElegidos.Count);
        // Debug.Log("Triggers Elegidos: " + triggersElegidos.ToString());
        BlockQueue blockQueue = BlockQueueGenerator.GetBlockQueue(triggersElegidos);
        //enviar al avatar
        targetAvatar.GetComponent<AnimationComposer>().ClearAnims();
        targetAvatar.GetComponent<AnimationComposer>().AddBlockQueue(blockQueue);
    }

    /// <summary> Se asigna al avatar la animacion que se quiere visualizar por separado  - Autor : Facundo Mozo. Actualizacion, ahora se paran las animaciones previas y reproduce la actual - Modificacion : Facundo Mozo - 29-10-2021
    /// </summary>
    public void PreviewAnimacion(string animName)
    {
        List<TuplaScriptableObject> triggerElegido = new List<TuplaScriptableObject>();
        AnimacionItem anim = animaciones.Find(x => x.Trigger == animName); //buscar el trigger 
        TuplaScriptableObject tuplaAux = new TuplaScriptableObject
        {
            Trigger = anim.Trigger,
            Vector = anim.Vector
        };
        triggerElegido.Add(tuplaAux);
        //Debug.Log("Cantidad de animaciones seleccionadas: " + triggerElegido.Count);
        //Debug.Log("Trigger PREVIEW Elegido: " + animName);
        BlockQueue blockQueue = BlockQueueGenerator.GetBlockQueue(triggerElegido);
        // Se eliminan las animaciones que esten en queue y se reproduce la actual
        targetAvatar.GetComponent<AnimationComposer>().ClearAnims();
        //enviar al avatar
        targetAvatar.GetComponent<AnimationComposer>().AddBlockQueue(blockQueue);
    }
    
    /// <summary>activa el panel  - Autor : Camila Garcia Petiet
    /// </summary>
    public void ActivarPanel()
    {
        Canvas_AnimationEditor.SetActive(true);
    }

    /// <summary>desactiva el panel  - Autor : Camila Garcia Petiet
    /// </summary>    
    public void DesactivarPanel()
    {
        Canvas_AnimationEditor.SetActive(false);
    }

    public void MostrarIngresoNombre()
    {
        ingresoNombre.SetActive(true);
    }
    
    /// <summary> Setea parte del cuerpo si estas son diferentes, sino la parte del cuerpo se torna indefinida
    /// Autores : Juan Dure y Tobias Malbos
    /// </summary>
    public void SetearParteDelCuerpo(string parteDelCuerpo)
    {
        if (this.parteDelCuerpo != parteDelCuerpo)
        {
            this.parteDelCuerpo = parteDelCuerpo;
        }
        else
        {
            this.parteDelCuerpo = PARTE_DEL_CUERPO_INDEFINIDA;
        }
        
        ActualizarListadoAnimaciones();
    }

    /// <summary> Setea emocion si las emociones son diferentes, sino la emocion se torna indefinida
    /// Autores : Juan Dure y Tobias Malbos
    /// </summary>    
    public void SetearEmocion(string emocion)
    {
        if (this.emocion != emocion)
        {
            this.emocion = emocion;
        }
        else
        {
            this.emocion = EMOCION_INDEFINIDA;
        }
        
        ActualizarListadoAnimaciones();
    }
}
