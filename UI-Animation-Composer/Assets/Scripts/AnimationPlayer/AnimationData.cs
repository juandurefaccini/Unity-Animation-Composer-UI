using UnityEngine;
using UnityEngine.Serialization;

namespace AnimationPlayer
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AnimationData", order = 1)]
    public class AnimationData : ScriptableObject
    {
        [FormerlySerializedAs("Intensidad")] public double intensidad;
        [FormerlySerializedAs("Trigger")] public string trigger;
        [FormerlySerializedAs("Nombre")] public string nombre;   
    }
}