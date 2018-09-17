using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Octo_tree : MonoBehaviour
{
    public GameObject[] leaves;
    public GameObject branch;
    public GameObject leafNode;
    public Octo_tree collect_orb;
    public Octo_Arrays collect_out;

    public static Octo_Arrays[] collect;
    public static Octo_Arrays outOctoList;
    public float World_sides = 0;
    public static int total_layers = 0;

    public int layers = 0;
    public int segments;
    public static int totalsegments;
    private int numb = 0;
    public static int idname;
    public float dimensions;
    public static int leaf_total = -1;
    public static float Cube_side;

    public bool stump = false;
    // Use this for initialization
    void Start()
    {

        leaves = new GameObject[8];
        if (stump)
        {
            total_layers = layers;
            idname = 0;
            collect_orb = gameObject.GetComponent<Octo_tree>();
            segments = (int)Mathf.Pow(2, total_layers);
            totalsegments = segments;
            collect = new Octo_Arrays[segments * segments * segments];
            Cube_side = World_sides / segments;
            outOctoList = collect_out;
            //leaf_total = (int)Mathf.Pow(segments, 3);
        }
        segments = (int)Mathf.Pow(2, total_layers);
        dimensions = (World_sides) / 2;
        if (layers > 1)
        {
            CreateBranch();
        }
        else
        {
            makeLeaves();
        }


    }

    public void CreateBranch()
    {
        numb = 0;

        for (int x = 0; x < 2; x++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    GameObject point = Instantiate(branch, (transform.position + new Vector3(x * dimensions, j * dimensions, k * dimensions)), transform.rotation) as GameObject;
                    point.SetActive(false);
                    point.transform.SetParent(transform);
                    point.name = "" + x + " " + j + " " + k + " " + numb;
                    Octo_tree treescript = point.GetComponent<Octo_tree>();
                    treescript.startNode(World_sides, (layers - 1), branch, leafNode, collect_orb);
                    leaves[numb] = point;
                    point.SetActive(true);

                    numb++;
                }
            }
        }
    }

    public void makeLeaves()
    {
        numb = 0;

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                for (int z = 0; z < 2; z++)
                {
                    GameObject newBranch = Instantiate(leafNode, transform.position + new Vector3(x * dimensions, y * dimensions, z * dimensions), transform.rotation) as GameObject;
                    newBranch.SetActive(false);
                    leaves[numb] = newBranch;
                    newBranch.transform.SetParent(transform);
                    newBranch.name = "" + Pos_id(newBranch.transform);
                    leaf_total++;
                    collect_orb.GetComponent<Octo_tree>().addleaf(Pos_id(newBranch.transform), newBranch.GetComponent<Octo_Arrays>());
                    idname++;
                    numb++;
                    newBranch.SetActive(true);
                    newBranch.GetComponent<Octo_Arrays>().set_idZones(Pos_id(newBranch.transform));
                    newBranch.GetComponent<Octo_Arrays>().Close_zones();


                }
            }
        }

    }
    public void startNode(float radius, int stacks, GameObject bush, GameObject node, Octo_tree leaf_lists)
    {
        World_sides = radius / 2;
        layers = stacks;
        branch = bush;
        leafNode = node;
        collect_orb = leaf_lists;
    }
    public void addleaf(int numb, Octo_Arrays stick)
    {
        collect[numb] = stick;
    }

    public int Pos_id(Transform pos)
    {
        Vector3 leafpoint = pos.position;
        int jx;
        int jy;
        int jz;
        jx = (int)(leafpoint.x / dimensions);
        jy = (int)(leafpoint.y / dimensions);
        jz = (int)(leafpoint.z / dimensions);
        int headingTo = jx + (jy * segments) + (jz * segments * segments);
        return headingTo;
    }
}
