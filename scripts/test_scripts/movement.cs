using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    public Transform xaxis_Left;
    public Transform xaxis_Right;
    public Transform yaxis_Up;
    public Transform yaxis_Down;
    public Transform zaxis_Forward;
    public Transform zaxis_Back;

    public float x_percent = 50;
    public float y_percent = 50;
    public float z_percent = 50;

    public float toggle_speed;

    public float scrollx;
    public float scrolly;
    public float scrollz;

    public float x_axis_waggle;
    public float y_axis_waggle;
    public float z_axis_waggle;
    public float z_zip_speed;

    //zoom attack button
    public bool zoom_state;
    public float zoom_timer;
    public float zoom_timer_max;


    public bool ram_state;
    public Transform dash_target;
    public List<GameObject> dash_list;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        y_percent = Mathf.Clamp(y_percent, 0f, 100f);
        x_percent = Mathf.Clamp(x_percent, 0f, 100f);
        z_percent = Mathf.Clamp(z_percent, 0f, 100f);

        if (zoom_timer > 0)
        {
            zoom_timer -= Time.deltaTime;
            /*
            */
            if (dash_target != null)
            {
                ram_state = true;
                transform.localPosition = Vector3.Lerp(transform.localPosition, transform.parent.transform.InverseTransformPoint(dash_target.position), Time.deltaTime * z_zip_speed);
            }
            else
            {
                if (zoom_timer > zoom_timer_max / 2)
                {
                    z_percent = Mathf.Lerp(z_percent, 100, z_zip_speed * Time.deltaTime);
                    if (z_percent > 99)
                    {
                        z_percent = 100;
                    }

                }
                else if (zoom_timer < zoom_timer_max / 2 && zoom_timer > 0)
                {

                    z_percent = Mathf.Lerp(z_percent, 0, z_zip_speed * Time.deltaTime);
                    if (z_percent < 1)
                    {
                        z_percent = 0;
                    }

                }
            }

        }
        else
        {
            zoom_state = false;
            ram_state = false;
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;

        }

        if (Input.GetAxis("Horizontal") != 0 && ram_state == false)
        {
            x_percent += Time.deltaTime * Input.GetAxis("Horizontal") * toggle_speed;
            if (x_percent >=100)
            {
                x_percent = 100;
            }
            else if (x_percent <= 0)
            {
                x_percent = 0;
            }
        }
        if (Input.GetAxis("Vertical") != 0 && ram_state == false)
        {
            y_percent += Time.deltaTime * Input.GetAxis("Vertical") * toggle_speed;
            if (y_percent >= 100)
            {
                y_percent = 100;
            }
            else if (y_percent <= 0)
            {
                y_percent = 0;
            }
        }
        if (Input.GetMouseButton(1) && zoom_state == false)
        {
            setTarget();

            zoom_state = true;
            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        
            zoom_timer = zoom_timer_max;
        }


        scrolly = y_percent / 100;
        scrollx = x_percent / 100;
        scrollz = z_percent / 100;

        x_axis_waggle = xaxis_Left.localPosition.x + ((xaxis_Right.localPosition.x - xaxis_Left.localPosition.x) * scrollx);
        y_axis_waggle = yaxis_Down.localPosition.y + ((yaxis_Up.localPosition.y - yaxis_Down.localPosition.y) * scrolly);
        z_axis_waggle = zaxis_Back.localPosition.z + ((zaxis_Forward.localPosition.z - zaxis_Back.localPosition.z) * scrollz);

        //replace z with zaxiswaggle
        if (zoom_state && dash_target != null)
        {
            // transform.position = Vector3.Lerp(transform.position, new Vector3(x_axis_waggle, y_axis_waggle, z_axis_waggle), Time.deltaTime * z_zip_speed);
        }
        else if (zoom_state && dash_target == null)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(x_axis_waggle, y_axis_waggle, z_axis_waggle), Time.deltaTime * z_zip_speed * 3);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(x_axis_waggle, y_axis_waggle, z_axis_waggle), Time.deltaTime * z_zip_speed * 1.5f);

        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            dash_list.Add(other.gameObject);
            
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (dash_target == other.gameObject)
            {
                dash_target = null;
            }
            dash_list.Remove(other.gameObject);

        }
    }
    public bool dashing()
    {
        return zoom_state;
    }
    public void setTarget()
    {
        //dash_target = dash_list.
        dash_target = null;
        float smallest_dist = 1000f;
        if (dash_list.Count > 0)
        {
            for (int i = 0; i < dash_list.Count; i++)
            {
                if (dash_list[i] == null)
                {
                    continue;
                    //dash_list[i] = null;
                    //dash_list.RemoveAt(i);

                }
                else
                { 
                    if (Vector3.Distance(dash_list[i].transform.position, transform.position) < smallest_dist)
                    {
                        smallest_dist = Vector3.Distance(dash_list[i].transform.position, transform.position);
                        dash_target = dash_list[i].transform;

                    }
                }
            }

            dash_list.Clear();

        }
    }




}
