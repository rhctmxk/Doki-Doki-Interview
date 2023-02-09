using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove2 : MonoBehaviour
{
    // Player�� �̵� �ӵ�
    public float moveSpeed;
    Animator anim;
    void Start()
    {
        // �ִϸ����� ����
        anim = GetComponent<Animator>();
    }

    Vector3 inputPos;
    Vector2 dir;
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

        //transform.Translate(new Vector2(inputX, inputY)*Time.deltaTime*moveSpeed);
    }
}
