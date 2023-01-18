using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    MyObject myChar;
    public AudioClip[] BGM;
    public AudioClip[] SFX;
    
    public int BGM_Num;
    
    public AudioSource audioSource;
    SoundPool soundPool;

    private static SoundManager _instance = null;

    private SoundManager()
    { }

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Singleton == null");
            }
            return _instance;
        }
    }

    private void Awake()
    {

        myChar = MyObject.MyChar;
        _instance = this;

        soundPool = GetComponent<SoundPool>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        //audioSource.pitch = Time.timeScale;
        if (!myChar.muteBGM)
        {
            if (!audioSource.isPlaying)
            {
                if (SceneManager.GetActiveScene().name == "TitleScene")
                {
                    if (TitleManager.instance.VideoPanel.activeSelf == false)
                    {                        
                        PlayBGM();
                    }                    
                }
                else
                {
                    audioSource.enabled = true;
                    PlayBGM();
                }
                
            }
        }
        else if (myChar.muteBGM)
        {
            MuteBGM();
        }
        BGMCheck();
    }
    public void PlayBGM()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            if (!TitleManager.instance.BgmCheck)
            {
                audioSource.clip = BGM[14];
                audioSource.Play();
                TitleManager.instance.BgmCheck = true;
            }
            else
            {
                audioSource.clip = BGM[15];
                audioSource.Play();
            }            

        }
        if (SceneManager.GetActiveScene().name == "LobbiScene")
        {
            if (LobbiManager.instance.BlackSmithPanel.activeSelf == false && LobbiManager.instance.CashShopPanel.activeSelf == false)
            {
                audioSource.clip = BGM[0];
                audioSource.Play();
            }            
            if (LobbiManager.instance.BlackSmithPanel.activeSelf == true  && LobbiManager.instance.CashShopPanel.activeSelf == false)
            {
                audioSource.clip = BGM[17];
                audioSource.Play();
            }
            if (LobbiManager.instance.CashShopPanel.activeSelf == true  && LobbiManager.instance.BlackSmithPanel.activeSelf == false)
            {
                audioSource.clip = BGM[18];
                audioSource.Play();
            }


        }

        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            if (GameManager.Instance.Clear_ResultPanel.activeSelf)
            {
                audioSource.clip = BGM[2];
                //BGM_Num = 4;
            }
            else if (GameManager.Instance.Fail_ResultPanel.activeSelf)
            {
                audioSource.clip = BGM[3];
                //BGM_Num = 5;
            }
            else if (GameManager.Instance.PreferencesWindow.activeSelf)
            {
                audioSource.clip = BGM[16];
                //BGM_Num = 16;
            }
            else
            {
                if (!myChar.Tutorial)
                {
                    switch (myChar.Chapter)
                    {
                        case 0:
                            Basic_50_Stage(5, 4, 10, 7);
                            break;
                        case 1:
                            Basic_50_Stage(5, 4, 10, 9);
                            break;
                        case 2:
                            Hero_30_Stage(5, 4, 10, 11, 19);
                            break;
                        case 3:
                            Basic_50_Stage(5, 4, 10, 8);
                            break;
                        case 4:
                            Basic_50_Stage(5, 4, 10, 6);
                            break;
                        case 5:
                            Hero_30_Stage(5, 4, 10, 11, 19);
                            break;

                    }
                }
                else if (myChar.Tutorial)
                {
                    if (myChar.BossCheck)
                    {
                        if (!myChar.CrownGetBgm)
                        {
                            audioSource.clip = BGM[11];
                        }
                        else if (myChar.CrownGetBgm)
                        {
                            audioSource.clip = BGM[12];
                        }
                    }
                    else if (!myChar.BossCheck)
                    {
                        audioSource.clip = BGM[7];
                    }
                }
            }
            audioSource.Play();
        }

    }

    public void MuteBGM()
    {
        audioSource.Stop();
    }

    public void PlaySfx(int _sfx, float _volume = 1)
    {
        // if (myChar.effectSoundMute) return;

        // int count = SoundPool.Instance.soundAmount;
        if (!myChar.muteEffectSound)
        {
            Transform tempSfx = SoundPool.Instance.soundPool.Spawn(SoundPool.Instance.SoundPrefab);
            SFX sfx = tempSfx.GetComponent<SFX>();
            sfx.Play(SFX[_sfx], _volume);
        }

        // count--;
    }

    private void BGMCheck()
    {

        if (SceneManager.GetActiveScene().name == "LobbiScene")
        {
            if (LobbiManager.instance.BlackSmithPanel.activeSelf == false && LobbiManager.instance.CashShopPanel.activeSelf == false)
            {
                if (audioSource.clip != BGM[0])
                {
                    audioSource.enabled = false;
                }
            }
            if (LobbiManager.instance.BlackSmithPanel.activeSelf == true && LobbiManager.instance.CashShopPanel.activeSelf == false)
            {
                if (audioSource.clip != BGM[17])
                {
                    audioSource.enabled = false;
                }
            }
            if (LobbiManager.instance.CashShopPanel.activeSelf == true && LobbiManager.instance.BlackSmithPanel.activeSelf == false)
            {
                if (audioSource.clip != BGM[18])
                {
                    audioSource.enabled = false;
                }
            }
        }
        if (SceneManager.GetActiveScene().name == "StageScene")
        {
            if (GameManager.Instance.Clear_ResultPanel.activeSelf)
            {
                if (audioSource.clip != BGM[2])
                {
                    audioSource.enabled = false;
                }
            }
            else if (GameManager.Instance.Fail_ResultPanel.activeSelf)
            {
                if (audioSource.clip != BGM[3])
                {
                    audioSource.enabled = false;
                }
            }
            else if (GameManager.Instance.PreferencesWindow.activeSelf)
            {
                if (audioSource.clip != BGM[16])
                {
                    audioSource.enabled = false;
                }
            }
            else
            {
                if (!myChar.Tutorial)
                {
                    switch (myChar.Chapter)
                    {
                        case 0:
                            Basic_50_StageCheck(5, 4, 10, 7);
                            break;
                        case 1:
                            Basic_50_StageCheck(5, 4, 10, 9);
                            break;
                        case 2:
                            Hero_30_StageCheck(5, 4, 10, 11, 19);
                            break;
                        case 3:
                            Basic_50_StageCheck(5, 4, 10, 8);
                            break;
                        case 4:
                            Basic_50_StageCheck(5, 4, 10, 6);
                            break;
                        case 5:
                            Hero_30_StageCheck(5, 4, 10, 11, 19);
                            break;

                    }
                }
                else if (myChar.Tutorial)
                {
                    if (myChar.BossCheck)
                    {
                        if (!myChar.CrownGetBgm)
                        {
                            if (audioSource.clip != BGM[11])
                            {
                                audioSource.enabled = false;
                            }
                        }
                        else if (myChar.CrownGetBgm)
                        {
                            if (audioSource.clip != BGM[12])
                            {
                                audioSource.enabled = false;
                            }
                        }
                    }
                    else if (!myChar.BossCheck)
                    {
                        if (audioSource.clip != BGM[7])
                        {
                            audioSource.enabled = false;
                        }
                    }
                }
            }
        }
    }
    private void Basic_50_Stage(int Store, int Reward, int Boss, int Main)
    {
        if (((myChar.Stage % 10) == 3) || ((myChar.Stage % 10) == 6))
        {
            audioSource.clip = BGM[Store];
        }
        else if ((myChar.Stage % 10) == 9)
        {
            audioSource.clip = BGM[Reward];
        }
        else if ((myChar.Stage % 10) == 0)
        {
            audioSource.clip = BGM[Boss];
        }
        else
        {
            audioSource.clip = BGM[Main];
        }
    }
    private void Hero_30_Stage(int Store, int Reward, int Boss, int HeroBoss, int Main)
    {
        if (((myChar.Stage % 10) == 3) || ((myChar.Stage % 10) == 6))
        {
            audioSource.clip = BGM[Store];
        }
        else if ((myChar.Stage % 10) == 9)
        {
            audioSource.clip = BGM[Reward];
        }
        else if ((myChar.Stage % 10) == 0)
        {
            if (myChar.Stage == 30)
            {
                if (!myChar.CrownGetBgm)
                {
                    audioSource.clip = BGM[HeroBoss];
                }
                else if (myChar.CrownGetBgm)
                {
                    audioSource.clip = BGM[12];
                }
            }
            else
            {
                audioSource.clip = BGM[Boss];
            }
        }
        else
        {
            audioSource.clip = BGM[Main];
        }
    }
    private void Basic_50_StageCheck(int Store, int Reward, int Boss, int Main)
    {
        if (((myChar.Stage % 10) == 3) || ((myChar.Stage % 10) == 6))
        {
            if (audioSource.clip != BGM[Store])
            {
                audioSource.enabled = false;
            }
        }
        else if ((myChar.Stage % 10) == 9)
        {
            if (audioSource.clip != BGM[Reward])
            {
                audioSource.enabled = false;
            }
        }
        else if ((myChar.Stage % 10) == 0)
        {
            if (audioSource.clip != BGM[Boss])
            {
                audioSource.enabled = false;
            }
        }
        else
        {
            if (audioSource.clip != BGM[Main])
            {
                audioSource.enabled = false;
            }
        }
    }
    private void Hero_30_StageCheck(int Store, int Reward, int Boss, int HeroBoss, int Main)
    {
        if (((myChar.Stage % 10) == 3) || ((myChar.Stage % 10) == 6))
        {
            if (audioSource.clip != BGM[Store])
            {
                audioSource.enabled = false;
            }
        }
        else if ((myChar.Stage % 10) == 9)
        {
            if (audioSource.clip != BGM[Reward])
            {
                audioSource.enabled = false;
            }
        }
        else if ((myChar.Stage % 10) == 0)
        {
            if (myChar.Stage == 30)
            {
                if (!myChar.CrownGetBgm)
                {
                    if (audioSource.clip != BGM[HeroBoss])
                    {
                        audioSource.enabled = false;
                    }
                }
                else if (myChar.CrownGetBgm)
                {
                    if (audioSource.clip != BGM[12])
                    {
                        audioSource.enabled = false;
                    }
                }
                
            }
            else
            {
                if (audioSource.clip != BGM[Boss])
                {
                    audioSource.enabled = false;
                }
            }
        }
        else
        {
            if (audioSource.clip != BGM[Main])
            {
                audioSource.enabled = false;
            }
        }
    }
}
