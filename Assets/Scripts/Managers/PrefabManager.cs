using CESCO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    // ����, �÷��̾�
    //[SerializeField] private GameObject spawnBug;
    [SerializeField] private GameObject[] bugPrefabs;
    //[SerializeField] private GameObject target;
    [SerializeField] private GameObject playerHasTool;
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject[] hitPrefabs;

    private List<GameObject> objs;
    private List<GameObject>[] bugPools;
    private List<GameObject>[] hitPools;

    private void Awake()
    {
        objs = new List<GameObject>();
        //objs.Insert((int)OBJ_TYPE.TARGET, target);
        objs.Insert((int)OBJ_TYPE.PLAYER_HAS, playerHasTool);
        //objs.Insert((int)OBJ_TYPE.PLAYER, player);

        bugPools = new List<GameObject>[bugPrefabs.Length];
        for (int i = 0; i < bugPools.Length; ++i)
        {
            bugPools[i] = new List<GameObject>();
        }
        InitializeBug(30);

        hitPools = new List<GameObject>[hitPrefabs.Length];
        for (int i = 0; i < hitPools.Length; ++i)
        {
            hitPools[i] = new List<GameObject>();
        }
        InitializeHit(20);
    }

    private void InitializeBug(int count)
    {
        for (int type = 0; type < bugPrefabs.Length; ++type)
        {
            for (int i = 0; i < count; ++i)
            {
                GameObject newBug = Instantiate(bugPrefabs[type], transform);
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

    public GameObject GetBug(BUG_TYPE type)
    {
        GameObject selectBug = null;

        // ������ Ǯ�� ��Ȱ��ȭ �� ���ӿ�����Ʈ ����
        foreach(GameObject bug in bugPools[(int)type])
        {
            if (!bug.activeSelf)
            {
                // �߰��ϸ� selectBug�� �Ҵ�
                selectBug = bug;
                selectBug.SetActive(true);
                break;
            }
        }

        // �� ã������
        if (!selectBug)
        {
            // ���Ӱ� �����ϰ� selectBug�� �Ҵ�
            selectBug = Instantiate(bugPrefabs[(int)type], transform);
            bugPools[(int)type].Add(selectBug);
        }

        return selectBug;
    }

    public GameObject GetHit(HIT_OBJ_TYPE type)
    {
        GameObject selectHit = null;

        // ������ Ǯ�� ��Ȱ��ȭ �� ���ӿ�����Ʈ ����
        foreach(GameObject hit in hitPools[(int)type])
        {
            if (!hit.activeSelf)
            {
                // �߰��ϸ� selectBug�� �Ҵ�
                selectHit = hit;
                selectHit.SetActive(true);
                break;
            }
        }

        // �� ã������
        if (!selectHit)
        {
            // ���Ӱ� �����ϰ� selectBug�� �Ҵ�
            selectHit = Instantiate(hitPrefabs[(int)type], transform);
            hitPools[(int)type].Add(selectHit);
        }

        return selectHit;
    }

}
