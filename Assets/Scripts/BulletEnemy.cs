using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour {

    Vector3 target;
    Vector3 direction;

    [SerializeField]
    float speed;

	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        direction = (target - transform.position).normalized;
        transform.Translate(direction * Time.deltaTime * speed);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {            
            collision.gameObject.GetComponent<Animator>().SetTrigger("Hit");
            GameObject go = GameObject.Find("GameManeger");
            go.SendMessage("damaged", 1f);
            Destroy(this.gameObject);
        }
    }
}
