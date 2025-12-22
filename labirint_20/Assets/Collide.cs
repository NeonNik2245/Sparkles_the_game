using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour
{
    public StumpMinigame minigame;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            minigame.ActivateMinigame();
        }
    }
}
