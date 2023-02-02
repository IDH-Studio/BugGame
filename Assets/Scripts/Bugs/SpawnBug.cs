using CESCO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SpawnBug : MonoBehaviour
{
    [SerializeField] private BUG_TYPE bugType;

    public void Init()
    {
        GameManager.instance.prefabManager.InitBug(bugType);
    }

    public void Spawn(Vector2 position)
    {
        GameManager.instance.prefabManager.GetBug(bugType).GetComponent<Bug>().SetBug(position);
    }

    public void ChangeBug(BUG_TYPE type)
    {
        bugType = type;
    }
}
