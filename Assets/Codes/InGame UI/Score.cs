using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    public TextMeshProUGUI scoreText;
    public int score = 0;
    private float timer = 0f;
    private float scoreIncreaseInterval = 1f;

    private void Awake()
    {
        Instance = this;
        Enemy.MonsterDestroyedEvent += OnMonsterDestroyed;
    }

    private void Update()
    {
        //Ÿ�̸Ӹ� ������Ʈ�Ͽ� �ð��� ����
        timer += Time.deltaTime;

        // scoreIncreaseInterval���� ������ ������Ŵ
        if (timer >= scoreIncreaseInterval)
        {
            score += 10;
            UpdateScoreText();
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
       
    }
    void OnMonsterDestroyed(Enemy monster, int scoreValue)
    {
        // ���� �ı� �̺�Ʈ �߻� �� ȣ��Ǵ� �޼ҵ�
        IncreaseScore(scoreValue);
    }
    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreText();  
    }

    void UpdateScoreText()
    {
        scoreText.text =  score.ToString();
    }
}
