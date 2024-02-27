using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private float timer = 0f;
    private float scoreIncreaseInterval = 1f;

    private void Update()
    {
        //Ÿ�̸Ӹ� ������Ʈ�Ͽ� �ð��� ����
        timer += Time.deltaTime;

        // scoreIncreaseInterval���� ������ ������Ŵ
        if (timer >= scoreIncreaseInterval)
        {
            score++;
            UpdateScoreText();
            timer = 0f; // Ÿ�̸� �ʱ�ȭ
        }
    }

    public void IncreaseScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }
}
