using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_script : MonoBehaviour {
    public float cam_slide;
    public float cam_slide2;
    public Transform player;
    public Transform Right;
    public Transform Left;
    public Transform up;
    public Transform down;
    public float cam_follow_speed;
    public float cam_move_speed;
    public Transform campoint;
    public Quaternion rotation;
    public Vector3 velocity = Vector3.zero;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        cam_slide = Input.GetAxis("Horizontal") + 1;
        cam_slide2 = Input.GetAxis("Vertical") + 1;
        cam_slide2 = Mathf.Clamp(cam_slide2, 0f, 2f);
        cam_slide = Mathf.Clamp(cam_slide, 0f,2f);


        campoint.position = Vector3.Lerp(campoint.position, up.position + (down.position - up.position) * (cam_slide2 / 2), cam_move_speed);
        campoint.position = Vector3.Lerp(campoint.position, Left.position + (Right.position - Left.position) * (cam_slide / 2), cam_move_speed);
        transform.position = Vector3.Lerp(transform.position,campoint.position, cam_follow_speed * Time.deltaTime );
        //transform.position = Vector3.SmoothDamp(transform.position, up.position + (down.position - up.position) * (cam_slide2 / 2), ref velocity, cam_move_speed);

        transform.LookAt(player);

    }
}
