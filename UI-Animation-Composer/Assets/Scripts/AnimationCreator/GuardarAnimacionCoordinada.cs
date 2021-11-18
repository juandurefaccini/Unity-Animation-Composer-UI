using System.IO;
using System.Collections.Generic;
using System.Linq;
using AnimationComposer;
using AnimationComposerUI;
using AnimationPlayer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace AnimationCreator
{
    public class GuardarAnimacionCoordinada : MonoBehaviour, IGuardarAnimacion
    {
        public TMP_Text nombreAnimacion;
        public AnimatorCoordinatorUI coordinador;
        public GameObject mensaje;
        public TMP_Text errorMessageText;


        private const string PathCustomAnims = "/Resources/CoordinateAnimations/";
        /// <summary> Funcion que se llama cuando el usuario presiona guardar del panel Ingreso Nombre en el coordinador de animaciones - Autor: Facundo Mozo
        /// </summary>    
        public void GuardarAnimacion()
        {
            if (!ESValido(nombreAnimacion.text))
            {
                MostrarMensaje("El nombre del archivo no es valido");
            }
            else
            {
                if (!coordinador.AreAnimacionesSeleccionadas())
                {
                    MostrarMensaje("Se debe seleccionar 1 animacion por avatar");
                }
                else
                {
                    List<BlockQueue> animacion = coordinador.GenerateBlockQueues();
                    AnimacionCoordinada coordinada1 = new AnimacionCoordinada(coordinador.GetDesfase1(), 'A', animacion[0]);
                    AnimacionCoordinada coordinada2 = new AnimacionCoordinada(coordinador.GetDesfase2(), 'B', animacion[1]);
                    List<AnimacionCoordinada> animaciones = new List<AnimacionCoordinada>();
                    animaciones.Add(coordinada1);
                    animaciones.Add(coordinada2);
                    string json = JsonHelper.ToJson(animaciones);
                    File.WriteAllText(Application.dataPath + PathCustomAnims + nombreAnimacion.text + ".json", json);
                    coordinador.BorrarAnimacionesSeleccionadas();
                    gameObject.SetActive(false);
                }
            }
        }

        public int CantidadComponentes() => 1;

        /// <summary> Abre una caja de dialogo con un mensaje - Autor: Tobias Malbos
        /// </summary>
        /// <param name="message"> Mensaje a mostrar </param>
        private void MostrarMensaje(string message)
        {
            Debug.Log(message);
            mensaje.SetActive(true);
            errorMessageText.text = message;
        }

        private static bool ESValido(string nombre) => (!File.Exists(Application.dataPath + PathCustomAnims + nombre + ".json") && nombre.Length > 0);
    }



}
