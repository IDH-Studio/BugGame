using CESCO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] spawners;
    private Coroutine spawnCoroutine = null;

    [SerializeField] private float minDelay;
    [SerializeField] private float maxDelay;

    private float min;
    private float max;
    private BUG_TYPE bugType;

    public BUG_TYPE BugType { get { return bugType; } }

    private void Awake()
    {
        min = minDelay;
        max = maxDelay;
        bugType = BUG_TYPE.NONE;
    }

    public void Init()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
        min = minDelay;
        max = maxDelay;
        spawnCoroutine = null;
        bugType = BUG_TYPE.NONE;
    }

    public void RemoveBug()
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<SpawnBug>().Init();
        }
    }

    public void Next()
    {
        //minDelay -= Random.Range(0.01f, 0.1f);
        if (min < max)
        {
            max -= Random.Range(0.1f, max / 10); // 재량껏 바꾸기
        }
        ChangeBugs();
    }

    private void ChangeBugs()
    {
        bugType = (BUG_TYPE)Random.Range(0, System.Enum.GetNames(typeof(BUG_TYPE)).Length - 1);
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<SpawnBug>().ChangeBug(bugType);
        }
    }

    public void StartSpawnBug()
    {
        ChangeBugs();
        int index = Random.Range(0, spawners.Length);
        float delay = Random.Range(1f, 3f);
        spawnCoroutine = StartCoroutine(BugSpawn(delay, index));
    }

    public void StopSpawnBug()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
        }
    }

    public void SpawnBugs()
    {
        int index = Random.Range(0, spawners.Length);
        float delay = Random.Range(min, max);
        spawnCoroutine = StartCoroutine(BugSpawn(delay, index));
    }

    IEnumerator BugSpawn(float delay, int index)
    {
        yield return new WaitForSeconds(delay);
        spawners[index].GetComponent<SpawnBug>().Spawn(spawners[index].transform.position);
        SpawnBugs();
    }
}
