using System.Collections.Generic;
using UnityEngine;

namespace AnimationComposer
{
    public class AnimationComposer : MonoBehaviour
    {
        private int _animsInProgress;
        private bool _started;

        private readonly BlockQueue _blockQueue = new BlockQueue();
        private Animator _animator;

        public void SignalAnimationComplete() => --_animsInProgress;

        public void AddBlock(Block block) => _blockQueue.Enqueue(block);

        public void AddBlockQueue(BlockQueue blockQueue)
        {
            foreach (Block block in blockQueue.GetBlocks())
            {
                _blockQueue.Enqueue(block);
            }
        }

        public void ClearAnims()     
        {         
            var clear = new List<LayerInfo>();

            for (var l = 1; l < _animator.layerCount; l++)
            {
                clear.Add(new LayerInfo("clear" + _animator.GetLayerName(l)));
            }

            _blockQueue.Clear();
            _animsInProgress = 0;
            _blockQueue.Enqueue(new Block(clear));
        }
        
        private void Start()
        {
            _animator = GetComponent<Animator>(); // Asigno el controller del personaje
            var exitBehaviours = _animator.GetBehaviours<ExitBehaviour>();
            
            foreach (var behaviour in exitBehaviours)
            {
                behaviour.compositionController = this;
            }

            StartAnimations();
        }

        private void Update()
        {
            if (!_started || _blockQueue.IsEmpty() || _animsInProgress != 0) return;
            
            Block currentBlock = _blockQueue.Dequeue();
            ExecuteAnimationBlock(currentBlock);
        }
        
        private void ExecuteAnimationBlock(Block block)
        {
            _animsInProgress = 0;
            
            // Ejecuto el bloque
            foreach (LayerInfo layerInfo in block.GetLayerInfos()) // Por cada trigger de cada capa
            {
                _animator.SetTrigger(layerInfo.DestinyState); // Lo ejecuto
                
                if (!layerInfo.DestinyState.Contains("clear"))
                {
                    ++_animsInProgress;
                }
            }
        }
        
        private void StartAnimations() => _started = true;
    }
}
