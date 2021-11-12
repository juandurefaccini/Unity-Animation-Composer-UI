using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnimationComposer;
using UnityEngine; // Para directory
// Para parsear el nombre de la carpeta de un path

namespace AnimationPlayer
{
    public class BibliotecaAtomicas: IBibliotecaAnimaciones
    {
        public static readonly Dictionary<string, Dictionary<string, List<AnimationData>>> AtomicAnimations = CargarAnimaciones();

        private const string AnimationPath = "/Assets/Resources/ScriptableObjects/TriggersEmotions";
        private const string ResourcesPath = "ScriptableObjects/TriggersEmotions/";
        
        private static BibliotecaAtomicas _instance;

        private BibliotecaAtomicas() { }

        /// <summary> Obtiene la instancia del singleton de BibliotecaAtomicas - Autor: Tobias Malbos
        /// </summary>
        /// <returns></returns>
        public static IBibliotecaAnimaciones GETInstance() => _instance ??= new BibliotecaAtomicas();

        private static Dictionary<string, Dictionary<string, List<AnimationData>>> CargarAnimaciones()
        {
            //Cargar un mapa de layers
            var animations = new Dictionary<string, Dictionary<string, List<AnimationData>>>();
            string animationPath = Directory.GetCurrentDirectory() + AnimationPath;
            string[] layerEntries = Directory.GetDirectories(animationPath);
            
            //Por cada layer en Resources/ScriptableObjects/TriggersEmotions
            foreach (string layerPath in layerEntries)
            {
                string layer = layerPath.Split(Path.DirectorySeparatorChar).Last();
                animations.Add(layer, new Dictionary<string, List<AnimationData>>());
                string[] emotionEntries = Directory.GetDirectories(layerPath);
                
                //Por cada emocion en una layer, cargar su coleccion de scriptable objects
                foreach (string emotionPath in emotionEntries)
                {
                    string emotion = emotionPath.Split(Path.DirectorySeparatorChar).Last();
                    animations[layer].Add(emotion, new List<AnimationData>());
                    AnimationData[] listTuplas = Resources.LoadAll<AnimationData>(ResourcesPath + layer + "/" + emotion);
                    
                    foreach (AnimationData tupla in listTuplas)
                    {
                        animations[layer][emotion].Add(tupla);
                    }
                }
            }

            return animations;
        }

        /// <summary> Devuelve una animacion dado su nombre - Autor: Tobias Malbos
        /// </summary>
        /// <param name="name"> Nombre de la animacion </param>
        /// <returns></returns>
        public BlockQueue GETAnimation(string name)
        {
            foreach (var layer in AtomicAnimations)
            {
                foreach (var emotion in layer.Value)
                {
                    foreach (AnimationData animationData in emotion.Value)
                    {
                        if (animationData.trigger == name)
                        {
                            return new BlockQueue(new List<Block> { new Block(animationData.trigger) });
                        }
                    }
                }
            }

            return null;
        }
    }
}