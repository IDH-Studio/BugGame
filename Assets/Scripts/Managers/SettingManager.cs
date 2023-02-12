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
        // 설정 취소 버튼 누를 시 동작
        // 설정 화면 닫고 이전 화면 출력
        Disable();
    }

    public void Check()
    {
        // 설정 확인 버튼 누를 시 동작
        // 설정한 값에 맞게 조절
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
        int spriteIndex = (int)(BGM.value / soundStandard);
        spriteIndex = spriteIndex > 3 ? 3 : spriteIndex;

        BGMHandle.sprite = speakerSprites[spriteIndex];
    }

    public void OnEffectSliderDown()
    {
        int spriteIndex = (int)(Effect.value / soundStandard);
        spriteIndex = spriteIndex > 3 ? 3 : spriteIndex;

        EffectHandle.sprite = speakerSprites[spriteIndex];
    }
}
