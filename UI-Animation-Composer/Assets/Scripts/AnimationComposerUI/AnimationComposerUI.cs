using System.Collections.Generic;
using System.Linq;
using AnimationComposer;
using AnimationCreator;
using AnimationPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace AnimationComposerUI
{
    public class AnimationComposerUI : MonoBehaviour
    {
        public List<Animacion> TriggersSeleccionados = new List<Animacion>();
        private readonly List<Animacion> _animacionesAtomicas = new List<Animacion>();
        public GameObject targetAvatar;

        // UI
        public GameObject canvasListaResultadosNodoPadre;
        public TextMeshProUGUI canvasIndicadorCantAnimaciones;
        public GameObject contenedorBotonesEmocion;
        public GameObject contenedorBotonesParteDelCuerpo;
        public GameObject mensajeNoAnims;
        
        // Resultados
        public GameObject prefabListaResultados;

        // Previsualizacion
        private string _parteDelCuerpo;
        private string _emocion;

        // Estas constantes se utilizan cuando no se ha seleccionado alguna parte del cuerpo o emocion 
        private const string ParteDelCuerpoIndefinida = "Parte del Cuerpo Indefinida";
        private const string EmocionIndefinida = "Emocion Indefinida";

        /// <summary> Agrega a la lista de triggers seleccionados el trigger seleccionado - Autor : Juan Dure
        /// </summary>
        internal void AddAnimToBlockQueue(string trigger, string layer)
        {
            Animacion agregada = _animacionesAtomicas.Find(q => q.Trigger == trigger);
            // Elimino el trigger que trabaje sobre la misma parte del cuerpo
            TriggersSeleccionados.RemoveAll(q => q.Layer == layer); 
            TriggersSeleccionados.Add(agregada);
        }

        /// <summary> Borra los triggers seleccionados - Autor : Juan Dure
        /// </summary>
        /// ACTUALIZACION 3/11/21 Tobias Malbos : Actualizado para que desactive los botones de Clear de las capas afectadas
        public void BorrarAnimacionesSeleccionadas()
        {
            // Se eliminan las animaciones que esten en queue
            targetAvatar.GetComponent<AnimationComposer.AnimationComposer>().ClearAnims(); 
            
            TriggersSeleccionados.ForEach(a =>
            {
                Button botonCapa = GetBotonCapa(a);
                Transform transClear = botonCapa.transform.Find("Clear");
                transClear.gameObject.SetActive(false);
            });
            
            TriggersSeleccionados.Clear();
        }

        /// <summary> Se asignan las animaciones al avatar para que las ejecute - Autor : Camila Garcia Petiet.
        /// </summary>
        /// ACTUALIZACION 31/10/2021 Juan Dure - Refactorizacion con nueva lista, uso de LINQ para filtrar la parte util de la lista
        /// ACTUALIZACION 3/11/21 Tobias Malbos : Actualizado para que llame al PlayAnimation del AnimationPlayer
        public void ReproducirAnimacion()
        {
            if (TriggersSeleccionados.Count > 0)
            {
                TriggersSeleccionados.ForEach(q => Debug.Log(q.Nombre));
                // Filtro la parte que me interesa, los AnimationData
                List<AnimationData> animationDatas = TriggersSeleccionados.Select(q => q.AnimacionData).ToList();
                BlockQueue blockQueue = BlockQueueGenerator.GetBlockQueue(animationDatas);
                targetAvatar.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueue);
            }
            else
            {
                Debug.Log("No se seleccionaron animaciones");
            }
        }

        /// <summary> Setea parte del cuerpo si estas son diferentes, sino la parte del cuerpo se torna indefinida
        /// Autores : Juan Dure y Tobias Malbos
        /// </summary>
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion usando operador ternario
        /// ACTUALIZACION 31/10/21 Facundo Mozo : Arreglo de operador ternario, añanido de habilitacion/Deshabilitacion de Emociones si se selecciona el layer "Base"
        public void SetearParteDelCuerpo(string layer)
        {
            _parteDelCuerpo = _parteDelCuerpo != layer ? layer : ParteDelCuerpoIndefinida;
            contenedorBotonesEmocion.GetComponent<Columna>().SetColumnInteractability(_parteDelCuerpo != "BaseLayer");
            ActualizarListadoAnimaciones();
        }

        /// <summary> Setea emocion si las emociones son diferentes, sino la emocion se torna indefinida
        /// Autores : Juan Dure y Tobias Malbos
        /// </summary>    
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion usando operador ternario
        /// ACTUALIZACION 31/10/21 Facundo Mozo : Arreglo de operador ternario, añanido de habilitacion/Deshabilitacion de Layer "Base" si hay una emocion seleccionada
        public void SetearEmocion(string emocion)
        {
            _emocion = _emocion != emocion ? emocion : _emocion = EmocionIndefinida;
            contenedorBotonesParteDelCuerpo.GetComponent<Columna>().SetButtonInteractability("Base", _emocion == EmocionIndefinida);
            ActualizarListadoAnimaciones();
        }

        /// <summary> Borrar emocion seleccionada de cierta parte del cuerpo
        /// Autores : Juan Dure
        /// </summary>
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion con nueva lista usando LINQ
        // Borro todos los trigger seleccionados (siempre va a ser uno o ninguno) tales que se cumpla que existe una animacion con ese trigger en esa parte del cuerpo
        public void BorrarTriggerParteDelCuerpo(string layer) => TriggersSeleccionados.RemoveAll(q => q.Layer == layer);

        private void Start()
        {
            TriggersSeleccionados = new List<Animacion>();
            CargarAnimacionesAtomicas();
            _parteDelCuerpo = ParteDelCuerpoIndefinida;
            _emocion = EmocionIndefinida;
            ActualizarListadoAnimaciones();
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
        private void SetAnimacionesCargadas(int cantidad) => canvasIndicadorCantAnimaciones.text = cantidad + " animaciones cargadas";
        
        /// <summary> Obtiene las animaciones filtradas por parte del cuerpo, si no se ha seleccionado alguna emocion,
        /// o por parte del cuerpo y emocion, si se ha seleccionado una emocion - Autores : Juan Dure y Tobias Malbos
        /// </summary>
        /// <returns> Lista de strings con los nombres de las animaciones</returns>
        /// Actualizacion 30-10-21 Facundo Mozo - Ahora se permite filtrar animaciones unicamente por emocion
        private List<Animacion> GETAnimacionesFiltradas()
        {
            // Retornar animaciones filtradas de la lista de animaciones
            List<Animacion> animacionesFiltradas = new List<Animacion>();
            
            if (_parteDelCuerpo != ParteDelCuerpoIndefinida)
            {
                animacionesFiltradas = _animacionesAtomicas.Where(q => q.Layer == _parteDelCuerpo).ToList();
                
                if (_emocion != EmocionIndefinida)
                {
                    animacionesFiltradas = animacionesFiltradas.Where(q => q.Emocion == _emocion).ToList();
                }
            }
            else if (_emocion != EmocionIndefinida)
            {
                animacionesFiltradas = _animacionesAtomicas.Where(q => q.Emocion == _emocion).ToList();
            }

            return animacionesFiltradas;
        }
        
        /// <summary> Actualizamos el listado de animaciones - Autor : Juan Dure
        /// </summary>
        /// ACTUALIZACION 29/10/2021 Pedro Procopio : Actualizado para que funcione con los nuevos datos de la clase AnimationDataScriptableObject
        /// ACTUALIZACION 1/11/2021 Tobias Malbos : Actualizado para asignar el boton de la capa al Item Prefab y para
        /// activar el mensaje de sin animaciones en caso de que la cantidad de animaciones filtradas sea 0
        private void ActualizarListadoAnimaciones()
        {
            List<Animacion> listaAnimacionesFiltradas = GETAnimacionesFiltradas();

            // Limpiar lista de resultados
            foreach (Transform child in canvasListaResultadosNodoPadre.transform)
            {
                Destroy(child.gameObject);
            }
            
            // Activa el mensaje de sin animaciones cuando la cantidad de animaciones filtradas es 0
            mensajeNoAnims.SetActive(listaAnimacionesFiltradas.Count == 0);
            
            // Crear lista de resultados
            listaAnimacionesFiltradas.ForEach(animacion =>
            {
                GameObject itemAgregado = Instantiate(prefabListaResultados, canvasListaResultadosNodoPadre.transform, false);
                Button botonCapa = GetBotonCapa(animacion);
                AnimationScriptItem scriptItem = itemAgregado.GetComponent<AnimationScriptItem>();

                scriptItem.botonCapa = botonCapa;
                scriptItem.Animacion = animacion;
            });
        }
        
        /// <summary> Devuelve el boton asociado a la parte del cuerpo que anima la animacion dada - Autor : Tobias Malbos
        /// </summary>
        /// <returns></returns>
        private Button GetBotonCapa(Animacion animacion)
        {
            // Se remueven los ultimos 5 caracteres de la parte del cuerpo seleccionada o bien el substring "Layer"
            string capa = animacion.Layer.Remove(animacion.Layer.Length - 5);
            string nombreBoton = "Boton" + capa;
            Transform botonCapa = contenedorBotonesParteDelCuerpo.transform.Find(nombreBoton);

            return botonCapa.GetComponent<Button>();
        }
    }
}