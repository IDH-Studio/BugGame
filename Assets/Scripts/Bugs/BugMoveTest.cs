using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoveTest : MonoBehaviour
{
    public Transform target;
    private Vector3 direction;
    private Vector3 dirVec;
    public float cycle = 1f;
    public float height = 20f;
    public float speed = 0.15f;

    public int[] moveDegs = { -45, -30, -15, 15, 30, 45 };
    public int moveDeg;
    public float minMoveDelay = 0.3f;
    public float maxMoveDelay = 1.3f;
    public float moveDelay = 0.8f;
    public float moveTime = 0.0f;
    private int isUp;

    private void Awake()
    {
        direction = transform.position - target.position;
        moveDeg = moveDegs[0];
        isUp = 0;
        Debug.Log(moveDegs.Length);
    }

    private void Update()
    {
        direction = transform.position - target.position;
        dirVec = direction.normalized;
        LookTarget();
        Debug.Log(moveDeg);
        float cos = -(height * Mathf.Cos(moveDeg * Mathf.Deg2Rad));
        float sin = -(height * Mathf.Sin(moveDeg * Mathf.Deg2Rad));

        Vector3 moveVec = new Vector3(direction.x, direction.y);

        if (isUp == 1)
        {
            moveVec = new Vector3(0, sin * dirVec.y);
        }
        else
        {
            moveVec = new Vector3(cos * dirVec.x, 0);
        }

        transform.position += moveVec * speed * Time.deltaTime;

        // 랜덤 이동 로직
        moveTime += Time.deltaTime;
        if (moveTime >= moveDelay)
        {
            moveDeg = moveDegs[Random.Range(0, moveDegs.Length)];
            moveDelay = Random.Range(minMoveDelay, maxMoveDelay);
            moveTime = 0.0f;
            isUp = Random.Range(0, 2);
        }
    }

    IEnumerator randomMove()
    {
        yield return new WaitForSeconds(moveDelay);

    }

    void LookTarget()
    {
        float angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1);
        transform.rotation = rotation;
    }

}
