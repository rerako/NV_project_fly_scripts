using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_button : MonoBehaviour {
    Scene loadedLevel;
    // Use this for initialization
    void Start ()
    {
        loadedLevel = SceneManager.GetActiveScene();

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButton("Submit"))
        {
            SceneManager.LoadScene(loadedLevel.buildIndex);

        }

    }
}
