using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    // Player의 이동 속도
    public float moveSpeed;
    Animator anim;
    void Start()
    {
        // 애니메이터 접근
        anim = GetComponent<Animator>();
    }

    Vector3 inputPos;
    Vector2 dir;
    void Update()
    {
        // 마우스 포지션 가져옴
        if (Input.GetMouseButton(0))
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // 스크린 좌표 기준 -> World 좌표로 바꿔줌


        // 캐릭터가 바라보는 방향 = 마우스 포지션 - 플레이어 포지션 
        dir = inputPos - transform.position;

        if (dir != Vector2.zero)
            anim.SetBool("ismove", true);
        else
            anim.SetBool("ismove", false);

        // L,R -> X / F, B -> Y로 값 넘김   
        anim.SetFloat("inputx", dir.x);
        anim.SetFloat("inputy", dir.y);

        // 인자값 만큼 이동(시작 위치 -> 목적지_playerobject 이동)
        transform.position =
            Vector2.MoveTowards(transform.position, inputPos, Time.deltaTime * moveSpeed);

        //transform.Translate(new Vector2(inputX, inputY)*Time.deltaTime*moveSpeed);
    }
}
