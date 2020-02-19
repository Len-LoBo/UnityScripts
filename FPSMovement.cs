using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Movement")]

public class FPSMovement : MonoBehaviour
{
    //movement speed
    [SerializeField] float speed = 6.0f;
    //gravity
    [SerializeField] float gravity = -9.8f;

    private CharacterController _charController;


    void Start()
    {
        //stores character controller from gameObject
        _charController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        //gets movement from Horizontal and Vertical virtual mappings
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        //creates movement vector
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        //clamps magnitude of vector so diagonal movement doesn't scale up
        movement = Vector3.ClampMagnitude(movement, speed);

        //makes movement frame rate independent
        movement *= Time.deltaTime;

        //changes movement from global to local coordinates
        movement = transform.TransformDirection(movement);

        //keeps gravity in global coordinates
        movement.y = gravity;

        //performs movement on character controller
        _charController.Move(movement);
        
    }
}
