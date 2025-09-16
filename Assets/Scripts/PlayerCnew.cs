using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SlimeC;
using UnityEngine.UI;

public class PlayerCnew : MonoBehaviour,FightInterface
{
    public static PlayerCnew instance;
    public CharacterController controller;
    public float walkSpeed = 4;
    public Animator ani;
    public Transform Tf;
    public GameObject weaponObj;
    public  Collider weaponCollider;
    int Combo = 0;
    public float ComboResetTime = 2f;
    float curComboTime =0;
    //float ComboTimer;
    bool isControl = true;

    public int HP;
    public int MaxHP =100;
    public int level = 1;
    public int nextExp = 0; 
    public int curExp = 0;
    public int atkPower = 1;


    public SkinnedMeshRenderer myRenderer;
    public Color HitColor;
    public Color DefaultColor;
    public Texture DefaultTexture;
    public Texture HitTexture;

    public Text Thp;
    public Text Tlevel;
    public Text TMaxhp;
    public Text Texp;
    public Text Tnextexp;
    public Image HpBar;
    public Image expBar;

    public GameObject LossPanel;

    public void UpdateHpUI()
    {
        Thp.text = HP.ToString();
        TMaxhp.text = MaxHP.ToString();   
        HpBar.fillAmount = (float)HP / MaxHP;
    }

    public void UpdateLvUI()
    {
        Texp.text = curExp.ToString();
        Tlevel.text = level.ToString();
        Tnextexp.text = GameConfig.Instance.getNextExp(level).ToString();
        expBar.fillAmount = (float)curExp / (float)GameConfig.Instance.getNextExp(level);
    }

    public void UPdateCurExpUI()
    {
        Texp.text = curExp.ToString();
        expBar.fillAmount = (float)curExp / (float)GameConfig.Instance.getNextExp(level);
    }


    private void Awake()
    {
        instance = this;
        ani = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        Tf = GetComponent<Transform>();
        //weaponObj.SetActive(false);
    }
    void Start()
    { 
        HP = MaxHP;
        UpdateHpUI();
        UpdateLvUI();
    }

    // Update is called once per frame
    void Update()
    {
        Atk();
        Move();
        ResetParams();

        //Test();
    }
    //void Test()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        AddExp(20);
    //    }
    //}
    private void Move()
    {

        if (!isControl) return;
        Vector3 dir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0 , Input.GetAxis("Vertical")));
        ani.SetFloat("Xinput", Input.GetAxis("Horizontal"));
        ani.SetFloat("Yinput", Input.GetAxis("Vertical"));
        controller.SimpleMove(dir * walkSpeed);
        //ani.SetFloat("水平速度", Input.GetAxis("Horizontal"));
        //ani.SetFloat("垂直速度", Input.GetAxis("Vertical"));
    }
    void Atk()
    {
        //if (Input.GetMouseButtonDown(0) && isControl)
        if (InputC.instance.atkBtn && isControl)
        {
        
            //ComboTimer = 2;
            //ani.SetTrigger("ATK");
            //ani.SetInteger("Comboo", ani.GetInteger("Comboo") + 1);
            //ani.SetInteger("Comboo", (ani.GetInteger("Comboo") + 1) % 3);
            ani.SetTrigger("ATK");
            ani.SetInteger("Comboo",Combo);
            Combo++;
            if(Combo>2)
            {
                Combo = 0;
            }
            curComboTime = ComboResetTime;
            isControl = false;
            //if (ani.GetInteger("Comboo") == 2)
            //{
            //    isControl = false;
            //}
        }
    }
    void ResetParams()
    {
        if(curComboTime>0)
        {
            curComboTime -= Time.deltaTime;
        }
        else
        {
            Combo = 0;
            ani.SetInteger("Comboo", 0);
        }
        //if (ComboTimer <= 0 && ani.GetInteger("Comboo") != 0)
        //{
        //    ani.SetInteger("Comboo", 0);
        //}
        //else
        //{
        //    ComboTimer -= Time.deltaTime;
        //} 
    }

    public void enbaleWeapon()
    {
        //weaponObj.SetActive(true);
        weaponCollider.enabled = true;
    }
    public void closeWeapon()
    {
        //weaponObj.SetActive(false);
        weaponCollider.enabled = false;
    }

    
    public void resetControlState()
    {
        isControl = true;
     
    }


    public void Hit(int atkPower)
    {
        StartCoroutine(HitFlash());
        HP -= atkPower;
        UpdateHpUI();
        if (HP <= 0)
        {
            LossPanel.SetActive(true);
        }
        //else
        //{
        //    ani.SetTrigger("Hurt");
        //}

    }

    IEnumerator HitFlash()
    {
        myRenderer.materials[1].color = HitColor;
        myRenderer.materials[2].color = HitColor;
        yield return new WaitForSeconds(0.1f);
        myRenderer.materials[1].color = DefaultColor;
        myRenderer.materials[2].color = DefaultColor;
    }

    public void AddExp(int exp)
    {
        curExp += exp;
        if (!DolevelUp())
        {
            UPdateCurExpUI();
        }
        else
        {
            UPdateCurExpUI();
            //int nextExp = GameConfig.Instance.getNextExp(level);
            // if (curExp>= nextExp)
            // {
            //    LevelUp(curExp-nextExp);
            // }
        }
    }

    void LevelUp(int leftExp)
    {
        level++;
        curExp = leftExp;
        nextExp = GameConfig.Instance.getNextExp(level);
        MaxHP = GameConfig.Instance.getMaxHp(level);
        HP = MaxHP;

        UpdateLvUI();
        UpdateHpUI();
    }

    bool DolevelUp()
    {
        bool isLevelUp = false;
        while (true)
        {
            nextExp = GameConfig.Instance.getNextExp(level);
            if (curExp >= nextExp)
            {
                isLevelUp = true;
                LevelUp(curExp - GameConfig.Instance.getNextExp(level));
            }
            else
            {
                break;
            }
        }
        return isLevelUp;
    }

}
