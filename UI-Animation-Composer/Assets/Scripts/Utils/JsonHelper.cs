using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AnimationComposer;
using AnimationPlayer;
using Newtonsoft.Json;
using UnityEngine;

namespace Utils
{
    public class JsonHelper
    {
        private const string AtributoIndefinido = "Atributo Indefinido";

        private static string _tabs = "";

        /// <summary> Crea un texto json de una cola de bloques a partir de una animacion compuesta - Autor: Tobias Malbos
        /// </summary>
        /// <param name="compuesta"> Animacion compuesta </param>
        /// <returns></returns>
        /// ACTUALIZACION 6/11/21 Tobias Malbos : Actualizado para que recibe una AnimacionCompuesta como parametro
        public static string ToJson(AnimacionCompuesta compuesta)
        {
            if (compuesta.Animacion.IsEmpty())
            {
                throw new ArgumentException("La cantidad de bloques provistas debe ser mayor a 0");
            }

            string result = "{";
            _tabs += "\t";
            result += "\n" + SerializeBlockQueue(compuesta);
            _tabs = _tabs.Remove(_tabs.Length - 1);
            result += "\n}";
        
            return result;
        }
        /// <summary> Crea un texto json de una cola de bloques a partir de una animacion coordinada - Autor: Facundo Mozo
        /// </summary>
        /// <param name="coordinada"> Animacion coordinada </param>
        /// <returns></returns>
        public static string ToJson(List<AnimacionCoordinada> coordinada)
        {
            if (coordinada[0].Animacion.IsEmpty() || coordinada[1].Animacion.IsEmpty())
            {
                throw new ArgumentException("La cantidad de bloques provistas debe ser mayor a 0");
            }

            string result = "{";
            _tabs += "\t";
            result += "\n"+_tabs+"\"avatar1\" : {";
            _tabs += "\t";
            result += "\n"+ SerializeBlockQueue(coordinada[0]);
            _tabs = _tabs.Remove(_tabs.Length - 1);
            result += "\n" + _tabs + "},";
            result += "\n" + _tabs + "\"avatar2\" : {";
            _tabs += "\t";
            result += "\n" + SerializeBlockQueue(coordinada[1]);
            _tabs = _tabs.Remove(_tabs.Length - 1);
            result += "\n" + _tabs + "}";
            _tabs = _tabs.Remove(_tabs.Length - 1);
            result += "\n}";

            return result;
        }
        
        /// <summary> Crea un texto json de una cola de bloques a partir de una animacion compuesta - Autor: Juan Dure
        /// </summary>
        /// <param name="compuesta"> Animacion compuesta </param>
        /// <returns></returns>
        public static string ToJson(String nombreAccion, Dictionary<string,string> props)
        {
            string result = "\"" + nombreAccion + "\":";
            _tabs += "\t";
            result += "\n" + JsonConvert.SerializeObject(props);
            _tabs = _tabs.Remove(_tabs.Length - 1);
            result += "\n";
        
            return result;
        }

        /// <summary> Dado el contenido de un archivo .json, genera un objeto AnimacionCompuesta. Si el contenido es invalido
        /// se devuelve null - Autor: Tobias Malbos
        /// </summary>
        /// <param name="json"> Contenido del archivo json </param>
        /// <returns></returns>
        /// ACTUALIZACION 5/11/21 Tobias Malbos : Actualizado para que retorne una AnimacionCompuesta (antes devolvia una BlockQueue)
        public static AnimacionCompuesta FromJson(string json)
        {
            if (json[0] != '{' || json[json.Length - 1] != '}')
            {
                Debug.Log("Error. Json mal especificado");
                return null;
            }

            string emocion = GetAttribute(json, "\"emocion\"");
            string cadenaIntensidad = GetAttribute(json, "\"intensidad\"");
            double intensidad = 0D;

            if (cadenaIntensidad != AtributoIndefinido)
            {
                intensidad = double.Parse(cadenaIntensidad, CultureInfo.InvariantCulture);
            }
        
            BlockQueue animacion = new BlockQueue();
            int posicion = json.IndexOf('[');

            while (posicion != 0)
            {
                posicion = json.IndexOf('[', posicion + 1);

                if (posicion == -1)
                {
                    break;
                }
            
                Block block = DeserializeBlock(json, posicion + 1);            
                posicion = json.IndexOf(']', posicion + 1);

                if (block != null)
                {
                    animacion.Enqueue(block);
                }
            }
        
            return new AnimacionCompuesta(emocion, intensidad, animacion);
        }

        /// <summary> Busca dentro del contenido, el atributo pasado por parametro, y retorna el valor asociado. Si el
        /// atributo no existe dentro del json, se devuelve la constante ATRIBUTO_INDEFINIDO - Autor : Tobias Malbos
        /// </summary>
        /// <param name="json"> Contenido del json </param>
        /// <param name="attribute"> Nombre del atributo a buscar </param>
        /// <returns> Valor del atributo </returns>
        private static string GetAttribute(string json, string attribute)
        {
            int posicion = json.IndexOf(attribute, StringComparison.Ordinal);

            if (posicion == -1) return AtributoIndefinido;
            
            int finalAtributo = json.IndexOf('"', posicion);
            posicion = json.IndexOf(':', posicion + 1);
            posicion = json.IndexOf('"', posicion + 1) + 1;
                
            if (posicion != -1 && finalAtributo != -1 && finalAtributo > posicion)
            {
                return json.Substring(posicion, finalAtributo - posicion);
            }

            return AtributoIndefinido;
        }

