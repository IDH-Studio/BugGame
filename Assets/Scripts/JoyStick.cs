using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform stick;

    private Vector3 movePosition;
    public Vector3 MovePosition { get { return movePosition; } }
    private float radius;
    private bool canMove = false;
    private bool isTouch = false;
    public bool IsTouch
    {
        get { return isTouch; }
    }

    void Start()
    {
        //gameObject.SetActive(false);
        radius = background.rect.width * 0.5f;
    }

    private void OnEnable()
    {
        //canMove = true;
    }

    private void OnDisable()
    {
        //canMove = false;
        //movePosition = Vector3.zero;
    }

    //public void SetPosition(Vector2 position)
    //{
    //    background.position = position;
    //    stick.position = position;
    //}

    public void Drag(Vector2 position)
    {
        if (canMove == true)
        {
            Vector2 value = position - (Vector2)background.position;

            value = Vector2.ClampMagnitude(value, radius);
            stick.localPosition = value;

            float distance = Vector2.Distance(background.position, stick.position) / radius;
            value = value.normalized;
            movePosition = new Vector3(value.x * distance, value.y * distance);
        }
    }

    //public void OnDrag(PointerEventData eventData)
    //{
    //    if (isTouch)
    //    {
    //        Vector2 value = eventData.position - (Vector2)background.position;

    //        value = Vector2.ClampMagnitude(value, radius);
    //        transform.localPosition = value;

    //        float distance = Vector2.Distance(background.position, stick.position) / radius;
    //        value = value.normalized;
    //        movePosition = new Vector3(value.x * distance, value.y * distance);
    //    }
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    gameObject.SetActive(false);
    //    isTouch = false;
    //    //transform.localPosition = Vector3.zero;
    //    movePosition = Vector3.zero;
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    isTouch = true;
    //    gameObject.SetActive(true);
    //    background.localPosition = eventData.position;
    //    stick.localPosition = eventData.position;
    //}

    public void OnUp()
    {
        isTouch = false;
        canMove = false;
        //transform.localPosition = Vector3.zero;
        movePosition = Vector3.zero;
    }

    public void OnDown(Vector2 position)
    {
        isTouch = true;
        canMove = true;
        background.position = position;
        stick.position = position;
    }
}
