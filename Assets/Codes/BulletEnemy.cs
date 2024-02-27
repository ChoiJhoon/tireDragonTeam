using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(1); // ���ط��� ���ϴ� ������ ����
                Destroy(gameObject); // �߻�ü �ı�
            }
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }


}