        /// <summary> Deserializa un bloque dado el contenido y una posicion - Autor: Tobias Malbos
        /// </summary>
        /// <param name="json"> Contenido del archivo json </param>
        /// <param name="posicion"> Posicion dentro del json </param>
        /// <returns></returns>
        private static Block DeserializeBlock(string json, int posicion)
        {
            List<LayerInfo> triggers = new List<LayerInfo>();
            int ultimoCorchete = json.IndexOf(']', posicion);
        
            do
            {
                int primerasComillas = json.IndexOf('"', posicion + 1) + 1;
                int ultimasComillas = json.IndexOf('"', primerasComillas + 1);
                string trigger = json.Substring(primerasComillas, ultimasComillas - primerasComillas);
                triggers.Add(new LayerInfo(trigger));
                posicion = json.IndexOf(',', posicion + 1);
            } while (posicion <= ultimoCorchete && posicion != -1);

            return triggers.Count > 0 ? new Block(triggers) : null;
        }
    
        /// <summary> Serializa una lista de bloques en base a una animacion compuesta - Autor: Tobias Malbos
        /// </summary>
        /// <param name="compuesta"> Animacion compuesta </param>
        /// <returns></returns>
        /// ACTUALIZACION 6/11/21 Tobias Malbos : Actualizado para que reciba una AnimacionCompuesta como parametro y para que guarde la emocion y la intensidad en el json
        private static string SerializeBlockQueue(AnimacionCompuesta compuesta)
        {
            List<Block> blocks = compuesta.Animacion.GetBlocks().ToList();
            string serializedBlockQueue = _tabs + "\"emocion\": \"" + compuesta.Emocion + "\",";
            serializedBlockQueue += "\n" + _tabs + "\"intensidad\": \"" + compuesta.Intensidad + "\",";
            serializedBlockQueue += "\n" + _tabs + "\"bloques\": [";
            _tabs += "\t";

            for (int i = 0; i < compuesta.Animacion.GetBlocks().Count; ++i)
            {
                serializedBlockQueue += "\n" + _tabs + "{";
                _tabs += "\t";
                serializedBlockQueue += "\n" + SerializeBlock(blocks[i]);
                _tabs = _tabs.Remove(_tabs.Length - 1);
                serializedBlockQueue += "\n" + _tabs + "},";
            }

            serializedBlockQueue = serializedBlockQueue.Remove(serializedBlockQueue.Length - 1);
            _tabs = _tabs.Remove(_tabs.Length - 1);
            serializedBlockQueue += "\n" + _tabs + "]";

            return serializedBlockQueue;
        }

        /// <summary> Serializa una lista de bloques en base a una animacion compuesta - Autor: Facundo Mozo
        /// </summary>
        /// <param name="coordinada"> Animacion coordinada </param>
        /// <returns></returns>
        private static string SerializeBlockQueue(AnimacionCoordinada coordinada)
        {
            List<Block> blocks = coordinada.Animacion.GetBlocks().ToList();
            string serializedBlockQueue = _tabs + "\"avatar\": \"" + coordinada.Avatar + "\",";
            serializedBlockQueue += "\n" + _tabs + "\"desfase\": \"" + coordinada.Desfase + "\",";
            serializedBlockQueue += "\n" + _tabs + "\"bloques\": [";
            _tabs += "\t";

            for (int i = 0; i < coordinada.Animacion.GetBlocks().Count; ++i)
            {
                serializedBlockQueue += "\n" + _tabs + "{";
                _tabs += "\t";
                serializedBlockQueue += "\n" + SerializeBlock(blocks[i]);
                _tabs = _tabs.Remove(_tabs.Length - 1);
                serializedBlockQueue += "\n" + _tabs + "},";
            }

            serializedBlockQueue = serializedBlockQueue.Remove(serializedBlockQueue.Length - 1);
            _tabs = _tabs.Remove(_tabs.Length - 1);
            serializedBlockQueue += "\n" + _tabs + "]";

            return serializedBlockQueue;
        }
    
        /// <summary> Serializa un bloque en funcion de los triggers que lo componen - Autor: Tobias Malbos
        /// </summary>
        /// <param name="bloque"></param>
        /// <returns></returns>
        /// ACTUALIZACION 6/11/21 Tobias Malbos : Actualizado para que reciba un Block como parametro
        private static string SerializeBlock(Block bloque)
        {
            string serializedBlock = "";
            List<LayerInfo> triggers = bloque.GetLayerInfos();

            if (triggers.Count <= 0) return serializedBlock;
            
            serializedBlock += _tabs + "\"triggers\": [";
            _tabs += "\t";
        
            foreach (LayerInfo trigger in triggers)
            {
                serializedBlock += "\n" + _tabs + "\"" + trigger.DestinyState + "\",";
            }

            serializedBlock = serializedBlock.Remove(serializedBlock.Length - 1);
            _tabs = _tabs.Remove(_tabs.Length - 1);
            serializedBlock += "\n" + _tabs + "]";

            return serializedBlock;
        }
    }
}
