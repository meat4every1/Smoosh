﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitbox : MonoBehaviour {


    public GameObject player;
    public GameObject queue;
    ChallengerCon p;
    HBQ q;
    public Transform pos;
    public Renderer hbrend;

    //properties of a hitbox used imminently
    public bool active; //turns on hitbox
    public int activeOn; //first frame hitbox becomes active -- !!!!move activeOn to player, to prevent attacks from happening if a player is interupted!!!!
    public float size;
    public int duration;
    public Vector3 location;
    public bool tethered; //punch vs missile
    public Vector3 direction; // hitbox listens for updates to this

    //may just use direct manipulation of 'direction' to do this
    public bool grav; // is the projectile affected by gravity
    public float floaty; //how gravity affects it

    //properties that are only important on contact
    public int playerNum; //who spawned the hitbox
    public Vector3 angle; //angle the victom is sent off at
    public int dmg;
    public int sdmg; //sheild dmg
    public bool grab; //grabs >> shields
    public int priority;  //The raiting at wich a move will overide another simulataneously cast and overlapping move
    public float bkb; //base knockback
    public float skb; //scaling knockback
    public int atkDir;

    public bool clanked; //tells if another move clanked the current hitbox 

    public string special; //used for weird moves, hitbox listens for updates to this



    //pass-on traits - spawn a new hitbox when the current one finishes

    // Use this for initialization
    void Start () {
        hbrend = GetComponent<Renderer>();

        special = "";
        clanked = false;
        active = false;
        duration = 0;
        pos = GetComponent<Transform>();
        p = player.GetComponent<ChallengerCon>();
        q = queue.GetComponent<HBQ>();
        size = 0.3f;

        hbrend.material.color = Color.yellow;

    }
	
	// Update is called once per frame
	void Update () {

        if (duration > 0)
        {
            p = player.GetComponent<ChallengerCon>(); //refactor potential
            duration--;
            //Debug.Log("Yo it's in!", gameObject);
            if (activeOn > 0) {
                activeOn--;
                if (activeOn == 0)
                {
                    active = true;
                    pos.position = player.transform.position + location;
                    hbrend.material.color = Color.red;
                }
            }
            
            //pos.position = size; rescale the hitbox
            pos.transform.localScale = new Vector3(size, size, size);
            if (tethered)
            {
                pos.position = player.transform.position + location;

            } else
            {
                pos.position = pos.position + direction;
            }

            //return to offscreen position
            if (duration==0 || clanked) {
                pos.position = new Vector3(-10, 5, 3);
                hbrend.material.color = Color.yellow;
                clanked = false;
                active = false;
                //push into q
                q = queue.GetComponent<HBQ>(); //refactor potential
                q.EnQ(this);
            }
            
        } 

        

    }
}
