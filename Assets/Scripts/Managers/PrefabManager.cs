using CESCO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // 벌레, 플레이어
    [SerializeField] private GameObject[] bugPrefabs;
    [SerializeField] private GameObject[] hitPrefabs;
    [SerializeField] private GameObject playerHasTool;

    [Header("▼ HP Bar")]
    [SerializeField] private GameObject hpImagePrefab;
    [SerializeField] private GameObject hpBackgroundImagePrefab;

    private List<GameObject> objs;
    private List<GameObject>[] bugPools;
    private List<GameObject>[] hitPools;

    private void Awake()
    {
        objs = new List<GameObject>();
        objs.Insert((int)OBJ_TYPE.PLAYER_HAS, playerHasTool);
        objs.Insert((int)OBJ_TYPE.GAUGE_IMAGE, hpImagePrefab);
        objs.Insert((int)OBJ_TYPE.GAUGE_BG_IMAGE, hpBackgroundImagePrefab);

        bugPools = new List<GameObject>[bugPrefabs.Length];
        for (int i = 0; i < bugPools.Length; ++i)
        {
            bugPools[i] = new List<GameObject>();
        }
        InitializeBug(20);

        hitPools = new List<GameObject>[hitPrefabs.Length];
        for (int i = 0; i < hitPools.Length; ++i)
        {
            hitPools[i] = new List<GameObject>();
        }
        InitializeHit(10);
    }

    private void InitializeBug(int count)
    {
        for (int type = 0; type < bugPrefabs.Length; ++type)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject newBug = Instantiate(bugPrefabs[type], transform);

                newBug.GetComponent<Bug>().SetHPCanvas();
                newBug.GetComponent<Bug>().SetHPBar(
                    RequestInstantiate(OBJ_TYPE.GAUGE_BG_IMAGE, newBug.GetComponent<Bug>().HpCanvas.transform),
                    RequestInstantiate(OBJ_TYPE.GAUGE_IMAGE, newBug.GetComponent<Bug>().HpCanvas.transform));

                newBug.SetActive(false);
                bugPools[type].Add(newBug);
            }
        }
    }
    
    private void InitializeHit(int count)
    {
        for (int type = 0; type < hitPools.Length; ++type)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject newHit = Instantiate(hitPrefabs[type], transform);
                newHit.SetActive(false);
                hitPools[type].Add(newHit);
            }
        }
    }

    public void InitBug(BUG_TYPE type)
    {
        foreach (GameObject bug in bugPools[(int)type])
        {
            if (bug.activeSelf)
            {
                bug.SetActive(false);
            }
        }
    }

    public GameObject RequestInstantiate(OBJ_TYPE objType)
    {
        return Instantiate(objs[(int)objType]);
    }
    
    public GameObject RequestInstantiate(OBJ_TYPE objType, Transform parent)
    {
        return Instantiate(objs[(int)objType], parent);
    }


    public GameObject GetBug(BUG_TYPE type)
    {
        GameObject selectBug = null;

        // 선택한 풀의 비활성화 된 게임오브젝트 접근
        foreach(GameObject bug in bugPools[(int)type])
        {
            if (!bug.activeSelf)
            {
                // 발견하면 selectBug에 할당
                selectBug = bug;
                //selectBug.SetActive(true);
                break;
            }
        }

        // 못 찾았으면
        if (!selectBug)
        {
            // 새롭게 생성하고 selectBug에 할당
            selectBug = Instantiate(bugPrefabs[(int)type], transform);
            bugPools[(int)type].Add(selectBug);
        }

        return selectBug;
    }

    public GameObject GetHit(HIT_OBJ_TYPE type)
    {
        GameObject selectHit = null;

        // 선택한 풀의 비활성화 된 게임오브젝트 접근
        foreach(GameObject hit in hitPools[(int)type])
        {
            if (!hit.activeSelf)
            {
                // 발견하면 selectBug에 할당
                selectHit = hit;
                selectHit.SetActive(true);
                break;
            }
        }

        // 못 찾았으면
        if (!selectHit)
        {
            // 새롭게 생성하고 selectBug에 할당
            selectHit = Instantiate(hitPrefabs[(int)type], transform);
            hitPools[(int)type].Add(selectHit);
        }

        return selectHit;
    }

}
