using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float attackCooldown = 3f; // �� ���� ���� ����
    public float movementSpeed = 2f;  // ���� ���� ������ �ӵ�
    public float bulletSpeed = 5f;     // �Ѿ� �̵� �ӵ�
    public float bulletAmplitude = 2f; // ���� ��� ���� (����)


    float minY = -7f;          // �̵� ������ �ּ� Y ��ġ
     float maxY = 6f;           // �̵� ������ �ִ� Y ��ġ

    public GameObject bulletPrefab;    // �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ� �߻� ��ġ

    void Start()
    {
        // �ڷ�ƾ ����
        StartCoroutine(BossAttackCoroutine());
    }

    private void Update()
    {
        float newY = Mathf.PingPong(Time.time * movementSpeed, maxY - minY) + minY;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }


    IEnumerator BossAttackCoroutine()
    {
        while (true)
        {
            // �����ϰ� ���� ����
            // int randomPattern = Random.Range(1, 4); // ������ 1���� 3������ ���� �� �ϳ��� ���� ����
            int randomPattern = 1;
            // ���õ� ���� ����
            switch (randomPattern)
            {
                case 1:
                    AttackPattern1();
                    break;

                case 2:
                    AttackPattern2();
                    break;

                case 3:
                    AttackPattern3();
                    break;
            }

            // ���� ���ݱ��� ���
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    void AttackPattern1()
    {

      
    }
        void AttackPattern2()
    {
        Debug.Log("���� ���� 2 ����");
        // ���Ͽ� ���� ���� ����
    }

    void AttackPattern3()
    {
        Debug.Log("���� ���� 3 ����");
        // ���Ͽ� ���� ���� ����
    }
}
