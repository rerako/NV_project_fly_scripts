using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Movement : MonoBehaviour {
    /*
     * Mainly for keyboard and mouse controls
     * 
     * To dos:
     *  Long term:
     *      fine tune everything:
     *  Short Term:
     *      make wall grind stick to wall, for forward movement.
     *      add in air controls.
     *      create easy UI for experimentation.
     *      Jump down speed toggle.
     * 
     * 
     * 
    */


  
     public Vector3 direction;
     public Vector3 follow_dir;

     public float x_axis;
     public float y_axis;
     public float power_axis;
     public float total_velo;

     [Range(0.0f, 1.0f)]
     public float delay;
     public float speed_mult;


    public Transform forward;
    public float speed_mult_side;
    public float speed_mult_forw;
    public float speed_mult_turn;
    public float total_speed_square;
    public float speed_snap;
    public float wall_jump_force_side;
    public float wall_jump_force_up;
    public float extra_wall_jump_force;
    public float timer;
    public float drag_timer;
    public float fall_down_force;
    public float fall_down_force_max;
    public float fall_down_force_min;

    public float jump_speed;

    public int speed_snap_limit;

    public Vector3 velocity;
    private Vector3 euler_angles;

    private Rigidbody r_body;

    public bool ground;
    public bool walljump;
    public bool wall_run_R;
    public bool wall_run_L;

    public bool jump;
    public bool first_contact = true;
    public bool drag_snap;


    public RaycastHit left_hit;
    public RaycastHit right_hit;
    public RaycastHit down_hit;


    // Use this for initialization
    void Start ()
    {
        r_body = gameObject.GetComponent<Rigidbody>();
        extra_wall_jump_force = 1;
        follow_dir = Vector3.zero;


    }

    // Update is called once per frame
    private void Update()
    {
        // controller axis input
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

        // timer zones;

        if (drag_timer < 1 && drag_snap)
        {
            drag_timer += Time.deltaTime * 5;

        }
        else
        {
            drag_snap = false;
        }
        if (timer > 0 ) {
            timer -= Time.deltaTime;
            walljump = true;

        }
        else
        {
            walljump = false;
        }

        //Mathf.Clamp(extra_wall_jump_force, 1, 5);
        // turn object
        //transform.Rotate(0, Input.GetAxis("Horizontal") * speed_mult_turn * Time.deltaTime, 0);
    }
    void FixedUpdate ()
    {
        //gizmo bug testing
        Debug.DrawRay(transform.position, transform.right * -0.75f, Color.blue);
        Debug.DrawRay(transform.position, transform.right * 0.75f, Color.blue);
        Debug.DrawRay(forward.position, forward.right * -1.5f, Color.blue);
        Debug.DrawRay(forward.position, forward.right * 1.5f, Color.blue);



        //tracking
        velocity = r_body.velocity;
        total_speed_square = velocity.x * velocity.x + velocity.z * velocity.z;
        Debug.DrawRay(transform.position, transform.up * -1.5f, Color.blue);
        if (Physics.Raycast(transform.position, transform.up * -1, out down_hit, 1.5f))
        {
            if (down_hit.transform.CompareTag("terrain"))
            {
                ground = true;
                jump = false;
            }

        }
        else
        {
            ground = false;
        }

        //if on ground
        if (ground)
        {
            fall_down_force = 0;
            if (r_body.drag <= 1)
            {
                drag_snap = true;
                r_body.drag = drag_timer;
            }
            else
            {

                drag_timer = 0;
            }
            if (total_speed_square < speed_snap_limit)
            {
                speed_snap = 1.5f;
            }
            else
            {
                speed_snap = 1;
            }
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
            //if grounded
            if (Input.GetButton("Jump"))
            {
                r_body.velocity = new Vector3(velocity.x, jump_speed, velocity.z);
            }
        }
        /* rail grinding
         * 
        else if () { }
        */
        // if in air
        else
        {
            r_body.drag = 0;
            drag_snap = false;
            Debug.DrawRay(transform.position, transform.right * -0.75f, Color.blue);
            Debug.DrawRay(transform.position, transform.right * 0.75f, Color.blue);
            //total speed wall jump limitations
            if(total_speed_square > 75)
            {
                //left side wall jump
                //Physics.SphereCast(transform.position, 0.5f, transform.right * -1, out left_hit, 0.8f
                if (Physics.Raycast(transform.position, transform.right * -1, out left_hit, 0.8f) && walljump == false)
                {
                    if (left_hit.transform.gameObject.transform.CompareTag("wall"))
                    {
                        wall_run_L = true;
                        if (Physics.Raycast(forward.position, forward.right * -1, out left_hit, 1.5f) && first_contact)
                        {
                            r_body.AddForce(Vector3.Normalize(left_hit.point - transform.position) * 100);
                            first_contact = false;
                        }

                        if (Input.GetAxis("Vertical") != 0 && total_speed_square < 200)
                        {
                            r_body.drag = 0.5f;
                            r_body.AddForce(transform.forward * y_axis * speed_mult/2 * Time.deltaTime);
                        }


                        r_body.velocity = new Vector3(velocity.x, 0.3f, velocity.z);
                        //r_body.constraints = RigidbodyConstraints.FreezePositionY;
                        //r_body.constraints = RigidbodyConstraints.FreezeRotationY;
                        if (Input.GetButton("Jump"))
                        {

                            if (extra_wall_jump_force < 1.5f)
                            {
                                extra_wall_jump_force += Time.deltaTime;
                            }
                            jump = true;
                        }
                        else if (jump)
                        {
                            r_body.AddForce(transform.right * wall_jump_force_side);
                            r_body.velocity = new Vector3(velocity.x, wall_jump_force_up * extra_wall_jump_force, velocity.z);
                            timer = 0.5f;
                            jump = false;
                            wall_run_L = false;
                            wall_run_R = false;
                            first_contact = true;


                        }
                        else
                        {
                            extra_wall_jump_force = 0.75f;
                        }
                    }
                    else
                    {
                        jump = false;
                    }
                }
                //right side wall jump
                //(Physics.SphereCast(transform.position, 0.5f, transform.right, out right_hit, 0.8f)
                else if (Physics.Raycast(transform.position, transform.right, out right_hit, 0.8f) && walljump == false)
                {
                    if (right_hit.transform.gameObject.transform.CompareTag("wall"))
                    {
                        wall_run_R = true;

                        if (Physics.Raycast(forward.position, forward.right * 1, out right_hit, 1.5f) && first_contact)
                        {
                            r_body.AddForce(Vector3.Normalize(right_hit.point - transform.position) * 100);
                            first_contact = false;
                        }
                        if (Input.GetAxis("Vertical") != 0 && total_speed_square < 200)
                        {
                            r_body.drag = 0.5f;
                            r_body.AddForce(transform.forward * y_axis * speed_mult/2 * Time.deltaTime);
                        }



                        r_body.velocity = new Vector3(velocity.x, 0.3f, velocity.z);
                        if (Input.GetButton("Jump"))
                        {

                            if (extra_wall_jump_force < 1.5f)
                            {
                                extra_wall_jump_force += Time.deltaTime;
                            }
                            jump = true;

                        }
                        else if (jump)
                        {
                            r_body.AddForce(transform.right * -1 * wall_jump_force_side );
                            r_body.velocity = new Vector3(velocity.x,wall_jump_force_up * extra_wall_jump_force, velocity.z);
                            timer = 0.2f;
                            jump = false;
                            wall_run_L = false;
                            wall_run_R = false;

                            first_contact = true;

                        }
                        else
                        {
                            extra_wall_jump_force = 0.75f;
                        }
                    }
                    else
                    {
                        jump = false;
                    }
                }
                // not wall grinding

                else
                {
                    //probably nothing belongs here for now
                    wall_run_L = false;
                    wall_run_R = false;


                }


            }

            //if falling & only falling
            if (velocity.y < fall_down_force_min )
            {

                if (velocity.y > fall_down_force_max)
                {
                    fall_down_force += Time.deltaTime;
                    //Debug.Log("falling falling falling");
                    r_body.velocity = new Vector3(velocity.x, velocity.y - fall_down_force, velocity.z);
                }
                else
                {
                    r_body.velocity = new Vector3(velocity.x, velocity.y - (fall_down_force * 0.5f), velocity.z);

                }

            }



        }


    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("terrain"))
        {
            ground = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("terrain"))
        {
            ground = false;
        }
    }
    */
}
