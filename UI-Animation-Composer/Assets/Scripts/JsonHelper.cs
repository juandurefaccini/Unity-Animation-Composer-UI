using System;
using System.Collections.Generic;
using System.Globalization;
using AnimationBlockQueue;
using AnimationComposerUI;
using UnityEngine;

public class JsonHelper
{
    public static string ATRIBUTO_INDEFINIDO = "Atributo Indefinido";
    
    private static string _tabs = "";

    /// <summary> Crea un texto json de un bloque a partir de una lista de triggers - Autor: Tobias Malbos
    /// </summary>
    /// <param name="nombreBloque"> Nombre del bloque </param>
    /// <param name="triggers"> Lista que contiene todos los triggers de un bloque </param>
    /// <returns></returns>
    public static string ToJson(string nombreBloque, List<string> triggers)
    {
        return ToJson(new List<string>{ nombreBloque }, new List<List<string>>{ triggers });
    }
    
    /// <summary> Crea un texto json de una cola de bloques a partir de una lista de bloques con sus triggers - Autor: Tobias Malbos
    /// </summary>
    /// <param name="nombreBloques"> Nombre de cada uno de los bloques </param>
    /// <param name="blocks"> Lista que contiene todos bloques con sus respectivos triggers </param>
    /// <returns></returns>
    public static string ToJson(List<string> nombreBloques, List<List<string>> blocks)
    {
        if (blocks.Count == 0)
        {
            throw new ArgumentException("La cantidad de bloques provistas debe ser mayor a 0");
        }
        
        if (nombreBloques.Count != blocks.Count)
        {
            throw new ArgumentException("La cantidad de nombres para los bloques provistas no coincide " +
                                           "con la cantidad de bloques con triggers dados");
        }
        
        string result = "{";
        _tabs += "\t";
        result += "\n" + SerializeBlockQueue(nombreBloques, blocks);
        _tabs = _tabs.Remove(_tabs.Length - 1);
        result += "\n}";
        
        return result;
    }

    /// <summary> Dado el contenido de un archivo .json, genera un objeto AnimacionCompuesta. Si el contenido es invalido
    /// se devuelve null - Autor: Tobias Malbos
    /// </summary>
    /// <param name="json"> Contenido del archivo json </param>
    /// <returns></returns>
    /// ACTUALIZACION 5/11/21 Tobias Malbos : Actualizado para que retorne una AnimacionCompuesta (antes devolvia una BlockQueue)
    public static AnimacionCompuesta fromJson(string json)
    {
        if (json[0] != '{' || json[json.Length - 1] != '}')
        {
            Debug.Log("Error. Json mal especificado");
            return null;
        }

        string emocion = GetAtribute(json, "\"emocion\"");
        string cadenaIntensidad = GetAtribute(json, "\"intensidad\"");
        double intensidad = 0D;

        if (cadenaIntensidad != ATRIBUTO_INDEFINIDO)
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
    /// <param name="atribute"> Nombre del atributo a buscar </param>
    /// <returns> Valor del atributo </returns>
    private static string GetAtribute(string json, string atribute)
    {
        int posicion = json.IndexOf(atribute, StringComparison.Ordinal);

        if (posicion != -1)
        {
            int finalAtributo;
            
            posicion = json.IndexOf(':', posicion + 1);
            posicion = json.IndexOf('"', posicion + 1) + 1;
            finalAtributo = json.IndexOf('"', posicion);

            if (posicion != -1 && finalAtributo != -1 && finalAtributo > posicion)
            {
                return json.Substring(posicion, finalAtributo - posicion);
            }
        }
        
        return ATRIBUTO_INDEFINIDO;
    }

    /// <summary> Deserializa un bloque dado el contenido y una posicion - Autor: Tobias Malbos
    /// </summary>
    /// <param name="json"> Contenido del archivo json </param>
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

        if (triggers.Count > 0)
        {
            return new Block(triggers);
        }
        
        return null;
    }
    
    /// <summary> Serializa una lista de bloques en base a una lista de listas de triggers y los nombres de
    /// los respectivos bloques - Autor: Tobias Malbos
    /// </summary>
    /// <param name="nombreBloques"> Lista que contiene los nombres de los bloques </param>
    /// <param name="blocks"> Lista con todos los triggers por cada bloque </param>
    /// <returns></returns>
    private static string SerializeBlockQueue(List<string> nombreBloques, List<List<string>> blocks)
    {
        string serializedBlockQueue = _tabs + "\"bloques\": [";
        _tabs += "\t";

        for (int i = 0; i < nombreBloques.Count; ++i)
        {
            serializedBlockQueue += "\n" + _tabs + "{";
            _tabs += "\t";
            serializedBlockQueue += "\n" + SerializeBlock(nombreBloques[i], blocks[i]);
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
    /// <param name="nombreBloque"> Nombre del bloque </param>
    /// <param name="triggers"> Triggers que constituyen al bloque </param>
    /// <returns></returns>
    private static string SerializeBlock(string nombreBloque, List<string> triggers)
    {
        if (triggers.Count < IngresoNombre.MIN_ATOMICAS)
        {
            throw new ArgumentException(
                "La cantidad de triggers debe ser mayor o igual a " + IngresoNombre.MIN_ATOMICAS);
        }
        
        string serializedBlock = _tabs + "\"nombre_bloque\": \"" + nombreBloque + "\",";
        serializedBlock += "\n" + _tabs + "\"triggers\": [";
        _tabs += "\t";
        
        foreach (string trigger in triggers)
        {
            serializedBlock += "\n" + _tabs + "\"" + trigger + "\",";
        }

        serializedBlock = serializedBlock.Remove(serializedBlock.Length - 1);
        _tabs = _tabs.Remove(_tabs.Length - 1);
        serializedBlock += "\n" + _tabs + "]";

        return serializedBlock;
    }
}
