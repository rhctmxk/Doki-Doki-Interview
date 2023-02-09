using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassInvoke : MonoBehaviour
{
    float time = 0;
    float blinktime = 0.1f;
    float xtime = 0;
    float waittime = 0.2f;



    // Update is called once per frame
    void Update()
    {
            if(xtime<blinktime) // 깜빡
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - xtime * 10); //꺼졌다가
            }
            else if(xtime<waittime + blinktime)
            {

            }
            else
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 110);
                //켜졌다가
                if(xtime> waittime+ blinktime * 2)
                {
                    xtime = 0;
                    waittime *= 0.9f; //깜빡이는 시간 줄어들기/
                    if(waittime < 0.02f)
                    {
                        time = 0;
                        waittime = 0.2f;
                        this.gameObject.SetActive(false);
                    }
                }
            }
            xtime += Time.deltaTime;
        
        time += Time.deltaTime;
    }

    //대략 7초 정도
}


