using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using CESCO;

public class HitCheckScript : MonoBehaviour
{
    [SerializeField] private float damage;
    public float Damage { get { return damage; } }

    private float hitDelay = 0.1f;
    private bool hitDisappear = true;
    private float attackTime = 0;
    private float attackDelay;
    private TOOL toolType;

    public void Show(Vector3 movePos)
    {
        transform.position = movePos;
        StartCoroutine(Hit());
    }

    public void ChangeImage(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public GameObject ChangeInfo(float radius, float damage, TOOL toolType, float hitDelay = 0.1f, float attackDelay = 0, bool hitDisappear = true)
    {
        transform.localScale = new Vector3(radius, radius, radius);
        this.damage = damage;
        this.hitDelay = hitDelay;
        this.hitDisappear = hitDisappear;
        this.attackDelay = attackDelay;
        this.toolType = toolType;
        return gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bug" && hitDisappear)
        {
            //GameManager.instance.scoreManager.PlusScore();
            collider.gameObject.GetComponent<Bug>().HitDamage(damage, toolType, hitDelay);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Bug" || hitDisappear) return;

        if (attackTime <= attackDelay)
        {
            // 아직 공격하면 안됨
            attackTime += Time.deltaTime;
        }
        else
        {
            // 공격
            other.gameObject.GetComponent<Bug>().HitDamage(damage, toolType, hitDelay);
            attackTime = 0;
        }
    }

    IEnumerator Hit()
    {
        yield return new WaitForSeconds(hitDelay);
        gameObject.SetActive(false);
    }
}
