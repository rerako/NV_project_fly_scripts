using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour {
    public float max_size;
    public float current_size;
    public float timer;
    public Color color;
    // Use this for initialization
    void Start () {
        color = this.GetComponent<MeshRenderer>().material.color;


        Destroy(this.gameObject, timer);

    }

    // Update is called once per frame
    void Update () {
        if(color.a > 0)
        {
          color.a -= Time.deltaTime * timer * 3;
        }
        this.GetComponent<MeshRenderer>().material.color = color;
        if (current_size < max_size)
        {
            current_size = Mathf.Lerp(current_size,max_size, max_size/timer * Time.deltaTime);
            transform.localScale = new Vector3(current_size, current_size, current_size);
        }

	}
}
