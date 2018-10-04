using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_simple_attack : MonoBehaviour {
    public Transform target;
    public bool start;
    public float shoot_timer;
    public float max_shoot_time;
    public Transform gun_point;

    public GameObject bullet;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (start)
        {
            if (target != null)
            {
                transform.LookAt(target);
            }
            if (shoot_timer < 0)
            {

                GameObject drone = null;
                drone = Instantiate(bullet, gun_point.position, gun_point.rotation) as GameObject;
                Destroy(drone, 2f);
                shoot_timer = max_shoot_time;

            }
            else
            {
                shoot_timer -= Time.deltaTime;
            }
        }
		
	}
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            start = true;
            target = collision.gameObject.transform;
        }
    }
    public void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            start = false;
            target = null;
        }
    }
}
