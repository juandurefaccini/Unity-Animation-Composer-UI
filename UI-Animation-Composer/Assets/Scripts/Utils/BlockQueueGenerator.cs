using System.Collections.Generic;
using AnimationComposer;
using AnimationPlayer;

namespace Utils
{
    public static class BlockQueueGenerator
    {
        public static BlockQueue GetBlockQueue(List<AnimationData> triggerScriptableObjects)
        {
            List<Block> blocks = new List<Block> { new Block() };

            foreach (AnimationData tupla in triggerScriptableObjects)
            {
                blocks[0].AddLayerInfo(new LayerInfo(tupla.trigger));
            }
        
            blocks.Add(new Block(GetCleanBlock())); // Agrego un bloque con un trigger por defecto
            return new BlockQueue(blocks);
        }
        public static List<LayerInfo> GetCleanBlock()
        {
            return new List<LayerInfo> {
                new LayerInfo("clearBaseLayer"),
                new LayerInfo("clearTorsoLayer"),
                new LayerInfo("clearBothArmsLayer"),
                new LayerInfo("clearLeftHandLayer"),
                new LayerInfo("clearLeftArmLayer"),
                new LayerInfo("clearRightHandLayer"),
                new LayerInfo("clearRightArmLayer"),
                new LayerInfo("clearLegsLayer"),
                new LayerInfo("clearFaceLayer")};
        }

        public static BlockQueue GetBlockQueue(List<List<AnimationData>> triggerScriptableObjects)
        {
            List<Block> blocks = new List<Block> { new Block() };
            Block bloque= new Block();
            
            foreach (List<AnimationData> lista in triggerScriptableObjects)
            {
                foreach (AnimationData tupla in lista)
                {    
                    bloque.AddLayerInfo(new LayerInfo(tupla.trigger));
                }
                
                blocks.Add(bloque);
                blocks.Add(new Block(GetCleanBlock()));
            }
            
            return new BlockQueue(blocks);
        }
    }
}
