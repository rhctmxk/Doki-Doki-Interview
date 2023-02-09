using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeSceneBtn()
    {
        switch (this.gameObject.name)
        {
            case "Gamestart":
                SceneManager.LoadScene("START");
                break;
            case "Option":
                SceneManager.LoadScene("OPTION");
                break;
            case "Information":
                SceneManager.LoadScene("INFORMATION");
                break;
        }
    }   
}
