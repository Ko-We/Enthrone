using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalSoundCheck : MonoBehaviour
{
    MyObject myChar;
    public AudioSource _audio;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        myChar = MyObject.MyChar;
        _audio = GetComponent<AudioSource>();
        if (myChar.muteEffectSound)
        {
            _audio.mute = true;
        }
        else if (myChar.muteEffectSound)
        {
            _audio.mute = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myChar.muteEffectSound)
        {
            _audio.mute = true;
        }
        else if (myChar.muteEffectSound)
        {
            _audio.mute = false;
        }
    }
}
