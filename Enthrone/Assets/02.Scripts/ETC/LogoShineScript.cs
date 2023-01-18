using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoShineScript : MonoBehaviour
{
    MyObject myChar;
    Animator _anim;
    [SerializeField]
    Material _shader;
    Image _image;
    public float Shine = 0f;
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        _anim = GetComponent<Animator>();
        _shader = gameObject.transform.GetChild(0).GetComponent<Image>().material;
        _image = gameObject.transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_image.color.a == 1)
        {
            if (Shine < 1)
            {
                Shine += Time.deltaTime * 2f;
            }            
            _shader.EnableKeyword("SHINE_ON");
            _shader.SetFloat("_ShineLocation", Shine);
        }
        if (Shine >= 1)
        {
            _shader.DisableKeyword("SHINE_ON");
            _anim.SetBool("Rewind", true);
        }
    }
    public void TutorialEnd()
    {
        GameManager.Instance.MoveLobbyTime(1f);
    }
}
