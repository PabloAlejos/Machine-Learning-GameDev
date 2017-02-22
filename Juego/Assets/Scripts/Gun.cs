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
    private float heatValue;
    [SerializeField]
    private float coolingTime = 20;
    private float startCoolingTime;
    private float maxHeatValue = 100;
    bool overHeat;

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
            HeatValue += 5;
            Instantiate(bulletType, transform.position, Quaternion.identity);
            nextShotTime += fireRate / 10;
            startCoolingTime = Time.time + coolingTime;
        }

        if (HeatValue >= 100)
        {
            overHeat = true;  
        }
    }

    void Update()
    {    
        if (Time.time > coolingTime)
        {
            if (HeatValue > 0)
            {
                HeatValue -= 10 * Time.deltaTime;
            } 
        }
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
