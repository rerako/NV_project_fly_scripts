using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    public int x;
    public int y;
    public int z;
    Vector3 rotation;
    public Transform pos;
    public LineRenderer beam;
    public float multi;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        multi += Time.deltaTime / 4;
        rotation = new Vector3(   x, -Input.GetAxis("Horizontal") * y, -Input.GetAxis("Vertical") * z);

        transform.Rotate(rotation * multi * Time.deltaTime);
        beam.SetPosition(0, beam.gameObject.transform.position);

        beam.SetPosition(1, pos.position);
    }
}
