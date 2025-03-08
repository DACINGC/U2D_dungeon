using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string iD;
    public bool isActive;
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
    }
    [ContextMenu("���ɴ浵���ID")]
    private void GenerateId()
    {
        //ϵͳ�������ID
        iD = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            if (!isActive)
                AudioManager.instance.PlaySFX(5, transform, false);
            GameManager.instance.LastPastCheckPointId = iD;
            ActiveCheckPont();
            isActive = true;
        }
    }
    public void ActiveCheckPont()
    {
        anim.SetTrigger("Active");
    }
}
