using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Enemy monster = other.GetComponent<Enemy>();
            if (monster != null)
            {
                monster.TakeDamage(1); // ���ط��� ���ϴ� ������ ����
                Destroy(gameObject); // �߻�ü �ı�
            }

            BossController Boss = other.GetComponent<BossController>();
            if (Boss != null)
            {
                Boss.TakeDamage(1);
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        
    
    }
}
