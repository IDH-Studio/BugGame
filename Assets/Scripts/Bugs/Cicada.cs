using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Cicada : Bug
{
    public float minMoveDelay = 1f;
    public float maxMoveDelay = 3f;
    public int minMoveDeg = 0;
    public int maxMoveDeg = 359;

    private int moveDeg;
    private float moveDelay = 0;
    private float moveTime = 0.0f;
    private int moveType = 1;

    private void Start()
    {
        sprite.flipX = true;
        moveDeg = Random.Range(minMoveDeg, maxMoveDeg);
    }

    protected override void Move()
    {
        float cos = Mathf.Cos(moveDeg * Mathf.Deg2Rad);
        float sin = Mathf.Sin(moveDeg * Mathf.Deg2Rad);
        Vector3 moveVec = moveType == 0 ? -new Vector3(cos, sin) : new Vector3(cos, sin);

        moveVec -= dirVec;

        transform.position += moveVec * speed * Time.deltaTime;

        LookTarget(moveVec);

        moveTime += Time.deltaTime;
        if (moveTime >= moveDelay)
        {
            moveDeg = Random.Range(minMoveDeg, maxMoveDeg);
            moveDelay = Random.Range(minMoveDelay, maxMoveDelay);
            moveTime = 0.0f;
            moveType = Random.Range(0, 1);
        }
    }

    protected void LookTarget(Vector3 move)
    {
        angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1);
        transform.rotation = rotation;
    }
}
