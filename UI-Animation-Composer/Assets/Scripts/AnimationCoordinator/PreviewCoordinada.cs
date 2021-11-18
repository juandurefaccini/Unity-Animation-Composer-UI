using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AnimationComposer;
using AnimationPlayer;
using Utils;


public class PreviewCoordinada : MonoBehaviour
{
    /// <summary> Metodo que reproduce una preview de la animacion coordinada con los valores parametros. Genera los blockqueues correspondientes a cada animacion custom y inicia dos corrutinas para ejecutarlas en cada avatar.
    /// Autor : Facundo Mozo
    /// </summary>
    public void reproducirAnimacionCordinada(GameObject avatar1, GameObject avatar2, float offset1, float offset2, string animacion1, string animacion2)
    {
        BlockQueue blockQueueAvatar1 = GenerateBlockQueue(animacion1);
        blockQueueAvatar1.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
        BlockQueue blockQueueAvatar2 = GenerateBlockQueue(animacion2);
        blockQueueAvatar2.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
        StartCoroutine(wait(offset1, blockQueueAvatar1, avatar1));
        StartCoroutine(wait(offset2, blockQueueAvatar2, avatar2));     
    }

    public BlockQueue GenerateBlockQueue(string animacion)
    {
        BlockQueue blockQueue = new BlockQueue();
        Queue<Block> bloques = BibliotecaPersonalizadas.GETInstance().GETAnimation(animacion).GetBlocks();
        foreach (Block block in bloques)
        {
            blockQueue.Enqueue(block);
        }
        return blockQueue;
    }

    /// <summary> Metodo utilizado para reproducir la animacion de un avatar con un timepo de desfase
    /// Autor : Facundo Mozo
    /// </summary>
    IEnumerator wait(float tiempo, BlockQueue queue, GameObject avatar)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        avatar.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(queue);
    }
}
