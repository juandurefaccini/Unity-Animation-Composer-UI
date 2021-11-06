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
        public Slider sliderIntensidad;
        public TMP_Dropdown emocionDropbox;
        public TMP_Text errorMessageText;

        public GameObject editorAnimaciones;
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
        public void AceptarIngresoNombre()
        {
            int cantidadTriggers = editorAnimaciones.GetComponent<AnimationComposerUI>().TriggersSeleccionados.Count;
            string nombre = inputField.text;

            if (esValido(nombre) && cantidadTriggers >= MIN_ATOMICAS)
            {
                GuardarAnimacion(nombre);
                MostrarMensaje("La animacion fue guardada exitosamente.");
                gameObject.SetActive(false);
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

        /// <summary> Ejecuta el guardado de la animacion en un archivo json - Autor: Tobias Malbos
        /// </summary>
        /// <param name="nombreAnimacion"> Nombre que tendra el .json a generar </param>
        /// ACTUALIZACION 31/10/21 Juan Dure : Refactorizacion con nueva lista
        /// ACTUALIZACION 2/11/21 Tobias Malbos : Actualizado para que agregue la nueva animacion dentro de BibliotecaPersonalizadas
        /// ACTUALIZACION 6/11/21 Tobias Malbos : Actualizado para que lea la intensidad y la emocion de los componentes correspondientes
        public void GuardarAnimacion(string nombreAnimacion)
        {
            List<Animacion> triggersSeleccionados = editorAnimaciones.GetComponent<AnimationComposerUI>().TriggersSeleccionados;
            BlockQueue animacion = BlockQueueGenerator.GetBlockQueue(triggersSeleccionados);
            AnimacionCompuesta compuesta = new AnimacionCompuesta(emocionDropbox.captionText.text, sliderIntensidad.value, animacion);
            BibliotecaPersonalizadas.CustomAnimations.Add(nombreAnimacion, compuesta);
            string json = JsonHelper.ToJson(nombreAnimacion, compuesta);
            File.WriteAllText(Application.dataPath + PATH_CUSTOM_ANIMS + nombreAnimacion + ".json", json);
            CancelarIngresoNombre();
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
            this.mensaje.SetActive(true);
            errorMessageText.text = mensaje;
        }
    }
}
