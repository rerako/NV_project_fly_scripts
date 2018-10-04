using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flight : MonoBehaviour {

    public Transform target;
    public float turnspeed;
    public float forwardspeed;
    public float timer;
    public float t_accel;
    public int path_id;
    public float resetTimer;
    public Vector3 newDir;
    public guider core;
    public bool hit;
	// Use this for initialization
	void Start () {
        timer = resetTimer;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (hit == false)
        {
            turnspeed += Time.deltaTime * 0.02f * t_accel;

            Vector3 targetDir = target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnspeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
            transform.position += transform.forward * forwardspeed * Time.deltaTime;
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                core.addposition(transform.position);
                timer = resetTimer;
            }
            if (targetDir.sqrMagnitude < 1)
            {
                hit = true;
                core.addposition(target.position);
            }
        }
        else
        {

        }
    }
    public void setTarg(Transform point)
    {
        target = point;
    }
    public void setcore(guider point)
    {
        core = point;
    }
}
