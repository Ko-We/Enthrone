using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxScript : MonoBehaviour
{
    MyObject myChar;
    Material _shader;

    public List<GameObject> Item = new List<GameObject>();

    private Animator _anim;
    private Rigidbody2D _rigidbody;

    public GameObject DeathEffect;
    public float BoxHp = 5;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        _shader = GetComponent<Renderer>().material;
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttackHit(float Damage)
    {
        BoxHp -= Damage;
        _shader.EnableKeyword("HITEFFECT_ON");
        Invoke("HitEffectEnd", 0.1f);
        if (BoxHp <= 0)
        {
            _anim.SetBool("Death", true);
            _rigidbody.velocity = Vector2.zero;
            //PlayerTargeting.Instance.BoxList.RemoveAt(0);
            gameObject.layer = LayerMask.NameToLayer("DeathMonster");
            gameObject.tag = "DeathMonster";
        }
    }

    public void ItemDrop()
    {
        // 0 : 골드 / 1 : 잼 / 2 : 하트 / 3 : 쉴드
        if (Random.Range(0, 100) < (50 + myChar.Luck))  //골드
        {
            GameObject Gold = Instantiate(Item[0], transform.position, Quaternion.identity);

            Gold.transform.parent = transform.parent.parent;
            Gold.transform.localScale = new Vector2(1, 1);
            //Gold.GetComponent<Rigidbody2D>().velocity = new Vector2(Gold.GetComponent<Rigidbody2D>().velocity.x, 2f);
        }
        if (Random.Range(0, 100) < (5 + myChar.Luck))   //젬
        {
            GameObject Gem = Instantiate(Item[1], transform.position, Quaternion.identity);

            Gem.transform.parent = transform.parent.parent;
            Gem.transform.localScale = new Vector2(1, 1);
        }
        if (Random.Range(0, 100) < (15 + myChar.Luck))   //하트
        {
            GameObject Heart = Instantiate(Item[2], transform.position, Quaternion.identity);

            Heart.transform.parent = transform.parent.parent;
            Heart.transform.localScale = new Vector2(1, 1);
         }
        if (Random.Range(0, 100) < (10 + myChar.Luck))   // 쉴드
        {
            GameObject Shield = Instantiate(Item[3], transform.position, Quaternion.identity);

            Shield.transform.parent = transform.parent.parent;
            Shield.transform.localScale = new Vector2(1, 1);
        }

    }
    private void HitEffectEnd()
    {
        _shader.DisableKeyword("HITEFFECT_ON");
    }
    public void destroy()
    {
        Destroy(gameObject);
    }
}
