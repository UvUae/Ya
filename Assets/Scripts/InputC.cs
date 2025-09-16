using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputC : MonoBehaviour
{
    public static InputC instance; 
    public Vector2 m_Movement;
    public Vector3 m_Camera;
    public bool m_atkTrigger;
    public float Xinput;
    public float Yinput;
    public Vector2 MouseInput;
    public bool jumpInput;
    public bool atkBtn;

    private void Awake()
    {
        instance = this; 
    } 
    void Start()
    { 
    }  
    void Update()
    { 
        //  AD     ×óÓÒ                                  WS   ÉÏÏÂ¼ýÍ·
        //m_Movement.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));    
        //m_Camera.Set( Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"),Input.GetAxis("Mouse ScrollWheel") );
        //m_atkTrigger = Input.GetMouseButtonDown(0);

        //Debug.Log(m_Camera.z);
    
        Xinput = Input.GetAxis("Horizontal");
        Yinput = Input.GetAxis("Vertical");
        atkBtn = Input.GetMouseButtonDown(0);
        MouseInput.Set(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}
