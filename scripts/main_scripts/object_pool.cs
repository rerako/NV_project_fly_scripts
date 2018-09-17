using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_pool : MonoBehaviour
{


    //just list to hold prefabs
    public GameObject[] enemyVariants;
    public GameObject[] bulletVariants;
    public GameObject[] allyVariants;

    //just list to hold objects
    public GameObject[] enemyPool;
    public GameObject[] bulletPool;
    public GameObject[] allyPool;

    //just list to hold all gameobjects
    public GameObject[,] enemylist;
    public GameObject[,] bulletlist;
    public GameObject[,] allylist;

    // Use this for initialization
    public int[] max_enemy;
    public int[] max_bullet;
    public int[] max_ally;

    //spawn counter
    public int[] orb_enemy;
    public int[] orb_bullet;
    public int[] orb_ally;

    GameObject orb;
    octo_sorting sorter;
    public bool spawn_enemies;
    public bool spawn_bullets;
    public int spawn_amount;
    int id_give = 0;
    void Start()
    {
        if(spawn_amount == null)
        {
            spawn_amount = 100;
        }
        enemylist = new GameObject[5, spawn_amount];
        bulletlist = new GameObject[5, spawn_amount];
        allylist = new GameObject[5, spawn_amount];

        max_enemy = new int[5];
        max_bullet = new int[5];
        max_ally = new int[5];

        orb_enemy = new int[5];
        orb_bullet = new int[5];
        orb_ally = new int[5];

        if (spawn_enemies)
        {
            SpawnLoop(1, 1, spawn_amount);

        }

        if (spawn_bullets)
        {
            SpawnLoop(2, 1, spawn_amount);

        }

    }


    //type  = enemy/bullet/obstacle list
    //y = variant enemy1 enemy2...
    // x = id enemy number 1---
    void SpawnLoop(int typer, int vary, int max)
    {
        //enemy
        if (typer == 1)
        {
            for (int y = 0; y < vary; y++)
            {
                for (int x = 0; x < max; x++)
                {
                    orb = Instantiate(enemyVariants[y], transform.position, transform.rotation, enemyPool[y].transform);
                    // all enemy orbs have a empty parent
                    //first child must be main center
                    sorter = orb.transform.GetChild(0).GetComponent<octo_sorting>();
                    //Debug.Log("" + x);
                    sorter.objSetter(typer, y, x);
                    enemylist[y, x] = orb;
                    max_enemy[y]++;
                }
            }

        }
        //bullet
        else if (typer == 2)
        {
            for (int y = 0; y < vary; y++)
            {
                for (int x = 0; x < max; x++)
                {
                    orb = Instantiate(bulletVariants[y], transform.position, transform.rotation, bulletPool[y].transform);
                    sorter = orb.transform.GetChild(0).GetComponent<octo_sorting>();
                    sorter.objSetter(typer, y, x);
                    bulletlist[y, x] = orb;
                    max_bullet[y]++;

                }
            }

        }
    }
    public Transform fetch_orb(int typer, int vary)
    {
        GameObject orbal = null;
        if(typer == 1)
        {
            orbal = enemylist[vary, orb_enemy[vary]];
            orb_enemy[vary]++;
            if (spawn_amount <= orb_enemy[vary])
            {
                orb_enemy[vary] = 0;
            }
        }
        else if (typer == 2)
        {
            orbal = bulletlist[vary, orb_bullet[vary]];
            orb_bullet[vary]++;
            orbal.transform.GetChild(0).GetComponent<bullet_move>().reset();
            if (spawn_amount <= orb_bullet[vary])
            {
                orb_bullet[vary] = 0;
            }
        }

        return orbal.transform.GetChild(0);
    }

}
