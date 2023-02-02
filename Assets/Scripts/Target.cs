using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Target : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI showHP;
    private float hp = 200;

    public float HP
    {
        get { return hp; }
    }

    public void Init()
    {
        hp = 200;
    }

    private void OnDisable()
    {
        Init();
    }

    private void OnEnable()
    {
        transform.position = Vector3.zero;
        showHP.text = "HP: " + hp;
    }

    //private void Start()
    //{
    //    transform.position = Vector3.zero;
    //    showHP.text = "HP: " + hp;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bug")
        {
            hp -= 10;
            showHP.text = "HP: " + hp;
        }
    }
}
