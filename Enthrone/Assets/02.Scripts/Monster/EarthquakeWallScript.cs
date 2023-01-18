using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthquakeWallScript : MonoBehaviour
{
    public List<GameObject> Earthquake = new List<GameObject>();
    public GameObject EmergencyCheck;
    public bool isEarthquake = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            if (isEarthquake)
            {
                for (int i = 0; i < (Earthquake.Count / 2); i++)
                {
                    StartCoroutine(EarthquakeAttack(i * 0.2f, i * 2));
                }
                isEarthquake = false;
                //col.gameObject.transform.parent.GetComponent<Animator>().enabled = true;
                //col.gameObject.GetComponent<BossPatternScript>().Crush = false;
                col.gameObject.GetComponent<BossPatternScript>().HeadCrushCheck = true;
                col.gameObject.GetComponent<BossPatternScript>().EarthquakeCheck = false;
                col.gameObject.GetComponent<BossPatternScript>().Earthquake = false;
                //col.gameObject.transform.parent.GetComponent<Animator>().enabled = true;
                col.gameObject.GetComponent<BossPatternScript>()._anim.SetBool("Crush", false);
                col.gameObject.GetComponent<BossPatternScript>().HeadPos = col.gameObject.GetComponent<BossPatternScript>().RockBossHeadPos.transform.position - col.gameObject.transform.position;
                SoundManager.Instance.PlaySfx(65);

            }
        }
    }
    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    if (col.gameObject.tag == "Monster")
    //    {
    //        if (isEarthquake)
    //        {
    //            for (int i = 0; i < 5; i++)
    //            {
    //                if (i != 0)
    //                {
    //                    StartCoroutine(EarthquakeAttack(i * 0.2f, i * 2));
    //                }
    //                else if (i == 0)
    //                {
    //                    StartCoroutine(EarthquakeAttack(i * 0.2f, i * 2));
    //                }
                    
    //            }
    //            isEarthquake = false;
    //            col.gameObject.GetComponent<BossPatternScript>().Crush = false;
    //        }
    //    }
    //}

    public IEnumerator EarthquakePattern()
    {
        EmergencyCheck.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(2f);
        EmergencyCheck.GetComponent<SpriteRenderer>().enabled = false;
        isEarthquake = true;
    }

    IEnumerator EarthquakeAttack(float Time, int checkNum)
    {
        yield return new WaitForSeconds(Time);
        for (int i = checkNum; i < checkNum + 2; i++)
        {
            Earthquake[i].SetActive(true);
        }
    }
}
