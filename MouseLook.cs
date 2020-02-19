using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Rigidbody body;

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;

    //sensitivy for horizontal and vertical rotation
    [SerializeField] float sensitivityHor = 9.0f;
    [SerializeField] float sensitivityVert = 9.0f;

    //min and max for vertical rotation
    [SerializeField] float minVert = -45.0f;
    [SerializeField] float maxVert = 45.0f;

    // vertical rotation vector component
    private float _rotationX = 0;


    //on start
    void Start()
    {
        body = GetComponent<Rigidbody>();

        //freezes rigid body
        freezeBodyRotation();
    }


    //on update
    void Update()
    {
        getMouseInput();

    }


    //freezes Rotation of rigidBody, pervents game physics from influencing rotation
    private void freezeBodyRotation()
    {
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }


    //determines rotation axis and calls corresponding rotation function
    private void getMouseInput()
    {
        //horizontal rotation
        if (axes == RotationAxes.MouseX)
        {
            xMouseLook();
        }
        //vertical rotation
        else if (axes == RotationAxes.MouseY)
        {
            yMouseLook();
        }
        //simultaneous horizontal and vertical rotation
        else
        {
            xyMouseLook();
        }
    }


    // rotates horizontally and vertically based on X and Y Mouse Input, and sensitivity settings
    private void xyMouseLook()
    {
        // calculates vertical rotation
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;

        //limits vertical rotation between min and max
        _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

        //calculates horiztonal rotation
        float delta = Input.GetAxis("Mouse X") * sensitivityHor;
        float rotationY = transform.localEulerAngles.y + delta;

        //performs rotation
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }


    //rotates vertically based on Y Mouse input and vertical sensitivity setting
    private void yMouseLook()
    {
        //calculates and limits vertical rotation
        _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
        _rotationX = Mathf.Clamp(_rotationX, minVert, maxVert);

        float rotationY = transform.localEulerAngles.y;

        // performs vertical rotation
        transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
    }


    //rotates horizontally based on X Mouse input and horizontal sensitivity setting
    private void xMouseLook()
    {
        //performs horizontal rotation
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
    }
}
