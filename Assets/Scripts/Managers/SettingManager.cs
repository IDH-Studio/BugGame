using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSound = CESCO.SOUND;
using CESCO;

public class SettingManager : MonoBehaviour
{
    public Slider BGM;
    public Slider Effect;

    
    private void ShowSetting()
    {
        BGM.value = GameManager.instance.soundManager.BGM.volume;
        Effect.value = GameManager.instance.soundManager.Effect.volume;
    }

    public void Enable()
    {
        ShowSetting();
    }

    public void Disable() { GameManager.instance.screenManager.PrevScreen(); }

    public void Cancel()
    {
        // ���� ��� ��ư ���� �� ����
        // ���� ȭ�� �ݰ� ���� ȭ�� ���
        Disable();
    }

    public void Check()
    {
        // ���� Ȯ�� ��ư ���� �� ����
        // ������ ���� �°� ����
        Apply();
        Disable();
    }

    public void Apply()
    {
        GameManager.instance.soundManager.ChangeVolume(GameSound.BGM, BGM.value);
        GameManager.instance.soundManager.ChangeVolume(GameSound.EFFECT, Effect.value);

        ShowSetting();
    }
}
