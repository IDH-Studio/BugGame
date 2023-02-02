using CESCO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject joyStick;

    void Update()
    {
        if (Input.touchCount > 0 && GameManager.instance.GameState == CESCO.GAME_STATE.RUNNING)
        {
            Touch firstTouch = Input.GetTouch(0);

            print("touch.position.x: " + firstTouch.position.x);
            print("Screen.width: " + Screen.width);
            print("touch.position.x < Screen.width: " + (firstTouch.position.x < Screen.width));

            switch(firstTouch.phase)
            {
                case TouchPhase.Began:
                    if (firstTouch.position.x < Screen.width / 2)
                    {
                        print("터치 다운");
                        joyStick.SetActive(true);
                        joyStick.GetComponent<JoyStick>().OnDown(firstTouch.position);
                    }
                    break;
                case TouchPhase.Moved:
                    joyStick.GetComponent<JoyStick>().Drag(firstTouch.position);
                    break;
                case TouchPhase.Ended:
                    print("터치 업");
                    joyStick.SetActive(false);
                    joyStick.GetComponent<JoyStick>().OnUp();
                    break;
            }

            //Touch secondTouch = Input.GetTouch(1);
            //switch (secondTouch.phase)
            //{
            //    case TouchPhase.Began:
            //        if (secondTouch.position.x > Screen.width / 2)
            //        {
            //            GameManager.instance.CurrentPlayer.Hit();
            //        }
            //        break;
            //}
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        print("터치 업");
        joyStick.SetActive(false);
        joyStick.GetComponent<JoyStick>().OnUp();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("터치 다운");
        joyStick.SetActive(true);
        joyStick.GetComponent<JoyStick>().OnDown(eventData.position);
    }
}
