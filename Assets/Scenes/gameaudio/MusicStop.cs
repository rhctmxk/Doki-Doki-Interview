using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStop : MonoBehaviour
{
    GameObject BackgroundMusic;
    AudioSource backmusic;

    public void BackGroundMusicOffButton() //������� Ű�� ���� ��ư
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //������� �����ص�
        if (backmusic.isPlaying) backmusic.Pause();
        else backmusic.Play();
    }
}
