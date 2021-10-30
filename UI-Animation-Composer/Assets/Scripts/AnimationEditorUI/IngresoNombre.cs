using TMPro;
using UnityEngine;
using System.IO;
using UnityEditor.Build.Reporting;

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
    public void AceptarIngresoNombre()
    {
        string nombre = inputField.text;
        
        if (esValido(nombre) && AnimationEditor.triggers_seleccionados.Count >= MIN_ATOMICAS)
        {
            GuardarAnimacion(nombre);
            mostrarMensaje("La animacion fue guardada exitosamente.");
            gameObject.SetActive(false);
        }
        else if (AnimationEditor.triggers_seleccionados.Count >= MIN_ATOMICAS)
        {
            mostrarMensaje("Error al guardar la animacion. No se ingreso un nombre valido.");
        }
        else
        {
            mostrarMensaje("Error al guardar la animacion. La animacion creada debe tener al menos " + MIN_ATOMICAS + " animacion atomica.");
        }
    }
    
    /// <summary> Ejecuta el guardado de la animacion en un archivo json - Autor: Tobias Malbos
    /// </summary>
    /// <param name="nombreAnimacion"> Nombre que tendra el .json a generar </param>
    public void GuardarAnimacion(string nombreAnimacion)
    {
        string json = JsonHelper.ToJson(nombreAnimacion, AnimationEditor.triggers_seleccionados);
        File.WriteAllText(Application.dataPath + PATH_CUSTOM_ANIMS + nombreAnimacion + ".json", json);
        CancelarIngresoNombre();
    }

    /// <summary> Verifica que el nombre no este vacio y que no haya otro archivo con el mismo nombre dentro del directorio
    /// Autor: Tobias Malbos
    /// </summary>
    /// <param name="nombre"></param>
    /// <returns></returns>
    private bool esValido(string nombre)
    {
        return !File.Exists(Application.dataPath + PATH_CUSTOM_ANIMS + nombre + ".json") && nombre.Length > 0;
    }

    /// <summary> Abre una caja de dialogo con un mensaje - Autor: Tobias Malbos
    /// </summary>
    /// <param name="mensaje"> Mensaje a mostrar </param>
    private void mostrarMensaje(string mensaje)
    {
        this.mensaje.SetActive(true);
        errorMessageText.text = mensaje;
    }
}
