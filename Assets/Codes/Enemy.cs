using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    private float nextFireTime = 0f; // ���� �߻� �ð�
    public float maxMap = 10f;


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

    private void Update()
    {
    }

    private void FixedUpdate()
    {
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
            
            // �Ǵ� �ٸ� ó���� ������ �� �ֽ��ϴ�.
        }
    }
    void FireProjectile()
    {
        // �߻�ü�� �����ϰ� �������� �߻�
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.Euler(0, 0, 90f));
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

    // ���� �ı� �̺�Ʈ�� ������ ��������Ʈ �� �̺�Ʈ
    public delegate void MonsterDestroyedEventHandler(Enemy monster, int score);
    public static event MonsterDestroyedEventHandler MonsterDestroyedEvent;

    
}

