﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Objects Interaction"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
