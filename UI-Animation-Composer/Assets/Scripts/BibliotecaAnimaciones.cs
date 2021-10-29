using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnimationBlockQueue;
using UnityEngine;

public class BibliotecaAnimaciones
{
    public static Dictionary<string, Dictionary<string, List<AnimationData>>> animations;

    public BlockQueue getAnimation(string name)
    {
        return null;
    }
    
    public static void CargarAnimaciones()
    {
        //Cargar un mapa de layers
        char dsc = Path.DirectorySeparatorChar; //Directory Separator Char
        animations = new Dictionary<string, Dictionary<string, List<AnimationData>>>();
        string animationpath = Directory.GetCurrentDirectory();
        animationpath = animationpath + dsc + "Assets" + dsc + "Resources" + dsc + "ScriptableObjects" + dsc + "TriggersEmotions";
        // Debug.Log(animationpath);
        string[] layerEntries = Directory.GetDirectories(animationpath);
        //Por cada layer en Resources/ScriptableObjects/TriggersEmotions
        foreach (string layerpath in layerEntries)
        {
            string layer = layerpath.Split(Path.DirectorySeparatorChar).Last();
            animations.Add(layer, new Dictionary<string, List<AnimationData>>());
            string[] emotionEntries = Directory.GetDirectories(layerpath);
            //Por cada emocion en una layer, cargar su coleccion de scriptable objects
            foreach (string emotionpath in emotionEntries)
            {
                string emotion = emotionpath.Split(Path.DirectorySeparatorChar).Last();
                animations[layer].Add(emotion, new List<AnimationData>());
                string resourcespath = "ScriptableObjects/TriggersEmotions";
                try // No es necesario, ahora LoadAll retorna una lista vacia y no null.
                {
                    AnimationData[] listTuplas = (AnimationData[])Resources.LoadAll<AnimationData>(resourcespath + dsc + layer + dsc + emotion);
                    foreach (AnimationData tupla in listTuplas)
                    {
                        animations[layer][emotion].Add(tupla);
                    }
                    // Debug.Log(layer + dsc + emotion + " posee " + animations[layer][emotion].Count + " tuplas");
                }
                catch
                {
                    // Debug.Log("No hay animaciones para " + layer + " en la emocion " + emotion);
                }
            }
        }
    }

    public static Dictionary<string, List<AnimationData>> GetTriggersByEmocion(string emocion) // Object --> Nuevo scriptable Object
    {
        Dictionary<string, List<AnimationData>> retorno = new Dictionary<string, List<AnimationData>>();
        Debug.Log(emocion);
        // layer es de tipo <string, Dictionary> 
        foreach (var layer in animations)
        {
            retorno.Add(layer.Key, animations[layer.Key][emocion]);
        }
        return retorno;
    }
}