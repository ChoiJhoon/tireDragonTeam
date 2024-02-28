using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (score < 3000)
        {
             randomMonsterIndex = 0;
            spawnInterval = 5;
        }
        else if (score < 5000)
        {
            randomMonsterIndex = Random.Range(0, 2);

            spawnInterval = 7;
        }
        else
        {
            randomMonsterIndex = Random.Range(0, 3);
            spawnInterval = 10 ;
        }



        // �������� ���Ϳ� ��ġ ����
        //int randomMonsterIndex = Random.Range(0, monsterPrefabs.Length);
        int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);

            
        // ���õ� ���Ϳ� ��ġ�� ���� ����
       Instantiate(monsterPrefabs[randomMonsterIndex], spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);

    }







}
