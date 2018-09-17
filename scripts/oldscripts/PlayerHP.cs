using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    int hp;
    public int hp_max;
    public Slider mainSlider;
    // Use this for initialization
    void Start()
    {
        hp = hp_max;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
        if (mainSlider)
        {
            mainSlider.value = divide(hp, hp_max);

        }

    }
    public void hpminus()
    {
        hp = hp - 1;
    }
    public float divide(float x, float y)
    {
        return x / y;
    }
}

