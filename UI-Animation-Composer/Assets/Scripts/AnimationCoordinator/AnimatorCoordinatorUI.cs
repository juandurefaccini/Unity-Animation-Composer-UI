using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AnimationComposerUI;
using AnimationComposer;
using AnimationPlayer;
using Utils;


namespace AnimationCreator
{
    public class AnimatorCoordinatorUI : MonoBehaviour
    {
        public GameObject avatar1;
        public GameObject avatar2;
        public GameObject slider1;
        public GameObject slider2;
        public GameObject columna1;
        public GameObject columna2;
        private GameObject[] animacionesSeleccionadas;
        
        

        
        
        public void reproducirAnimacionCordinada(float offset1, float offset2, string animacion1, string animacion2)
        {
            BlockQueue blockQueueAvatar1 = GenerateBlockQueue(animacion1);
            blockQueueAvatar1.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
            BlockQueue blockQueueAvatar2 = GenerateBlockQueue(animacion2);
            blockQueueAvatar2.Enqueue(new Block(BlockQueueGenerator.GetCleanBlock()));
            float diferencia = 0;
            if ( offset1 < offset2)
            {
                diferencia = offset2 - offset1;
                StartCoroutine(wait(offset1,blockQueueAvatar1,avatar1));
                //avatar1.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueueAvatar1);
                StartCoroutine(wait(offset1+diferencia,blockQueueAvatar2,avatar2));
                //StartCoroutine(wait(diferencia));
                //avatar2.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueueAvatar2);
            }
            else
            {
                if (offset1 == offset2)
                {
                    StartCoroutine(wait(offset1,blockQueueAvatar1,avatar1));
                    StartCoroutine(wait(offset1,blockQueueAvatar2,avatar2));
                    //StartCoroutine(wait(offset1));
                    //avatar1.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueueAvatar1);
                    //avatar2.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueueAvatar2);
                }
                else
                {
                    if (offset1 > offset2)
                    {
                        diferencia = offset1 - offset2;
                        StartCoroutine(wait(offset2,blockQueueAvatar2,avatar2));
                        StartCoroutine(wait(offset2+diferencia,blockQueueAvatar1,avatar1));
                        //StartCoroutine(wait(diferencia));
                        //avatar2.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueueAvatar2);
                        //StartCoroutine(wait(offset1));
                        //avatar1.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(blockQueueAvatar1);
                    }
                }
            }
        }

        IEnumerator wait(float tiempo,BlockQueue queue, GameObject avatar)
        {
            yield return new WaitForSecondsRealtime(tiempo);
            avatar.GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation(queue);
        }



        public void previewAnimacionCordinada()
        {
            float off1 = slider1.transform.GetComponent<Slider>().value;
            float off2 = slider2.transform.GetComponent<Slider>().value;
            GameObject colum1 = columna1.GetComponent<Columna>()._ultimoBoton;
            string anim1 = "";
            if (colum1 != null)
                anim1 = colum1.GetComponentInChildren<TextMeshProUGUI>().text;  
            GameObject colum2 = columna2.GetComponent<Columna>()._ultimoBoton;
            string anim2 = "";
            if (colum2 != null)
                anim2 = colum2.GetComponentInChildren<TextMeshProUGUI>().text;  
            
            reproducirAnimacionCordinada(off1, off2, anim1, anim2);
            //reproducirAnimacionCordinada(off1, off2, "Gesto enojo", "Gesto enojo");

        }


        /// <summary> Se reproduce la animacion creada hasta el momento - Autor : Tobias Malbos
            /// </summary>

            /// <summary> Genera una BlockQueue con las animaciones compuestas seleccionadas - Autor : Tobias Malbos
            /// </summary>
            /// <returns></returns>
            public BlockQueue GenerateBlockQueue(string animacion)
            {
                BlockQueue blockQueue = new BlockQueue();
                Queue<Block> bloques =BibliotecaPersonalizadas.GETInstance().GETAnimation(animacion).GetBlocks();  
                foreach (Block block in bloques)
                {
                    blockQueue.Enqueue(block);
                }
                return blockQueue;
            }
    }
}
