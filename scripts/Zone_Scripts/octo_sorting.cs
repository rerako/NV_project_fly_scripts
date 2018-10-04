using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class octo_sorting : MonoBehaviour
{
    private float boundary; // faucets
    private int xpoint;
    private int ypoint;
    private int zpoint;
    public int segs;
    public Octo_Arrays zone;
    public int orb_type;// object type enemy, bullet, obstacle
    public int orb_variant;//object variant enemy1 enemy2
    public int id;//id number
    public int stockx;
    public int stocky;
    public int stockz;
    public bool sorting;
    public int leafstore;
    public GameObject mesh;
    public int onList;
    public int list_numb;
    //public Vector3 pointer;
    // Use this for initialization
    void Start()
    {

        // id needs an objectpool to determine order
        //orbtype can be decided with tag or object pool
        segs = Octo_tree.totalsegments;
        boundary = Octo_tree.Cube_side;
        leafstore = Octo_tree.leaf_total;
        zone = Octo_tree.collect[sorter(transform.position)];
        sorting = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sorting)
        {
            list_numb = sorter(transform.position);
            if (list_numb > Octo_tree.leaf_total || list_numb < 0)
            {
                switch (orb_type)
                {
                    case 1:
                        if (zone != null)
                        {
                            zone.RemoveECollect(orb_variant, id);
                        }
                        onList = list_numb;
                        zone = Octo_tree.outOctoList;
                        zone.AddECollect(gameObject, orb_variant, id);
                        break;
                    case 2:


                        if (zone != null)
                        {
                            zone.RemoveBCollect(orb_variant, id);
                        }
                        onList = list_numb;
                        zone = Octo_tree.outOctoList;
                        zone.AddBCollect(gameObject, orb_variant, id);
                        break;
                    case 3:
                        break;
                    default:
                        print("Incorrect orbtype");
                        break;
                }
            }
            else
            {
                if (list_numb != onList)
                {
                    //Octo_tree.collect[list_numb].AddEList(id, orb_type);
                    switch (orb_type)
                    {
                        case 1:

                            if (zone != null)
                            {
                                zone.RemoveECollect(orb_variant, id);
                            }
                            onList = list_numb;
                            zone = Octo_tree.collect[list_numb];
                            zone.AddECollect(gameObject, orb_variant, id);

                            break;
                        case 2:

                            if (zone != null)
                            {
                                zone.RemoveBCollect(orb_variant, id);
                            }
                            onList = list_numb;
                            zone = Octo_tree.collect[list_numb];
                            zone.AddBCollect(gameObject, orb_variant, id);
                            break;

                    }

                    //Octo_tree.collect[onList].RemoveEList(id, orb_type);
                    //Debug.Log("added");


                }


            }
        }

        // Octo_tree.collect_orb[];
    }


    public void objSetter(int orb_type_numb, int orb_var, int idnumb)
    {
        orb_type = orb_type_numb;
        orb_variant = orb_var;
        id = idnumb;

    }
    public int sorter(Vector3 pointer)
    {
        xpoint = (int)(pointer.x / boundary);
        ypoint = (int)(pointer.y / boundary);
        zpoint = (int)(pointer.z / boundary);

        if (xpoint > segs)
        {
            xpoint = -1;
        }
        if (ypoint > segs)
        {
            ypoint = -1;
        }
        if (zpoint > segs)
        {
            zpoint = -1;
        }
        if (stockx != xpoint)
        {
            stockx = xpoint;
        }
        if (stockx != ypoint)
        {
            stocky = ypoint;
        }
        if (stockz != zpoint)
        {
            stockz = zpoint;

        }
        return xpoint + ypoint * segs + zpoint * segs * segs;
    }

    public void setOn()
    {
        mesh.SetActive(true);
    }
    public void setOff()
    {
        mesh.SetActive(false);
    }
    public bool getMesh()
    {
        return mesh.activeSelf;
    }
}
