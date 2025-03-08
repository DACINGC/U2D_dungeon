using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    public int sfxMinDistance = 4;
    public bool playBGM;
    public int bgmIdex;
    private bool canPlay;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;

        Invoke(nameof(CanPlaySFX), 0.1f);
    }

    private void Start()
    {
        if (!playBGM)
            StopAllBGM();
        else
        {
            if (!bgm[bgmIdex].isPlaying)
                PlayBGM(bgmIdex);
        }
    }
    public void PlaySFX(int _index, Transform _sourceTran, bool _changePitch)
    {
        if (canPlay == false)
            return;

        //if (sfx[_index].isPlaying)
        //    return;

        if (_sourceTran != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _sourceTran.position) > sfxMinDistance)
            return;

        if (_index < sfx.Length)
        {
            if (_changePitch)
                sfx[_index].pitch = Random.Range(0.8f, 1.2f);

            sfx[_index].Play();
        }
    }

    public void StopSFX(int _index)
    {
        sfx[_index].Stop();
    }

    public void PlayBGM(int _index)
    { 
        StopAllBGM();
        bgm[_index].Play();
    }
    public void StopAllBGM()
    { 
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void CanPlaySFX() => canPlay = true;

}
