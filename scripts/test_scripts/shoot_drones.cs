using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot_drones : MonoBehaviour {
    public Transform target;
    public GameObject path_drone;
    public GameObject motherbase;
    public float start_up_timer;
    // Use this for initialization
    void Start () {
        Screen.lockCursor = true;
        //motherbase = gameObject.GetComponent<guider>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        float mouseInputx = Input.GetAxis("Mouse X");
        float mouseInputy = Input.GetAxis("Mouse Y");
        Vector3 lookhere = new Vector3(mouseInputy, mouseInputx, 0);
        transform.Rotate(lookhere);
        if (Input.GetMouseButtonDown(0))
        {
            GameObject drone = Instantiate(path_drone, transform.position, transform.rotation) as GameObject;
            GameObject mbase = Instantiate(motherbase, transform.position, transform.rotation) as GameObject;
            flight ai = drone.GetComponent<flight>();
            ai.setTarg(target);
            ai.setcore(mbase.GetComponent<guider>());

        }
    }


}
