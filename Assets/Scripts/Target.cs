using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Target : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI showHP;
    [SerializeField] private float hp;

    private float healthPoint;

    public float HP { get { return healthPoint; } }


    private void Awake() { Init(); }

    public void Init() { healthPoint = hp; }

    void ShowHP() { showHP.text = "HP: " + healthPoint; }


    private void OnDisable() { Init(); }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        ShowHP();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bug")
        {
            healthPoint -= 10;
            ShowHP();
        }
    }
}
