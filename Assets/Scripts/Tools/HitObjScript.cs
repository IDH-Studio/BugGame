using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjScript : MonoBehaviour
{
    [SerializeField] private Sprite baseImage;
    [SerializeField] private float animationDelay;
    [SerializeField] private float hitImageSize;

    public void Show(Vector3 movePos)
    {
        transform.position = movePos;
        StartCoroutine(HitAnimation());
    }

    public void ChangeImage()
    {
        GetComponent<SpriteRenderer>().sprite = baseImage;
    }

    public void ChangeImage(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public GameObject ChangeInfo(float radius)
    {
        radius *= hitImageSize;
        transform.localScale = new Vector3(radius, radius, radius);
        return gameObject;
    }
    
    public GameObject ChangeInfo(float radius, float animationDelay)
    {
        radius *= hitImageSize;
        transform.localScale = new Vector3(radius, radius, radius);
        this.animationDelay = animationDelay;
        return gameObject;
    }

    IEnumerator HitAnimation()
    {
        yield return new WaitForSecondsRealtime(animationDelay);
        gameObject.SetActive(false);
    }
}
