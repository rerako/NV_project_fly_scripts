using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// version 1.5
//forcemode change

public class Auto_rails : MonoBehaviour
{
    float ratio;
    public float scroll;
    [Range(0f, 100f)]
    public float scroll_multiplier;
    [Range(0f, 10f)]
    public float push_force = 1;
    [Range(0f, 10f)]
    public float grind_force = 1;
    [Range(0f, 100f)]
    public float up_force = 1;
    public Transform aPos;
    public Transform bPos;

    private Transform startPos;
    public Transform scrollPos;
    private Transform endPos;

    private Transform grinder;
    private float total_dist;
    private float timer;


    public bool grinding;
    public bool bounce;


    // Use this for initialization
    void Start()
    {
        total_dist = Vector3.Distance(aPos.position, bPos.position);

    }

    // Update is called once per frame
    void Update()
    {

        if (grinding && Input.GetKeyDown(KeyCode.Space))
        {
            grinding = false;
            if (grinder.GetComponent<Rigidbody>() != null)
            {
                grinder.GetComponent<Rigidbody>().AddForce((endPos.position - startPos.position).normalized * push_force * scroll_multiplier, ForceMode.VelocityChange);

            }
            grinder = null;
            startPos = null;
            endPos = null;
        }
        if (grinding && scroll < 1)
        {
            timer += Time.deltaTime;
            scroll = Mathf.Clamp(scroll, 0, 1f);
            scroll += ((Time.deltaTime / total_dist) * scroll_multiplier);
            scrollPos.position = startPos.position + ((endPos.position - startPos.position) * scroll);
            grinder.position = scrollPos.position;
        }
        else if (grinding && scroll >= 1)
        {
            if (grinder.GetComponent<Rigidbody>() != null)
            {
                grinder.GetComponent<Rigidbody>().AddForce((endPos.position - startPos.position).normalized * push_force * scroll_multiplier, ForceMode.VelocityChange);
                grinder.GetComponent<Rigidbody>().AddForce((endPos.position - startPos.position).normalized * grind_force, ForceMode.VelocityChange);
                if (bounce)
                {
                    grinder.GetComponent<Rigidbody>().AddForce(grinder.transform.up * up_force, ForceMode.VelocityChange);

                }

            }
            grinding = false;
            grinder = null;
            startPos = null;
            endPos = null;
        }

    }
    public void OnTriggerEnter(Collider grind)
    {
        if (grind.gameObject.CompareTag("Player") && grinding == false)
        {
            grinder = grind.transform;
            grinding = true;

            if (Vector3.Distance(grinder.transform.forward + grinder.transform.position, aPos.position) < Vector3.Distance(grinder.transform.position, aPos.position))
            {
                startPos = bPos;
                endPos = aPos;
            }
            else
            {
                startPos = aPos;
                endPos = bPos;
            }
            grindPoint();
        }
    }
    void OnTriggerExit(Collider grind)
    {
        if (grind.gameObject.CompareTag("Player") && grinding == true)
        {
            grinder = null;
            grinding = false;
            startPos = null;
            endPos = null;
            scroll = 0;
        }
    }
    void grindPoint()
    {
        float p1 = Vector3.Distance(grinder.position, startPos.position);
        float p2 = Vector3.Distance(startPos.position, endPos.position);
        scroll = p1 / p2;
    }

}
