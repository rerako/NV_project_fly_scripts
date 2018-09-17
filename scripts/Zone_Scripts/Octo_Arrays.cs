using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octo_Arrays : MonoBehaviour
{

    public int[] surround_zones;
    public int zone_id;
    public GameObject[,] enemylist;
    public List<int>[] E_Lists;
    public List<int> enemy1_id;
    public List<int> enemy2_id;
    public List<int> enemy3_id;
    public List<int> enemy4_id;
    public List<int> enemy5_id;
    public fly_ai_script spotter;

    public bool switch_on;
    public bool switch_off;

    public bool mesh_on;
    public bool mesh_off;
    public octo_sorting checker;
    public int[] bullet_Zone;
    public GameObject[,] bulletlist;
    public List<int>[] B_Lists;
    public List<int> bullet1_id;
    public List<int> bullet2_id;
    public List<int> bullet3_id;
    public List<int> bullet4_id;
    public List<int> bullet5_id;


    public GameObject[,] allylist;
    public List<int>[] A_Lists;
    public List<int> ally1_id;
    public List<int> ally2_id;
    public List<int> ally3_id;
    public List<int> ally4_id;
    public List<int> ally5_id;
    // Use this for initialization
    void Awake()
    {
        bullet_Zone = new int[5] { 5, 5, 5, 5, 5 };
        surround_zones = new int[7];
        E_Lists = new List<int>[5];
        enemylist = new GameObject[5, 500];
        E_Lists[0] = enemy1_id;
        E_Lists[1] = enemy2_id;
        E_Lists[2] = enemy3_id;
        E_Lists[3] = enemy4_id;
        E_Lists[4] = enemy5_id;

        B_Lists = new List<int>[5];
        bulletlist = new GameObject[5, 500];
        B_Lists[0] = bullet1_id;
        B_Lists[1] = bullet2_id;
        B_Lists[2] = bullet3_id;
        B_Lists[3] = bullet4_id;
        B_Lists[4] = bullet5_id;

        A_Lists = new List<int>[5];
        allylist = new GameObject[5, 500];
        A_Lists[0] = ally1_id;
        A_Lists[1] = ally2_id;
        A_Lists[2] = ally3_id;
        A_Lists[3] = ally4_id;
        A_Lists[4] = ally5_id;

        Close_zones();
        //mesh_on = true;
        switch_off = true;
        mesh_off = true;

        //switch_on = true;
    }

    //Update is called once per frame
    void Update()
    {
        if (switch_on)
        {
            if (mesh_on)
            {
                Turn_on();
                /*
                for (int x = 0; x < 7; x++)
                {
                    Octo_tree.collect[surround_zones[x]].Turn_on();
                }
                */
                mesh_on = false;
            }
        }
        else if (switch_off)
        {
            if (mesh_off)
            {
                Turn_off();
                /*
                for (int x = 0; x < 7 ;x++) {
                    Octo_tree.collect[surround_zones[x]].Turn_off(); 
                }
                */
                mesh_off = false;

            }
        }
    }

    void checkCollide()
    {
        for (int E_Type = 0; E_Type < 5; E_Type++)
        {
            for (int E_numb = 0; E_numb < E_Lists[E_Type].Count; E_numb++)
            {
                for (int B_Type = 0; B_Type < 5; B_Type++)
                {
                    for (int B_numb = 0; B_numb < B_Lists[B_Type].Count; B_numb++)
                    {
                        if (collision(enemylist[E_Type, E_Lists[E_Type][E_numb]].transform, bulletlist[B_Type, B_Lists[B_Type][B_numb]].transform, bullet_Zone[B_Type]))
                        {
                            //collision




                        }
                    }
                }
            }
        }
    }
    void checkAggro()
    {
        for (int E_Type = 0; E_Type < 5; E_Type++)
        {
            for (int E_numb = 0; E_numb < E_Lists[E_Type].Count; E_numb++)
            {
                spotter = enemylist[E_Type, E_Lists[E_Type][E_numb]].GetComponent<fly_ai_script>();
                if (!spotter.chasing())
                {
                    for (int A_Type = 0; A_Type < 5; A_Type++)
                    {
                        for (int A_numb = 0; A_numb < A_Lists[A_Type].Count; A_numb++)
                        {
                            if (collision(enemylist[E_Type, E_Lists[E_Type][E_numb]].transform, allylist[A_Type, A_Lists[A_Type][A_numb]].transform, 400))
                            {
                                spotter.chase(allylist[A_Type, A_Lists[A_Type][A_numb]].transform);
                            }
                        }
                    }
                }

            }
        }
    }
    public void AddECollect(GameObject orb, int type, int orb_id)
    {
        enemylist[type, orb_id] = orb;
        E_Lists[type].Add(orb_id);

        if (switch_on)
        {
            checker = orb.GetComponent<octo_sorting>();
            if (!checker.getMesh())
            {
                checker.setOn();
            }
        }

    }
    public void RemoveECollect(int type, int orb_id)
    {
        //Debug.Log("" + type);

        E_Lists[type].Remove(orb_id);
        enemylist[type, orb_id] = null;

    }



    public void AddBCollect(GameObject orb, int type, int orb_id)
    {
        bulletlist[type, orb_id] = orb;
        B_Lists[type].Add(orb_id);
        if (switch_on)
        {
            /*
            checker = bulletlist[type, orb_id].gameObject.GetComponent<octo_sorting>();
            if (!checker.getMesh())
            {
                checker.setOn();
            }
            */
        }
    }
    public void RemoveBCollect(int type, int orb_id)
    {

        B_Lists[type].Remove(orb_id);
        bulletlist[type, orb_id] = null;
        if (!switch_on)
        {
             /*
            checker = bulletlist[type, orb_id].gameObject.GetComponent<octo_sorting>();
            if (checker.getMesh())
            {
                checker.setOff();
            }
            */
        }
    }

    public void AddACollect(GameObject orb, int type, int orb_id)
    {

    }

    public bool collision(Transform id1, Transform id2, float distance)
    {
        if (squaredDist_Mag(id1.position, id2.position) < distance)
        {
            return true;
        }
        return false;
    }
    public void set_idZones(int id)
    {
        zone_id = id;
    }
    public void Close_zones()
    {
        if ((zone_id + Octo_tree.totalsegments) < (Octo_tree.totalsegments * Octo_tree.totalsegments * Octo_tree.totalsegments))
        {
            surround_zones[0] = (zone_id + Octo_tree.totalsegments);
        }
        else
        {
            surround_zones[0] = -1;
        }
        if ((zone_id + Octo_tree.totalsegments * Octo_tree.totalsegments) < (Octo_tree.totalsegments * Octo_tree.totalsegments * Octo_tree.totalsegments))
        {
            surround_zones[1] = (zone_id + Octo_tree.totalsegments * Octo_tree.totalsegments);

        }
        else
        {
            surround_zones[1] = -1;
        }
        if ((zone_id + 1) < (Octo_tree.totalsegments * Octo_tree.totalsegments * Octo_tree.totalsegments))
        {
            surround_zones[2] = (zone_id + 1);

        }
        else
        {
            surround_zones[2] = -1;
        }
        if ((zone_id - Octo_tree.totalsegments) > -1)
        {
            surround_zones[3] = (zone_id - Octo_tree.totalsegments);
        }
        else
        {
            surround_zones[3] = -1;
        }
        if ((zone_id - Octo_tree.totalsegments * Octo_tree.totalsegments) > -1)
        {
            surround_zones[4] = (zone_id - Octo_tree.totalsegments * Octo_tree.totalsegments);

        }
        else
        {
            surround_zones[4] = -1;
        }
        if ((zone_id - 1) > -1)
        {
            surround_zones[5] = (zone_id - 1);

        }
        else
        {
            surround_zones[5] = -1;
        }
        surround_zones[6] = zone_id;
    }

    //turns off fields which the player is not a part of
    public void Turn_off()
    {
        for (int z = 0; z < 5; z++)
        {
            for (int x = 0; x < E_Lists[z].Count; x++)
            {
                checker = enemylist[z, E_Lists[z][x]].gameObject.GetComponent<octo_sorting>();
                if (checker.getMesh())
                {
                    checker.setOff();

                }

            }
            for (int x = 0; x < B_Lists[z].Count; x++)
            {
                //grab mesh, turn it off
                //
                //checker = bulletlist[z, B_Lists[z][x]].gameObject.GetComponent<octo_sorting>()

            }
        }
        checker = null;
       // mesh_on = false;

    }
    //turns on fields which the player is not a part of
    public void Turn_on()
    {
        for (int z = 0; z < 5; z++)
        {
            for (int x = 0; x < E_Lists[z].Count; x++)
            {
                checker = enemylist[z, E_Lists[z][x]].gameObject.GetComponent<octo_sorting>();
                if (!checker.getMesh())
                {
                    checker.setOn();

                }

            }
            for (int x = 0; x < B_Lists[z].Count; x++)
            {
                //bulletlist[z, B_Lists[z][x]].gameObject.GetComponent<octo_sorting>().setOn();

            }
        }
        checker = null;

        //mesh_on = true;

    }
    public float squaredDist_Mag(Vector3 obj1, Vector3 obj2)
    {
        Vector3 offset = obj2 - obj1;
        float sqrLen = offset.sqrMagnitude;
        return sqrLen;
    }
}
