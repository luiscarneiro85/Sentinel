using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunConfig : MonoBehaviour {

    [SerializeField]
    float delay = 0.5f;
    [SerializeField]
    float reloadDelay = 0.8f;
    [SerializeField]
    int bulletNumber = 6;
    [SerializeField]
    int bulletMax = 6;
    [SerializeField]
    int amountAmmo = 3;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public float getDelay()
    {
        return delay;
    }

    public float getReloadDelay()
    {
        return reloadDelay;
    }

    public int getBulletNumber()
    {
        return bulletNumber;
    }

    public int getBulletMax()
    {
        return bulletMax;
    }

    public int getAmountAmmo()
    {
        return amountAmmo;
    }
}
