using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    //public int childId;
    public bool canLerp;
    public GameObject fireballOrbitPlayerController;
    public GameObject originLerp;
    public GameObject targetLerp;
    public float speedLerp;
    public float timeOfLastDeath;
    public float counterTimeOfLastDeath;
    public float counterLerp;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        counterTimeOfLastDeath = 0;
        speedLerp = 5f;
        counterLerp = 0;
        velocity = Vector3.zero;
        canLerp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.parent==null)
        {
            DeathCheaker();
        }
        else
        {
            counterTimeOfLastDeath = 0;
        }

        transform.position += velocity * Time.deltaTime;
        LerpNow();
    }
    public void DeathCheaker()
    {
        if(counterTimeOfLastDeath>=timeOfLastDeath)
        {
            Death();
        }
        else
        {
            counterTimeOfLastDeath += Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
        Death();
            
    }
    public void Death()
    {
        if (this.transform.parent != null)
        {
            Destroy(originLerp);
            Destroy(targetLerp);
            transform.parent = null;
            fireballOrbitPlayerController.GetComponent<FireballOrbitPlayerController>().DeleteBall();
            
        }
        if (this.transform.parent == null)
        {
            Destroy(this.gameObject);
        }
    }
    public void LerpNow()
    {
        if(originLerp==null||targetLerp==null||counterLerp>=1)
        {
            canLerp = false;
            counterLerp = 0;
        }
        if (canLerp)
        {
            transform.position = Vector3.Lerp(originLerp.transform.position, targetLerp.transform.position, counterLerp);
            counterLerp += Time.deltaTime*speedLerp;
        }
    }
        
}
