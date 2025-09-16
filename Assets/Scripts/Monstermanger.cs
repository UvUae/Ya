using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstermanger : MonoBehaviour
{
    public Spawner[] spawners;

    private void Awake()
    {
        spawners = new Spawner[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            spawners[i] = transform.GetChild(i).GetComponent<Spawner>();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           for (int i = 0;i < spawners.Length; i++) {
                spawners[i].isEnable = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].isEnable = false;
            }
        }
    }

}
