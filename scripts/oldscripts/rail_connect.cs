using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rail_connect : MonoBehaviour
{
    public List<Transform> blocks;
    public List<GameObject> rails;
    public GameObject ghost_posts;
    private Transform point_a;
    private Transform point_b;
    private int direction;
    public bool donut;
    private bool grinds;
    private Transform grinder;
    public GameObject posts;
    public Transform scroll_point;

    private int chain_id1;
    private int chain_id2;
    public int chain_point;

    private float total_dist;
    private float timer;
    private float scroll = 0;

    [Range(0f, 10f)]
    public float push_force = 1;
    [Range(0f, 10f)]
    public float grind_force = 1;
    [Range(0f, 100f)]
    public float up_force = 1;
    [Range(0f, 100f)]
    public float scroll_multiplier;

    // Use this for initialization
    void Start()
    {
        create();
    }

    // Update is called once per frame
    void Update()
    {
        scroll = Mathf.Clamp(scroll, 0, 1f);

        if (grinds)
        {
            total_dist = Vector3.Distance(point_b.position, point_a.position);
            if (scroll < 1)
            {
                timer += Time.deltaTime;
                scroll += ((Time.deltaTime / total_dist) * scroll_multiplier);
                if (grinder == null || point_a == null || point_b == null || scroll_point == null)
                {

                }
                else
                {
                    scroll_point.position = point_a.position + ((point_b.position - point_a.position) * scroll);
                    grinder.position = scroll_point.position;
                }
            }
            else if (scroll >= 1)
            {
                scroll = 0;
                if (direction == 1)
                {
                    connect();

                }
                else if (direction == -1)
                {
                    connect2();
                }
                else
                {
                    grinds = false;
                    grinder = null;
                    point_a = null;
                    point_b = null;
                }
            }
            if (Input.GetButton("Jump"))
            {

                if (grinder.GetComponent<Rigidbody>() != null)
                {
                    grinder.GetComponent<Rigidbody>().AddForce((point_b.position - point_a.position).normalized * push_force * scroll_multiplier, ForceMode.VelocityChange);
                    grinder.GetComponent<Rigidbody>().AddForce((point_b.position - point_a.position).normalized * grind_force, ForceMode.VelocityChange);
                    grinder.GetComponent<Rigidbody>().AddForce(grinder.transform.up * up_force, ForceMode.VelocityChange);


                }
                grinds = false;
                grinder = null;
                point_a = null;
                point_b = null;
                chain_point = -5; // bug test

            }
        }
        else
        {
            if (grinds == true)
            {
                if (grinder.GetComponent<Rigidbody>() != null)
                {
                    grinder.GetComponent<Rigidbody>().AddForce((point_b.position - point_a.position).normalized * push_force * scroll_multiplier, ForceMode.VelocityChange);
                    grinder.GetComponent<Rigidbody>().AddForce((point_b.position - point_a.position).normalized * grind_force, ForceMode.VelocityChange);
                    grinder.GetComponent<Rigidbody>().AddForce(grinder.transform.up * up_force, ForceMode.VelocityChange);


                }
            }

            grinds = false;
            grinder = null;
            point_a = null;
            point_b = null;
            chain_point = -5; // bug test

        }
    }
    void create()
    {

        for (int x = 0; x < (blocks.Count - 1); x++)
        {
            float dist = Vector3.Distance(blocks[x].position, blocks[x + 1].position);

            blocks[x].LookAt(blocks[x + 1].position);
            GameObject rail_trigger = Instantiate(ghost_posts, (blocks[x].position + blocks[x + 1].position) / 2, blocks[x].rotation) as GameObject;

            GameObject rail = Instantiate(posts, (blocks[x].position + blocks[x + 1].position) / 2 + new Vector3(0, -1f, 0), blocks[x].rotation) as GameObject;
            rail_trigger.GetComponent<addTo>().setPoints( blocks[x].position, blocks[x + 1].position, gameObject.GetComponent<rail_connect>(), x , x + 1);
            rails.Add(rail);
            rail.name = "" + (x + 1);
            rail.transform.localScale = new Vector3(0.5f, 0.5f, dist);
            rail_trigger.transform.localScale = new Vector3(0.5f, 0.5f, dist + 0.1f);
            rail.transform.parent = transform;
            rail_trigger.transform.parent = transform;

        }
        if (donut)
        {
            float dist = Vector3.Distance(blocks[0].position, blocks[blocks.Count - 1].position);

            blocks[0].LookAt(blocks[blocks.Count - 1].position);

            GameObject rail_trigger = Instantiate(ghost_posts, (blocks[blocks.Count - 1].position + blocks[0].position) / 2, blocks[0].rotation) as GameObject;
            GameObject rail = Instantiate(posts, (blocks[blocks.Count - 1].position + blocks[0].position) / 2 + new Vector3(0, -1f, 0), blocks[0].rotation) as GameObject;
            rail_trigger.GetComponent<addTo>().setPoints( blocks[blocks.Count - 1].position, blocks[0].position, gameObject.GetComponent<rail_connect>(), blocks.Count - 1, 0);

            rails.Add(rail);
            rail.name = "" + blocks.Count;
            rail.transform.localScale = new Vector3(0.5f, 0.5f, dist);
            rail_trigger.transform.localScale = new Vector3(0.5f, 0.5f, dist + 0.1f);
            rail.transform.parent = transform;
            rail_trigger.transform.parent = transform;
        }
    }

    public void insert_position(int x1, int x2, int point, float scroll_perc, GameObject player)

    {
        if(point == 1)
        {
            point_a = blocks[x1];
            point_b = blocks[x2];
            scroll = scroll_perc;

            if (x1 == blocks.Count-1)
            {
                chain_point = -1;

            }
            else
            {
                chain_point = x1;

            }
        }
        if (point == -1)
        {
            point_a = blocks[x2];
            point_b = blocks[x1];
            scroll = 1f - scroll_perc;

            if (x2 == 0)
            {
                chain_point = blocks.Count;
            }else
            {
                chain_point = x2;

            }
        }


        direction = point;
        //Debug.Log("" + scroll);
        grinder = player.transform;
        grinds = true;
    }
    public bool returnGrinds()
    {
        return grinds;
    }

    void connect()
    {
        chain_point++;
        //if circular chain
        if (donut)
        {
            if (chain_point < (blocks.Count - 1))
            {
                point_a = blocks[chain_point];
                point_b = blocks[chain_point + 1];
            }
            else if (chain_point >= blocks.Count - 1)
            {

                point_a = blocks[chain_point];
                point_b = blocks[0];
                chain_point = -1;
            }

        }
        //if not circular
        else
        {

            if (chain_point <= (blocks.Count - 2))
            {
                point_a = blocks[chain_point];
                point_b = blocks[chain_point + 1];
            }
            //if at end
            else if (chain_point > blocks.Count - 2)
            {

                grinds = false;
                //Debug.Log("scurry ho!?");
                grinder.GetComponent<Rigidbody>().AddForce((point_b.position - point_a.position).normalized * scroll_multiplier, ForceMode.VelocityChange);
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

    }
    void connect2()
    {

        chain_point--;
        if (chain_point >= 1)
        {
            point_a = blocks[chain_point];
            point_b = blocks[chain_point - 1];
        }

        else if (chain_point < 1)
        {
            if (donut)
            {
                point_a = blocks[0];
                point_b = blocks[blocks.Count - 1];
                chain_point = blocks.Count;

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
        else if (chain_point >= blocks.Count)
        {
            chain_point = blocks.Count - 1;
            Debug.Log("squak");
        }

    }
}
