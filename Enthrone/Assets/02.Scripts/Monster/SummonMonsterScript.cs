using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMonsterScript : MonoBehaviour
{
    public enum SummonType
    {
        Icicle,
        WhiteCat,
        IceWisp,
        WinterYeti
    }

    public GameObject Portal;
    public GameObject Monster;
    public GameObject Image;

    public bool TraceMonster;
    private int MonsterCnt;
    [SerializeField]
    private bool MonsterCheck;
    private bool SummonCheck = false;

    private void Awake()
    {
        Portal.SetActive(false);
        Image.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SummonedTime());
    }

    // Update is called once per frame
    void Update()
    {
        MonsterCntCheck();
    }
    private void MonsterCntCheck()
    {
        for (int i = 0; i < transform.parent.parent.Find("Monster").childCount; i++)
        {
            if (i == 0)
            {
                MonsterCnt = 0;
            }
            if (transform.parent.parent.Find("Monster").GetChild(i).tag == "Monster")
            {
                MonsterCnt++;
                MonsterCheck = true;
            }
        }
        if (MonsterCnt == 0)
        {
            MonsterCheck = false;
            if (!SummonCheck)
            {
                StartCoroutine(SummonedTime());
            }
        }
    }
    IEnumerator SummonedTime()
    {
        Portal.SetActive(true);
        SummonCheck = true;
        yield return new WaitForSeconds(2f);
        GameObject Summoned = Instantiate(Monster, transform.position, Quaternion.identity);
        Summoned.transform.parent = transform.parent;
        Summoned.transform.rotation = transform.rotation;
        if (!TraceMonster)
        {
            Summoned.transform.localScale = new Vector3(6, 6, 6);
        }
        else if (TraceMonster)
        {
            Summoned.transform.localScale = new Vector3(1, 1, 1);
        }
        
        Destroy(transform.gameObject, 1f);
    }
}
