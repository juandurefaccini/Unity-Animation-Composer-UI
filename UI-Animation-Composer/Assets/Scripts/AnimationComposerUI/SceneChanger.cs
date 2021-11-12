using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimationComposerUI
{
    public class SceneChanger : MonoBehaviour
    {
        /// <summary> Ejecuta el cambio de escena - Autor : Tobias Malbos
        /// </summary>
        /// <param name="sceneID"> ID de la escena a cambiar </param>
        public void ChangeScene(int sceneID)
        {
            SceneManager.LoadScene(sceneID);
        }
    }
}