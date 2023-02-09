using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public GameObject maskitem;
    public GameObject enermy;
    public AudioSource lifeUp;
    public AudioSource lifeDown;
    // public enermy tempScript = null;
    public int num = 1;
    SpriteRenderer spriteRenderer;
    Animator anim;


    [SerializeField]
    private NewJoy virtualJoystick;
    private float moveSpeed = 7;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    Vector3 inputPos;
    Vector2 dir;
    // Update is called once per frame
    void Update()
    {
        float x = virtualJoystick.Horizontal();
        float y = virtualJoystick.Vertical();

        if (Input.GetMouseButton(0)) //마우스 좌측 버튼이 눌릴때 포지션 가져옴
            //마우스 좌표 스크린 기준, 월드 좌표로 바꿈
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        anim.SetFloat("inputx", dir.x - x); //좌우 화살표
        anim.SetFloat("inputy", dir.y - y); //상화 화살표

        if (x != 0 || y != 0)
        {
            anim.SetBool("ismove", true);
            transform.position = Vector2.MoveTowards(transform.position,
               transform.position += new Vector3(x, y, 0), moveSpeed * Time.deltaTime);
        }
        else
            anim.SetBool("ismove", false);

        ScreenChk();
    }
/*
    void Update()
    {
        // ���콺 ������ ������
        if (Input.GetMouseButton(0))
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // ��ũ�� ��ǥ ���� -> World ��ǥ�� �ٲ���


        // ĳ���Ͱ� �ٶ󺸴� ���� = ���콺 ������ - �÷��̾� ������ 
        dir = inputPos - transform.position;

        if (dir != Vector2.zero)
            anim.SetBool("ismove", true);
        else
            anim.SetBool("ismove", false);

        // L,R -> X / F, B -> Y�� �� �ѱ�   
        anim.SetFloat("inputx", dir.x);
        anim.SetFloat("inputy", dir.y);

        // ���ڰ� ��ŭ �̵�(���� ��ġ -> ������_playerobject �̵�)
        transform.position =
            Vector2.MoveTowards(transform.position, inputPos, Time.deltaTime * moveSpeed);
        ScreenChk();
        //transform.Translate(new Vector2(inputX, inputY)*Time.deltaTime*moveSpeed);
    }
    */
    // {
    //     float inputX = Input.GetAxisRaw("Horizontal");
    //     float inputY = Input.GetAxisRaw("Vertical");

    //     if (inputX != 0 || inputY != 0)
    //         anim.SetBool("ismove", true);
    //     else
    //         anim.SetBool("ismove", false);

    //     anim.SetFloat("inputx",inputX);
    //     anim.SetFloat("inputy", inputY);

    //     transform.Translate(new Vector2(inputX, inputY) * Time.deltaTime * moveSpeed);
    //     ScreenChk();

    // }

    private void ScreenChk()
    {
        //Vector3 worlpos = (0f, 0f, 0f);
        //Vector3 worlpos = Camera.main.WorldToViewportPoint(this.transform.position);
        Vector3 worlpos = this.transform.position;
        if (this.transform.position.x < -1.5f) worlpos.x = -1.5f;
        if (this.transform.position.x > 7.75f) worlpos.x = 7.75f;
        if (this.transform.position.y < -3.75f) worlpos.y = -3.75f;
        if (this.transform.position.y > 3.2f) worlpos.y = 3.2f;
        this.transform.position = worlpos;
        //this.transform.position = Camera.main.ViewportToWorldPoint(worlpos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "bubbleObj")
        {
            // tempScript = GameObject.Find("enermy").GetComponent<enermy>();
            // if(tempScript != null) 
            //     tempScript.stopSignal = true;

            OnDamaged();

            GameObject findObj = GameObject.Find("heart"+num.ToString());
            if(findObj){
                findObj.GetComponent<Renderer>().enabled = false;
                lifeDown.Play();
                num++;
            }
            if(num > 5) {
                GameObject obj = GameObject.Find("enermy");
                
                if(obj){
                    obj.GetComponent<enermy>().stopSignal = true;
                    GameObject.Find("player").SetActive(false);
                    SceneManager.LoadScene("END");
                    // menuSet.SetActive(true);//게임오버 나오기
                }
            }
            Invoke("OffDamaged", 1); //1초 무적
        }

        else if(collision.gameObject.tag == "heartObj")
        {
            if(num > 1){
                num--;
                GameObject findObj = GameObject.Find("heart"+num.ToString());
                if(findObj){
                    findObj.GetComponent<Renderer>().enabled = true;
                }
                
            }
            lifeUp.Play();
            GameObject.Find("heartObj").SetActive(false);
        }

        else if(collision.gameObject.tag == "maskitem")
        {
            maskitem.SetActive(true);
            enermy.GetComponent<enermy>().maskTime=Time.time;
            enermy.GetComponent<enermy>().maskStop=true;
            // Invoke("maskitemeat", 2); //2초 기다렸다가 마스크 없앰
            GameObject maskObj = GameObject.Find("maskitem");
                if(maskObj){
                    maskObj.GetComponent<Renderer>().enabled = true;
                }
                
            lifeUp.Play();
            GameObject.Find("maskitem").SetActive(false);

        }
    }

    
    public void maskitemeat() //마스크 보여지기
    {
        maskitem.SetActive(false);
        enermy.GetComponent<enermy>().maskStop=false;
    }

    void OnDamaged() //무적상태
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
    }

    void OffDamaged() //무적 해제
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void Retry() //게임다시 시작
    {
        SceneManager.LoadScene("START");
    }

    public void GameExit() //게임종료
    {
        Application.Quit();
    }

}