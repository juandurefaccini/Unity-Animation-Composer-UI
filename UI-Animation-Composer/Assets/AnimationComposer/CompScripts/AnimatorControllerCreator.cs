/* COMENTADO YA QUE DABA ERROR A LA HORA DE BUILDEAR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Serialization;

public class AnimatorControllerCreator : MonoBehaviour
{
    [Serializable] public struct StateDefinition
    {
        public string stateName;
        public Motion stateAnimation;
        public string parameterName;
    }
    [Serializable] public struct LayerDefinition
    {
        public string layerName;
        public StateDefinition[] stateDefinitions;
        public AvatarMask mask;

    }

    public LayerDefinition[] layers;
    public string controllerName = "default";
    void Start()
    {
        CreateController();
    }

    void CreateController()
    {
        var controller =
            AnimatorController.CreateAnimatorControllerAtPath(
                ("Assets/" + controllerName + ".controller"));
        
        //capa base
        var defaultState = controller.layers[0].stateMachine.AddState(layers[0].stateDefinitions[0].stateName);
        defaultState.motion = layers[0].stateDefinitions[0].stateAnimation;
        controller.layers[0].stateMachine.defaultState = defaultState;
        
        //otras capas
        for (int i = 1; i < layers.Length; i++)
        {
            var currentLayerDef = layers[i];
            controller.AddLayer(currentLayerDef.layerName); //la layer queda con numero i
            
            //para que Unity nos deje modificar las propiedades de controller.layers
            AnimatorControllerLayer[] l = controller.layers;
            l[i].avatarMask = currentLayerDef.mask;
            l[i].defaultWeight = 1;
            controller.layers = l;
            
            var currentStateMachine = controller.layers[i].stateMachine;
            
            //estados
            var layerEmptyState = currentStateMachine.AddState("empty");
            currentStateMachine.defaultState = layerEmptyState;
            //layerEmptyState.AddStateMachineBehaviour<ReadyBehaviour>();
            var clearTransition = currentStateMachine.AddAnyStateTransition(layerEmptyState);
            string clearLayerAnimationParameter = "clear" + layers[i].layerName;
            controller.AddParameter(clearLayerAnimationParameter, AnimatorControllerParameterType.Trigger);
            clearTransition.AddCondition(AnimatorConditionMode.If, 0, clearLayerAnimationParameter);

            foreach (var stateDefinition in currentLayerDef.stateDefinitions)
            {
                //el parametro para ir de ready al nuevo estado
                controller.AddParameter(stateDefinition.parameterName, AnimatorControllerParameterType.Trigger);
                //agrega el nuevo estado con su nombre y  animacion
                var state = currentStateMachine.AddState(stateDefinition.stateName);
                state.motion = stateDefinition.stateAnimation;
                //agrega la transicion del ready al estado agregado
                var transition = currentStateMachine.AddAnyStateTransition(state);
                transition.AddCondition(AnimatorConditionMode.If, 0, stateDefinition.parameterName);
                transition.hasExitTime = true;
                transition.exitTime = 1;
                state.AddStateMachineBehaviour<ExitBehaviour>();
            }
        }
    }
}
*/
