using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class SlimeC : MonoBehaviour,FightInterface
{
    public enum EnemyState
    {
        Idle,
        Patrol,
        Pursuit,
        Attack,
        //Hurt,
        Die
    }

    public enum EnemyType
    {
        Slime1,
        Slime2,
    }


    public EnemyState enemyState = EnemyState.Idle;
    public EnemyType enemyType;
    public float IdleTime = 3f;
    float curIdleTime;
    public float CheckDistance = 5;
    public Transform target;
    public Transform[] patrolPoints;
     int patrolIndex ;
    public float WalkSpeed = 2;
    public CharacterController cc;
    public Animator ani;
    public Collider ATKCollider;
    public float AttackDistance = 3;
    public float PursuitSpeed = 3;
    public float MaxPursuitDistance = 10;
    public int HP;
    public int maxHP = 10;
    public bool isDie = false;
    public float AtkCd = 2;
    float curAtkCd;
    public float RotateSpeed = 15;

    public SkinnedMeshRenderer myRenderer;
    public Color HitColor;
    public Color DefaultColor;
    public Texture DefaultTexture;
    public Texture HitTexture;

    public int atkPower = 1;
    public Image hpBar;


    public GameObject Bullet;
    Vector3 shootOffset = new Vector3(0, 0.39f, 2.13f);

    public int exp;

    public void Awake()
    {
        cc = GetComponent<CharacterController>();
        ani = GetComponent<Animator>();
    }


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateHpBar();
    }

    private void OnEnable()
    {
       StartCoroutine(initBornPos());
    }

    IEnumerator initBornPos()
    {
        yield return new WaitForSeconds(IdleTime);
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i].SetParent(null);
        }
    }

    public void UpdateHpBar()
    {
        hpBar.fillAmount = (float)HP / maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        EveryFrame();
    }

    void EveryFrame()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                DoIdle();
                break;
            case EnemyState.Patrol:
                DoPatrol();
                break;
            case EnemyState.Pursuit:
                DoPursuit();
                break;
            case EnemyState.Attack:
                DoAttack();
                break;
            case EnemyState.Die:
                DoDie();
                break;
            default:
                break;
        }

       if(enemyState == EnemyState.Die  && isDie){

            ani.SetTrigger("Die");

        }

       hpBar.transform.rotation= Camera.main.transform.rotation;
    }

    void DoIdle()
    {
        CheckPlayer();
        curIdleTime += Time.deltaTime;
        if (curIdleTime >= IdleTime)
        {
            curIdleTime = 0;
            enemyState = EnemyState.Patrol;
        }
    }
    bool CheckPlayer()
    {
        if (Vector3.Distance(transform.position, target.position) < CheckDistance)
        {
            enemyState = EnemyState.Pursuit;
            return true;
        }
        return false;
    }

    void DoPatrol()
    {
        this.transform.LookAt(new Vector3(patrolPoints[patrolIndex].position.x, this.transform.position.y, patrolPoints[patrolIndex].position.z));

        Quaternion quaternion = Quaternion.LookRotation(new Vector3(target.position.x, this.transform.position.y, target.position.z) - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, Time.deltaTime * RotateSpeed);

        Vector3 offset = this.transform.forward*WalkSpeed;
        cc.SimpleMove(offset);
        if (Vector3.Distance(new Vector3(patrolPoints[patrolIndex].position.x, transform.position.y, patrolPoints[patrolIndex].position.z),
                                                            transform.position)<=0.05f)
        {
            patrolIndex = ++patrolIndex  % patrolPoints.Length;

            enemyState = EnemyState.Idle;
        }
       
    }       
    void DoPursuit()
    {   
        float  distanceTemp = Vector3.Distance(new Vector3(target.position.x,this.transform.position.y, target.position.z), transform.position);
        if (distanceTemp > MaxPursuitDistance)
        {
            enemyState = EnemyState.Idle;
            return;
        }
        this.transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));

        Quaternion quaternion = Quaternion.LookRotation(new Vector3(target.position.x, this.transform.position.y, target.position.z) - this.transform.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, quaternion, Time.deltaTime * RotateSpeed);

        Vector3 offset = this.transform.forward * PursuitSpeed;
        cc.SimpleMove(offset);
        if (distanceTemp < AttackDistance)
        {
            enemyState = EnemyState.Attack;
            return;
        }
    }
    void DoAttack()
    {
        curAtkCd -= Time.deltaTime; 
        if (curAtkCd <= 0)
        {

            float  distanceTemp = Vector3.Distance(new Vector3(target.position.x,this.transform.position.y, target.position.z), transform.position);
            if (distanceTemp > AttackDistance)
            {
                if (distanceTemp > MaxPursuitDistance)
                {
                    enemyState = EnemyState.Idle;
                    return;
                }
                else
                {
                    enemyState = EnemyState.Pursuit;
                    return;
                }
            }
            else
            {
                Attack();
                curAtkCd = AtkCd;
            }
        
        }

    }

   
    void Attack()
    {
        if(enemyType==EnemyType.Slime2)
        ani.SetTrigger("ATK");

        else if (enemyType == EnemyType.Slime1) {


            this.transform.LookAt(new Vector3(target.position.x, this.transform.position.y, target.position.z));
            ani.SetTrigger("ATK2");
        }
           
    }

    void Shoot()
    {
        //SoundManager.instance.PlaySingle(SoundManager.instance.musicClips[1]);
        GameObject bullet = Instantiate(Bullet);
        bullet.transform.position = this.transform.position + this.transform.TransformDirection(shootOffset);
        bullet.transform.rotation = this.transform.rotation;
        bullet.GetComponent<Bullet>().damage = atkPower;
        //GameObject bullet= Instantiate(Bullet, this.transform.position + this.transform.forward * 1.5f + new Vector3(0,1,0), Quaternion.identity);
        //bullet.GetComponent<Rigidbody>().velocity = this.transform.forward * 10;
        //Destroy(bullet, 3);
    }

    void EnableAtkCollider()
    {
        ATKCollider.enabled = true;
    }

    void DisableAtkCollider()
    {
        ATKCollider.enabled = false;
    }

    void DoDie()
    {
        if (!isDie)
        {
            isDie = true;
            ani.SetTrigger("Die");
            Reward();
        }
    }

    void Reward()
    {
        PlayerCnew.instance.AddExp(exp);

    }

    public void Hit(int atkPower)
    {
        StartCoroutine(HitFlash());
        HP -= atkPower;
        UpdateHpBar();
        if (HP <= 0)
        {
            enemyState = EnemyState.Die;      
        }
        //else
        //{
        //    ani.SetTrigger("Hurt");
        //}

    }

    IEnumerator HitFlash()
    {
        myRenderer.material.mainTexture = HitTexture;
        yield return new WaitForSeconds(0.1f);
        myRenderer.material.mainTexture = DefaultTexture;
    }

    public void Dead()
    {
        StartCoroutine(DestroySelf());
    }

    IEnumerator DestroySelf()
    {
       
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
