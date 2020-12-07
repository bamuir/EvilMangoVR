using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetObjects : MonoBehaviour
{
    List<GameObject> collectibles;

    private void Start()
    {
        ResetGame.ResetCoinsAndPotions += ResetCollectibles;
        //collectibles = new List<GameObject>();

        collectibles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Coin"));
        collectibles.AddRange(GameObject.FindGameObjectsWithTag("Potion"));
    }

    void ResetCollectibles()
    {
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(true);
        }
    }
}
