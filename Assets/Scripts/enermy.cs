using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/** 
<Parameters>
- maxTime : 침방울의 세기
- AddFoce's first parameter : 침방울의 포물선 궤적
*/

public class enermy : MonoBehaviour
{
    //public GameObject enermy;
    public GameObject bubbleObj;
    public GameObject heartObj;
    public GameObject maskitem;
    public Text scoreText;
    public bool stopSignal = false;
    public bool maskStop = false;
    private bool heartActing = true;
    private bool maskActing = true;

    private float startTime; 
    public float maskTime=0;
    public float maxTime=50;
    private float timer=0;
    public int scorePoint=50;
    private int addPoint=0;
    /**
    - scorePoint : 기본 상승 포인트(시간 초에 따라 +1)
    - addPoint : 특수 침방울에 따른 추가 포인트
    */ 
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        Debug.Log((int)startTime);
        stopSignal = false;
        // Invoke("AddingPoint", 1f); // 1초 후 AddingPoint 실행
    }

    /*
    heart에 물리법칙 안받게 rigidbody 설정하는 법
    : body type kinematic 하고 isTrigger check!
    */

    void AppearingHeart()
    {
        if(heartObj.activeSelf == false && heartActing){
            heartActing = false;
            heartObj.SetActive(true);
            Debug.Log(1);
            Vector3 position = heartObj.transform.localPosition; 
            position.x = Random.Range(-1.5f, 7.5f); 
            position.y = Random.Range(-3.5f, 3.5f);
            heartObj.transform.localPosition = position;
        }
    }

    void AppearingMask()
    {
        if(maskitem.activeSelf == false && maskActing){
            maskActing = false;
            maskitem.SetActive(true);
            Debug.Log(1);
            Vector3 position = maskitem.transform.localPosition; 
            position.x = Random.Range(-1.5f, 7.75f); 
            position.y = Random.Range(-3.75f, 3.2f);
            maskitem.transform.localPosition = position;
        }
    }

    void AddingPoint()
    {
        scorePoint = ((int)Time.time - (int)startTime) + addPoint;
        scoreText.text = "" + scorePoint;
        PlayerPrefs.SetInt("scorePoint", scorePoint);
        PlayerPrefs.Save();
        if((Time.time - startTime) > 15 && (Time.time - startTime) <= 30) // 10초 이후에 침방울 개수 많아짐
            maxTime = 10;
        
        if((Time.time - startTime) > 30)
            maxTime = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(!stopSignal){
            if((((Time.time - startTime) / 20) >= 1) && ((int)Time.time - (int)startTime) % 20 == 0){
                AppearingHeart();
            }
            if((((Time.time - startTime) / 20) >= 1) && (((int)Time.time - (int)startTime) % 20 == 3))
            {   
                if(heartObj.activeSelf == true)
                    heartObj.SetActive(false);
                heartActing = true;
            }
            if((((Time.time - startTime) / 14) >= 1) && ((int)Time.time - (int)startTime) % 14 == 0){
                AppearingMask();
            }
            if((((Time.time - startTime) / 14) >= 1) && (((int)Time.time - (int)startTime) % 17 == 0))
            {   
                if(maskitem.activeSelf == true)
                    maskitem.SetActive(false);
                maskActing = true;
            }
            if(maskTime > 0 && Time.time > maskTime + 2){
                Debug.Log("maskTime");
                maskTime = 0;
                GameObject.Find("player").GetComponent<PlayerMove>().maskitemeat();
            }
            if(!maskStop){
                if(timer > maxTime) {
                    // Debug.Log(i);
                    float i = Random.Range(-10, 15);
                    
                    // heart drop(20")
                    
                    GameObject bubble = Instantiate(bubbleObj, transform.position, transform.rotation);
        // arrObj[i % 4]
                    Rigidbody2D rigid = bubble.GetComponent<Rigidbody2D>();
                    Vector2 dirVec = new Vector2(10, i);

                    // Debug.Log(i);
                    // Debug.Log(dirVec.normalized * (90-(i * 6)));
                    rigid.transform.Rotate(new Vector3(0f,0f,(float)(90 + i * 2.5))); // 물방울 회전각
                    rigid.AddForce(dirVec, ForceMode2D.Impulse); // ForceMode2D.Impulse : 강하게 나가는 효과 위함
                    Destroy(bubble, 20); // 침방울이 20초 뒤 모두 없어짐(근데 이 코드 리소스를 많이 잡아먹어서 없애는게 나음)
                    timer = 0;
                }
            }
            timer += 1;
            AddingPoint();    
        }
    }
    public void stopBubble()
    {
        stopSignal = true;
    }

}
