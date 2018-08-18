using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour {

    Vector3 target;
    Vector3 direction;
    public GameObject aim;
    public Transform fireSpot;
    public GameObject bullet;
    public GameObject gm;
    Animator anim;

    public List<GameObject> arrayBullet = new List<GameObject>();
    public Sprite bulletSprite;

    float delay;
    float reloadDelay;
    int bulletNumber;
    int bulletMax;
    int amountAmmo;

    GunConfig gScript;
    
    bool isReady = true;
    bool isReloading = false;
    bool reloadCicle = true;

    private void Awake()
    {
        gScript = this.GetComponent<GunConfig>();
        getConfigVariables();
    }

    // Use this for initialization
    void Start () {
        aim.SetActive(false);
        anim = GetComponent<Animator>();

        gm.SendMessage("updateAmountAmmo", amountAmmo);
        fillBulletCase();

        


    }
	
	// Update is called once per frame
	void Update () {

        if (bulletNumber <= 0) isReloading = true;
        if (amountAmmo <= 0) amountAmmo = 0;

        if (isReloading && reloadCicle)
        {
            reload();
        }
        
        //verifica se foi precionado a tela se é possivel atirar e se existe munição restante
        if (Input.GetMouseButtonDown(0) && isReady && bulletNumber > 0)
        {
            bulletNumber -= 1;
            paintBulletCase();
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            aim.transform.position = target;
            StartCoroutine(fired());
            if (isReloading)
            {
                isReloading = false;
                reloadCicle = true;
            }
            //desativa o disparo por um curto periodo de tempo
            StartCoroutine(delayCount());
        } 
    }

    void getConfigVariables()
    {
        delay = gScript.getDelay();
        reloadDelay = gScript.getReloadDelay();
        bulletNumber = gScript.getBulletNumber();
        bulletMax = gScript.getBulletMax();
        amountAmmo = gScript.getAmountAmmo();
    }

    public void fillBulletCase()
    {
        for (int i = 0; i < bulletNumber; i++)
        {
            arrayBullet[i].GetComponent<Image>().sprite = bulletSprite;
        }

        paintBulletCase();
    }

    public int getAmountAmmo()
    {
        return amountAmmo;
    }

    void paintBulletCase()
    {
        for (int i = 0; i < bulletMax; i++)
        {
            if (i < bulletNumber)
            {
                arrayBullet[i].SetActive(true);
            }
            else
            {
                arrayBullet[i].SetActive(false);
            }
        }
    }
    
    void reload()
    {
        if(bulletNumber < bulletMax)
        {
            
            StartCoroutine(reloading());
        }
    }

    public void setIsReloading()
    {
        isReloading = true;
        reload();
    }



    IEnumerator fired()
    {
        Instantiate(bullet, fireSpot.position,transform.rotation);
        anim.SetTrigger("Fire");
        aim.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        aim.SetActive(false);
    }

    IEnumerator delayCount()
    {
        isReady = false;
        yield return new WaitForSeconds(delay);
        isReady = true;
    }

    IEnumerator reloading()
    {
        reloadCicle = false;
        yield return new WaitForSeconds(reloadDelay);
        if (isReloading && amountAmmo > 0)
        {
            bulletNumber += 1;
            amountAmmo -= 1;
            gm.SendMessage("updateAmountAmmo", amountAmmo);
        }
        paintBulletCase();
        reloadCicle = true;
        
    }


}
