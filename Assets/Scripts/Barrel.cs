using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour {


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.position.y < -11) Destroy(this.gameObject);
		
	}

    public void changeGravity()
    {
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            GetComponent<Animator>().SetTrigger("Hit");

        }

        if (collision.gameObject.tag == "BulletWinchester")
        {
            //GetComponent<Rigidbody2D>().gravityScale = 1;
            GetComponent<Animator>().SetTrigger("Hit");
        }

        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SendMessage("loseHp",2f);
            Destroy(this.gameObject);
        }
    }
}
