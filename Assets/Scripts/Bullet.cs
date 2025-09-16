using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 10f;
    public float lifeTime = 3f;
    public int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (this.transform.forward * speed * Time.deltaTime);
        lifeTime -= Time.deltaTime;
        if(lifeTime<=0)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<FightInterface>(out FightInterface fightInterface))
        {
            other.GetComponent<FightInterface>().Hit(damage);
        }
    }
}
