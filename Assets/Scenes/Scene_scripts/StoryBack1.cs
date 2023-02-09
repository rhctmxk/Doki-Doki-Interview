using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryBack1 : MonoBehaviour
{
    int sceneIndex;
    GameObject BackgroundMusic;
    AudioSource backmusic;

    void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    public void PreviousScene()
    {
        SceneManager.LoadScene(sceneIndex - 1);
    }

    public void BackGroundMusicOffButton() //������� Ű�� ���� ��ư
    {
        BackgroundMusic = GameObject.Find("BackgroundMusic");
        backmusic = BackgroundMusic.GetComponent<AudioSource>(); //������� �����ص�
        if (backmusic.isPlaying) backmusic.Pause();
    }

}
