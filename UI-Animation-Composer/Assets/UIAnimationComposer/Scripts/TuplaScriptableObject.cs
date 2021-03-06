using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnimationData
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TuplaScriptableObject", order = 1)]
    public class TuplaScriptableObject : ScriptableObject
    {
        public double[] Vector;
        public String Trigger;
    }

}