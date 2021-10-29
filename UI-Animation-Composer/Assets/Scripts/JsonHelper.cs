using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class JsonHelper
{
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
    
    
    /// <summary> Crea un texto json de un bloque a partir de una lista de bloques con sus triggers - Autor: Tobias Malbos
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
