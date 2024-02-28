using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningSceneManager : MonoBehaviour
{
    public float delayInSeconds = 5f;  // �ڵ����� �Ѿ ������ �ð� (��)

    // Start is called before the first frame update
    void Start()
    {
        // ������ ������ �ð� �Ŀ� ���� ������ �ڵ����� �̵��ϴ� �ڷ�ƾ ����
        StartCoroutine(LoadNextSceneAfterDelay());
    }
    IEnumerator LoadNextSceneAfterDelay()
    {
        // ������ �ð���ŭ ��ٸ� �Ŀ�
        yield return new WaitForSeconds(delayInSeconds);

        // ���� ���� �ε����� �����ͼ� ���� ���� �����ϸ� �̵�

        SceneManager.LoadScene("_3");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
