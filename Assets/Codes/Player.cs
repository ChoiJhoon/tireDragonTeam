using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    public GameObject BulletPrefabs; // �߻�ü ������
    public Transform BulletSpawnPoint; // �߻�ü ���� ��ġ

    public Vector2 inputVec;
    public float MoveSpeed = 1.0f;
    public int health = 3; // �÷��̾� �����


    Rigidbody2D rigid;
    SpriteRenderer spriter;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter= GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        // �����̽��ٸ� ������ �߻�ü �߻�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireProjectile();
        }

        spriter.flipX = inputVec.x <0 ;
    }

    private void FixedUpdate()
    {
        Vector2 nextVc = inputVec * MoveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVc);
    }
    void FireProjectile()
    {  // �߻�ü�� �����ϰ� ���������� �߻�
        if (BulletPrefabs != null && BulletSpawnPoint != null)
        {
            GameObject projectile = Instantiate(BulletPrefabs, BulletSpawnPoint.position, Quaternion.Euler(0, 0, 90f));

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = Vector3.right * 10f; // �߻�ü�� �ʱ� �ӵ��� ���� (���������� 10�� �ӵ��� �߻�)
        }
        else
        {
            Debug.LogError("�߻�ü ������ �Ǵ� �߻�ü ���� ��ġ�� �������� �ʾҽ��ϴ�. Inspector���� �����ϼ���.");
        }
    }

    public void TakeDamage(int damage)
    {
        // �÷��̾� ����� ����
        health -= damage;

        // ������� 0 ���ϸ� �÷��̾ �ı��ϰų� �ٸ� ó�� ����
        if (health <= 0)
        {
            Destroy(gameObject);
            // �Ǵ� ���ӿ��� ó�� ���� ������ �� �ֽ��ϴ�.
        }
    }
   

}