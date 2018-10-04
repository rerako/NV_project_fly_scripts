using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_simple_move : MonoBehaviour {
    public Transform destination;
    public float speed;
    public rail_editing_showline enemy_trail;
    public List<Transform> rail_array;
    public int trail_count = 0;
    public float delay;
    public float delay_time;
    public bool move;
    public bool delay_type;
    public bool keep_move;

    // Use this for initialization
    void Start () {
	    if(enemy_trail != null)
        {
            rail_array = enemy_trail.give_rail_points();
            destination = rail_array[trail_count];
        }
        move = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (delay < 0  )
        {
            if (move && destination != null)
            {
                if (Vector3.Distance(transform.position, destination.position) > 0.1f)
                {

                    transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);

                }
                else
                {

                    if (trail_count < rail_array.Count - 1)
                    {
                        trail_count++;
                        destination = rail_array[trail_count];
                    }
                    else
                    {
                        if (keep_move)
                        {

                        }
                        else
                        {
                            move = false;
                        }
                    }
                    if (delay_type)
                    {
                        delay = delay_time;
                    }
                }
            }

        }
        else
        {
            delay -= Time.deltaTime;

        }


    }
    public void set_destination(Transform fly_zone)
    {
        destination = fly_zone;
    }
    public void set_move()
    {
        keep_move = true;
    }
}
