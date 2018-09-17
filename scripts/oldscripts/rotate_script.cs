using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_script : MonoBehaviour {
    public int x;
    public int y;
    public int z;
    Vector3 rotation;

    // Use this for initialization
    void Start () {
        rotation = new Vector3(x,y,z);
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.Rotate(rotation * Time.deltaTime);
	}
}
