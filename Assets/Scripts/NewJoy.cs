using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NewJoy : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    Vector3 move;
    public RectTransform pad;
    public RectTransform stick;

    public void OnDrag(PointerEventData eventData) //손가락이 움직이는 부분
    {
            stick.position = eventData.position; //손가락 위치, 스틱이 움직이는 위치 

            //스틱을 패드안에 넣어둠, 스틱이 패드의 반지름 만큼 움직이도록 제한, 스틱이 패드 밖으로 나가지 않음
            stick.localPosition = Vector2.ClampMagnitude(eventData.position - (Vector2)pad.position, pad.rect.width * 0.5f);

            move = new Vector3(stick.localPosition.x, 0, stick.localPosition.y).normalized;
    }

    public void OnPointerDown(PointerEventData eventData) //처음 터치될때 
    {
        pad.position = eventData.position; //패드위치 터치되는 곳에 위치, 처음 손가락 댄 곳에 나타남
        pad.gameObject.SetActive(true); //터치하면 다시 나타남
    }

    public void OnPointerUp(PointerEventData eventData)// 손가락 올림
    {
        stick.localPosition = Vector2.zero; //원래 위치로 돌아감 
        pad.gameObject.SetActive(false); //손가락 올리면 패드 사라짐
    }

    public float Horizontal()
    {
        return stick.localPosition.x;
    }

    public float Vertical()
    {
        return stick.localPosition.y;
    }
}