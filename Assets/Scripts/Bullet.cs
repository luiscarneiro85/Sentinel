using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    Vector3 target;
    Vector3 direction;
    [SerializeField]
    float speed;
    bool is_active = true;
    GameObject gm;
    Animator anim;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("Aim");
        target = go.transform.position;
        anim = GetComponent<Animator>();
        GetComponent<CircleCollider2D>().enabled = false;
        gm = GameObject.Find("GameManeger");
    }
	
	// Update is called once per frame
	void Update () {
        

        if ((target.y - transform.position.y) > 0.1f && is_active)
        {
            direction = (target - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * speed);
            
        }
        if ((target.y - transform.position.y ) < 0.1f)
        {

            anim.SetTrigger("Hit");
            GetComponent<CircleCollider2D>().enabled = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("end"))
        {
            Destroy(this.gameObject);
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            is_active = false;
            anim.SetTrigger("Hit");
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag=="Winchester")
        {
            gm.gameObject.SendMessage("changeWeapon", 1);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Gun")
        {
            gm.gameObject.SendMessage("changeWeapon", 2);
            Destroy(collision.gameObject);
        }
    }

}
