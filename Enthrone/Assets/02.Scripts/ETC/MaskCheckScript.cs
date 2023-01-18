using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskCheckScript : MonoBehaviour
{
    MyObject myChar;
    public List<Sprite> Mask = new List<Sprite>();
    public RectTransform MaskRect;
    public Image MaskImage;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        MaskRect = GetComponent<RectTransform>();
        MaskImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myChar.Tutorial)
        {
            switch (myChar.TutorialNum)
            {
                case 0:
                    MaskImage.sprite = Mask[1];
                    MaskRect.sizeDelta = new Vector2(1000f, 450f);
                    transform.localPosition = new Vector3(20f, -590f, 0);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    MaskImage.sprite = Mask[2];
                    MaskRect.sizeDelta = new Vector2(270f, 400f);
                    transform.localPosition = new Vector3(205f, -565f, 0);
                    break;
                //case 4:
                //    break;
                //case 5:
                //    MaskImage.sprite = Mask[2];
                //    MaskRect.sizeDelta = new Vector2(270f, 400f);
                //    transform.localPosition = new Vector3(60f, -600f, 0);
                //    break;
                case 4:
                    break;
                case 5:
                    if (myChar.BossInfoCheck)
                    {
                        MaskImage.sprite = Mask[1];
                        MaskRect.sizeDelta = new Vector2(1000f, 450f);
                        transform.localPosition = new Vector3(20f, -590f, 0);
                    }
                    else
                    {

                    }
                    break;
            }
        }
    }
}
