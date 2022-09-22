using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    Water water;
    public PlayerMovement player, bottom;
    Collider2D collide;
    private Rigidbody2D rb;
    SpriteRenderer sr;
    public Sprite sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        water = this.gameObject.GetComponent<Water>();
        collide = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (player.waterI == true || bottom.waterI == true)
        {
            sr.sprite = sprite;
            collide.isTrigger = false;
            gameObject.layer = 8;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Debug.Log("player hit water");
            if (player.waterI == true)
            {
                Debug.Log("water item registered in water");
            }
            //gameObject.Find("Water").collider.isTrigger = true;
            //GetComponent<Collider>().isTrigger = false;
            //water.enabled = !water.enabled;
        }
    }
}

