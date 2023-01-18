using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGiftBoxScript : MonoBehaviour
{
    public MyObject myChar;
    public GameManager instance;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    public Animator _anim;    
    public GameObject[] ObjectUI;
    public GameObject Effect;
    public GameObject EffectPos;
    public float x, y;

    public GameObject Gold;
    public GameObject Gem;
    public GameObject Heart;
    public GameObject Shield;
    public GameObject SlotCoin;
    public GameObject SoulSpark;
    public GameObject HeroHeart;
    private bool LastCheck = false;
    private int DropCountCheck;
    private int DropCnt;
    private bool SoundCheck = false;
    public bool WindowCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        instance = GameManager.Instance;
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (transform.localPosition.y > -0.5f)
        //{
        //    _collider.enabled = false;
        //}
        //else if(transform.localPosition.y <= -0.5f)
        //{
        //    _collider.enabled = true;
        //}

        //if (transform.localPosition.y <= -0.6)
        //{
        //    _rigidbody.gravityScale = 0f;
        //    transform.localPosition = new Vector2(0, -0.61f);
        //    _collider.isTrigger = true;
        //}
        if (myChar.Key > 0)
        {
            ObjectUI[0].SetActive(true);
            ObjectUI[1].SetActive(false);
        }
        else
        {
            ObjectUI[0].SetActive(false);
            ObjectUI[1].SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            instance.GiftBox_Btn.SetActive(true);
            instance.Select_Btn.SetActive(false);
            instance.Jump_Btn.SetActive(false);
            instance.AD_Btn.SetActive(false);

            //if (myChar.Key <= 0)
            //{
            //    instance.GiftBoxWindow.SetActive(true);
            //}
            if (!WindowCheck)
            {
                instance.GiftBoxWindow.SetActive(true);
            }            
            ObjectUI[2].SetActive(true);
            if (!SoundCheck)
            {
                SoundManager.Instance.PlaySfx(18);
                SoundCheck = true;
            }
            

            GameManager.Instance.AdMerchin = transform.gameObject;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            _collider.isTrigger = true;
            gameObject.layer = 16;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        instance.GiftBox_Btn.SetActive(false);
        instance.Select_Btn.SetActive(false);
        instance.Jump_Btn.SetActive(true);
        instance.GiftBoxWindow.SetActive(false);
        instance.AD_Btn.SetActive(false);
        ObjectUI[2].SetActive(false);
        SoundCheck = false;
    }

    public void OpenBox()
    {
        SoundManager.Instance.PlaySfx(41);
        _anim.SetBool("Open", true);
    }

    public void OpenEffect()
    {
        GameObject Box = Instantiate(Effect, EffectPos.transform.position, Quaternion.identity);
        Box.transform.parent = transform.parent;
        Box.transform.rotation = Quaternion.Euler(-90, 45, 0);
        Box.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        Destroy(Box, 3f);
    }
    public void Itemdrop()
    {
        if (!myChar.Tutorial)
        {
            DropTemplateMgr Drop = instance.DropDataMgr;

            //int RanCntSlotCoin = Random.Range(Drop.GetTemplate(0).HeroMin, Drop.GetTemplate(0).HeroMax);
            //for (int i = 0; i < RanCntSlotCoin; i++)
            //{
            //    StartCoroutine(ItemIns(SlotCoin, i * 0.1f));
            //}

            int RanCntGold = Random.Range(Drop.GetTemplate(1).GiftMin, Drop.GetTemplate(1).GiftMax);
            for (int i = 0; i < RanCntGold; i++)
            {
                StartCoroutine(ItemIns(Gold, (i + RanCntGold) * 0.1f));
            }

            int RanCntGem = Random.Range(Drop.GetTemplate(2).GiftMin, Drop.GetTemplate(2).GiftMax);
            for (int i = 0; i < RanCntGem; i++)
            {
                StartCoroutine(ItemIns(Gem, (i + RanCntGem) * 0.1f));
            }

            int RanCntHeart = Random.Range(Drop.GetTemplate(3).GiftMin, Drop.GetTemplate(3).GiftMax);
            for (int i = 0; i < RanCntHeart; i++)
            {
                StartCoroutine(ItemIns(Heart, i * 0.1f));
            }

            int RanCntShield = Random.Range(Drop.GetTemplate(4).GiftMin, Drop.GetTemplate(4).GiftMax);
            for (int i = 0; i < RanCntShield; i++)
            {
                StartCoroutine(ItemIns(Shield, i * 0.1f));
            }

            int RanCntHero = Random.Range(Drop.GetTemplate(7).GiftMin, Drop.GetTemplate(7).GiftMax);
            for (int i = 0; i < RanCntHero; i++)
            {
                StartCoroutine(ItemIns(HeroHeart, i * 0.1f));
            }

            int RanCntSoulSpark = Random.Range(Drop.GetTemplate(6).GiftMin, Drop.GetTemplate(6).GiftMax);
            for (int i = 0; i < RanCntSoulSpark; i++)
            {
                StartCoroutine(ItemIns(SoulSpark, (i + 1f + RanCntGold + RanCntGem) * 0.1f));
                if (i == RanCntSoulSpark - 1)
                {
                    LastCheck = true;
                }
            }
            DropCountCheck = RanCntGold + RanCntGem + RanCntSoulSpark;

            //if (myChar.Chapter <= 2)
            //{
            //    DropTemplateMgr Drop = instance.DropDataMgr;

            //    int RanCntSlotCoin = Random.Range(Drop.GetTemplate(0).BossMin, Drop.GetTemplate(0).BossMax);
            //    for (int i = 0; i < RanCntSlotCoin; i++)
            //    {
            //        StartCoroutine(ItemIns(SlotCoin, i * 0.1f));
            //    }

            //    int RanCntGold = Random.Range(Drop.GetTemplate(1).BossMin, Drop.GetTemplate(1).BossMax);
            //    for (int i = 0; i < RanCntGold; i++)
            //    {
            //        StartCoroutine(ItemIns(Gold, (i + RanCntSlotCoin) * 0.1f));
            //    }

            //    int RanCntGem = Random.Range(Drop.GetTemplate(2).BossMin, Drop.GetTemplate(2).BossMax);
            //    for (int i = 0; i < RanCntGem; i++)
            //    {
            //        StartCoroutine(ItemIns(Gem, (i + RanCntSlotCoin + RanCntGold) * 0.1f));
            //    }

            //    int RanCntHeart = Random.Range(Drop.GetTemplate(3).BossMin, Drop.GetTemplate(3).BossMax);
            //    for (int i = 0; i < RanCntHeart; i++)
            //    {
            //        StartCoroutine(ItemIns(Heart, (i + RanCntSlotCoin) * 0.1f));
            //    }

            //    int RanCntShield = Random.Range(Drop.GetTemplate(4).BossMin, Drop.GetTemplate(4).BossMax);
            //    for (int i = 0; i < RanCntShield; i++)
            //    {
            //        StartCoroutine(ItemIns(Shield, (i + RanCntSlotCoin) * 0.1f));
            //    }

            //    int RanCntHero = Random.Range(Drop.GetTemplate(8).BossMin, Drop.GetTemplate(8).BossMax);
            //    for (int i = 0; i < RanCntHero; i++)
            //    {
            //        StartCoroutine(ItemIns(HeroHeart, (i + RanCntSlotCoin) * 0.1f));
            //    }

            //    int RanCntSoulSpark = Random.Range(Drop.GetTemplate(7).BossMin, Drop.GetTemplate(7).BossMax);
            //    for (int i = 0; i < RanCntSoulSpark; i++)
            //    {
            //        StartCoroutine(ItemIns(SoulSpark, (i + 1f + RanCntSlotCoin + RanCntGold + RanCntGem) * 0.1f));
            //        if (i == RanCntSoulSpark - 1)
            //        {
            //            LastCheck = true;
            //        }
            //    }
            //    DropCountCheck = RanCntSlotCoin + RanCntGold + RanCntGem + RanCntSoulSpark;
            //}
            //else if (myChar.Chapter >= 3)
            //{   //왕좌 드랍다른걸로 세팅해야됨
            //    DropTemplateMgr Drop = instance.DropDataMgr;

            //    //int RanCntSlotCoin = Random.Range(Drop.GetTemplate(0).HeroMin, Drop.GetTemplate(0).HeroMax);
            //    //for (int i = 0; i < RanCntSlotCoin; i++)
            //    //{
            //    //    StartCoroutine(ItemIns(SlotCoin, i * 0.1f));
            //    //}

            //    int RanCntGold = Random.Range(Drop.GetTemplate(1).HeroMin, Drop.GetTemplate(1).HeroMax);
            //    for (int i = 0; i < RanCntGold; i++)
            //    {
            //        StartCoroutine(ItemIns(Gold, (i + RanCntGold) * 0.1f));
            //    }

            //    int RanCntGem = Random.Range(Drop.GetTemplate(2).HeroMin, Drop.GetTemplate(2).HeroMax);
            //    for (int i = 0; i < RanCntGem; i++)
            //    {
            //        StartCoroutine(ItemIns(Gem, (i + RanCntGold) * 0.1f));
            //    }

            //    int RanCntHeart = Random.Range(Drop.GetTemplate(3).HeroMin, Drop.GetTemplate(3).HeroMax);
            //    for (int i = 0; i < RanCntHeart; i++)
            //    {
            //        StartCoroutine(ItemIns(Heart, i * 0.1f));
            //    }

            //    int RanCntShield = Random.Range(Drop.GetTemplate(4).HeroMin, Drop.GetTemplate(4).HeroMax);
            //    for (int i = 0; i < RanCntShield; i++)
            //    {
            //        StartCoroutine(ItemIns(Shield, i * 0.1f));
            //    }

            //    int RanCntHero = Random.Range(Drop.GetTemplate(7).HeroMin, Drop.GetTemplate(7).HeroMax);
            //    for (int i = 0; i < RanCntHero; i++)
            //    {
            //        StartCoroutine(ItemIns(HeroHeart, i * 0.1f));
            //    }

            //    int RanCntSoulSpark = Random.Range(Drop.GetTemplate(6).HeroMin, Drop.GetTemplate(6).HeroMax);
            //    for (int i = 0; i < RanCntSoulSpark; i++)
            //    {
            //        StartCoroutine(ItemIns(SoulSpark, (i + 1f + RanCntGold + RanCntGem) * 0.1f));
            //        if (i == RanCntSoulSpark - 1)
            //        {
            //            LastCheck = true;
            //        }
            //    }
            //    DropCountCheck = RanCntGold + RanCntGem + RanCntSoulSpark;
            //}
        }
        else
        {
            DropTemplateMgr Drop = instance.DropDataMgr;

            //int RanCntSlotCoin = Random.Range(Drop.GetTemplate(0).HeroMin, Drop.GetTemplate(0).HeroMax);
            //for (int i = 0; i < RanCntSlotCoin; i++)
            //{
            //    StartCoroutine(ItemIns(SlotCoin, i * 0.1f));
            //}

            int RanCntGold = Random.Range(Drop.GetTemplate(1).HeroMin, Drop.GetTemplate(1).HeroMax);
            for (int i = 0; i < RanCntGold; i++)
            {
                StartCoroutine(ItemIns(Gold, i * 0.1f));
            }

            int RanCntGem = Random.Range(Drop.GetTemplate(2).HeroMin, Drop.GetTemplate(2).HeroMax);
            for (int i = 0; i < RanCntGem; i++)
            {
                StartCoroutine(ItemIns(Gem, (i + RanCntGold) * 0.1f));
            }

            int RanCntHeart = Random.Range(Drop.GetTemplate(3).HeroMin, Drop.GetTemplate(3).HeroMax);
            for (int i = 0; i < RanCntHeart; i++)
            {
                StartCoroutine(ItemIns(Heart, i * 0.1f));
            }

            int RanCntShield = Random.Range(Drop.GetTemplate(4).HeroMin, Drop.GetTemplate(4).HeroMax);
            for (int i = 0; i < RanCntShield; i++)
            {
                StartCoroutine(ItemIns(Shield, i * 0.1f));
            }

            int RanCntHero = Random.Range(Drop.GetTemplate(7).HeroMin, Drop.GetTemplate(7).HeroMax);
            for (int i = 0; i < RanCntHero; i++)
            {
                StartCoroutine(ItemIns(HeroHeart, i * 0.1f));
            }

            int RanCntSoulSpark = Random.Range(Drop.GetTemplate(6).HeroMin, Drop.GetTemplate(6).HeroMax);
            for (int i = 0; i < RanCntSoulSpark; i++)
            {
                StartCoroutine(ItemIns(SoulSpark, (i + 1f + RanCntGold + RanCntGem) * 0.1f));
                if (i == RanCntSoulSpark - 1)
                {
                    LastCheck = true;
                }
            }
            DropCountCheck = RanCntGold + RanCntGem + RanCntSoulSpark;
        }
    }
    private void ItemInstanti(GameObject Object)
    {
        GameObject Item = Instantiate(Object, transform.position, Quaternion.identity);
        //Item.GetComponent<GiftBox_ItemScript>().StartPostion = gameObject.transform.position;
        Item.transform.parent = transform.parent.parent.parent;
        if (Object != SlotCoin)
        {
            Item.transform.localScale = new Vector2(1, 1);
        }
        else
        {
            Item.transform.localScale = new Vector2(6, 6);
        }
        
        float xCnt = Random.Range(-0.3f, 0.3f);
        float yCnt = Random.Range(0.0f, 0.3f);
        Item.GetComponent<Rigidbody2D>().AddForce(new Vector2(xCnt, yCnt) * 100);
        Item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Item.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void BoxOpenSound()
    {
        SoundManager.Instance.PlaySfx(42);
    }
    IEnumerator ItemIns(GameObject Object, float TimeCnt)
    {
        yield return new WaitForSeconds(TimeCnt);
        ItemInstanti(Object);
        GameManager.Instance.SFXSound(30);
        DropCnt++;
        if (DropCountCheck == DropCnt)
        {
            StartCoroutine(PickUpTrue());            
        }
    }
    IEnumerator PickUpTrue()
    {
        yield return new WaitForSeconds(1.5f);
        myChar.GiftboxDropCheck = true;

        //아이템먹고 왕좌활성화됨
        //StartCoroutine(ThroneActive());
        if (myChar.Tutorial || myChar.Chapter == 3)
        {
            StartCoroutine(ThroneActive());
        }
    }
    IEnumerator ThroneActive()
    {
        yield return new WaitForSeconds(2f);
        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(0).gameObject.SetActive(true);
    }

}
