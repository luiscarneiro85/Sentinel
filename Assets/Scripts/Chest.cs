using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {


    //GameObject gm;
    Animator anim;
    public GameObject prize;
    public GameObject spot;

    // Use this for initialization
    void Start () {
        //gm = GameObject.Find("GameManeger");
        anim = GetComponent<Animator>();


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            anim.SetTrigger("Hit");
        }
        if (collision.gameObject.tag == "BulletWinchester")
        {
            anim.SetTrigger("Hit");
        }
    }

    public void dropItem()
    {
        //Instantiate(prize, new Vector2(transform.position.x,transform.position.y + .5f), transform.rotation);
        //Instantiate(prize, spot.transform.position,transform.rotation);
        Instantiate(prize, transform.position, transform.rotation);
    }
}
