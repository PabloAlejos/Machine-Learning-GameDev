using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{

    public GameObject bulletType;
    public float fireRate = 10;
    private float nextShotTime;

    //Heat System
    [SerializeField]
    private HeatBar bar;
    public float heatValue;
    [SerializeField]
    private float coolingTime = 500;
    private float startCoolingTime;
    private float maxHeatValue = 150;
    bool overHeat;

    SoundController sounds;

    // Use this for initialization
    void Start()
    {
        nextShotTime = Time.time + fireRate / 10;
        MaxHeatValue = maxHeatValue;
        HeatValue = heatValue;
    }


    public void Shoot()
    {
        if (Time.time > nextShotTime && HeatValue < maxHeatValue && !overHeat)
        {
            HeatValue += 1;
            Instantiate(bulletType, transform.position, Quaternion.identity);
            nextShotTime = Time.time + fireRate / 10;
            startCoolingTime = Time.time + coolingTime;
        }

        if (HeatValue >= maxHeatValue)
        {
            overHeat = true;
            startCoolingTime = Time.time + coolingTime * 2;
        }
    }

    void Update()
    {    
        if (Time.time > startCoolingTime)
        {
            overHeat = false;
            if (HeatValue > 0)
            {
                HeatValue -= 10 * Time.deltaTime;
            } 
        }
    }


    public void CoolGun(float val)
    {
        heatValue -= val;
    }

    public float HeatValue
    {
        get
        {
            return heatValue;
        }

        set
        {
            
            this.heatValue = value;   
            bar.Value = heatValue;
        }
    }

    public float MaxHeatValue
    {
        get
        {
            return maxHeatValue;
        }

        set
        {
            this.maxHeatValue = value;
            bar.MaxValue = maxHeatValue;
        }
    }
}
