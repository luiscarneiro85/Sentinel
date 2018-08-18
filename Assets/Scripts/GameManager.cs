using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    int playerLifes;
    float playerHp;
    float playerHpMax = 10;
    int playerMoney;
    int playerScore;
    public int playerBomb;
    int playerBombMax = 3;

    public GameObject hpBar;
    GameObject player;
    public GameObject gun;
    public GameObject winchester;
    public GameObject ammoText;

    public bool isHit = false;

    //variavis do controle de fade
    public GameObject fade;
    Animator anim;

	// Use this for initialization
	void Start () {
        playerHp = playerHpMax;
        playerBomb = playerBombMax;
        fade.SetActive(true);
        anim = fade.GetComponent<Animator>();
        anim.SetTrigger("Out");
	}
	
	// Update is called once per frame
	void Update () {
        fillBarHp();

        if (playerBomb < 0) playerBomb = 0;
        if (playerBomb > playerBombMax) playerBomb = playerBombMax;
    }

    void updateAmountAmmo(int ammo)
    {
        ammoText.GetComponent<Text>().text = ammo.ToString();
    }

    public void damaged(float hp)
    {
        if(!isHit)
        {
            playerHp -= hp;
            if (playerHp <= 0)
            {
                playerHp = 0;
                gameOver();
            }

            StartCoroutine(isInvencible());
        }
        
    }

    public void gameOver()
    {
        fade.SetActive(true);
        anim.SetTrigger("In");
    }


    public void gainHp(float hp)
    {
        playerHp += hp;
        if (playerHp >= playerHpMax) playerHp = playerHpMax;
    }

    void fillBarHp()
    {
        hpBar.GetComponent<Image>().fillAmount = playerHp / playerHpMax;
    }

    public void changeWeapon(int id)
    {
        switch(id)
        {
            case 1:
                gun.SetActive(false);
                winchester.SetActive(true);
                break;
            case 2:
                winchester.SetActive(false);
                gun.SetActive(true);
                break;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        player.gameObject.SendMessage("fillBulletCase");
        updateAmountAmmo(player.GetComponent<Gun>().getAmountAmmo());
    }

    IEnumerator isInvencible()
    {
        isHit = true;
        yield return new WaitForSeconds(0.7f);
        isHit = false;
    }


}
