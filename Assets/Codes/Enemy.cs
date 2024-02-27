using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f; // ���� �̵� �ӵ�
    public Transform player; // �÷��̾��� Transform�� �����ϱ� ���� ����

    float maxMap = 10f;

    public enum diffEnemy
    {
        LineEnemy,
        FollowEnemy
    }

    public diffEnemy currentState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        switch(currentState){

            // ���������� ���� ��
            case diffEnemy.LineEnemy:
                MoveLeftToRight();
                break;
            // �÷��̾ ���� ���� ��
            case diffEnemy.FollowEnemy:
                FollowPlayer();
                break;

        }
    }

 

    void MoveLeftToRight()
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
        // �÷��̾� �������� ȸ��
        Vector2 direction = (player.position - transform.position).normalized;

        // ���͸� �÷��̾� ������ �̵�
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
