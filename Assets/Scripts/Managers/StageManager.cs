using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CESCO;
using UnityEngine.SceneManagement;

/*
 * ���������� �����ϴ� ���
 * 1. ���� ���� �����Ѵ�
    -> GameUI ���ܳ��� �Ŵ����� ���ܳ��� ��
    -> ����: ���ϴ� �� ���� ����
    -> ����: �������� �ϱ�� ����
 * 2. ����̳� ���� ������ �� ������Ʈ�� �����ؾ� ��
    -> �� ��ȯ�� �Ű澲�� �ʾƵ� ��
    -> ����: �������� �������� ��� ���� �� �ִ�.
    -> ����: ��������� �����ϱ�� �����.

    ���: 2������ �ϴ°� �� ������ ������ �´�.
*/

public class StageManager : MonoBehaviour
{
    public void NextStage()
    {
        /*
         * ���� ���������� �Ѿ�� �Ǹ� ���ھ �ʱ�ȭ �Ǿ�� �ϰ� �ð��� �ʱ�ȭ �Ǿ�� �Ѵ�.
         * ���� �����Ǿ��� ������ ��� ���� ���Ѿ� �ϸ� �� ���� Ÿ���� �̹���(�ӽ�),
         * ���� ���������� ���� �� ������ �������� �Ѵ�.
        */

        // ���� ���ھ� ���� �� �ʱ�ȭ
        //GameManager.instance.scoreManager.SaveScore();
        GameManager.instance.scoreManager.Init();

        // time �ʱ�ȭ �� Ÿ�̸� ����
        GameManager.instance.timeManager.TimeInit();
        GameManager.instance.timeManager.ProgressTimer();

        // �Ͻ����� ����(�ð� ��������)
        Time.timeScale = 1;

        // ���� ���� ����
        //foreach (GameObject bug in GameObject.FindGameObjectsWithTag("Bug"))
        //{
        //    Destroy(bug);
        //}
        GameManager.instance.spawnManager.RemoveBug();

        // ���� ���� ����
        // SpawnManager �̿�
        GameManager.instance.spawnManager.Next();

        // ��� ����
        // ImageManager(�ӽ�) �̿�
    }
}
