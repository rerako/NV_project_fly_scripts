using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotVect : MonoBehaviour {
    public Transform targ1;
    public Vector3 dots1;
    public Vector3 dots2;
    public float dot_numb;


    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        dots1 = transform.TransformDirection(Vector3.forward);
        dots2 = targ1.position - transform.position;
        dot_numb = Vector3.Dot(dots1, dots2);
    }
}
