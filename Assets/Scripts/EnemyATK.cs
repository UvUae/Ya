using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyATK : MonoBehaviour
{
    public SlimeC slime;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<FightInterface>(out FightInterface fightInterface))
        {
            other.GetComponent<FightInterface>().Hit(slime.atkPower);
        }
    }
}
