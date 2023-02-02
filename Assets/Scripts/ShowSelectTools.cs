using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSelectTools : MonoBehaviour
{
    [SerializeField] private GameObject offImage;
    [SerializeField] private GameObject onImage;
    [SerializeField] private GameObject selectToolObj;

    private bool isOn = false;

    public void ShowSelectTool()
    {
        isOn = !isOn;
        offImage.SetActive(!isOn);
        onImage.SetActive(isOn);
        selectToolObj.SetActive(isOn);
    }
}
