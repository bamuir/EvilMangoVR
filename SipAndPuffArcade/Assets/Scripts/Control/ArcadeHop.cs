using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is used on empty game objects inside of arcade games to create a path back to the main arcade/new game selection
// should probably be integrated with the game swtich script since we figured out how to create objects that persist through scenes in unity
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
