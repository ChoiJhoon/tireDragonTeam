using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;

    public float moveSpeed = 5f; // ���� �̵� �ӵ�
     Transform player; // �÷��̾��� Transform�� �����ϱ� ���� ����
    public int health = 3; // ���� �����
    public GameObject projectilePrefab; // �߻�ü ������
    public Transform projectileSpawnPoint; // �߻�ü ���� ��ġ
    public float fireInterval = 2f; // �߻� ����
    public  int m1Score = 50;

    public SpriteRenderer renderer; //SpriteRenderer ����� ���� �ҷ���
    private Color originalColor;//ù �÷��� �����ϱ� ���� ����
    public Color hitColor = Color.red;//hit ���� �� ������ �÷�
    public float hitDuration = 0.1f;//�ð� ����

    private float nextFireTime = 0f; // ���� �߻� �ð�
    public float maxMap = 10f;

    //���� HPUI ����
    public GameObject HPbar;
    public Canvas canvas;
    public RectTransform hpbarTransform;
    public float height = -1f;

    public Slider healthSlider; //UI�� HP ��ġ �ǵ�� 
    public int Maxhealth;//���Ͱ� �������ڸ����� HP�� �����ϴ� �Լ�
    

    private void Awake()
    {
    }
    public enum diffEnemy
    {
        LineEnemy,
        FollowEnemy,
        fireEnemy
    }

    public diffEnemy currentState;

    public void Start()
    {
        renderer = GetComponent<SpriteRenderer>();//SpriteRenderer ����� ����  
        originalColor = renderer.color; //ù �÷� ����

        Maxhealth = health;



        UpdateHealthUI();

        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();

        if (canvas != null)
        {
            hpbarTransform = Instantiate(HPbar, canvas.transform).GetComponent<RectTransform>(); // HPBar ��ġ ����
        }
        else
        {
            Debug.LogError("Canvas�� ã�� �� �����ϴ�!");
        }

        healthSlider = hpbarTransform.GetComponent<Slider>();
    }

    private void Update()
    {
        if (hpbarTransform != null)
        {
            Vector3 hpbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
            hpbarTransform.position = hpbarPos;
        }
       
    }

    private void FixedUpdate()
    {

        UpdateHealthUI();
        switch (currentState)
        {

            // ���������� ���� ��
            case diffEnemy.LineEnemy:
                MoveRightToLeft();
                break;

            // �÷��̾ ���� ���� ��
            case diffEnemy.FollowEnemy:
                FollowPlayer();
                break;
            // ���ڸ����� �߻縸 �ϴ� ��
            case diffEnemy.fireEnemy:
                if (Time.time > nextFireTime)
                {
                    FireProjectile();
                    nextFireTime = Time.time + fireInterval;
                }
               
                break;

        }
    }



    void MoveRightToLeft()
    {
        // ���͸� ���������� �̵�
        transform.Translate(Vector3.left * moveSpeed * Time.fixedDeltaTime);

        // ���� ���Ͱ� ȭ�� �������� ����� �ʱ� ��ġ�� �̵�
        if (transform.position.x < -maxMap) // �̵��� �ִ� x ��ǥ�� �����Ͻʽÿ�.
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        // ���͸� �ʱ� ��ġ�� �ǵ�����
        transform.position = new Vector3(maxMap, transform.position.y, transform.position.z);
    }
    void FollowPlayer()
    {
        player = Player.Instance.transform;
        // �÷��̾� �������� ȸ��
        Vector2 direction = (player.position - transform.position).normalized;

        if(player.transform.position.x > gameObject.transform.position.x) 
        {
            MoveRightToLeft();
        }
        else
        {
            // ���͸� �÷��̾� ������ �̵�
            transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
        }

      
    }
    public void TakeDamage(int damage)
    {
        // ���� ����� ����
        health -= damage;

        // ������� 0 ���ϸ� ���͸� �ı��ϰų� �ٸ� ó�� ����
        if (health <= 0)
        {
            OnMonsterDestroyed();
            Destroy(gameObject);
            if (hpbarTransform != null)
            {
                Destroy(hpbarTransform.gameObject);
            }

            // �Ǵ� �ٸ� ó���� ������ �� �ֽ��ϴ�.
        }
    }
    void FireProjectile()
    {
        // �߻�ü�� �����ϰ� �������� �߻�
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            Vector3 updateVec = projectileSpawnPoint.position + new Vector3(-1, 0, 0);
            GameObject projectile = Instantiate(projectilePrefab, updateVec , Quaternion.Euler(0, 0, 0));
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = new Vector2(-10f, 0f); // �߻�ü�� �ʱ� �ӵ��� ���� (�������� 10�� �ӵ��� �߻�)
        }
        else
        {
            Debug.LogError("�߻�ü ������ �Ǵ� �߻�ü ���� ��ġ�� �������� �ʾҽ��ϴ�. Inspector���� �����ϼ���.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Invoke("DeadPlayer", 1f);

        if (other.CompareTag("Player"))
        {
            StartCoroutine(HitAnima(hitColor, hitDuration));

            // HP�ٸ� �����մϴ�.
            if (hpbarTransform != null)
            {
                Destroy(hpbarTransform.gameObject);
            }
        }
    }
    void DeadPlayer(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1); // ���ط��� ���ϴ� ������ ����
                Destroy(gameObject);
                
            }            
        }
    }
    void OnMonsterDestroyed()
    {
        // ���� �ı� �̺�Ʈ�� �߻���Ŵ
        if (MonsterDestroyedEvent != null)
        {
            MonsterDestroyedEvent(this, m1Score); // ���� �ν��Ͻ��� ������ �Ķ���ͷ� ����
        }
    }

    IEnumerator HitAnima(Color color, float duration)
    {
        renderer.color = color;
        yield return new WaitForSeconds(duration);
        renderer.color = originalColor;
    }
        
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            if (Maxhealth > 0)
            {
                healthSlider.normalizedValue= (float)health / (float)Maxhealth; // ���� ü���� Slider ������ ����
                Debug.Log("HP �پ��");
            }
            else
            {
                Debug.LogError("Maxhealth�� 0���� Ŀ�� �մϴ�.");
            }
        }
    }

    // ���� �ı� �̺�Ʈ�� ������ ��������Ʈ �� �̺�Ʈ
    public delegate void MonsterDestroyedEventHandler(Enemy monster, int score);
    public static event MonsterDestroyedEventHandler MonsterDestroyedEvent;

    
}

