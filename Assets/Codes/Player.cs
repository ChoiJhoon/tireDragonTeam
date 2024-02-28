using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public GameObject BulletPrefabs; // �߻�ü ������
    public Transform BulletSpawnPoint; // �߻�ü ���� ��ġ
    public Color hitColor = Color.gray;    // �¾��� ���� ����
    public float hitDuration = 0.5f;        // �¾��� �� ������ �����Ǵ� �ð�
    public AudioClip hitSound;             // �¾��� ���� ����� �Ҹ�

    public Vector2 inputVec;
    public float MoveSpeed = 1.0f;
    public int health = 3; // �÷��̾� �����
    public int bullteSpeed = 10;

    //boss HP UI
    public GameObject HPbar;
    public Canvas canvas;
    public RectTransform hpbarTransform;

    public Slider healthSlider; //UI�� HP ��ġ �ǵ�� 
    public int Maxhealth;//���Ͱ� �������ڸ����� HP�� �����ϴ� �Լ�

    AudioManager audioManager;   // ����� �Ŵ���
    Rigidbody2D rigid;
    private Color originalColor;           // ������ ����
    private SpriteRenderer spriteRenderer; // ��������Ʈ ������

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Instance = this;
        originalColor = spriteRenderer.color;
        // AudioManager ������Ʈ�� �������ų� �߰��մϴ�.
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found. Make sure AudioManager script is attached to an object in the scene.");
        }
    }
    public void Start()
    {
        Maxhealth = health;
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

    }

    private void FixedUpdate()
    {
        Vector2 nextVc = inputVec * MoveSpeed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVc);
        UpdateHealthUI();

    }
    void FireProjectile()
    {  // �߻�ü�� �����ϰ� ���������� �߻�
        if (BulletPrefabs != null && BulletSpawnPoint != null)
        {
            GameObject projectile = Instantiate(BulletPrefabs, BulletSpawnPoint.position, Quaternion.identity);

            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = Vector3.right * bullteSpeed; // �߻�ü�� �ʱ� �ӵ��� ���� (���������� 10�� �ӵ��� �߻�)
            if (audioManager != null)
            {
                audioManager.PlayShootSound();
            }

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
            SceneManager.LoadScene("_G");
            // �Ǵ� ���ӿ��� ó�� ���� ������ �� �ֽ��ϴ�.
        }
    }
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            if (Maxhealth > 0)
            {
                healthSlider.normalizedValue = (float)health / (float)Maxhealth; // ���� ü���� Slider ������ ����
                Debug.Log("HP �پ��");
            }
            else
            {
                Debug.LogError("Maxhealth�� 0���� Ŀ�� �մϴ�.");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // �ҷ��� �¾��� ��
        if (other.CompareTag("Monster"))
        {
            // �÷��̾��� ���� ����
            spriteRenderer.color = hitColor;

            // ���� �Ҹ� ���
            if (audioManager != null && hitSound != null)
            {
                audioManager.PlayHated();
            }

            // ���� �ð� �Ŀ� ������ ������� �ǵ���
            Invoke("ResetColor", hitDuration);
        }
        void ResetColor()
        {
            spriteRenderer.color = originalColor;
        }

    }
}