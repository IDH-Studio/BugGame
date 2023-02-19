using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowToolsButton : MonoBehaviour
{
    [SerializeField] private GameObject offImage;
    [SerializeField] private GameObject onImage;
    [SerializeField] private GameObject selectToolObj;

    private bool isOn = false;

    private void Awake()
    {
        offImage.SetActive(true);
        onImage.SetActive(false);
        selectToolObj.SetActive(false);
    }

    public void Init()
    {
        ShowSelectTool();
    }

    public void ShowSelectTool()
    {
        isOn = !isOn;
        offImage.SetActive(!isOn);
        onImage.SetActive(isOn);
        selectToolObj.SetActive(isOn);
    }
}
