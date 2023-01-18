using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEffectScript : MonoBehaviour
{
    MyObject myChar;
    Animator _anim;
    [SerializeField]
    Material _shader;
    //public GameObject Shader;

    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        _anim = GetComponent<Animator>();
        _shader = GetComponent<Renderer>().sharedMaterial;

        //if (myChar.SelectHero == 3)
        //{
        //    _shader = Shader.GetComponent<Renderer>().sharedMaterial;
        //}
        //else
        //{
        //    _shader = GetComponent<Renderer>().sharedMaterial;
        //}
        ////값 얻어오기
        //float f = transform.GetComponent<Renderer>().material.GetFloat("_ColorStrength");

        ////값 설정
        //transform.GetComponent<Renderer>().material.SetFloat("_ColorStrength", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Effect();
    }


    private void Effect()
    {
        if (myChar.SelectStoneNum > 0)
        {
            _shader.EnableKeyword("OUTBASE_ON");
            switch (myChar.SelectStoneNum)
            {
                case 1:
                    _shader.SetColor("_OutlineColor", new Color(255 / 255f, 0 / 255f, 0 / 255f, 255 / 255f));
                    //_shader.SetColor("_OutlineColor", Color.red);
                    break;
                case 2:
                    _shader.SetColor("_OutlineColor", new Color(0 / 255f, 255 / 255f, 255 / 255f, 255 / 255f));
                    //_shader.SetColor("_OutlineColor", Color.cyan);
                    break;
                case 3:
                    _shader.SetColor("_OutlineColor", new Color(255 / 255f, 80 / 255f, 0 / 255f, 255 / 255f));
                    //_shader.SetColor("_OutlineColor", Color.yellow);
                    break;
                case 4:
                    _shader.SetColor("_OutlineColor", new Color(0 / 255f, 255 / 255f, 0 / 255f, 255 / 255f));
                    //_shader.SetColor("_OutlineColor", Color.green);
                    break;
                
            }

        }
        else if (myChar.SelectStoneNum == 0)
        {
            _shader.DisableKeyword("OUTBASE_ON");
        }
    }
}
