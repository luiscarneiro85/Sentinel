using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_1 : MonoBehaviour {

    float timeShoot;
    Animator anim;
    bool isActive = true;

    public GameObject bullet;
    public GameObject spot;

    public GameObject bar;
    float hp;
    [SerializeField]
    float hpMax;
    float aux = 0;

	// Use this for initialization
	void Start () {
        timeShoot = Random.Range(3, 7);
        anim = GetComponent<Animator>();
        hp = hpMax;
	}
	
	// Update is called once per frame
	void Update () {
        fillBar();

        if (isActive && anim.GetCurrentAnimatorStateInfo(0).IsTag("Ready"))
        {
            StartCoroutine(shoot(timeShoot));
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Fired"))
        {
            isActive = true;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("End"))
        {
            Destroy(this.gameObject,1f);
        }

    }

    IEnumerator shoot(float time)
    {
        isActive = false;
        yield return new WaitForSeconds(time);
        if(hp > 0)
        {
            anim.SetTrigger("Fire");
            yield return new WaitForSeconds(0.5f);
            Instantiate(bullet, spot.transform.position, spot.transform.rotation);
        }
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            hp--;
            if(hp <= 0)
            {
                anim.SetTrigger("Died");
                GetComponent<CapsuleCollider2D>().enabled = false;            }
        }

        if (collision.gameObject.tag == "BulletWinchester")
        {
            hp -=2;
            if (hp <= 0)
            {
                anim.SetTrigger("Died");
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }

    void fillBar()
    {
        aux = hp / hpMax;
        bar.GetComponent<Image>().fillAmount = aux;
    }

    public void loseHp(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            anim.SetTrigger("Died");
        }       
    }
}
