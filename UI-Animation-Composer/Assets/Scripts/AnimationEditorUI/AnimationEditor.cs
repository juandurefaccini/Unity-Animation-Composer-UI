using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationDataScriptableObject;
using AnimationBlockQueue;
using System.Linq;
using TMPro;

public class AnimationEditor : MonoBehaviour
{
    class AnimacionItem
    {
        public string Nombre;
        public double Intensidad;
        public string Trigger;
        public double[] Vector;
        public string Layer;
        public string Emocion;

        /// <summary> Carga valores en los atributos del objeto  - 
        /// Autor : Camila Garcia Petiet
        /// ACTUALIZACION 29/10/2021
        /// Modificacion : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
        /// Co-Autor: Pedro Procopio
        /// </summary>
        /// <param name="Nombre">Nombre a mostrar en la UI</param>
        /// <param name="Trigger">Trigger</param>
        /// <param name="Intensidad">Intensidad de la emocion</param>
        /// <param name="Layer">Layer</param>
        /// <param name="Emotion">Emocion</param>
        public void setAnim(string Nombre, double Intensidad, string Trigger, string Layer, string Emotion)
        {
            this.Nombre = Nombre;
            this.Intensidad = Intensidad;
            this.Trigger = Trigger;
            this.Layer = Layer;
            this.Emocion = Emotion;
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
    /// ACTUALIZACION 29/10/2021
    /// Modificacion : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
    /// Co-Autor: Pedro Procopio
    /// </summary>
    private void cargarAnimationItems()
    {
        animaciones = new List<AnimacionItem>();
        foreach (var parteDelCuerpo in BibliotecaAtomicas.AtomicAnimations)
        {
            foreach (var emocion in parteDelCuerpo.Value)
            {
                foreach (var tupla in emocion.Value)
                {
                    AnimacionItem aux = new AnimacionItem();
                    aux.setAnim(tupla.Nombre,tupla.Intensidad,tupla.Trigger, parteDelCuerpo.Key, emocion.Key);
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
        Debug.Log(BibliotecaPersonalizadas.getInstance().getAnimation("Ejemplo"));
        BibliotecaAtomicas.CargarAnimaciones(); //cargar la biblioteca de animaciones a la lista de BibliotecaAnimaciones
        cargarAnimationItems(); //cargar la lista de animaciones
        parteDelCuerpo = PARTE_DEL_CUERPO_INDEFINIDA;
        emocion = EMOCION_INDEFINIDA;
        ActualizarListadoAnimaciones();
    }

    /// <summary>Obtiene las animaciones filtradas por parte del cuerpo, si no se ha seleccionado alguna emocion,
    /// o por parte del cuerpo y emocion, si se ha seleccionado una emocion - Autores : Juan Dure y Tobias Malbos
    /// Modificacion: Facundo Mozo - 30-10-21
    /// Ahora se permite filtrar animaciones unicamente por emocion
    /// </summary>
    /// <param name="parteDelCuerpo">Parte del cuerpo</param>
    /// <param name="emocionSeleccionada">Emocion</param>
    /// <returns>Lista de strings con los nombres de las animaciones</returns>
    private List<AnimacionItem> getAnimacionesFiltradas()
    {
        // Retornar animaciones filtradas de la lista de animaciones
        List<AnimacionItem> animacionesFiltradas = new List<AnimacionItem>();
        if (parteDelCuerpo != PARTE_DEL_CUERPO_INDEFINIDA)
        {
            animacionesFiltradas = animaciones.Where(q => q.Layer == parteDelCuerpo).ToList();
            if (emocion != EMOCION_INDEFINIDA)
            {
                animacionesFiltradas = animacionesFiltradas.Where(q => q.Emocion == emocion).ToList();
                return animacionesFiltradas;
            }
        }
        else if (emocion != EMOCION_INDEFINIDA)
        {
            animacionesFiltradas = animaciones.Where(q => q.Emocion == emocion).ToList();
            return animacionesFiltradas;
        }

        return animacionesFiltradas;
    }

    /// <summary> Actualizamos el listado de animaciones - Autor : Juan Dure
    /// ACTUALIZACION 29/10/2021
    /// Modificacion : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
    /// Co-Autor: Pedro Procopio
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
            itemAgregado.GetComponent<AnimationScriptItem>().UpdateAnimName(q.Nombre);
            itemAgregado.GetComponent<AnimationScriptItem>().UpdateAnimTrigger(q.Trigger);
            itemAgregado.GetComponent<AnimationScriptItem>().UpdateParteDelCuerpo(q.Layer);
        });
    }

    /// <summary>Busca los triggers seleccionados en el panel y los añade a una lista de tipo AnimationData  - Autor : Camila Garcia Petiet
    /// ACTUALIZACION 29/10/2021
    /// Modificacion : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
    /// Co-Autor: Pedro Procopio
    /// </summary>
    ///<returns>Lista de AnimationData con las animaciones</returns>
    private List<AnimationData> getTriggers()
    {
        
        List<AnimationData> aux = new List<AnimationData>();
        foreach (var tr in triggers_seleccionados)
        {
            AnimacionItem anim = animaciones.Find(x => x.Trigger == tr); //buscar el trigger 
            AnimationData tuplaAux = new AnimationData
            {
                Trigger = anim.Trigger,
                Intensidad = anim.Intensidad,
                Nombre = anim.Nombre
            };
            aux.Add(tuplaAux);
        }
        return aux;
    }

    /// <summary>se asignan las animaciones al avatar para que las ejecute  - Autor : Camila Garcia Petiet. Actualizacion, ahora se paran las animaciones previas y reproduce la actual - Modificacion : Facundo Mozo - 29-10-2021
    /// </summary>
    public void ReproducirAnimacion()
    {
        List<AnimationData> triggersElegidos = this.getTriggers();
        if(triggersElegidos.Count > 0)
        {
            // Debug.Log("Cantidad de animaciones seleccionadas: " + triggersElegidos.Count);
            // Debug.Log("Triggers Elegidos: " + triggersElegidos.ToString());
            BlockQueue blockQueue = BlockQueueGenerator.GetBlockQueue(triggersElegidos);
            //enviar al avatar
            targetAvatar.GetComponent<AnimationComposer>().ClearAnims();
            targetAvatar.GetComponent<AnimationComposer>().AddBlockQueue(blockQueue);
        }
        else
        {
            Debug.Log("No se seleccionaron animaciones");
        }
    }

    /// <summary> Se asigna al avatar la animacion que se quiere visualizar por separado  - Autor : Facundo Mozo
    /// ACTUALIZACION 29/10/2021
    /// Modificacion : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
    /// Co-Autor: Pedro Procopio
    /// </summary>
    public void PreviewAnimacion(string animName)
    {
        List<AnimationData> triggerElegido = new List<AnimationData>();
        AnimacionItem anim = animaciones.Find(x => x.Trigger == animName);
        AnimationData tuplaAux = new AnimationData
        {
            Trigger = anim.Trigger,
            Nombre = anim.Nombre,
            Intensidad = anim.Intensidad
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
