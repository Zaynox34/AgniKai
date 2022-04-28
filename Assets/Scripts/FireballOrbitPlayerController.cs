using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballOrbitPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject prefabFireBall;
    [SerializeField] private GameObject prefabTargetBall;
    [SerializeField] private GameObject prefabOriginBall;
    [SerializeField] private GameObject fireBallGroup;
    [SerializeField] private GameObject targetBallGroup;
    [SerializeField] private GameObject originBallGroup;
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
    public void MeroriseOriginBall()
    {
        for (int i = 0; i < nbFireball; i++)
        {
            originBallGroup.transform.GetChild(i).position = targetBallGroup.transform.GetChild(i).position;
        }
    }
    public void NaturalSpawn()
    {

        if (spawnCounter >= spawnRythm && nbFireball < maxNbFireball)
        {
            MeroriseOriginBall();
            GameObject fireBall = Instantiate(prefabFireBall);
            fireBall.transform.parent = fireBallGroup.transform;
            fireBall.transform.position = fireBallGroup.transform.position;

            GameObject originBall = Instantiate(prefabOriginBall);
            originBall.transform.parent = originBallGroup.transform;
            originBall.transform.position = originBallGroup.transform.position;

            GameObject targetBall = Instantiate(prefabTargetBall);
            targetBall.transform.parent = targetBallGroup.transform ;
            targetBall.transform.position = targetBallGroup.transform.position;

            fireBall.GetComponent<FireBallManager>().originLerp = originBall;
            fireBall.GetComponent<FireBallManager>().targetLerp = targetBall;

            nbFireball++;
            ArangeBall();

            spawnCounter = 0;
        }
    }
    
    public void ArangeBall()
    {
        if (nbFireball > 0 && nbFireball <=maxNbFireball)
        {
            for (int i = 0; i < nbFireball; i++)
            {
                targetBallGroup.transform.GetChild(i).position = transform.position + sizeOrbit * new Vector3(Mathf.Cos(360 / nbFireball * i*Mathf.Deg2Rad),0, Mathf.Sin(360 / nbFireball * i * Mathf.Deg2Rad));
                targetBallGroup.transform.GetChild(i).position += new Vector3(0, 1, 0);
                
                fireBallGroup.transform.GetChild(i).GetComponent<FireBallManager>().canLerp = true;
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
        transform.parent.GetComponent<PlayerController>().playerSpeed=(maxSpeed-realSpeed)/3+8;

    }
    public void Feuer()
    {
        if (canFire && nbFireball > 0)
        {
            Destroy(fireBallGroup.transform.GetChild(nbFireball - 1).GetComponent<FireBallManager>().originLerp);
            Destroy(fireBallGroup.transform.GetChild(nbFireball - 1).GetComponent<FireBallManager>().targetLerp);
            if(realSpeed==0)
            {
                fireBallGroup.transform.GetChild(nbFireball - 1).GetComponent<FireBallManager>().velocity = directionFire*20;
            }
            else
            {
                fireBallGroup.transform.GetChild(nbFireball - 1).GetComponent<FireBallManager>().velocity = directionFire * realSpeed;
            }
            
            fireBallGroup.transform.GetChild(nbFireball - 1).parent=null;
            canFire =false;
            nbFireball--;
            ArangeBall();
        }
    }
    public void StopRotation()
    {
        speedOrbit = 0;
    }

}
