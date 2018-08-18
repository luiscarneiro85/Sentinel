using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burguer : MonoBehaviour {

    GameObject gm;

    // Use this for initialization
    void Start () {
     gm = GameObject.Find("GameManeger");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {

            gm.gameObject.SendMessage("gainHp", 3);
            Destroy(this.gameObject);
        }
    }
}
