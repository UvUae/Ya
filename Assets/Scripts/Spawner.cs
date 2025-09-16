using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public int  curNum;     //��ǰ��������
    public int maxNum;  //��ǰ����
    public int sum;  //�ܹ���������

    public float spawnTime;
    public float curspawnTime;
    public bool isEnable;
    public int curLiveNum   //��ǰ�������
    {
        get
        {
            return transform.childCount;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isEnable)
        {
            return;
        }
        if(curNum>=sum)
        {
            return;
        }
        if (curLiveNum >= maxNum)
        {

            return;
        }

        curspawnTime -= Time.deltaTime;
        if(curspawnTime<=0)
        {
            MonsSpawner();
            curspawnTime = spawnTime;
        }

    }

    void MonsSpawner()
    {
        GameObject temp= Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        temp.transform.parent = this.transform;
        Vector2 vector2 = Random.insideUnitCircle * 5;
        temp.transform.position= this.transform.position + new Vector3(vector2.x,0,vector2.y);
        curNum++;
     
    }
}
