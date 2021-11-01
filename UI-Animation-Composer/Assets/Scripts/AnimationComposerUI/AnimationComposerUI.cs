using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AnimationBlockQueue;
using AnimationDataScriptableObject;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class AnimationComposerUI : MonoBehaviour
    {

        public static List<Animacion> TriggersSeleccionados { get; } = new List<Animacion>();
        private List<Animacion> _animacionesAtomicas = new List<Animacion>();
        public GameObject targetAvatar;
        private string _emocion;

        // UI
        public GameObject Canvas_AnimationEditor;
        public GameObject Canvas_Lista_Resultados_NodoPadre;
        public GameObject Canvas_PantallaConfirmacion;
        public TextMeshProUGUI Canvas_IndicadorCantAnimaciones;
        public GameObject ContenedorBotonesEmocion;
        public GameObject ContenedorBotonesParteDelCuerpo;
        

        // Resultados
        public GameObject Prefab_Lista_Resultados;

        // Previsualizacion
        private string parteDelCuerpo;

        // Estas constantes se utilizan cuando no se ha seleccionado alguna parte del cuerpo o emocion 

        private const string PARTE_DEL_CUERPO_INDEFINIDA = "Parte del Cuerpo Indefinida";

        private const string EMOCION_INDEFINIDA = "Emocion Indefinida";

        void Start()
        {
            // Debug.Log(BibliotecaPersonalizadas.getInstance().getAnimation("Ejemplo"));
            BibliotecaAtomicas.CargarAnimaciones();
            CargarAnimacionesAtomicas();
            parteDelCuerpo = PARTE_DEL_CUERPO_INDEFINIDA;
            _emocion = EMOCION_INDEFINIDA;
            ActualizarListadoAnimaciones();
        }

        /// <summary> Agrega a la lista de triggers seleccionados el trigger seleccionado  - Autor : Juan Dure
        /// </summary>
        internal void AddAnimToBlockQueue(string trigger, string layer)
        {
            TriggersSeleccionados.RemoveAll(q => q.Layer == layer); // Elimino el trigger que trabaje sobre la misma parte del cuerpo
            TriggersSeleccionados.Add(_animacionesAtomicas.Find(q => q.Trigger == trigger));
        }

        /// <summary> Borra los triggers seleccionados  - Autor : Juan Dure
        /// </summary>
        public void BorrarAnimacionesSeleccionadas()
        {
            targetAvatar.GetComponent<AnimationComposer>().ClearAnims(); // Se eliminan las animaciones que esten en queue
            TriggersSeleccionados.Clear();
        }

        /// <summary>carga la lista de animaciones convirtiendo los triggers en un elemento de tipo AnimacionItem  - Autor : Camila Garcia Petiet
        /// </summary>
        /// ACTUALIZACION 29/10/2021 Pedro Procopio : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
        /// ACTUALIZACION 31/10/21 Juan Dure : Renaming y uso de un constructor
        private void CargarAnimacionesAtomicas()
        {
            foreach (var layer in BibliotecaAtomicas.AtomicAnimations)
            {
                foreach (var emocion in layer.Value)
                {
                    foreach (AnimationData tupla in emocion.Value)
                    {
                        _animacionesAtomicas.Add(new Animacion(tupla, layer.Key, emocion.Key));
                    }
                }
            }
            SetAnimacionesCargadas(_animacionesAtomicas.Count);
        }

        /// <summary> Actualiza el indicador de cantidad de animaciones cargadas  - Autor : Juan Dure
        /// </summary>
        private void SetAnimacionesCargadas(int cantidad)
        {
            Canvas_IndicadorCantAnimaciones.GetComponent<TextMeshProUGUI>().text = cantidad.ToString() + " animaciones cargadas";
        }

        /// <summary> Obtiene las animaciones filtradas por parte del cuerpo, si no se ha seleccionado alguna emocion,
        /// o por parte del cuerpo y emocion, si se ha seleccionado una emocion - Autores : Juan Dure y Tobias Malbos
        /// </summary>
        /// <returns> Lista de strings con los nombres de las animaciones</returns>
        /// Actualizacion 30-10-21 Facundo Mozo - Ahora se permite filtrar animaciones unicamente por emocion
        private List<Animacion> GETAnimacionesFiltradas()
        {
            // Retornar animaciones filtradas de la lista de animaciones
            List<Animacion> animacionesFiltradas = new List<Animacion>();
            if (parteDelCuerpo != PARTE_DEL_CUERPO_INDEFINIDA)
            {
                animacionesFiltradas = _animacionesAtomicas.Where(q => q.Layer == parteDelCuerpo).ToList();
                if (_emocion != EMOCION_INDEFINIDA)
                {
                    animacionesFiltradas = animacionesFiltradas.Where(q => q.Emocion == _emocion).ToList();
                    return animacionesFiltradas;
                }
            }
            else if (_emocion != EMOCION_INDEFINIDA)
            {
                animacionesFiltradas = _animacionesAtomicas.Where(q => q.Emocion == _emocion).ToList();
                return animacionesFiltradas;
            }

            return animacionesFiltradas;
        }

        /// <summary> Actualizamos el listado de animaciones - Autor : Juan Dure
        /// </summary>
        /// ACTUALIZACION 29/10/2021 Pedro Procopio : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
        private void ActualizarListadoAnimaciones()
        {
            List<Animacion> listaAnimacionesFiltradas = GETAnimacionesFiltradas();
            // Debug.Log("Cantidad de animaciones resultantes: " + listaAnimacionesFiltradas.Count);

            // Limpiar lista de resultados
            foreach (Transform child in Canvas_Lista_Resultados_NodoPadre.transform)
            {
                Destroy(child.gameObject);
            }

            // Crear lista de resultados
            listaAnimacionesFiltradas.ForEach(q =>
            {
                // Debug.Log(q);
                GameObject itemAgregado = Instantiate(Prefab_Lista_Resultados);

                itemAgregado.transform.SetParent(Canvas_Lista_Resultados_NodoPadre.transform, false);
                itemAgregado.GetComponent<AnimationScriptItem>().Animacion = q;
            });
        }

        /// <summary>se asignan las animaciones al avatar para que las ejecute  - Autor : Camila Garcia Petiet.
        /// Actualizacion, ahora se paran las animaciones previas y reproduce la actual - Modificacion : Facundo Mozo - 29-10-2021
        /// </summary>
        /// ACTUALIZACION 31/10/2021 Juan Dure - Refactorizacion con nueva lista, uso de LINQ para filtrar la parte util de la lista 
        public void ReproducirAnimacion()
        {
            if (TriggersSeleccionados.Count > 0)
            {
                // Debug.Log("Cantidad de animaciones seleccionadas: " + triggersElegidos.Count);
                TriggersSeleccionados.ForEach(q => Debug.Log(q.Nombre));
                BlockQueue blockQueue = BlockQueueGenerator.GetBlockQueue(TriggersSeleccionados.Select(q => q.AnimacionData).ToList()); // Filtro la parte que me interesa, los AnimationData
                targetAvatar.GetComponent<AnimationComposer>().ClearAnims();
                targetAvatar.GetComponent<AnimationComposer>().AddBlockQueue(blockQueue);
            }
            else
            {
                Debug.Log("No se seleccionaron animaciones");
            }
        }

        /// <summary> Se asigna al avatar la animacion que se quiere visualizar por separado  - Autor : Facundo Mozo
        /// </summary>
        /// ACTUALIZACION 29/10/2021 Pedro Procopio : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
        /// ACTUALIZACION : 31/10/21 Juan Dure : Refactorizacion con nueva lista de animaciones, implementacion con Linq
        public void PreviewAnimacion(string animName)
        {
            //Debug.Log("Cantidad de animaciones seleccionadas: " + triggerElegido.Count);
            //Debug.Log("Trigger PREVIEW Elegido: " + animName);
            BlockQueue blockQueue = BlockQueueGenerator.GetBlockQueue(_animacionesAtomicas.FindAll(q => q.Trigger == animName).Select(q => q.AnimacionData).ToList()); // Uso del FindAll para que me lo convierta a una lista
            targetAvatar.GetComponent<AnimationComposer>().ClearAnims(); // Se eliminan las animaciones que esten en queue
            targetAvatar.GetComponent<AnimationComposer>().AddBlockQueue(blockQueue); // Reproduce actual
        }

        /// <summary> Activa el panel del animation composer - Autor : Camila Garcia Petiet
        /// </summary>
        public void ActivarPanel() => Canvas_AnimationEditor.SetActive(true);

        /// <summary> Desactiva el panel del animation composer  - Autor : Camila Garcia Petiet
        /// </summary>    
        public void DesactivarPanel() => Canvas_AnimationEditor.SetActive(false);

        /// <summary> Activa el panel de confirmacion de nombre  - Autor : Camila Garcia Petiet
        /// </summary>    
        public void MostrarIngresoNombre()
        {
            Canvas_PantallaConfirmacion.SetActive(true);
        }

     

        /// <summary> Setea parte del cuerpo si estas son diferentes, sino la parte del cuerpo se torna indefinida
        /// Autores : Juan Dure y Tobias Malbos
        /// </summary>
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion usando operador ternario
        /// ACTUALIZACION 31/10/21 Facundo Mozo : Arreglo de operador ternario, añanido de habilitacion/Deshabilitacion de Emociones si se selecciona el layer "Base"
        public void SetearParteDelCuerpo(string layer)
        {
            this.parteDelCuerpo = (this.parteDelCuerpo != layer) ?  layer :  PARTE_DEL_CUERPO_INDEFINIDA;
            if (this.parteDelCuerpo == "BaseLayer")
            {
                ContenedorBotonesEmocion.GetComponent<Columna>().DeshabilitarEmociones();
            }else
            {
                ContenedorBotonesEmocion.GetComponent<Columna>().HabilitarEmociones();
            }
            ActualizarListadoAnimaciones();
        }

        /// <summary> Setea emocion si las emociones son diferentes, sino la emocion se torna indefinida
        /// Autores : Juan Dure y Tobias Malbos
        /// </summary>    
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion usando operador ternario
        /// ACTUALIZACION 31/10/21 Facundo Mozo : Arreglo de operador ternario, añanido de habilitacion/Deshabilitacion de Layer "Base" si hay una emocion seleccionada
        public void SetearEmocion(string emocion)
        {
            this._emocion = (this._emocion != emocion) ? emocion : this._emocion = EMOCION_INDEFINIDA;
            if (this._emocion != EMOCION_INDEFINIDA)
            {
                ContenedorBotonesParteDelCuerpo.GetComponent<Columna>().DeshabilitarBaseLayer();
            }
            else
            {
                ContenedorBotonesParteDelCuerpo.GetComponent<Columna>().HabilitarBaseLayer();
            }
            ActualizarListadoAnimaciones();
        }

        /// <summary> Borrar emocion seleccionada de cierta parte del cuerpo
        /// Autores : Juan Dure
        /// </summary>
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion con nueva lista usando LINQ
        public void BorrarTriggerParteDelCuerpo(string layer)
        {
            // Borro todos los trigger seleccionados (siempre va a ser uno o ninguno) tales que se cumpla que existe una animacion
            // con ese trigger en esa parte del cuerpo
            TriggersSeleccionados.RemoveAll(q => q.Layer == layer);
            ActualizarListadoAnimaciones();
        }
    }
}