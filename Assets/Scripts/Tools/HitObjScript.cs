using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjScript : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(NoHit());
    }

    public void changeRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, radius);
        Debug.Log("changeRadius: " + radius);
    }

    IEnumerator NoHit()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}
