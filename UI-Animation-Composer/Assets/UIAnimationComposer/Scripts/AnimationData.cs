﻿using System;
using UnityEngine;

namespace AnimationDataScriptableObject
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AnimationData", order = 1)]
    public class AnimationData : ScriptableObject
    {
        public double Intensidad;
        public String Trigger;
        public String Nombre;   
    }

}