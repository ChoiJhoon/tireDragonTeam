using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    AudioManager audioManager; // ����� �Ŵ���

    public float attackCooldown = 3f; // �� ���� ���� ����
    public float movementSpeed = 2f;  // ���� ���� ������ �ӵ�
    public float bulletSpeed = 5f;     // �Ѿ� �̵� �ӵ�
    public float currentAngle = 90f; // ���� �߻� ����

    public float maxHealth = 100f;     // ������ �ִ� ü��
    public float currentHealth;        // ���� ü��


    float minY = -7f;          // �̵� ������ �ּ� Y ��ġ
     float maxY = 6f;           // �̵� ������ �ִ� Y ��ġ

    public GameObject bulletPrefab;    // �Ѿ� ������
    public GameObject bulletPrefab1;    // �Ѿ� ������
    public Transform bulletSpawnPoint; // �Ѿ� �߻� ��ġ
    public Transform bulletSpawnPointUp; // �Ѿ� �߻� ��ġ
    public Transform bulletSpawnPointDown; // �Ѿ� �߻� ��ġ

    //BossHPUI
    public GameObject HPbar;
    public Canvas canvas;
    public RectTransform hpbarTransform;

    public Slider healthSlider; //UI�� HP ��ġ �ǵ�� 



    void Start()
    {
        currentHealth = maxHealth; // ���� �� ���� ü���� �ִ� ü������ �ʱ�ȭ
        // �ڷ�ƾ ����
        StartCoroutine(BossAttackCoroutine());

        UpdateHealthUI();

        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found. Make sure AudioManager script is attached to an object in the scene.");
        }

        healthSlider = hpbarTransform.GetComponent<Slider>();
    }

    private void Update()
    {
        float newY = Mathf.PingPong(Time.time * movementSpeed, maxY - minY) + minY;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        UpdateHealthUI();
    }

    IEnumerator BossAttackCoroutine()
    {
        while (true)
        {
            // �����ϰ� ���� ����
             int randomPattern = Random.Range(1, 3); // ������ 1���� 3������ ���� �� �ϳ��� ���� ����
            // int randomPattern = 2;
            // ���õ� ���� ����
            AttackPattern1();
            if(currentHealth < 50)
            {
                switch(randomPattern) 
                {
                    case 1:
                        AttackPattern2();
                        break;
                    case 2:
                        AttackPattern3();
                        break;
                }

            }

            // ���� ���ݱ��� ���
            yield return new WaitForSeconds(attackCooldown);
        }
    }

    void AttackPattern1()
    {
        Debug.Log("���������� �Ѿ� ���� ����");    

        // �Ѿ� �߻� ��ġ
        Vector3 spawnPosition = bulletSpawnPoint.position;

        // �Ѿ� ���� �� ����
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        // �Ѿ��� �ʱ� �̵� ���� (���⿡���� �������� �߻�)
        Vector3 bulletDirection = Vector3.left;

        // �Ѿ˿� �ʱ� �̵� ����� �ӵ� ����
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

        // �Ѿ��� ȸ�� �ڷ�ƾ ����
        StartCoroutine(RotateAndShoot(bullet, currentAngle)); // ���⿡ ���ϴ� ������ ������ �����ϼ���
    }

    IEnumerator RotateAndShoot(GameObject bullet, float maxRotationAngle)
    {
        float elapsedTime = 0f;

        while (bullet != null)
        {
            // ���� ���� (PingPong �Լ��� ����Ͽ� ���� 0���� maxRotationAngle���� �ݺ�)
            float currentRotation = Mathf.PingPong(elapsedTime * 2f, maxRotationAngle);

            // ȸ�� ����
            bullet.transform.rotation = Quaternion.Euler(0, 0, currentRotation);

            // ������ ���� �߻� ���� ����
            Vector3 bulletDirection = Quaternion.Euler(0, 0, currentRotation) * Vector3.left;

            // �Ѿ˿� �̵� ����� �ӵ� ����
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;

            // ��� �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }



    void AttackPattern2()
    {
        Debug.Log("�簥���� �Ѿ� ���� ����");

        // �Ѿ� �߻� ��ġ
        Vector3 spawnPosition = bulletSpawnPointUp.position;

        // �Ѿ� ���� �� ����
        GameObject bulletUp = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);
        GameObject bulletDown = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);

        // �Ѿ��� �ʱ� �̵� ���� (���� ��, ���� �Ʒ��� �߻�)
        Vector3 bulletDirectionUp = (Vector3.left + Vector3.up).normalized; // ���� �� �밢��
        Vector3 bulletDirectionDown = (Vector3.left - Vector3.up).normalized; // ���� �Ʒ� �밢��

        // �Ѿ˿� �ʱ� �̵� ����� �ӵ� ����
        bulletUp.GetComponent<Rigidbody2D>().velocity = bulletDirectionUp * bulletSpeed;
        bulletDown.GetComponent<Rigidbody2D>().velocity = bulletDirectionDown * bulletSpeed;

        // �Ѿ��� ȸ�� �ڷ�ƾ ����
        StartCoroutine(RotateAndShoot(bulletUp, -15f)); // ���� ���� �߻��ϸ鼭 ȸ��
        StartCoroutine(RotateAndShoot(bulletDown, 15f)); // ���� �Ʒ��� �߻��ϸ鼭 ȸ��

   

    }

    void AttackPattern3()
    {
        Debug.Log("�簥���� �Ѿ� ���� ����");

        // �Ѿ� �߻� ��ġ
        Vector3 spawnPosition = bulletSpawnPointDown.position;

        // �Ѿ� ���� �� ����
        GameObject bulletUp = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);
        GameObject bulletDown = Instantiate(bulletPrefab1, spawnPosition, Quaternion.identity);

        // �Ѿ��� �ʱ� �̵� ���� (���� ��, ���� �Ʒ��� �߻�)
        Vector3 bulletDirectionUp = (Vector3.left + Vector3.up).normalized; // ���� �� �밢��
        Vector3 bulletDirectionDown = (Vector3.left - Vector3.up).normalized; // ���� �Ʒ� �밢��

        // �Ѿ˿� �ʱ� �̵� ����� �ӵ� ����
        bulletUp.GetComponent<Rigidbody2D>().velocity = bulletDirectionUp * bulletSpeed;
        bulletDown.GetComponent<Rigidbody2D>().velocity = bulletDirectionDown * bulletSpeed;

        // �Ѿ��� ȸ�� �ڷ�ƾ ����
        StartCoroutine(RotateAndShoot(bulletUp, -15f)); // ���� ���� �߻��ϸ鼭 ȸ��
        StartCoroutine(RotateAndShoot(bulletDown, 15f)); // ���� �Ʒ��� �߻��ϸ鼭 ȸ��

        
    }

    // ������ �������� �޴� �Լ�
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (audioManager != null)
        {
            audioManager.PlayEnemyHitSound();
        }


        // ü���� 0 ���Ϸ� �������� ������ óġ
        if (currentHealth <= 0)
        {
            currentHealth = 0; // ü���� ������ ���� �ʵ��� ����
            UpdateHealthUI();
            SceneManager.LoadScene("_4");
            Debug.Log("���� óġ!");
            // ���⿡�� �߰����� ó���� �� �� �ֽ��ϴ�.
        }
        else
        {
            Debug.Log("���� ���� ü��: " + currentHealth);
        }
    }

    //���� HP UI
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            if (maxHealth > 0)
            {
                healthSlider.normalizedValue = (float)currentHealth / (float)maxHealth; // ���� ü���� Slider ������ ����
                Debug.Log("HP �پ��");
            }
            else
            {
                Debug.LogError("Maxhealth�� 0���� Ŀ�� �մϴ�.");
            }
        }
    }
}
