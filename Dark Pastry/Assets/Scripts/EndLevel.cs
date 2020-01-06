using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if(!other.CompareTag("Agata")) return;
        LevelData.LevelCompleted();
    }
}
