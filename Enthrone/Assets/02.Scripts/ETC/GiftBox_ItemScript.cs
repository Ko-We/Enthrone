using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftBox_ItemScript : MonoBehaviour
{
    public enum ItemType
    {
        Heart,
        Shield,
        Gold,
        Gem,
        Active,
        SlotCoin,
        SoulSpark,
        HeroHeart
    }

    MyObject myChar;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    public LayerMask _layerMask;

    private GameObject PlayerList;

    public Transform target;
    public Vector3 StartPostion;

    public ItemType itemType;
    public float MagneticRadius;
    public float ObjectCheck;

    public int Active_item_index;
    public int Active_item_Lv;
    public int[] Active_item_value;
    public float WallcheckRadius;

    private bool PlayerCheck;
    public bool isWallCheck;
    [SerializeField]
    private bool ObjectTF;
    private bool VeloZeroCheck;
    private float[] RandomRange = { -0.1f, 0.1f };
    private bool ObtainItemCheck = false;
    public bool CustomizeCost;
    //private float[] RandomRange = { -0.004f, -0.003f, 0.003f, 0.004f };
    // Start is called before the first frame update
    private void Awake()
    {
        myChar = MyObject.MyChar;
        GameObject Player = GameObject.Find("Player");
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 3f);
        _collider = GetComponent<Collider2D>();
        ObjectTF = true;
    }
    void Start()
    {
        Invoke("ItemTest", 0.5f);
    }

    // Update is called once per frame
    private void Update()
    {
        if (myChar.GiftboxDropCheck)
        {
            Magnetic();
            gameObject.layer = 13;
        }        

    }
    private void FixedUpdate()
    {
        TargetAutoCheck();
        SurroundCheckPlayer();


        if (myChar.Stage % 10 != 3 && myChar.Stage % 10 != 6)
        {
            if (!myChar.Tutorial)
            {
                if (myChar.Stage % 10 == 0)
                {
                    MagneticRadius = 5f;
                }
                else
                {
                    if (GameManager.Instance.PlayerSkill[9] < 1)
                    {
                        MagneticRadius = 1f;
                    }
                    else if (GameManager.Instance.PlayerSkill[9] >= 1)
                    {
                        MagneticRadius = 2f;
                    }
                }
            }
            else
            {
                MagneticRadius = 5f;
            }
        }
        else
        {
            MagneticRadius = 5f;
        }
    }

    public void TargetAutoCheck()
    {
        GameObject Player = GameObject.Find("Player");

        for (int i = 0; i < myChar.HeroNum; i++)
        {
            if (Player.transform.GetChild(i).gameObject.activeSelf == true)
            {
                if (Player.transform.GetChild(i).name != "SeasnalShield")
                {
                    target = Player.transform.GetChild(i);
                    PlayerList = target.gameObject;
                }

            }
        }
    }

    private void SurroundCheckPlayer()
    {
        PlayerCheck = Physics2D.OverlapCircle(transform.position, MagneticRadius, 1 << LayerMask.NameToLayer("Player"));
        isWallCheck = Physics2D.OverlapCircle(transform.position, WallcheckRadius, 1 << LayerMask.NameToLayer("Ground"));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (myChar.GiftboxDropCheck)
        {
            if (col.gameObject.tag == "Player")
            {
                DropList();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (myChar.GiftboxDropCheck)
        {
            if (col.gameObject.tag == "Player")
            {
                DropList();
            }
        }
    }

    private void HeartPoint()
    {
        if (GameManager.Instance.PlayerSkill[6] >= 1)
        {
            myChar.currentHp += 2;
        }
        else if (GameManager.Instance.PlayerSkill[6] == 0)
        {
            myChar.currentHp++;
        }
    }

    private void Magnetic()
    {
        if (itemType != ItemType.Active)
        {
            if (PlayerCheck)
            {
                _collider.isTrigger = true;
                _rigidbody.gravityScale = 0f;
                Vector2 dir = PlayerList.transform.position - transform.position;

                //_rigidbody.MovePosition((PlayerList.transform.position) * 0.5f * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, PlayerList.transform.position, Time.deltaTime * 10f);
            }
            else if (!PlayerCheck)
            {
                _collider.isTrigger = false;
                _rigidbody.gravityScale = 0.5f;
            }
        }
    }


    //private void ActiveItem()
    //{
    //    if (myChar.ActiveItem != 0)
    //    {
    //        GameManager.Instance.ActiveItemUse();
    //    }
    //    switch (Active_item_index)
    //    {
    //        case 1:
    //            myChar.ActiveItem = 1;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 2:
    //            myChar.ActiveItem = 2;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 3:
    //            myChar.ActiveItem = 3;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 4:
    //            myChar.ActiveItem = 4;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 5:
    //            myChar.ActiveItem = 5;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 6:
    //            myChar.ActiveItem = 6;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 7:
    //            myChar.ActiveItem = 7;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 8:
    //            myChar.ActiveItem = 8;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 9:
    //            myChar.ActiveItem = 9;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 10:
    //            myChar.ActiveItem = 10;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 11:
    //            myChar.ActiveItem = 11;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 12:
    //            myChar.ActiveItem = 12;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 13:
    //            myChar.ActiveItem = 13;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 14:
    //            myChar.ActiveItem = 14;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //        case 15:
    //            myChar.ActiveItem = 15;
    //            myChar.ActiveItme_Lv = Active_item_Lv;
    //            break;
    //    }
    //}

    private void DropList()
    {
        switch (itemType)
        {
            case ItemType.Heart:
                HeartPoint();
                GameManager.Instance.HealingEffect_On();
                SoundManager.Instance.PlaySfx(3, 0.5f);
                //myChar.currentHp++;
                break;
            case ItemType.Shield:
                myChar.Shield++;
                GameManager.Instance.ShieldEffect_On();
                SoundManager.Instance.PlaySfx(3, 0.5f);
                break;
            case ItemType.Gold:
                myChar.Coin++;

                SoundManager.Instance.PlaySfx(4, 0.5f);
                break;
            case ItemType.Gem:
                myChar.GainGem += GameManager.Instance.DropMultiDataMgr.GetTemplate(myChar.Chapter + 1).MultiDia;
                myChar.Gem += GameManager.Instance.DropMultiDataMgr.GetTemplate(myChar.Chapter + 1).MultiDia;
                myChar.SaveGem();
                SoundManager.Instance.PlaySfx(3, 0.5f);
                break;
            case ItemType.Active:
                //ActiveItem();
                SoundManager.Instance.PlaySfx(3, 0.5f);
                break;
            case ItemType.SlotCoin:
                //myChar.SlotCoin++;
                SoundManager.Instance.PlaySfx(3, 0.5f);

                myChar.SaveSlotCoin();
                break;
            case ItemType.SoulSpark:
                myChar.SoulSpark += GameManager.Instance.DropMultiDataMgr.GetTemplate(myChar.Chapter + 1).MultiSoul;
                myChar.GainSoulSpark += GameManager.Instance.DropMultiDataMgr.GetTemplate(myChar.Chapter + 1).MultiSoul;

                SoundManager.Instance.PlaySfx(3, 0.5f);

                myChar.SaveSoulSpark();
                break;
            case ItemType.HeroHeart:
                myChar.HeroHeart += GameManager.Instance.DropMultiDataMgr.GetTemplate(myChar.Chapter + 1).MultiHero;
                myChar.GainHeroHeart += GameManager.Instance.DropMultiDataMgr.GetTemplate(myChar.Chapter + 1).MultiHero;

                SoundManager.Instance.PlaySfx(3, 0.5f);

                myChar.SaveHeroHeart();
                break;
        }
        Destroy(transform.gameObject);
    }
    private void ItemTest()
    {
        ObjectTF = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, MagneticRadius);

        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, WallcheckRadius);
        //Gizmos.DrawWireSphere(transform.position, ObjectCheck);
    }
}
