using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CESCO;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background Object")]
    [SerializeField] private GameObject backgroundObj;

    [Header("Background Images")]
    [SerializeField] private Sprite startBackgroundImage;
    [SerializeField] private Sprite mosquitoBackgroundImage;
    [SerializeField] private Sprite flyBackgroundImage;
    [SerializeField] private Sprite cicadaBackgroundImage;
    [SerializeField] private Sprite cockroachBackgroundImage;
    [SerializeField] private SpriteRenderer targetObj;

    private List<Sprite> imageList;

    private void Awake()
    {
        imageList = new List<Sprite>();

        if (mosquitoBackgroundImage) imageList.Add(mosquitoBackgroundImage);
        if (flyBackgroundImage) imageList.Add(flyBackgroundImage);
        if (cicadaBackgroundImage) imageList.Add(cicadaBackgroundImage);
        if (cockroachBackgroundImage) imageList.Add(cockroachBackgroundImage);
        if (startBackgroundImage) imageList.Add(startBackgroundImage);
    }

    public void ChangeBackground()
    {
        if (GameManager.instance.GameState == GAME_STATE.START ||
            GameManager.instance.GameState == GAME_STATE.RUNNING)
        {
            int type = (int)GameManager.instance.spawnManager.BugType;
            backgroundObj.GetComponent<Image>().sprite = imageList[type];

            if (GameManager.instance.spawnManager.BugType == BUG_TYPE.FLY)
            {
                targetObj.color = new Color(0f, 32 / 255f, 231 / 255f);
            }
            else
            {
                targetObj.color = new Color(231 / 255f, 32 / 255f, 0f);
            }
        }
    }
}
