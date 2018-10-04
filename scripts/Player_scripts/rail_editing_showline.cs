using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class rail_editing_showline : MonoBehaviour {
    //public List<List<Transform>> rail_lists;
    public List<Transform> rail_array;
    public scroll rail_guider;
    public rail_editing_showline connect;
    public bool not_rail;
    public int connect_chain;
    // Use this for initialization
    void Start () {
        //rail_lists.Add(rail_array);

    }

    // Update is called once per frame
    void Update () {
        for (int i = 0; i < rail_array.Count - 1; i++)
        {
            Debug.DrawLine(rail_array[i].position, rail_array[i + 1].position, Color.green);
        }
    }
    public List<Transform> give_rail_points()
    {
        return rail_array;
    }
    public rail_editing_showline reconnect_rail()
    {
        return connect;
    }
    public int reconnect_rail_chainpoint()
    {
        return connect_chain;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!not_rail && other.CompareTag("Player"))
        {
            rail_guider.set_new_rail(rail_array,0);
            if(connect != null)
            {
                rail_guider.set_rail_connect(connect,connect_chain);
            }
        }
    }
}
