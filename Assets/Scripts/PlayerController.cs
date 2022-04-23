using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject fireBallOrbitPlayer;
    [SerializeField] private GameObject playerBody;
    [SerializeField] private Vector2 mouvementInput = Vector2.zero;
    [SerializeField] private float rotateFireInput = 0;
    [SerializeField] private bool stopedRotation = false;
    [SerializeField] private bool fired = false;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 10f;

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        mouvementInput = context.ReadValue<Vector2>();
    }
    public void OnRotateFire(InputAction.CallbackContext context)
    {
        rotateFireInput = context.ReadValue<float>();
        Debug.Log("aaa");
        fireBallOrbitPlayer.GetComponent<FireballOrbitPlayerController>().AxerlerBoule(rotateFireInput*100);
    }
    public void OnStopedRotation(InputAction.CallbackContext context)
    {
        stopedRotation = context.action.triggered;
    }
    public void Onfire(InputAction.CallbackContext context)
    {
        fired = context.action.triggered;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(mouvementInput.x, 0, mouvementInput.y);
        transform.position+=move * Time.deltaTime * playerSpeed;

        if (move != Vector3.zero)
        {
            playerBody.transform.forward = move;
        }
    }
}
