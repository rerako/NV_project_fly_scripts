using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move : MonoBehaviour {
    public float speed;
    public TrailRenderer trail;
    public bool explode;
    public GameObject explosion;

	// Use this for initialization
	void Start () {
        //trail = GetComponent<TrailRenderer>();
        if (trail != null)
        {
            trail.Clear();

        }

    }

    // Update is called once per frame
    void Update () {
        transform.position += transform.forward * Time.deltaTime * speed;
	}
    public void reset()
    {
        if (trail != null)
        {
            trail.Clear();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Terrain") || collision.gameObject.CompareTag("Enemy"))
        {
                if (explode)
                {
                    GameObject boom = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
              
                }
                Destroy(this.gameObject);
        }

    }
}
