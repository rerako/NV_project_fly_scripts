using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simple_move : MonoBehaviour {
    public float x_percent = 50;
    public float y_percent = 50;
    public Transform xaxis_Left;
    public Transform xaxis_Right;
    public Transform yaxis_Up;
    public Transform yaxis_Down;
    public Transform scroller;
    public float toggle_speed = 50;
    public float toggle_lag = 1;

    public float scrollx;
    public float scrolly;
    public bool extra_movement;
    public float x_axis_waggle;
    public float y_axis_waggle;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (extra_movement)
        {

        
        y_percent = Mathf.Clamp(y_percent, 0f, 100f);
        x_percent = Mathf.Clamp(x_percent, 0f, 100f);
        if (Input.GetAxis("Horizontal") != 0)
        {
            x_percent += Time.deltaTime * Input.GetAxis("Horizontal") * toggle_speed;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            y_percent += Time.deltaTime * Input.GetAxis("Vertical") * toggle_speed;
        }

        scrolly = y_percent / 100;
        scrollx = x_percent / 100;

        x_axis_waggle = xaxis_Left.localPosition.x + ((xaxis_Right.localPosition.x - xaxis_Left.localPosition.x) * scrollx);
        y_axis_waggle = yaxis_Down.localPosition.y + ((yaxis_Up.localPosition.y - yaxis_Down.localPosition.y) * scrolly);

        scroller.localPosition = Vector3.Lerp(scroller.localPosition, new Vector3(x_axis_waggle, y_axis_waggle,0), Time.deltaTime * toggle_lag);
        }
    }
}
