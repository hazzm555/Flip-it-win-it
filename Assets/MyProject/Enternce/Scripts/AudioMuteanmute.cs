using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class AudioMuteanmute : MonoBehaviour
{
    Toggle mytoggle; 
    // Start is called before the first frame update
    void Start()
    {
        mytoggle = GetComponent<Toggle>();
        if(AudioListener.volume == 0)
        {
            mytoggle.isOn = false;
        }
    }

 public void ToggleAudioValueChange(bool audioIn)
    {
         if(audioIn)
        {
            AudioListener.volume = 1;
        }
         else
        {
            AudioListener.volume = 0;
        }


    }
}
