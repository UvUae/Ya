using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.TryGetComponent<FightInterface>(out FightInterface fightInterface))
        {
            other.GetComponent<FightInterface>().Hit(PlayerCnew.instance.atkPower);
        }
    }
}
