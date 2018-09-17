using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Id_scripts : MonoBehaviour
{
    public int[] enemyID_list;


    public int[] bulletID_list;


    // Use this for initialization
    void Start()
    {
        enemyID_list = new int[5];
        bulletID_list = new int[5];

    }
    public int GetId(int obj_type, int type)
    {
        if (obj_type == 1)
        {
            int give = enemyID_list[type];
            enemyID_list[type]++;
            return give;
        }
        else if (obj_type == 2)
        {
            int give = bulletID_list[type];
            bulletID_list[type]++;
            return give;
        }
        else
        {
            Debug.Log("error: " + obj_type +" "+ type);
            return -1;
        }
    }
}
