using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    MyObject myChar;
    Animator _anim;
    [SerializeField]
    Material _shader;
    //AllIn1SpriteShader _allin1;
    private bool Test = true;
    public List<Texture> Tex = new List<Texture>();
    
    // Start is called before the first frame update
    void Start()
    {
        myChar = MyObject.MyChar;
        _anim = GetComponent<Animator>();
        _shader = GetComponent<SpriteRenderer>().sharedMaterial;
        //_allin1 = GetComponent<AllIn1Shader>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    myChar.Reflection = true;
        //    myChar.EffectOnCheck = true;

        //    myChar.ASPDPotion = false;
        //    myChar.PowerPotion = false;
        //    myChar.InvinciblePotion = false;
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    myChar.ASPDPotion = true;
        //    myChar.EffectOnCheck = true;

        //    myChar.Reflection = false;
        //    myChar.PowerPotion = false;
        //    myChar.InvinciblePotion = false;
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    myChar.PowerPotion = true;
        //    myChar.EffectOnCheck = true;

        //    myChar.Reflection = false;
        //    myChar.ASPDPotion = false;
        //    myChar.InvinciblePotion = false;
        //}
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    myChar.InvinciblePotion = true;
        //    myChar.EffectOnCheck = true;

        //    myChar.Reflection = false;
        //    myChar.ASPDPotion = false;
        //    myChar.PowerPotion = false;
        //}
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    myChar.EffectOnCheck = false;
            
        //}
        //if (!myChar.EffectOnCheck)
        //{
        //    myChar.Reflection = false;
        //    myChar.ASPDPotion = false;
        //    myChar.PowerPotion = false;
        //    myChar.InvinciblePotion = false;
        //}
        Effect();
    }

    public void Damage()
    {
        _anim.Play("Damage", 1);
    }

    private void Effect()
    {
        if (myChar.EffectOnCheck)
        {
            if (myChar.Reflection)      //반사포션
            {
                //_anim.Play("Reflection", 2);
                _anim.SetBool("Reflection", true);

                _shader.EnableKeyword("GLOW_ON");
                _shader.EnableKeyword("OUTBASE_ON");
                _shader.EnableKeyword("SHINE_ON");
                _shader.SetColor("_GlowColor", new Color32(255, 255, 255, 255));
                _shader.SetFloat("_Glow", 0);
                _shader.SetFloat("_GlowTexUsed", 0);
                _shader.SetColor("_OutlineColor", new Color32(255, 255, 255, 255));
                _shader.SetFloat("_OutlineGlow", 1.5f);
                _shader.SetFloat("_OutlineTexToggle", 0);
                _shader.SetColor("_ShineColor", new Color32(255, 255, 255, 255));
            }
            else if (myChar.ASPDPotion)     //공속포션
            {
                _anim.SetBool("ASPDPotion", true);
                _shader.EnableKeyword("GLOW_ON");
                _shader.EnableKeyword("OUTBASE_ON");
                _shader.EnableKeyword("GLITCH_ON");
                _shader.SetColor("_GlowColor", new Color32(10, 5, 0, 255));
                _shader.SetFloat("_GlowTexUsed", 1);
                _shader.SetColor("_OutlineColor", new Color32(255, 10, 0, 255));
                _shader.SetFloat("_OutlineGlow", 50f);
                _shader.SetFloat("_OutlineTexToggle", 1);
                _shader.SetTexture("_OutlineTex", Tex[0]);
                _shader.SetFloat("_GlitchAmount", 4f);
            }
            else if (myChar.PowerPotion)        //파워포션
            {
                _shader.EnableKeyword("GLOW_ON");
                _shader.EnableKeyword("OUTBASE_ON");
                _shader.EnableKeyword("GLITCH_ON");
                _shader.SetColor("_GlowColor", new Color32(10, 0, 0, 255));
                _shader.SetFloat("_Glow", 10);
                _shader.SetFloat("_GlowTexUsed", 1);
                _shader.SetColor("_OutlineColor", new Color32(255, 0, 45, 255));
                _shader.SetFloat("_OutlineGlow", 50f);
                _shader.SetFloat("_OutlineTexToggle", 1);
                _shader.SetTexture("_OutlineTex", Tex[0]);
                _shader.SetFloat("_GlitchAmount", 4f);
            }
            else if (myChar.InvinciblePotion)       //무적포션
            {
                _anim.SetBool("InvinciblePotion", true);
                _shader.EnableKeyword("GLOW_ON");
                _shader.EnableKeyword("OUTBASE_ON");
                _shader.EnableKeyword("GRADIENT_ON");
                _shader.EnableKeyword("HSV_ON");
                _shader.EnableKeyword("SHADOW_ON");
                _shader.EnableKeyword("SHINE_ON");
                _shader.SetColor("_GlowColor", new Color32(255, 255, 255, 255));
                _shader.SetFloat("_Glow", 0.3f);
                _shader.SetColor("_OutlineColor", new Color32(255, 255, 255, 255));
                _shader.SetFloat("_OutlineTexToggle", 1);
                _shader.SetFloat("_OutlineGlow", 1.5f);
                _shader.SetTexture("_OutlineTex", Tex[1]);
                _shader.SetFloat("_GradBlend", 0.4f);

            }
        }
        else
        {
            _anim.SetBool("ASPDPotion", false);
            _anim.SetBool("Reflection", false);
            _anim.SetBool("InvinciblePotion", false);
            _shader.DisableKeyword("GLOW_ON");
            _shader.DisableKeyword("OUTBASE_ON");
            _shader.DisableKeyword("SHINE_ON");
            _shader.DisableKeyword("GRADIENT_ON");
            _shader.DisableKeyword("HSV_ON");
            _shader.DisableKeyword("SHADOW_ON");
            _shader.DisableKeyword("GLITCH_ON");
        }
    }
}
