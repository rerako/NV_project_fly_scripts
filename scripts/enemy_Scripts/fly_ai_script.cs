using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly_ai_script : MonoBehaviour
{
    // nonstraight roaming
    public Vector3 target;
    //final destination
    public Vector3 target2;
    //protection zones
    public Vector3 targetZone;

    public Transform Shoot_Targ;
    public bool aggro;

    public short mode;
    public bool Post;
    public bool leader;
    public Vector3 pos;
    public float speed;
    public float maxspeed;
    public float boxLimit;
    public float targetLimit;
    public Transform forth;
    private float Modespeed;
    public float turn;
    // Use this for initialization
    void Start()
    {
        Post = false;
        mode = 0;
        Modespeed = speed * Time.deltaTime;
        target = 0.5f * (transform.position + target2);
        target2 = new Vector3(Random.Range(0, boxLimit), Random.Range(0, boxLimit), Random.Range(0, boxLimit));
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        { // freeroam or specific areas to defend and basic follow attack mode
            move();
            move2();
            setZone();
            surfMode();
            speedManipulate();
        }
        else if (mode == 1) // basic chase mode;
        {
            /*
            move();
            move2();
            //setZone2();
            surfMode();
            speedManipulate();*/
        }
        else if (mode == 2) // formation mode
        {
            //it is supposed to do nothing currently.
        }
        else if (mode == 3) // bogey tailing mode
        {
            // if close to player long enough, it will become a child of player and lose all movement, but shoot wildly at player
        }
        else if (mode == 4) // bomb mode
        {
            // if close map objective proceeds to bomb strike;
        }
        else
        {
            //off mode
        }
    }
    public void surfMode()
    { // sways left and right  towards the point to ensure a bit of sway in flight, once within 20 squares
        if (squaredDist_Mag(pos, target) < 5)
        {
            target = (0.5f * (pos + target2)) + Random.insideUnitSphere * 5;
        }
        else if (squaredDist_Mag(target2, target) < 400)
        {
            target = target2;
        }
        else if (squaredDist_Mag(target2, target) > squaredDist_Mag(target2, pos))
        {
            target = (0.5f * (pos + target2)) + Random.insideUnitSphere * 5;
        }
    }

    public void setZone()
    {
        if (aggro && squaredDist_Mag(pos, Shoot_Targ.position) < 900)
        {

        }
        else if (aggro && squaredDist_Mag(pos, Shoot_Targ.position) < 900)// moves towards player
        {
            target2 = Shoot_Targ.position;
        }
        else
        {        // free roam 
            aggro = false;
            if (!Post && squaredDist_Mag(pos, target2) < 25)
            {
                target2 = new Vector3(Random.Range(0, boxLimit), Random.Range(0, boxLimit), Random.Range(0, boxLimit));
            }
            //moves to target zone
            else if (Post && squaredDist_Mag(pos, target2) < 25)
            {
                target2 = new Vector3(targetZone.x + Random.Range(-targetLimit, targetLimit), targetZone.y + Random.Range(-targetLimit, targetLimit), targetZone.z + Random.Range(-targetLimit, targetLimit));
            }
        }
    }
    public void chase(Transform targs)
    {
        Shoot_Targ = targs;
        aggro = true;

    }

    public bool chasing()
    {
        return aggro;
    }
    // moves forward 
    public void move()
    {
        transform.position += transform.forward * Modespeed;
        pos = transform.position;

    }
    //turning
    public void move2()
    {

        forth.LookAt(target);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, forth.rotation, turn * Time.deltaTime);
    }
    //
    public void speedManipulate()
    {
        /*
        Vector3 dots1 = transform.TransformDirection(Vector3.forward);
        dots2 = 
        dot_numb = Vector3.Dot(transform.TransformDirection(Vector3.forward), target2 - transform.position);
         if (Vector3.Dot(transform.TransformDirection(Vector3.forward), target2 - transform.position) > 1 )

        */
        if (squaredDist_Mag(target2, pos) < squaredDist_Mag(target2, forth.position) && speed > 1f)
        {
            speed -= Time.deltaTime * 3;
        }
        else if (speed < maxspeed)
        {
            speed += Time.deltaTime * 3;
        }
        Modespeed = speed * Time.deltaTime;

    }

    public float squaredDist_Mag(Vector3 orb, Vector3 dest)
    {
        Vector3 offset = dest - orb;
        float sqrLen = offset.sqrMagnitude;
        return sqrLen;
    }
    public void setMode(short mode_numb)
    {
        mode = mode_numb;
    }
}
