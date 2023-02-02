using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCheckScript : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(Hit());
    }

    public void changeRadius(float radius)
    {
        transform.localScale = new Vector3(radius, radius, radius);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bug")
        {
            GameManager.instance.scoreManager.PlusScore();
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(0.1f);
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}
