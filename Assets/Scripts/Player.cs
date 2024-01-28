using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float sensitivity = 1f;
    public float maxJumpCooldown = .3f;

    CharacterController controller;
    Vector3 direction;
    Camera camera;

    float jumpcooldown = 0;

    float ya = 0;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpcooldown > 0) jumpcooldown -= Time.deltaTime;
        if (Input.touchCount > 0) MobileInput();
        else PCInput();
        
    }
    public void MobileInput(){

    }
    public void PCInput(){
        Cursor.lockState = Time.timeScale > 0 ? CursorLockMode.Locked : CursorLockMode.None;
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 rot = new Vector3(
            Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse X"),
            0
        );
        Movement(direction,rot);
    }
    public void Movement(Vector3 vector3 = new Vector3(),Vector3 rotation = new Vector3()){
        Vector3 moveDirection = transform.TransformDirection(vector3);
        
        if (controller.isGrounded){
            if (Input.GetButton("Jump") && jumpcooldown <= 0) jumpcooldown = maxJumpCooldown;
            if (jumpcooldown > 0)  moveDirection.y = 1;
        }
        else moveDirection.y = jumpcooldown > 0 ? 1 : Physics.gravity.y/speed;
        
        controller.Move(
            moveDirection * Time.deltaTime * speed
        );
    
        /*
        myCam.transform.localRotation = Quaternion.Euler(yr, 0, 0);
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        */
        //camera.transform.localRotation = Quaternion.Euler(rotation.x * Vector3.right);
        //transform.localRotation = Quaternion.Euler(rotation.y * Vector3.up);
        transform.Rotate(rotation.y * sensitivity * Vector3.up);
        ya += -rotation.x * sensitivity;
        ya = Mathf.Clamp(ya,-60,60);
        //camera.transform.Rotate(ya * Vector3.right);
        camera.transform.localRotation = Quaternion.Euler(ya,0,0);
    }
}
