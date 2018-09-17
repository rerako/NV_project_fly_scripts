using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_insidescript : MonoBehaviour {
    public Matrix4x4 cube;// cube space requires rotation, position, scale of object
    public Matrix4x4 inv_cube; // inverse cube comparing
    public Transform cubepoint1;  // cube mid point comparing
    public Transform point1; // point of actual testing
    public Transform point2; // comparing point

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {



        cube = Matrix4x4.TRS(cubepoint1.position, cubepoint1.rotation, cubepoint1.localScale);// takes the transform of the cube and The returned matrix is such that places things at position pos, oriented in rotation q and scaled by s.
        inv_cube = cube.inverse; // invert matrix
        point2.position = inv_cube.MultiplyPoint3x4(point1.position);// takes the position of the tracked object and translates it to the proper axis for x,y,z comparison

        if ((point2.position.x > -0.5f && point2.position.x< 0.5f) && (point2.position.y > -0.5f && point2.position.y< 0.5f) && (point2.position.z > -0.5f && point2.position.z< 0.5f))
        {
            Debug.Log("working");
        }
	}
}
