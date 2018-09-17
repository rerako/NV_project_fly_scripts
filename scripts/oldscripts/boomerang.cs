using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boomerang : MonoBehaviour {
    public Transform player;
    public float force;
    public float Timer;
    public bool one_Use;
    private shoot cannon_origin;
    // Use this for initialization
    void Start () {
		if(Timer < 0)
        {
            Timer = 3f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (player != null) {
            Vector3 normalize = Vector3.Normalize(player.position - transform.position);
            gameObject.GetComponent<Rigidbody>().AddForce(normalize * force * Time.deltaTime);
        }

        Timer -= Time.deltaTime;

        if(one_Use && Timer < 0)
        {
            DestroyObject(gameObject);
        }
        else if (!one_Use && Timer < 0)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().drag = 2f;
            if (Vector3.SqrMagnitude(transform.position - player.transform.position) < 1)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = false;

            }
        }
	}
    public void setTarg(Transform pos, shoot cannon) {

        player = pos;
        cannon_origin = cannon;
        
    }
    public void setTarg(Transform pos)
    {

        player = pos;
        //cannon_origin = cannon;

    }
    public void setFollow(float pow)
    {

        force = pow;
        //cannon_origin = cannon;

    }
    void OnCollisionEnter(Collision poke)
    {
        if (poke.gameObject.CompareTag("Player") && gameObject.CompareTag("enemy_bullet") == false)
        {
            cannon_origin.addammo();
            DestroyObject(gameObject);
        }
        if (poke.gameObject.CompareTag("Player") && gameObject.CompareTag("enemy_bullet") == true)
        {
            poke.gameObject.GetComponent<PlayerHP>().hpminus();
        }
        if (poke.gameObject.CompareTag("Target") && gameObject.CompareTag("enemy_bullet") == false)
        {
            DestroyObject(poke.gameObject);

        }

    }
    void OnTriggerEnter(Collider poke)
    {
        if (poke.gameObject.CompareTag("Player") && gameObject.CompareTag("enemy_bullet") == true)
        {
            poke.gameObject.GetComponent<PlayerHP>().hpminus();
            Destroy(gameObject);
        }
        if (poke.gameObject.CompareTag("Collect"))
        {
            poke.transform.SetParent(transform);
            poke.gameObject.GetComponent<SphereCollider>().radius = poke.gameObject.GetComponent<SphereCollider>().radius * 2;
            poke.transform.position = transform.position;
        }
    }
}
