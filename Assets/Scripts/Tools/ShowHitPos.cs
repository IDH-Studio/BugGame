using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;

public class ShowHitPos : MonoBehaviour
{
    [SerializeField] private float xOffset;
    [SerializeField] private float yOffset;
    [SerializeField] private float animationDelay;
    [SerializeField] private Animator animator;

    private Vector3 baseRotation;
    private Vector3 basePosition;
    private bool isPlayAnimation = false;
    private bool isFollowingAnimation = false;

    private void Awake()
    {
        baseRotation = transform.localEulerAngles;
    }

    public void SetCurTool(GameObject tool)
    {
        // 사이즈 조절
        transform.localScale = tool.transform.localScale;
    }

    void Update()
    {
        if (isPlayAnimation) { return; }

        GameObject curToolObj = GameManager.instance.CurrentPlayer.CurrentSelectTool;
        Tool curTool = curToolObj.GetComponent<Tool>();
        if (isFollowingAnimation)
        {
            transform.position = curToolObj.transform.position;
        }
        else
        {
            transform.position = new Vector3(curToolObj.transform.position.x + (xOffset * curTool.Radius),
            curToolObj.transform.position.y + (yOffset * curTool.Radius), 0);
        }
    }

    public void StartHitAnimation()
    {
        isPlayAnimation = true;
        basePosition = transform.position;

        transform.position = GameManager.instance.CurrentPlayer.CurrentSelectTool.transform.position;
        transform.localEulerAngles = GameManager.instance.CurrentPlayer.CurrentSelectTool.transform.localEulerAngles;
    }

    public void StartFollowHitAnimation()
    {
        isFollowingAnimation = true;
        basePosition = transform.position;

        transform.localEulerAngles = GameManager.instance.CurrentPlayer.CurrentSelectTool.transform.localEulerAngles;
    }

    public void StopHitAnimation()
    {
        isPlayAnimation = false;
        isFollowingAnimation = false;
        transform.position = basePosition;
        transform.localEulerAngles = baseRotation;
    }

    public void PlayHitAnimation(TOOL tool = TOOL.NONE, float animationTime = 0.2f)
    {
        switch (tool)
        {
            case TOOL.INSECTICIDE:
                animator.gameObject.transform.position = new Vector2(transform.position.x - 1.2f, transform.position.y - 0.3f);
                PlayAnimation("Play_Insecticide", animationTime);
                break;
            case TOOL.ELECTRIC_FLY_SWATTER:
                PlayAnimation("Play_Electric_Fly_Swatter", animationTime);
                break;
            default:
                StartCoroutine(HitAnimation());
                break;
        }
    }

    void PlayAnimation(string animationName, float animationTime)
    {
        //animator.SetTrigger(animationName);
        animator.SetBool(animationName, true);
        StartCoroutine(StopAnimation(animationName, animationTime));
    }

    IEnumerator HitAnimation()
    {
        StartHitAnimation();
        yield return new WaitForSeconds(animationDelay);
        StopHitAnimation();
    }

    IEnumerator StopAnimation(string animationName, float animationTime)
    {
        yield return new WaitForSeconds(animationTime);
        animator.SetBool(animationName, false);
        animator.gameObject.transform.position = transform.position;
    }
}
