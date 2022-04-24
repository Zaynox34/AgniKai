using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallManager : MonoBehaviour
{
    public bool canLerp;
    public GameObject originLerp;
    public GameObject targetLerp;
    public float speedLerp;
    public float counterLerp;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        speedLerp = 5f;
        counterLerp = 0;
        velocity = Vector3.zero;
        canLerp = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        LerpNow();
    }
    public void LerpNow()
    {
        if(counterLerp>=1)
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
