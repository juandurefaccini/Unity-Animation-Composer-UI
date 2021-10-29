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
    /// <param name="triggers"> Triggers que constituyen al bloque </param>
    /// <returns></returns>
    public static string ToJson(string nombreBloque, List<string> triggers)
    {
        string result = "{";
        _tabs += "\t";

        if (triggers.Count > 0)
        {
            result += "\n" + SerializeBlock(nombreBloque, triggers) + "\n";
        }
        
        _tabs = _tabs.Remove(_tabs.Length - 1);
        result += "}";
        
        return result;
    }

    /// <summary> Serializa un bloque en funcion de los triggers que lo componen - Autor: Tobias Malbos
    /// </summary>
    /// <param name="nombreBloque"> Nombre del bloque </param>
    /// <param name="triggers"> Triggers que constituyen al bloque </param>
    /// <returns></returns>
    private static string SerializeBlock(string nombreBloque, List<string> triggers)
    {
        string serializedBlock = _tabs + nombreBloque + ":[";
        _tabs += "\t";
        
        foreach (string trigger in triggers)
        {
            serializedBlock += "\n" + _tabs + trigger + ",";
        }

        serializedBlock = serializedBlock.Remove(serializedBlock.Length - 1);
        _tabs = _tabs.Remove(_tabs.Length - 1);
        serializedBlock += "\n" + _tabs + "]";

        return serializedBlock;
    }
}
