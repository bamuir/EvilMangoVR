using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArcadeHop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TranslationLayer.instance.GetButtonDown(ButtonCode.KeyExit))
        {
            SceneManager.LoadScene("AlphaArcade", LoadSceneMode.Single);
        }
    }
}
