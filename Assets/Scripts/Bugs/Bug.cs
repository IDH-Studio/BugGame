using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using CESCO;

public class Bug : MonoBehaviour
{
    [Header("Bug Infos")]
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float backSpeed = 1.5f;
    [SerializeField] private float size = 1f;

    private bool isCollision = false;
    private bool isMoving = true;
    private SpriteRenderer sprite;

    ParticleSystem ps;
    protected Vector3 direction;
    protected Vector3 dirVec;
    protected float angle;
    protected float speed = 0f;
    [SerializeField] protected float cycle;
    [SerializeField] protected float height;
    [SerializeField] private BUG_TYPE bugType;
    [SerializeField] private float deathDelay = 0.5f;
    [SerializeField] private Animator anim;

    public BUG_TYPE BugType
    {
        get { return bugType; }
    }

    //protected virtual void Init()
    //{
    //    //// ī�޶� �ش��ϴ� ��ǥ ������
    //    //float yPos = Camera.main.orthographicSize;
    //    //float xPos = yPos * Camera.main.aspect;

    //    //// ī�޶� ������ ������ ��ǥ�� ���� ����
    //    //float randomX = Random.Range(-xPos, xPos);
    //    //float randomY = Random.Range(-yPos, yPos);
    //    //transform.position = new Vector2(randomX, randomY);
    //    transform.localScale = new Vector2(this.size, this.size);

    //    // �ʿ��� ���� �ʱ�ȭ
    //    isCollision = false;
    //    isMoving = true;
    //    ps = GetComponent<ParticleSystem>();
    //    direction = transform.position - target.position;
    //    dirVec = direction.normalized;
    //    angle = 0.0f;
    //}

    public void SetBug(Vector2 position)
    {
        transform.position = position;
        transform.localScale = new Vector2(this.size, this.size);

        direction = transform.position - GameManager.instance.CurrentTarget.transform.position;
        dirVec = direction.normalized;        
    }

    private void Awake()
    {
        // �ʿ��� ���� �ʱ�ȭ
        isCollision = false;
        isMoving = true;
        ps = GetComponent<ParticleSystem>();
        angle = 0.0f;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        if (!isMoving) { return; }

        direction = transform.position - GameManager.instance.CurrentTarget.transform.position;
        dirVec = direction.normalized;

        LookTarget();

        sprite.flipY = transform.position.x < GameManager.instance.CurrentTarget.transform.position.x ? true : false;

        // �浹 �˻� �� ������
        if (isCollision)
        {
            // Ÿ�ٿ� �ε����� ���
            Vector2 _target = transform.position + direction;
            transform.position = Vector2.MoveTowards(transform.position, _target, speed * backSpeed * Time.deltaTime);
        }
        else
        {
            // ������
            Move();
        }
    }

    protected virtual void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameManager.instance.CurrentTarget.transform.position, speed * Time.deltaTime);
    }

    void LookTarget()
    {
        angle = Mathf.Atan2(dirVec.y, dirVec.x) * Mathf.Rad2Deg;
        Quaternion angleAxis = Quaternion.AngleAxis(angle, Vector3.forward);
        Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 1);
        transform.rotation = rotation;
    }

    private void OnEnable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        isCollision = false;
        isMoving = true;
        anim.SetBool("Death", false);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Tool")
        {
            ps.Play();
            Debug.Log("I'm die :c");
            isMoving = false;
            GetComponent<BoxCollider2D>().enabled = false;
            anim.SetBool("Death", true);
            StartCoroutine(Death());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            //Debug.Log("JMT");
            isCollision = true;
            StartCoroutine(Collision());
        }
    }

    IEnumerator Collision()
    {
        yield return new WaitForSeconds(0.5f);
        isCollision = false;
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathDelay);
        //Destroy(this.gameObject);
        gameObject.SetActive(false);
    }
}
