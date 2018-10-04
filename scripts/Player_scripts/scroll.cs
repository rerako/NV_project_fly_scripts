using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll : MonoBehaviour
{
    public List<Transform> rail_points;
    public Transform point_a;
    public Transform point_b;
    public Transform scroll_point;
    public float total_dist;
    public float Scroll_leftover_dist;
    public float timer;
    public float lag_total_dist;
    public float scroll_percent = 0;
    public float scroll_multiplier;
    public bool grinds;
    public Transform grinder;
    public Transform player;

    public Transform grinder_look;
    public float rotation_lag;
    public bool look;
    public Transform Look_area;

    public float scroll_lag;
    public rail_editing_showline list;
    public int chain_point = 0;
    public int next_chain_point = 0;

    public bool donut;

    // Use this for initialization
    void Start()
    {
        rail_points = list.give_rail_points();
        list = null;

        connect();
    }

    // Update is called once per frame
    void Update()
    {


        if (point_b != null && point_a != null)
        {
            total_dist = Vector3.Distance(point_b.position, point_a.position);
            Scroll_leftover_dist = Vector3.Distance(point_b.position, grinder.position);
        }


        scroll_percent = Mathf.Clamp(scroll_percent, 0, 1f);


        if (grinds)
        {
            lag_total_dist = Vector3.Distance(grinder.position, scroll_point.position);

            if (lag_total_dist > 3f && Look_area == false)
            {
                grinder_look.LookAt(scroll_point);
                grinder.rotation = Quaternion.Lerp(grinder.rotation, grinder_look.rotation, Time.deltaTime * rotation_lag);
                //player.rotation = grinder.rotation;
            }
            else if (Look_area == true)
            {
                grinder_look.LookAt(Look_area);
                grinder.rotation = Quaternion.Lerp(grinder.rotation, grinder_look.rotation, Time.deltaTime * rotation_lag);

            }
            else
            {

            }

            timer += Time.deltaTime;
            scroll_percent += ((Time.deltaTime / total_dist) * scroll_multiplier);
            if (grinder == null || point_a == null || point_b == null || scroll_point == null)
            {

            }
            else if (scroll_percent >= 1)
            {
                scroll_percent = 0;
                connect();
            }
            else
            {
                scroll_point.position = point_a.position + ((point_b.position - point_a.position) * scroll_percent);
                grinder.position = Vector3.Lerp(grinder.position, scroll_point.position,scroll_lag * Time.deltaTime);
                //grinder.LookAt(point_b);
            }

        }
    }
    public float sqr_MAG(Transform a, Transform b)
    {
        return (a.position - b.position).sqrMagnitude;
    }
    public float sqr_MAG(Vector3 a, Vector3 b)
    {
        return (a - b).sqrMagnitude;
    }



    void connect()
    {
        //if circular chain
        if (donut)
        {
            if (chain_point < (rail_points.Count - 1))
            {
                point_a = rail_points[chain_point];
                point_b = rail_points[chain_point + 1];
            }
            else if (chain_point >= rail_points.Count - 1)
            {

                point_a = rail_points[chain_point];
                point_b = rail_points[0];
                chain_point = -1;
            }

        }
        //if not circular
        else
        {

            if (chain_point <= (rail_points.Count - 2))
            {
                point_a = rail_points[chain_point];
                point_b = rail_points[chain_point + 1];
            }
            //if at end, but connected to another
            else if (chain_point > rail_points.Count - 2 && list != null)
            {
                rail_points = list.give_rail_points();
                chain_point = next_chain_point;
                point_a = rail_points[chain_point];
                point_b = rail_points[chain_point + 1];
                list = null;
                next_chain_point = -5;
            }
            //if at end
            else if (chain_point > rail_points.Count - 2)
            {

                grinds = false;
                //Debug.Log("scurry ho!?");
                //grinder.GetComponent<Rigidbody>().AddForce((point_b.position - point_a.position).normalized * scroll_multiplier, ForceMode.VelocityChange);
                grinder = null;
                point_a = null;
                point_b = null;
                chain_point = -5; // bug test

            }
            else if (chain_point < -1)
            {
                chain_point = 0;
                Debug.Log("squak");
            }
        }
        chain_point++;
    }

    void connect2()
    {

        chain_point--;
        if (chain_point >= 1)
        {
            point_a = rail_points[chain_point];
            point_b = rail_points[chain_point - 1];
        }

        else if (chain_point < 1)
        {
            if (donut)
            {
                point_a = rail_points[0];
                point_b = rail_points[rail_points.Count - 1];
                chain_point = rail_points.Count;

            }
            else
            {
                grinds = false;
                grinder = null;
                point_a = null;
                point_b = null;
                chain_point = -5; // bug test

            }
        }
        else if (chain_point >= rail_points.Count)
        {
            chain_point = rail_points.Count - 1;
            Debug.Log("squak");
        }

    }
    public void set_new_rail(List<Transform> new_set, int set_point)
    {
        //Debug.Log("scurry ho!?");
        //grinder.GetComponent<Rigidbody>().AddForce((point_b.position - point_a.position).normalized * scroll_multiplier, ForceMode.VelocityChange);
        chain_point = set_point;
        rail_points = new_set;
        point_a = null;
        point_b = null;
        scroll_percent = 0;
        connect();


    }
    public void set_rail_connect(rail_editing_showline connecting_rail, int chain_connect)
    {
        list = connecting_rail;
        next_chain_point = chain_connect;
    }

}
