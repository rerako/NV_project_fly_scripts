using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_shoot_bullet : MonoBehaviour
{
    public WaitForSeconds[] gun_bullet_cooldown;
    public float[] waitTime;
    public object_pool ammo_stache;
    public bool[] switches;
    public Transform cannon_point1;
    public Transform cannon_point2;
    GameObject bullet1;
    GameObject bullet2;
    public int ammo_type;
    public Animator anim;
    private Transform placeHolder;
    public bool anim_bd;
    public GameObject paddles;
    // Use this for initialization
    void Start()
    {
        placeHolder = transform.parent;
        anim = gameObject.GetComponent<Animator>();
        gun_bullet_cooldown = new WaitForSeconds[5];

        for (int x = 0; x < 5; x++)
        {
            gun_bullet_cooldown[x] = new WaitForSeconds( waitTime[x]);
        }
        switches = new bool[5];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !switches[ammo_type])
        {
            StartCoroutine(fire0(ammo_type));

        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            //anim.Play("barrel_dash");
            paddles.SetActive(true);

        }
        else {
            paddles.SetActive(false);

        }
        if (anim_bd)
        {
            //paddles.SetActive(true);

        }
        else
        {
            //paddles.SetActive(false);

        }
    }
    IEnumerator fire0(int ammo)
    {
        switches[ammo] = true;

        Transform bullet1 = ammo_stache.fetch_orb(2, 0);
        bullet1.gameObject.SetActive(false);

        bullet1.transform.position = cannon_point1.position;
        bullet1.transform.rotation = cannon_point1.rotation;
        bullet1.gameObject.SetActive(true);

        Transform bullet2 = ammo_stache.fetch_orb(2, 0);
        bullet2.gameObject.SetActive(false);

        bullet2.transform.position = cannon_point2.position;
        bullet2.transform.rotation = cannon_point2.rotation;
        bullet2.gameObject.SetActive(true);

        yield return gun_bullet_cooldown[ammo];
        switches[ammo] = false;


    }
    void root_motion_setOn() {
        anim.applyRootMotion = true;
        anim_bd = false;
    }
    void updatePHPos()
    {
        placeHolder.position = transform.position;
        placeHolder.rotation = transform.rotation;
    }
    void setPlayerPos() {
        transform.position = placeHolder.position;
        transform.rotation = placeHolder.rotation;
    }
    void root_motion_setOff()
    {
        anim.applyRootMotion = false;
        anim_bd = true;

    }
}
