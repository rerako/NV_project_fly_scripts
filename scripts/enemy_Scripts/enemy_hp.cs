using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_hp : MonoBehaviour {
    public float enemy_hp_count;
    public bool big_enemy;
    public GameObject player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet1"))
        {
            enemy_hp_count -= 5;
            check_health();
        }
        else if (other.gameObject.CompareTag("Bullet2"))
        {
            enemy_hp_count -= 1;
            check_health();
        }

    }
    public void OnCollisionEnter(Collision other)
    {
        //
        if (other.gameObject.CompareTag("Player_node"))
        {
            player = other.gameObject;
            if (player.GetComponent<movement>().dashing())
            {
                enemy_hp_count = 0;
                player.GetComponent<movement>().dashing_set(false);
            }

            check_health();
        }
        else
        {
            //hurt player
        }


    }
    public void check_health()
    {
        if(enemy_hp_count <= 0)
        {
            Destroy(gameObject);
        }
    }
}
