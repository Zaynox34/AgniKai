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

    // Start is called before the first frame update
    void Start()
    {
        nbFireball=1;
        sizeOrbit=5;
        speedOrbit=0;
        spawnCounter = 0;
        spawnRythm =10;
        maxNbFireball=10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
