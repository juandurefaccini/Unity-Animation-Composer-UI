using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Action_ReproducirAnimacion : IGetJson
{
    public String Avatar { get; set; }
    public String Animacion { get; set; }

    public override string GetJson()
    {
        Dictionary<string, string> props = new Dictionary<string, string>();
        props.Add("avatar",Avatar);
        props.Add("animacion",Animacion);
        return JsonHelper.ToJson("Accion_Agarrar",props);
    }
}
