using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Action_DarObjeto : IGetJson
{
    public String Avatar_A { get; set; }
    public String Avatar_B { get; set; }
    public String Animacion { get; set; }

    public override string GetJson()
    {
        Dictionary<string, string> props = new Dictionary<string, string>();
        props.Add("avatar_a",Avatar_A);
        props.Add("avatar_b",Avatar_B);
        props.Add("animacion",Animacion);
        return JsonHelper.ToJson("Accion_DarObjeto",props);
    }
}
