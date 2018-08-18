using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bomb : MonoBehaviour {

    float posX;
    float posY;
    int count = 0;
    public GameObject explosion;
    public GameObject textBomb;
    bool isReady = false;
    int maxExplosions = 20;
    [SerializeField]
    bool isActive = false;
    GameManager gm;
    bool enemyBombDamage = false;


	// Use this for initialization
	void Start () {
        gm = gameObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        paintNumberBombs();
        randomPosition();
        if (count < maxExplosions && isReady)
        {
            isActive = true;
            StartCoroutine(makeExplosion());
        }
        else if(count >= maxExplosions)
        {
            isActive = false;
            isReady = false;
            count = 0;
            enemyBombDamage = false;
        }

        if (isActive)
        {
            bombIsActive();
        }

    }

    void paintNumberBombs()
    {
        textBomb.GetComponent<Text>().text = "X " + gm.playerBomb;
    }

    void randomPosition()
    {
        posX = Random.Range(2.3f, 16f);
        posY = Random.Range(-.5f, -7f);
    }

    IEnumerator makeExplosion()
    {
        isReady = false;
        yield return new WaitForSeconds(.1f);
        Instantiate(explosion, new Vector2(posX, posY), transform.rotation);
        count++;
        isReady = true;
    }

    void bombIsActive()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("EnemyBullet");

        foreach(GameObject obj in go)
        {
            Destroy(obj.gameObject);
        }

        if (!enemyBombDamage)
        {
            enemyBombDamage = true;
            GameObject[] gObj = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject obj in gObj)
            {
                obj.GetComponent<Transform>().SendMessage("loseHp", 1f);
            }
        }
        
    }

    public void changeIsReady()
    {
        
        if (!isActive && gm.playerBomb > 0)
        {
            isReady = true;
            gm.playerBomb--;
        }
        

    }


}
