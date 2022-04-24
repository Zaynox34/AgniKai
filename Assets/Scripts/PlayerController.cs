using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    [SerializeField] private GameObject fireBallOrbitPlayer;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private Vector2 mouvementInput = Vector2.zero;
    [SerializeField] private float rotateFireInput = 0;
    [SerializeField] private bool stopedRotation = false;
    [SerializeField] private bool fired = false;
    [SerializeField] private float power;

    // Start is called before the first frame update
    void Start()
    {      
        playerSpeed = 8f;
        power = 2f;

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        mouvementInput = context.ReadValue<Vector2>();
    }
    public void OnRotateFire(InputAction.CallbackContext context)
    {
        rotateFireInput = context.ReadValue<float>();
        
    }
    public void OnStopedRotation(InputAction.CallbackContext context)
    {
        stopedRotation = context.action.triggered;
        if (context.started == true)
        {
            fireBallOrbitPlayer.GetComponent<FireballOrbitPlayerController>().StopRotation();
        }
    }
    public void Onfire(InputAction.CallbackContext context)
    {
        fired = context.started;
        if (context.started==true)
        {
            
            fireBallOrbitPlayer.GetComponent<FireballOrbitPlayerController>().canFire = true;
            fireBallOrbitPlayer.GetComponent<FireballOrbitPlayerController>().directionFire = playerBody.transform.forward * power;
        }
    }
    // Update is called once per frame
    void Update()
    { 
        Vector3 move = new Vector3(mouvementInput.x, 0, mouvementInput.y);
        transform.position+=move * Time.deltaTime * playerSpeed;
        fireBallOrbitPlayer.GetComponent<FireballOrbitPlayerController>().AxerlerBoule(rotateFireInput);
        if (move != Vector3.zero)
        {
            playerBody.transform.forward = move;
        }
    }
}
