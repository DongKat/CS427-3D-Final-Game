using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamBob : MonoBehaviour
{
    public Animator anim;
    bool walk;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal") | Input.GetButton("Vertical"))
        {
            walk = true;
            anim.ResetTrigger("idle");
            anim.ResetTrigger("sprint");
            anim.SetTrigger("walk");
            if (walk && Input.GetButton("Sprint"))
            {
                anim.ResetTrigger("idle");
                anim.ResetTrigger("walk");
                anim.SetTrigger("sprint");
            }
        }
        else
        {
            anim.ResetTrigger("walk");
            anim.ResetTrigger("sprint");
            anim.SetTrigger("idle");
            walk = false;
        }
    }
}
