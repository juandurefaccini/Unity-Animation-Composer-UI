using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Action_Mover : IGetJson
{
    public String Avatar { get; set; }
    public String Ubicacion { get; set; }

    public override string GetJson()
    {
        Dictionary<string, string> props = new Dictionary<string, string>();
        props.Add("avatar",Avatar);
        props.Add("ubicacion",Ubicacion);
        return JsonHelper.ToJson("Accion_Moverse",props);
    }
}