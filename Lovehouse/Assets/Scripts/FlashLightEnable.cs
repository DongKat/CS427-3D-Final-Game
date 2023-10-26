using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightEnable : MonoBehaviour
{
    public GameObject fl_light;
    public bool fl_toggle;
    public AudioSource fl_enable;
    void Start()
    {
        if(fl_toggle == false)
        {
            fl_light.SetActive(false); 
        }
        if(fl_toggle == true)
        {
            fl_light.SetActive(true);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)) 
        {
            fl_toggle = !fl_toggle;
            // fl_enable.Play();
            fl_light.SetActive(fl_toggle);
        }   
    }
}
