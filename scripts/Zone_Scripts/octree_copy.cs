using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class octree_copy : MonoBehaviour {
    // public GameObject box;
    public float worldsize;
    public static float dimension;
    public int limit;
    public int tier;
    public GameObject[] Listquad;
    public GameObject branch;
    public GameObject UpperBranch;
    //public catagorize[] root;
    //public static octree_07 RootNode;
    public int count = 0;
    public static int leaf_count;
    public bool first;
    public GameObject leafs;
    public GameObject pool;
    public int boxlimit;
    public int[] barcode;
    public GameObject outsideNode;
    //public catagorize outBox;
    public List<Transform> MapNodesPos;
   // public List<catagorize> MapNodes;
    public int Control_Points;
    //public List<catagorize> leafList;
    // Use this for initialization
    void Start()
    {
        Listquad = new GameObject[8];
        boxlimit = (int)Mathf.Pow(2, (limit + 1));
        if (first)
        {
            dimension = (worldsize / (Mathf.Pow(2f, (limit + 1))));
            //Control_Points = MapNodesPos.Count;

            leaf_count = 0;
            barcode = new int[limit + 1];
            tier = limit;
            if (UpperBranch == null)
            {
                UpperBranch = gameObject;
            }

        }
        if (tier > 0)
        {
            CreateBranch();
        }
        else
        {
            CreateLeaves();
        }

    }
    public float giveDim()
    {
        return dimension;
    }
    public int giveLim()
    {
        return limit;
    }

    public void lowertier(int max, int tiers, float world, GameObject leaves, GameObject stick, GameObject boom, GameObject upperB, GameObject Out)
    {
        limit = max;
        barcode = new int[limit + 1];
        tier = tiers - 1;
        worldsize = world / 2;
        leafs = leaves;
        branch = stick;
        UpperBranch = upperB;
        outsideNode = Out;
        pool = boom;
    }

    /*
    public void setbound(Vector3 transformposition)
    {
        //below 0
        if (x != -1 && transformposition.x < 0)
        {
            x = -1;
        }
        //above zero
        else if (x != -1 && transformposition.x / dimension >= limit)
        {
            x = -1;
        }
        //inbetween
        else
        {
            if (x != (int)(transformposition.x / dimension))
            {
                x = (int)(transformposition.x / dimension);

            }
        }
        if (y != -1 && transformposition.y < 0)
        {
            y = -1;
        }
        else if (y != -1 && transformposition.y / dimension >= limit)
        {
            y = -1;
        }
        else
        {
            if (y != (int)(transformposition.y / dimension))
            {
                y = (int)(transformposition.y / dimension);
            }
        }
        if (z != -1 && transformposition.z < 0)
        {
            z = -1;
        }
        else if (z != -1 && transformposition.z / dimension >= limit)
        {
            z = -1;
        }
        else
        {
            if (z != (int)(transformposition.z / dimension))
            {
                z = (int)(transformposition.z / dimension);
            }
        }
    }
    */

    public void setID(int size)
    {
        barcode = new int[size];
        //barcode = code;
    }
/*
    public catagorize OnCapZone(Transform node)
    {

        if (Control_Points > 0)
        {
            for (int j = 0; j < Control_Points; j++)
            {
                if ((node.position.x - MapNodesPos[j].position.x) > 0 && (node.position.x - MapNodesPos[j].position.x) < 100)
                {
                    if ((node.position.y - MapNodesPos[j].position.y) > 0 && (node.position.y - MapNodesPos[j].position.y) < 100)
                    {
                        if ((node.position.z - MapNodesPos[j].position.z) > 0 && (node.position.z - MapNodesPos[j].position.z) < 100)
                        {
                            return MapNodes[j];
                        }
                    }
                }
            }
        }
        return null;
    }
    */
    public void updateBar(int[] bar)
    {
        for (int x = 0; x < limit; x++)
        {
            barcode[x] = bar[x];
        }
    }
    public void setBar(int moto, int noto, int ascii)
    {
        barcode[moto - noto] = ascii;
        //Debug.Log("" + ascii);


    }
    /*
    public GameObject GrabZone(GameObject pin)
    {
        GameObject leaf;
        int rank = getZone(pin.transform);
        //Debug.Log("rank" + rank);
        if (rank == -1)
        {
            return outsideNode;
        }

        if (tier > 0)
        {


            leaf = Listquad[rank].GetComponent<octree_07>().GrabZone(pin);
            return leaf;

        }

        else
        {
            //rank = setZone(pin, transform.position);

            leaf = Listquad[rank];
            return leaf;

        }

    }

    */
    public int getZone(Transform point)
    {
        /* the point are certainly to have a bigger vector3 points
        worldsize 50
        
        3,0,0
        0,0,0 


        3/(50/2) == 0
        25/(50/2) == 1
        
        Debug.Log("worldx" + transform.position.x + "worldy" + transform.position.y + "worldz" + transform.position.z);
        Debug.Log("worldsize" + worldsize / 2);
        Debug.Log(point.transform.position.x);
        Debug.Log("x: " + (((int)((point.position.x - transform.position.x) / (worldsize / 2)))));
        Debug.Log("y: " + (((int)((point.position.y - transform.position.y) / (worldsize / 2)))));
        Debug.Log("z: " + (((int)((point.position.z - transform.position.z) / (worldsize / 2)))));

        Debug.Log("x: " + (point.transform.position.x - transform.position.x));
        Debug.Log("tier: " + tier);
        */
        int numb = -1;
        if ((int)((point.position.x - transform.position.x) / (worldsize / 2)) == 0)
        {
            if ((int)((point.position.y - transform.position.y) / (worldsize / 2)) == 0)
            {
                if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 0)
                {
                    numb = 0;
                }
                else if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 1)
                {
                    numb = 1;
                }
            }
            else if ((int)((point.position.y - transform.position.y) / (worldsize / 2)) == 1)
            {
                if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 0)
                {
                    numb = 2;

                }
                else if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 1)
                {
                    numb = 3;

                }
            }

        }
        else if ((int)((point.position.x - transform.position.x) / (worldsize / 2)) == 1)
        {
            if ((int)((point.position.y - transform.position.y) / (worldsize / 2)) == 0)
            {
                if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 0)
                {
                    numb = 4;

                }
                else if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 1)
                {
                    numb = 5;

                }
            }
            else if ((int)((point.position.y - transform.position.y) / (worldsize / 2)) == 1)
            {
                if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 0)
                {
                    numb = 6;

                }
                else if ((int)((point.position.z - transform.position.z) / (worldsize / 2)) == 1)
                {
                    numb = 7;

                }
            }
        }
        else
        {

        }


        return numb;
    }
    public int setZone(GameObject point, Vector3 world)
    {
        int xid = -1;



        if (((int)((point.transform.position.x - world.x) / (worldsize / 2))) == 0)
        {
            if (((int)(point.transform.position.y - world.y) / (worldsize / 2)) == 0)
            {
                if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 0)
                {
                    xid = 0;
                    //Debug.Log("" + barcode[limit - tier]);


                }
                else if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 1)
                {
                    xid = 1;
                }
            }
            else if (((int)(point.transform.position.y - world.y) / (worldsize / 2)) == 1)
            {
                if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 0)
                {
                    xid = 2;

                }
                else if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 1)
                {
                    xid = 3;

                }
            }
        }
        else if (((int)(point.transform.position.x - world.x) / (worldsize / 2)) == 1)
        {
            if (((int)(point.transform.position.y - world.y) / (worldsize / 2)) == 0)
            {
                if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 0)
                {
                    xid = 4;

                }
                else if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 1)
                {
                    xid = 5;

                }
            }
            else if (((int)(point.transform.position.y - world.y) / (worldsize / 2)) == 1)
            {
                if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 0)
                {
                    xid = 6;

                }
                else if (((int)(point.transform.position.z - world.z) / (worldsize / 2)) == 1)
                {
                    xid = 7;

                }
            }
        }

        return xid;
    }


    public void CreateBranch()
    {
        for (int x = 0; x < 2; x++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    GameObject point = Instantiate(branch, (transform.position + (new Vector3(x * worldsize / 2, j * worldsize / 2, k * worldsize / 2))), transform.rotation) as GameObject;
                    point.SetActive(false);
                    //point.AddComponent<octree_07>();
                    /*octree_07 treescript = point.GetComponent<octree_07>();
                    treescript.setID(limit);

                    treescript.lowertier(limit, tier, worldsize, leafs, branch, pool, gameObject, outsideNode);
                    treescript.updateBar(barcode);
                    treescript.setBar(limit, tier, setZone(point, transform.position));
                    */
                    point.transform.SetParent(transform);
                    point.name = "" + x + " " + j + " " + k + " " + count;
                    point.SetActive(true);
                    Listquad[count] = point;
                    count++;
                }
            }
        }
        count = 0;
    }
    /*
    public void attach_leaf(int id, catagorize note)
    {
        root[id] = note;
    }
    */

    public void CreateLeaves()
    {
        for (int x = 0; x < 2; x++)
        {
            for (int j = 0; j < 2; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    GameObject leaf = Instantiate(leafs, (transform.position + (new Vector3(x * worldsize / 2, j * worldsize / 2, k * worldsize / 2))), transform.rotation) as GameObject;
                    leaf.SetActive(false);
                    leaf.transform.SetParent(transform);
                    leaf.name = "" + List_sorter(leaf.transform.position);
                    leaf.SetActive(true);
                    Listquad[count] = leaf;
                    leaf_count = leaf_count + 1;
                    count++;
                }
            }
        }
        count = 0;
    }

    /*
    public int[] sort(Transform locate)
    {
        int[] log = new int[limit + 1];
        if (tier > 0)
        {
            if (((int)(locate.transform.position.x - transform.position.x) / (worldsize/2)) == 0)
            {
                if (((int)(locate.transform.position.y - transform.position.y) / (worldsize/2)) == 0)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 0)
                    {
                        Listquad[0].GetComponent<octree_07>().sort(locate);
                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 1)
                    {
                        Listquad[1].GetComponent<octree_07>().sort(locate);

                    }
                }
                else if (((int)(locate.transform.position.y - transform.position.y) / (worldsize/2)) == 1)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 0)
                    {
                        Listquad[2].GetComponent<octree_07>().sort(locate);

                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 1)
                    {
                        Listquad[3].GetComponent<octree_07>().sort(locate);

                    }
                }
            }
            else if (((int)(locate.transform.position.x - transform.position.x) / (worldsize/2)) == 1)
            {
                if (((int)(locate.transform.position.y - transform.position.y) / (worldsize/2)) == 0)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 0)
                    {
                        Listquad[4].GetComponent<octree_07>().sort(locate);

                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 1)
                    {
                        Listquad[5].GetComponent<octree_07>().sort(locate);

                    }
                }
                else if (((int)(locate.transform.position.y - transform.position.y) / (worldsize/2)) == 1)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 0)
                    {
                        Listquad[6].GetComponent<octree_07>().sort(locate);

                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / (worldsize/2)) == 1)
                    {
                        Listquad[7].GetComponent<octree_07>().sort(locate);

                    }
                }
            }
        }
        else
        {
            if (((int)(locate.transform.position.x - transform.position.x) / worldsize) == 0)
            {
                if (((int)(locate.transform.position.y - transform.position.y) / worldsize) == 0)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 0)
                    {
                        barcode[tier] = 0;
                        log = barcode;
                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 1)
                    {
                        barcode[tier] = 1;

                        log = barcode;

                    }
                }
                else if (((int)(locate.transform.position.y - transform.position.y) / worldsize) == 1)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 0)
                    {
                        barcode[tier] = 2;

                        log = barcode;
                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 1)
                    {
                        barcode[tier] = 3;

                        log = barcode;
                    }
                }
            }
            else if (((int)(locate.transform.position.x - transform.position.x) / worldsize) == 1)
            {
                if (((int)(locate.transform.position.y - transform.position.y) / worldsize) == 0)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 0)
                    {
                        barcode[tier] = 4;

                        log = barcode;
                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 1)
                    {
                        barcode[tier] = 5;

                        log = barcode;
                    }
                }
                else if (((int)(locate.transform.position.y - transform.position.y) / worldsize) == 1)
                {
                    if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 0)
                    {
                        barcode[tier] = 6;

                        log = barcode;
                    }
                    else if (((int)(locate.transform.position.z - transform.position.z) / worldsize) == 1)
                    {
                        barcode[tier] = 7;

                        log = barcode;
                    }
                }
            }
        }
        return log;
    }
    */
    //goes through a specific tree branch  to return a leaf


    public int List_sorter(Vector3 leafpoint)
    {
        int jx;
        int jy;
        int jz;
        jx = (int)(leafpoint.x / dimension);
        jy = (int)(leafpoint.y / dimension);
        jz = (int)(leafpoint.z / dimension);
        int headingTo = (jx * (boxlimit * boxlimit)) + (jy * boxlimit) + jz;
        return headingTo;
    }

}