using System.Collections.Generic;
using AnimationBlockQueue;
using AnimationDataScriptableObject;

public class BlockQueueGenerator
{
    private const string DEFAULT_TRIGGER = " ";
    public static BlockQueue GetBlockQueue(List<AnimationData> triggerScriptableObjects)
    {
        List<Block> blocks = new List<Block>();
        blocks.Add(new Block());   //El constructor vacio crea la lista de layer info sin necesidad de pasarsela
        foreach (AnimationData tupla in triggerScriptableObjects)
        {
            blocks[0].AddLayerInfo(new LayerInfo(tupla.Trigger));
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
        List<Block> blocks = new List<Block>();
        blocks.Add(new Block());   //El constructor vacio crea la lista de layer info sin necesidad de pasarsela
        Block bloque= new Block();
        foreach (List<AnimationData> lista in triggerScriptableObjects)
        {
            foreach(AnimationData tupla in lista)
            {    
                bloque.AddLayerInfo(new LayerInfo(tupla.Trigger));
            }
            blocks.Add(bloque);
            blocks.Add(new Block(GetCleanBlock()));
        }
        return new BlockQueue(blocks);
    }
}
