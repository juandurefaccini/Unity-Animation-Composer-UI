using System.IO; // Para directory
using System.Collections.Generic;
using AnimationBlockQueue;
using UnityEngine;

public class BibliotecaPersonalizadas: BibliotecaAnimaciones
{
    public static readonly Dictionary<string, BlockQueue> CustomAnimations = CargarAnimaciones();

    private const string CUSTOM_ANIM_PATH = "/Resources/CustomAnimations/";
    
    private static BibliotecaPersonalizadas _instance;
    
    /// <summary> Obtiene la instancia del singleton de BibliotecaPersonalizadas - Autor: Tobias Malbos
    /// </summary>
    /// <returns></returns>
    public static BibliotecaAnimaciones getInstance()
    {
        if (_instance == null)
        {
            _instance = new BibliotecaPersonalizadas();
        }

        return _instance;
    }

    /// <summary> Carga un diccionario con el nombre y la BlockQueue asociada a partir del directorio
    /// de las animaciones personalizadas - Autor: Tobias Malbos
    /// </summary>
    /// <returns></returns>
    private static Dictionary<string, BlockQueue> CargarAnimaciones()
    {
        Dictionary<string, BlockQueue> animations = new Dictionary<string, BlockQueue>();
        string animsDirectory = Application.dataPath + CUSTOM_ANIM_PATH;
        string[] files = Directory.GetFiles(animsDirectory, "*.json");
        
        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string jsonContent = File.ReadAllText(animsDirectory + fileName + ".json");
            BlockQueue blockQueue = JsonHelper.fromJson(jsonContent);

            if (blockQueue != null)
            {
                animations.Add(fileName, blockQueue);
            }
        }

        return animations;
    }

    /// <summary> Devuelve una animacion dado su nombre - Autor: Tobias Malbos
    /// </summary>
    /// <param name="name"> Nombre de la animacion </param>
    /// <returns></returns>
    public BlockQueue getAnimation(string name)
    {
        return CustomAnimations[name];
    }
}