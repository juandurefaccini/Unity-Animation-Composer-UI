using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AnimationComposerUI
{
    public class Columna : MonoBehaviour
    {
        public Color buttonSelectedColor;
        public Color buttonDeselectedColor;
        public Color textSelectedColor;
        public Color textDeselectedColor;
        public GameObject _ultimoBoton;

        /// <summary> Vuelve al color original al ultimo boton clickeado y actualiza el valor del ultimo boton clickeado
        /// Autora : Camila Garcia Petiet
        /// </summary>
        /// <param name="boton"> Boton presionado </param>
        public void UpdateButton(GameObject boton)
        {
            if (_ultimoBoton != null)
            {
                ChangeActualButtonColors(textDeselectedColor,buttonDeselectedColor);
            } 
        
            if (_ultimoBoton == boton)
            {
                _ultimoBoton = null;
            }
            else
            {
                _ultimoBoton = boton;
                ChangeActualButtonColors(textSelectedColor, buttonSelectedColor);
            }
        }

        public void UpdateButtonTab(GameObject boton)
        {
            if (_ultimoBoton != null)
            {
                if (_ultimoBoton != boton){
                    ChangeActualButtonColors(textDeselectedColor,buttonDeselectedColor);
                }
            } 
        
            if (_ultimoBoton != boton)
            {
                _ultimoBoton = boton;
                ChangeActualButtonColors(textSelectedColor, buttonSelectedColor);
            }
        }

        public void UpdateButtonPrefab(GameObject boton)
        {
            if (_ultimoBoton != null)
            {
                ChangeActualButtonColors(Color.white,Color.white);
            } 
        
            if (_ultimoBoton == boton)
            {
                _ultimoBoton = null;
            }
            else
            {
                _ultimoBoton = boton;
                ChangeActualButtonColors(Color.white, Color.cyan);
            }
        }

        /// <summary> Setea la interactabilidad de los botones de la columna - Autor : Facundo Mozo
        /// </summary>
        /// <param name="interactable"> Define si la columna es interactuable o no </param>
        /// ACTUALIZACION Tobias Malbos 11/11/21 : Generalizacion a una sola funcion
        public void SetColumnInteractability(bool interactable)
        {
            Button[] allChildren = gameObject.GetComponentsInChildren<Button>();
        
            foreach (Button child in allChildren)
            {
                child.interactable = interactable;
            }
        }

        /// <summary> Deshabilitar boton base layer si se selecciona una emocion - Autor : Facundo Mozo
        /// </summary>
        /// <param name="nombre"> Nombre del boton a setear la interactividad </param>
        /// <param name="interactable"> Define si la columna es interactuable o no </param>
        /// ACTUALIZACION Tobias Malbos 11/11/21 : Generalizacion a una sola funcion
        public void SetButtonInteractability(string nombre, bool interactable)
        {
            Button[] allChildren = gameObject.GetComponentsInChildren<Button>();
        
            foreach (Button child in allChildren)
            {
                TextMeshProUGUI tmpName = child.gameObject.GetComponentInChildren<TextMeshProUGUI>();

                if (tmpName == null || tmpName.text != nombre) continue;
            
                child.interactable = interactable;
                return;
            }
        }

        /// <summary> Cambia el color de texto y relleno del boton actual - Autora : Camila Garcia Petiet
        /// </summary>
        /// <param name="textColor"> Color del texto </param>
        /// <param name="fillColor"> Color de relleno </param>
        /// ACTUALIZACION 6/11/21 Tobias Malbos : Cambiado los colores al seleccionar el boton
        /// ACTUALIZACION 8/11/21 Tobias Malbos : Cambiado para que utilice colores no constantes
        private void ChangeActualButtonColors(Color textColor, Color fillColor)
        {
            _ultimoBoton.transform.Find("Text (TMP)").GetComponent<TMP_Text>().color = textColor;
            _ultimoBoton.GetComponent<Image>().color = fillColor;
        }
    }
}