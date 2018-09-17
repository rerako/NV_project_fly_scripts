using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addTo : MonoBehaviour
{
    public rail_connect origin;
    public Vector3 point1;
    public Vector3 point2;
    public float scroll_percent;
    public float p1;
    public float p2;

    public int c1;
    public int c2;

    public Vector3 Pposition;
    public Vector3 Pposition2;

    // Use this for initialization
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player") && !Input.GetButton("Jump") && origin.returnGrinds() == false)
        {


            if (Vector3.Distance(player.transform.position + player.transform.forward, point1) > Vector3.Distance(player.transform.position, point1))
            {
                //scroll_percent = grindPoint(point1, point2, player.transform);
                p1 = Vector3.Distance(player.transform.position, point2);
                p2 = Vector3.Distance(point1, point2);
                scroll_percent = grindPoint(p1, p2);
                origin.insert_position(c1, c2, 1, 1f - scroll_percent, player.gameObject);

            }
            else
            {
                p1 = Vector3.Distance(player.transform.position, point1);
                p2 = Vector3.Distance(point2, point1);
                scroll_percent = grindPoint(p1, p2);
                origin.insert_position(c1, c2, -1, scroll_percent, player.gameObject);
            }

        }
    }
    public float grindPoint(float x, float y)
    {
        return x / y;
    }
    public void setPoints(Vector3 pos1, Vector3 pos2, rail_connect ori, int x1, int x2)
    {
        point1 = pos1;
        point2 = pos2;
        origin = ori;
        c1 = x1;
        c2 = x2;
    }
}
