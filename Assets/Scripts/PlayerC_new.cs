using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerC_new : MonoBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 3;
    public Animator ani;
    public GameObject weaponObj;
    float ComboTimer;
    bool isControl = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    //if (Input.GetKey(KeyCode.W))
    //{
    //    Debug.Log("�㰴����W��");
    //}
    // Update is called once per frame
    void Update()
    {  
        Atk();
        Move();
        ResetParams();
    }

    void Move()
    {
        Vector3 dir = transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"),
            0,Input.GetAxisRaw("Vertical")));
        controller.SimpleMove(dir * walkSpeed);
        ani.SetFloat("ǰ���ٶ�" , Input.GetAxisRaw("Vertical"));
        ani.SetFloat("�����ٶ�",  Input.GetAxisRaw("Horizontal")); 
        //controller.Move(dir * walkSpeed * Time.deltaTime); //Time.deltaTime ��ǰ֡����һ֡��ʱ���ֵ 
        // �豸1  ֡����30    һ����ִ��30�� update 30��  Time.deltaTime 1/30
        // �豸2  ֡����60    һ����ִ��60�� update 60��  Time.deltaTime 1/60 
        // Horizontal  A���� -1  D���� 1 
        // Vertical    W���� 1   S���� -1 
        //new Vector3(Input.GetAxisRaw("Horizontal"), 0,Input.GetAxisRaw("Vertical"))
        // ����ֻ������ W  
        // new Vector3(0,0,1)
        // ����ֻ������ S
        // new Vector3(0,0,-1)
        // ����ֻ������ A
        // new Vector3(-1,0,0)
        // ����ֻ������ D
        // new Vector3(1,0,0) 
        // W A
        // new Vector3(-1,0,1) 
        // transform.TransformDirection 
    }
    void Atk()
    {
        if (Input.GetMouseButtonDown(0) && isControl)
        {
            isControl = false;
            ComboTimer = 2;
            ani.SetTrigger("��������"); 
            ani.SetInteger("����" ,  (ani.GetInteger("����") + 1) % 3 ); 
        }
    }
    public void  enableWeapon()
    {
        weaponObj.SetActive(true);
    }
    public void disableWeapon()
    {
        weaponObj.SetActive(false);
    }
    void ResetParams()
    {
        if ( ComboTimer <=0 && ani.GetInteger("����") != 0)
        {
            ani.SetInteger("����" , 0);
        }
        else
        {
            ComboTimer -= Time.deltaTime;
        }
    }
    public void resetControl()
    {
        isControl = true;
    }
}
