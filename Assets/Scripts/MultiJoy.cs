using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MultiJoy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    // ĳ���� �ڸ� ����
    public Transform player;
    Vector3 move;
    public float speed;


    public RectTransform pad;
    public RectTransform stick;

    public void OnDrag(PointerEventData eventData)
    {
        stick.position = eventData.position;

        stick.localPosition =
            Vector2.ClampMagnitude(eventData.position -
            (Vector2)pad.position, pad.rect.width * 0.5f);

        move = new Vector3(stick.localPosition.x, 0,
            stick.localPosition.y).normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pad.position = eventData.position;
        pad.gameObject.SetActive(true);

        StartCoroutine("Movement");

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stick.localPosition = Vector2.zero;
        pad.gameObject.SetActive(false);

        StopCoroutine("Movement");
        //���Ⱚ �ʱ�ȭ
        move = Vector3.zero;
    }

    // ĳ���� �̵�
    IEnumerator Movement()
    {
        while (true)
        {
            player.Translate(move * speed * Time.deltaTime);

            if (move != Vector3.zero)
                player.rotation = Quaternion.Slerp(player.rotation,
                    Quaternion.LookRotation(move), 5 * Time.deltaTime);
        }
    }
}
