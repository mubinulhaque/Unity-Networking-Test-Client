using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform cameraObject;
    public Camera playerCamera;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            ClientSend.PlayerShoot(cameraObject.forward);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ClientSend.PlayerThrow(cameraObject.forward);
        }
    }

    private void FixedUpdate()
    {
        UIManager.instance.playerCamera = playerCamera;
        UIManager.instance.setDrawUsernames(true);
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] inputs = new bool[]
        {
            Input.GetKey(KeyCode.W),
            Input.GetKey(KeyCode.S),
            Input.GetKey(KeyCode.A),
            Input.GetKey(KeyCode.D),
            Input.GetKey(KeyCode.Space)
        };

        ClientSend.PlayerMovement(inputs);
    }
}