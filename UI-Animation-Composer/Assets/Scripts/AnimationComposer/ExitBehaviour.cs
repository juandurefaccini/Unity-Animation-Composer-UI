using UnityEngine;

namespace AnimationComposer
{
    public class ExitBehaviour : StateMachineBehaviour
    {
        public AnimationComposer compositionController;
        
        private bool _notified;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => _notified = false;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (!(stateInfo.normalizedTime % 1 > 0.99) || _notified) return;
            
            compositionController.SignalAnimationComplete();
            _notified = true; //para que pregunte una sola vez
        }
    }
}
