using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip shootSound; // �߻� �Ҹ� AudioClip
    public AudioClip enemyHitSound;   // ���� �¾��� �� �Ҹ� AudioClip
    public AudioClip hateSound; // ���� �Ҹ�

    private AudioSource audioSource;

    void Start()
    {
        // AudioSource ������Ʈ�� �������ų� �߰��մϴ�.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // �߻� �Ҹ��� ����ϴ� �Լ�
    public void PlayShootSound()
    {
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }
    // ���� �¾��� �� �Ҹ��� ����ϴ� �Լ�
    public void PlayEnemyHitSound()
    {
        if (enemyHitSound != null)
        {
            audioSource.PlayOneShot(enemyHitSound);
        }
    } 
    public void PlayHated()
    {
        if (enemyHitSound != null)
        {
            audioSource.PlayOneShot(hateSound);
        }
    }
}
