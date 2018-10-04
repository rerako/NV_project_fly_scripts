using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joystick_movement : MonoBehaviour {
    public Vector3 direction;
    public Vector3 follow_dir;
    public Vector3 velocity;

    public float x_axis;
    public float y_axis;
    public float power_axis;
    public float total_velo;

    [Range(0.0f, 1.0f)]
    public float delay;
    public float speed_mult;
    private Rigidbody r_body;

    // Use this for initialization
    void Start () {
        r_body = gameObject.GetComponent<Rigidbody>();

        follow_dir = new Vector3(0, 0, 0 );
    }

    // Update is called once per frame
    void FixedUpdate () {
        velocity = r_body.velocity;
        x_axis = Input.GetAxis("Horizontal");
        y_axis = Input.GetAxis("Vertical");
        power_axis = Mathf.Sqrt(x_axis * x_axis + y_axis * y_axis);

        if (x_axis * x_axis + y_axis * y_axis > 1f)
        {
            x_axis = x_axis / power_axis;
            y_axis = y_axis / power_axis;
        }
        direction = new Vector3(x_axis, 0, y_axis);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            follow_dir = Vector3.Lerp(follow_dir, direction, delay);

            r_body.AddForce(transform.forward * y_axis * speed_mult * Time.deltaTime);
            r_body.AddForce(transform.right * x_axis * speed_mult * Time.deltaTime);


        }
        else
        {
            follow_dir = Vector3.Lerp(follow_dir, Vector3.zero, delay);

        }


    }
}
