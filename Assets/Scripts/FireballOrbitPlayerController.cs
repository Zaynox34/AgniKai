using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballOrbitPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject prefabFireBall;
    [SerializeField] private int nbFireball;
    [SerializeField] private float sizeOrbit;
    [SerializeField] private float speedOrbit;
    [SerializeField] private float spawnRythm;
    [SerializeField] private float spawnCounter;
    [SerializeField] private int maxNbFireball;
    [SerializeField] private int maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        nbFireball=0;
        sizeOrbit=5;
        speedOrbit=0;
        spawnCounter = 0;
        spawnRythm =2;
        maxNbFireball=10;
    }

    // Update is called once per frame
    void Update()
    {
        BougetesBoule();
        if (spawnCounter>=spawnRythm && nbFireball<maxNbFireball)
        {
            nbFireball++;
            GameObject fireBall =Instantiate(prefabFireBall);
            fireBall.transform.parent = transform;
            ArangeBall();
            spawnCounter = 0;
        }
        
        
        
        spawnCounter += Time.deltaTime;
    }
    public void ArangeBall()
    {
        if (nbFireball > 0 && nbFireball <=10)
        {
            for (int i = 0; i < nbFireball; i++)
            {
                transform.GetChild(i).position = transform.position + sizeOrbit * new Vector3(Mathf.Cos(360 / nbFireball * i*Mathf.Deg2Rad), 0, Mathf.Sin(360 / nbFireball * i * Mathf.Deg2Rad));
            }
        }
    }
    public void BougetesBoule()
    {
       transform.Rotate(new Vector3(0,speedOrbit,0)*Time.deltaTime);
    }
    public void AxerlerBoule(float ax)
    {
        speedOrbit+=ax;
    }
}
