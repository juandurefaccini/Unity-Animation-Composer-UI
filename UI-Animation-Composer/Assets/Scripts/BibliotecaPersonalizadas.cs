using System.IO; // Para directory
using System.Linq; // Para parsear el nombre de la carpeta de un path
using System.Collections.Generic;
using AnimationBlockQueue;
using UnityEngine;

public class BibliotecaPersonalizadas: BibliotecaAnimaciones
{
    public static Dictionary<string, BlockQueue> custom_animations = CargarAnimaciones();

    private static BibliotecaPersonalizadas _instance = null;
    
    public static BibliotecaAnimaciones getInstance()
    {
        if (_instance == null)
        {
            _instance = new BibliotecaPersonalizadas();
        }

        return _instance;
    }
    public static Dictionary<string, BlockQueue> CargarAnimaciones()
    {
        return null;
    }

    public BlockQueue getAnimation(string name)
    {
        throw new System.NotImplementedException();
    }
}