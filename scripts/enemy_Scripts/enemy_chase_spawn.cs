using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_chase_spawn : MonoBehaviour {
    public Transform[] start_positions;
    public Transform[] end_positions;
    public float timer;
    public float launch_per_second = 5;
    public int spawn_point;
    public int destination_point;
    public float life_time;
    public bool auto_spawn;


    public GameObject enemy_1;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (auto_spawn && timer < 0)
        {
            GameObject drone = null;
            drone = Instantiate(enemy_1, start_positions[spawn_point].position, start_positions[spawn_point].rotation) as GameObject;
            drone.GetComponent<enemy_simple_move>().set_destination(end_positions[destination_point]);
            drone.GetComponent<enemy_simple_move>().set_move();

            spawn_point = (int) Random.Range(0f,13f);
            destination_point = (int) Random.Range(0f, 13f);

            Destroy(drone, life_time);
            drone = null;
            timer = launch_per_second;
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
    public void spawn_method(int spawn_id, int end_id)
    {
        GameObject drone = null;
        drone = Instantiate(enemy_1, start_positions[spawn_id].position, start_positions[spawn_id].rotation) as GameObject;
        drone.GetComponent<enemy_simple_move>().set_destination(end_positions[end_id]);
        drone.GetComponent<enemy_simple_move>().set_move();

    }
}
