using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Cambiado por Pedro Procopio para que funcione correctamente el Nuevo ScriptableObject
namespace AnimationDatax
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TuplaScriptableObject", order = 1)]
    public class TuplaScriptableObject : ScriptableObject
    {
        public double[] Vector;
        public String Trigger;
    }

}