using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject[] monsterPrefabs; // �پ��� ���� ������ �迭
    public Transform[] spawnPoints; // ���Ͱ� ���� ��ġ �迭

     int score;
     int randomMonsterIndex;
    int spawnInterval = 3;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InvokeRepeating("SpawnRandomMonster", 0f, spawnInterval);       
    }
    private void Update()
    {
        
    }
    void SpawnRandomMonster()
    {
        score = Score.Instance.score;

        if (score < 1000)
        {
             randomMonsterIndex = 0;
            spawnInterval = 5;
        }
        else if (score < 3000)
        {
            randomMonsterIndex = Random.Range(0, 2);

            spawnInterval = 4;
        }
        else if(score < 7000 )
        {
            randomMonsterIndex = Random.Range(0, 3);
            spawnInterval = 3 ;
        }
        else if (score > 10000)
        {
            SceneManager.LoadScene("_2");
        }
       
        


        // �������� ���Ϳ� ��ġ ����
        //int randomMonsterIndex = Random.Range(0, monsterPrefabs.Length);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);

            
        // ���õ� ���Ϳ� ��ġ�� ���� ����
       Instantiate(monsterPrefabs[randomMonsterIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);

    }







}
