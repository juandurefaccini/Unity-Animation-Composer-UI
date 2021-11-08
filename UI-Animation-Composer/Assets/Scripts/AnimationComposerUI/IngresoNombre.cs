using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnimationBlockQueue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class IngresoNombre : MonoBehaviour
    {
        public TMP_InputField inputField;
        public TMP_Text errorMessageText;

        public GameObject mensaje;

        public const int MIN_ATOMICAS = 1;

        private const string PATH_CUSTOM_ANIMS = "/Resources/CustomAnimations/";

        /// <summary> Funcion que se llama cuando el usuario presiona cancelar - Autor: Tobias Malbos
        /// </summary>
        public void CancelarIngresoNombre()
        {
            inputField.text = "";
            gameObject.SetActive(false);
        }

        /// <summary> Se encarga de verificar que el nombre ingresado sea valido y
        /// que al menos haya una cantidad MIN_ATOMICAS de animaciones - Autor: Tobias Malbos
        /// </summary>
        /// ACTUALIZACION 2/11/21 Tobias Malbos : Actualizado para no acceder a la variable TriggersSeleccionados de manera estatica
        public void AceptarIngresoNombre(string tipoComponente)
        {
            if (GetComponent(tipoComponente) is IGuardarAnimacion guardadoAnimacion)
            {
                int cantidadTriggers = guardadoAnimacion.CantidadComponentes();
                string nombre = inputField.text;

                if (esValido(nombre) && cantidadTriggers >= MIN_ATOMICAS)
                {
                    guardadoAnimacion.GuardarAnimacion();
                    MostrarMensaje("La animacion fue guardada exitosamente.");
                    CancelarIngresoNombre();
                }
                else if (cantidadTriggers >= MIN_ATOMICAS)
                {
                    MostrarMensaje("Error al guardar la animacion. No se ingreso un nombre valido.");
                }
                else
                {
                    MostrarMensaje("Error al guardar la animacion. La animacion creada debe tener al menos " + MIN_ATOMICAS + " animacion atomica.");
                }
            }
        }

        /// <summary> Verifica que el nombre no este vacio y que no haya otro archivo con el mismo nombre dentro del directorio
        /// Autor: Tobias Malbos
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        private bool esValido(string nombre) => !File.Exists(Application.dataPath + PATH_CUSTOM_ANIMS + nombre + ".json") && nombre.Length > 0;

        /// <summary> Abre una caja de dialogo con un mensaje - Autor: Tobias Malbos
        /// </summary>
        /// <param name="mensaje"> Mensaje a mostrar </param>
        private void MostrarMensaje(string mensaje)
        {
            Debug.Log(mensaje);
            this.mensaje.SetActive(true);
            errorMessageText.text = mensaje;
        }
    }
}
