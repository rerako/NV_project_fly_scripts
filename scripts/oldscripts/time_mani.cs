using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class time_mani : MonoBehaviour
{
    [Range(0f, 1f)]public float time_scale;
    //public system_options note;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = time_scale;
            //Debug.Log("working");
        }
        else
        {

         Time.timeScale = 1;
        }
    }
}
