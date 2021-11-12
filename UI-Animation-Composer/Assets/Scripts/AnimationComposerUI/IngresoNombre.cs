using System.IO;
using AnimationCreator;
using TMPro;
using UnityEngine;

namespace AnimationComposerUI
{
    public class IngresoNombre : MonoBehaviour
    {
        public TMP_InputField inputField;
        public TMP_Text errorMessageText;

        public GameObject mensaje;

        private const int MINAtomicas = 1;
        private const string PathCustomAnims = "/Resources/CustomAnimations/";

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
            if (!(GetComponent(tipoComponente) is IGuardarAnimacion guardadoAnimacion)) return;
            
            int cantidadTriggers = guardadoAnimacion.CantidadComponentes();
            string nombre = inputField.text;

            if (ESValido(nombre) && cantidadTriggers >= MINAtomicas)
            {
                guardadoAnimacion.GuardarAnimacion();
                MostrarMensaje("La animacion fue guardada exitosamente.");
                CancelarIngresoNombre();
            }
            else if (cantidadTriggers >= MINAtomicas)
            {
                MostrarMensaje("Error al guardar la animacion. No se ingreso un nombre valido.");
            }
            else
            {
                MostrarMensaje("Error al guardar la animacion. La animacion creada debe tener al menos " + MINAtomicas + " animacion atomica.");
            }
        }

        /// <summary> Verifica que el nombre no este vacio y que no haya otro archivo con el mismo nombre dentro del directorio
        /// Autor: Tobias Malbos
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        private static bool ESValido(string nombre) => !File.Exists(Application.dataPath + PathCustomAnims + nombre + ".json") && nombre.Length > 0;

        /// <summary> Abre una caja de dialogo con un mensaje - Autor: Tobias Malbos
        /// </summary>
        /// <param name="message"> Mensaje a mostrar </param>
        private void MostrarMensaje(string message)
        {
            Debug.Log(message);
            mensaje.SetActive(true);
            errorMessageText.text = message;
        }
    }
}
