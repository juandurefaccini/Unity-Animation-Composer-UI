using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomAnimationLoader : MonoBehaviour
{
    private static string CUSTOM_ANIMS_PATH = Application.dataPath + "/Resources/CustomAnimations/";
    
    // Start is called before the first frame update
    void Start()
    {
        string[] files = Directory.GetFiles(CUSTOM_ANIMS_PATH);

        foreach (string file in files)
        {
            
        }
    }
}
