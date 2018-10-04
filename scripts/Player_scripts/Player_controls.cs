using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controls : MonoBehaviour
{
    public Rigidbody testbod;
    public Transform forward;
    public float forward_Speed;
    public float boost_max;
    float boost_Speed;
    public Transform orb_point;
    public float rotate_Speed;
    private float rotate_norm;
    public Transform body;
    public float fix_angle_Timer;



    // Use this for initialization
    void Start()
    {
        rotate_norm = rotate_Speed * Time.deltaTime;
        testbod = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //testbod.AddForce(gameObject.transform.forward * 0.00001f * Time.deltaTime);
        transform.position += transform.forward * Time.deltaTime * forward_Speed * boost_Speed;
        orb_point.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, 0);
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);

        if (Input.GetAxis("Horizontal") != 0)
        {

            //transform.Rotate(Vector3.forward * Input.GetAxis("Horizontal") * -rotate_norm * 2, Space.Self);
            transform.Rotate(0,Input.GetAxis("Horizontal") * rotate_norm * 1f,0);

        }
        if (Input.GetAxis("Vertical") != 0 )
        {
            if (transform.eulerAngles.x > 285 || transform.eulerAngles.x < 75f)
            {
                transform.Rotate(Input.GetAxis("Vertical") * rotate_norm * 2, 0, 0);
            }
            else if (transform.eulerAngles.x <= 285 && transform.eulerAngles.x > 240f)
            {
                transform.eulerAngles = new Vector3(285f, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else if (transform.eulerAngles.x >= 75f && transform.eulerAngles.x < 120f)
            {
                transform.eulerAngles = new Vector3(75, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            else { }
        }
        
        if (transform.rotation != orb_point.rotation && (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0))
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, orb_point.rotation, Time.deltaTime * 2.5f);
        }
        

        if (Input.GetKey(KeyCode.Mouse1))
        {
            body.Rotate(0, 0, 10f);
            fix_angle_Timer = 2;
        }
        else if(!Input.GetKey(KeyCode.Mouse1) && fix_angle_Timer > 0)
        {
            if (fix_angle_Timer >= 0)
            {
                body.rotation = Quaternion.Slerp(body.rotation, transform.rotation, (2 - fix_angle_Timer) );
            }
            fix_angle_Timer -= Time.deltaTime;

        }
        else if (!Input.GetKey(KeyCode.Mouse1) && Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            body.rotation = Quaternion.Slerp(body.rotation, transform.rotation, Time.deltaTime * 0.5f);

        }
        
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Mouse1))
        {
            boost_Speed = boost_max;
            //Time.timeScale = 0.5f;
        }
        else
        {
            boost_Speed = 1;
        }
    }
}
