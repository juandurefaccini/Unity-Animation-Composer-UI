using System.Collections.Generic;
using System.Linq;

namespace AnimationComposer
{
    public class BlockQueue
    {
        // Cola de prioridad de bloques que nos permite secuencializar las animaciones
        private readonly Queue<Block> _blocks;

        public BlockQueue() => _blocks = new Queue<Block>();
        public BlockQueue(IEnumerable<Block> blocks) => _blocks = new Queue<Block>(blocks);

        // Encolar
        public void Enqueue(Block block) => _blocks.Enqueue(block);

        public Queue<Block> GetBlocks() => _blocks;
        
        // Desencolar
        public Block Dequeue() => _blocks.Dequeue();

        public bool IsEmpty() => _blocks.Count == 0; // Peek agarra la primera, si es null esta vacia

        public void Clear() => _blocks.Clear();

        public override string ToString() => _blocks.Aggregate("", (current, block) => current + (block + "\n"));
    }
    
    public class Block
    {
        // Clase encargada de almacenar los distintos cambios que se le haran a los layers
        private readonly List<LayerInfo> _stateTransitions;
        
        // DEPRECATED
        public Block(List<LayerInfo> stateTransitions) => _stateTransitions = stateTransitions;

        public Block(string stateTransition) => _stateTransitions = new List<LayerInfo> { new LayerInfo(stateTransition) };

        public Block() => _stateTransitions = new List<LayerInfo>();

        // Diccionario para almacenar el layer a editar y el valor del mismo
        public List<LayerInfo> GetLayerInfos() => _stateTransitions;

        public void AddLayerInfo(LayerInfo layerInfo) => _stateTransitions.Add(layerInfo);
    }
    
    public class LayerInfo
    {
        public string DestinyState { get; }

        public LayerInfo(string destinyState) => DestinyState = destinyState;
    }
}

