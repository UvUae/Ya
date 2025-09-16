using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArpgCameraC : MonoBehaviour
{
     
    // Start is called before the first frame update
    public Transform target;
    float Xspeed =150;
    float Yspeed = 125;

    float x = 0;
    float y = 0;

    float distance = 3;
    float zoomRate = 80;
    float yMinLimit = 10;
    float yMaxlimit = 70;
    float disMinLimit = 2;
    float disMaxLimit = 5;

    public Vector3 offset = new Vector3(0,0,0);
    Vector3 rotateOffset = new Vector3(0,0,-3);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        x += InputC.instance.MouseInput.x * Xspeed * Time.deltaTime;
        y -= InputC.instance.MouseInput.y * Yspeed * Time.deltaTime;
        //x += InputC.instance.m_Camera.x * Xspeed * Time.deltaTime;
        //y -= InputC.instance.m_Camera.y * Yspeed * Time.deltaTime;
        y = clampAngle(y,yMinLimit,yMaxlimit );
        Quaternion rotation = Quaternion.Euler(y,x,0);
        transform.rotation = rotation;
        if(InputC.instance.Xinput!=0 || InputC.instance.Yinput !=0)
        {
            target.transform.rotation = Quaternion.Euler(0,x,0);
        }
        distance -= (InputC.instance.m_Camera.z * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        //if (InputC.instance.m_Movement.x !=0 || InputC.instance.m_Movement.y !=0)
        //{
        //    target.transform.rotation = Quaternion.Euler(0,x,0);
        //}
        //distance -= (InputC.instance.m_Camera.z * Time.deltaTime) * zoomRate * Mathf.Abs(distance);
        distance = Mathf.Clamp(distance , disMinLimit, disMaxLimit);
        transform.position = target.position + offset + rotation * (rotateOffset * distance) ;
        
    }

    float  clampAngle(float angle, float min ,float max)
    {
        if (angle >360)
        {
            angle -= 360;
        }else if (angle < -360)
        {
            angle += 360;
        }
        return Mathf.Clamp(angle,min,max);
    }

}
