using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowImgCheckScript : MonoBehaviour
{
    public GameObject Content;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Content.GetComponent<RectTransform>().anchoredPosition.y > 0)
        {
            gameObject.GetComponent<Image>().enabled = false;
            if (transform.childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).transform.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = true;
            if (transform.childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).transform.gameObject.SetActive(true);
                }
            }
        }
    }

    public void ArrowOnCheck()
    {
        Content.GetComponent<RectTransform>().anchoredPosition = new Vector2(Content.GetComponent<RectTransform>().anchoredPosition.x, 0);
        gameObject.GetComponent<Image>().enabled = true;
    }
}
