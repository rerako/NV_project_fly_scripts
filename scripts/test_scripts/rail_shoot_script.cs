using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rail_shoot_script : MonoBehaviour {
    //public Transform target;
    public GameObject bullet;
    public GameObject bullet2;

    public float distance;
    public Transform forward_point;
    public Transform gun_point;
    public float timer;
    public float charge_timer;

    public float shoot_per_second;
    public Vector3 position;
    public Transform position_aim;

    public Vector3 aim;
    public float x_axis;
    public float y_axis;
    public Camera cam;
    RaycastHit hit;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //position = Input.mousePosition;
        //position += cam.transform.forward * 100f; // Make sure to add some "depth" to the screen point 

        //aim = cam.ScreenToWorldPoint(position);
        //position_aim.position = cam.transform.forward * 100f;
        //Debug.DrawLine(cam.transform.position, position_aim.localPosition, Color.blue);
        //Debug.DrawLine(cam.transform.position, aim, Color.yellow);


        position = Vector3.Normalize(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z) - cam.transform.position);
        position = Input.mousePosition;
        position += forward_point.localPosition; 
        aim = cam.ScreenToWorldPoint(position);
        transform.LookAt(aim);
        if (Input.GetMouseButton(0))
        {


            if (timer < 0)
            {
                GameObject drone = null;
                drone = Instantiate(bullet, gun_point.position, gun_point.rotation) as GameObject;
                drone.transform.LookAt(aim);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        //Debug.Log("enemy hit");
                        drone.transform.LookAt(hit.transform);

                    }
                }
                Destroy(drone, 0.75f);
                drone = null;
                timer = shoot_per_second;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if (Input.GetMouseButton(2))
        {
            charge_timer += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(2) )
        {
            if (charge_timer > 1f)
            {
                GameObject drone = Instantiate(bullet2, gun_point.position, gun_point.rotation) as GameObject;
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f))
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        //Debug.Log("enemy hit");
                        drone.transform.LookAt(hit.transform);

                    }
                }
                else
                
                //{
                    drone.transform.LookAt(aim);
                //}
                Destroy(drone, 1f);
            }
            charge_timer = 0;

        }
    }
}
