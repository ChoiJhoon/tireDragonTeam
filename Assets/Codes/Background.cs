using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float scrollSpeed = 2.0f;   // ��� �̵� �ӵ�
    public float resetPosition = -40.0f; // ����� X���� �� ������ �۾����� �ٽ� Ư�� ��ġ�� ���ư�
    public float resetTarget = 40.0f;    // ����� ���ư� Ư�� ��ġ�� X��

    void FixedUpdate()
    {
        // ����� �̵���Ŵ
        transform.Translate(Vector3.left * scrollSpeed * Time.fixedDeltaTime);

        // ����� X���� Ư�� ������ �۾����� �ٽ� Ư�� ��ġ�� ���ư�
        if (transform.position.x <= resetPosition)
        {
            // ����� Ư�� ��ġ�� �̵���Ŵ
            Vector3 newPos = new Vector3(resetTarget, transform.position.y, transform.position.z);
            transform.position = newPos;

            Debug.Log(transform.position.x);
        }
    }
}
