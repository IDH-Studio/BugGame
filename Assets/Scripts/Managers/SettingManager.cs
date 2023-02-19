using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSound = CESCO.SOUND;
using CESCO;

public class SettingManager : MonoBehaviour
{
    public Slider BGM;
    public Image BGMHandle;
    public Slider Effect;
    public Image EffectHandle;

    [SerializeField] private List<Sprite> speakerSprites;
    private float soundStandard = 0.25f;

    private void ShowSetting()
    {
        BGM.value = GameManager.instance.soundManager.BGM.volume;
        OnBGMSliderDown();
        Effect.value = GameManager.instance.soundManager.Effect.volume;
        OnEffectSliderDown();
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

    public void OnBGMSliderDown()
    {
        int spriteIndex;
        if (BGM.value == 0)
        {
            spriteIndex = 0;
        }
        else
        {
            spriteIndex = (int)(BGM.value / soundStandard) + 1;
            spriteIndex = spriteIndex > 4 ? 4 : spriteIndex;
        }

        BGMHandle.sprite = speakerSprites[spriteIndex];
    }

    public void OnEffectSliderDown()
    {
        int spriteIndex;
        if (Effect.value == 0)
        {
            spriteIndex = 0;
        }
        else
        {
            spriteIndex = (int)(Effect.value / soundStandard) + 1;
            spriteIndex = spriteIndex > 4 ? 4 : spriteIndex;
        }

        EffectHandle.sprite = speakerSprites[spriteIndex];
    }
}
