using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadeSceen : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Fade_In()
    {
        //anim.SetBool("IN", true);
    }
    public void Fade_Out()
    {
        //anim.SetBool("IN", false);
    }
}