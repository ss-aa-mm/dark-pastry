using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Objects Interaction"))
        {
            SceneManager.LoadScene("BakeryScene");
        }
    }
}
