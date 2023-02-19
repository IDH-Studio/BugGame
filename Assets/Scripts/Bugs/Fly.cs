using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : Bug
{
    public float minMoveDelay;
    public float maxMoveDelay;
    public int minMoveDeg;
    public int maxMoveDeg;

    private int moveDeg = 10;
    private float moveDelay = 0.5f;
    private float moveTime = 0.0f;
    private int moveType = 1;

    protected override void Move()
    {
        float cos;
        float sin;
        Vector3 moveVec;

        if (moveType == 1)
        {
            sin = Mathf.Abs(height * Mathf.Sin(moveDeg * Mathf.Deg2Rad));
            moveVec = new Vector3(-direction.x, sin * dirVec.y);
        }
        else if (moveType == 2)
        {
            cos = Mathf.Abs(height * Mathf.Cos(moveDeg * Mathf.Deg2Rad));
            moveVec = new Vector3(cos * dirVec.x, -direction.y);
        }
        else
        {
            moveVec = new Vector3(-direction.x, -direction.y);
        }

        transform.position += moveVec * speed * Time.deltaTime;

        LookTarget();

        moveTime += Time.deltaTime;
        if (moveTime >= moveDelay)
        {
            moveDeg = Random.Range(minMoveDeg, maxMoveDeg);
            moveDelay = Random.Range(minMoveDelay, maxMoveDelay);
            moveTime = 0.0f;
            moveType = Random.Range(1, 4);
        }
    }
}
