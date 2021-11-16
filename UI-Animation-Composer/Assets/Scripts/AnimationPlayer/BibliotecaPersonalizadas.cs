using System.Collections.Generic;
using System.IO;
using AnimationComposer;
using UnityEngine;
using Utils; // Para directory

namespace AnimationPlayer
{
    public class BibliotecaPersonalizadas: IBibliotecaAnimaciones
    {
        public static readonly Dictionary<string, AnimacionCompuesta> CustomAnimations = CargarAnimaciones();

        private const string CustomAnimPath = "/Resources/CustomAnimations/";
    
        private static BibliotecaPersonalizadas _instance;
    
        /// <summary> Obtiene la instancia del singleton de BibliotecaPersonalizadas - Autor: Tobias Malbos
        /// </summary>
        /// <returns></returns>
        public static IBibliotecaAnimaciones GETInstance() => _instance ??= new BibliotecaPersonalizadas();

        /// <summary> Carga un diccionario con el nombre y la BlockQueue asociada a partir del directorio
        /// de las animaciones personalizadas - Autor: Tobias Malbos
        /// </summary>
        /// <returns></returns>
        /// ACTUALIZACION 5/11/21 Tobias Malbos : Actualizado para que funcione con la AnimacionCompuesta
        private static Dictionary<string, AnimacionCompuesta> CargarAnimaciones()
        {
            var animations = new Dictionary<string, AnimacionCompuesta>();
            string animsDirectory = Application.dataPath + CustomAnimPath;
            string[] files = Directory.GetFiles(animsDirectory, "*.json");
        
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string jsonContent = File.ReadAllText(animsDirectory + fileName + ".json");
                AnimacionCompuesta blockQueue = JsonHelper.FromJson(jsonContent);

                if (blockQueue != null)
                {
                    animations.Add(fileName.Remove(fileName.Length - 1), blockQueue);
                }
            }
            foreach (string key in animations.Keys)
            {
                Debug.Log(key);
            }
            return animations;
        }

        /// <summary> Devuelve una animacion dado su nombre - Autor: Tobias Malbos
        /// </summary>
        /// <param name="name"> Nombre de la animacion </param>
        /// <returns></returns>
        /// ACTUALIZACION 5/11/21 Tobias Malbos : Actualizado para que funcione con la AnimacionCompuesta
        public BlockQueue GETAnimation(string name) => CustomAnimations[name].Animacion;
    }
}