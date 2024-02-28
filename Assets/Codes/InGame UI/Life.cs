using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Resources;

public class Life : MonoBehaviour
{
    private Player player;
    public TextMeshProUGUI healthText;

    void Start()
    {
        player = FindObjectOfType<Player>();
        // Life ��ũ��Ʈ�� ������ GameObject�� ����� Player ��ũ��Ʈ�� ã��

        if (Player.Instance.health == null)
        {
            Debug.LogError("Player ��ũ��Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    void Update()
    {
        // ���� PlayerHP ������ ����� �� ����
        healthText.text = " HP : " +  player.health.ToString();
    }
}
