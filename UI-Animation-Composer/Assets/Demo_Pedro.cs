using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_Pedro : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<AnimationPlayer.AnimationPlayer>().PlayAnimation("TraerTarea");
        }
    }
}
