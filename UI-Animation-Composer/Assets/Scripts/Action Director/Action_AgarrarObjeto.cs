using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Utils;

public class Action_AgarrarObjeto : IGetJson
{
    public String Avatar { get; set; }
    public String Objeto { get; set; }
    public String Animacion { get; set; }

    public override string GetJson()
    {
        Dictionary<string, string> props = new Dictionary<string, string>();
        props.Add("avatar",Avatar);
        props.Add("objeto",Objeto);
        props.Add("animacion",Animacion);
        return JsonHelper.ToJson("Accion_Agarrar",props);
    }
}
