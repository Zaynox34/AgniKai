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
    [SerializeField] private float realSpeed;
    public Vector3 directionFire;
    public bool canFire;

    // Start is called before the first frame update
    void Start()
    {
        nbFireball=0;
        sizeOrbit=5;
        speedOrbit=0;
        spawnCounter = 0;
        spawnRythm =2;
        maxSpeed = 80;
        maxNbFireball=10;
        canFire=false;
}

    // Update is called once per frame
    void Update()
    {
        BougetesBoule();
        NaturalSpawn();
        Feuer();
        spawnCounter += Time.deltaTime;
    }
    public void NaturalSpawn()
    {
        if (spawnCounter >= spawnRythm && nbFireball < maxNbFireball)
        {
            nbFireball++;
            GameObject fireBall = Instantiate(prefabFireBall);
            fireBall.transform.parent = transform;
            ArangeBall();
            spawnCounter = 0;
        }
    }
    public void ArangeBall()
    {
        if (nbFireball > 0 && nbFireball <=10)
        {
            for (int i = 0; i < nbFireball; i++)
            {
                transform.GetChild(i).position = transform.position + sizeOrbit * new Vector3(Mathf.Cos(360 / nbFireball * i*Mathf.Deg2Rad),0, Mathf.Sin(360 / nbFireball * i * Mathf.Deg2Rad));
                transform.GetChild(i).position += new Vector3(0, 1,0);
            }
        }
    }
    public void BougetesBoule()
    {
       transform.Rotate(new Vector3(0,speedOrbit,0)*Time.deltaTime);
    }
    public void AxerlerBoule(float ax)
    {

        if (Mathf.Abs((speedOrbit+ax) * Mathf.Deg2Rad * sizeOrbit) > maxSpeed)
        {
            realSpeed = 80;
        }
        else
        {
            speedOrbit += ax;
        }
        realSpeed = Mathf.Abs(speedOrbit * Mathf.Deg2Rad * sizeOrbit);
        transform.parent.GetComponent<PlayerController>().playerSpeed=(maxSpeed-realSpeed)/5+8;

    }
    public void Feuer()
    {
        if (canFire && nbFireball > 0)
        {
            Debug.Log("aaa");
            transform.GetChild(nbFireball - 1).GetComponent<FireBallManager>().velocity=directionFire*realSpeed;
            transform.GetChild(nbFireball - 1).parent=null;
            canFire=false;
            nbFireball--;
            ArangeBall();
        }
    }
    public void StopRotation()
    {
        speedOrbit = 0;
    }

}
