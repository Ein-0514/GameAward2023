using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundObject
{
    public string name; // �T�E���h�̖��O
    public AudioClip clip; // �I�[�f�B�I�N���b�v

    public void Play(AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }
}
public class SoundManager : MonoBehaviour
{
    [SerializeField] private List<SoundObject> bgmList; // BGM�̃��X�g
    [SerializeField] private List<SoundObject> seList; // SE�̃��X�g

    // BGM���Đ�����֐�
    public void PlayBGM(string bgmName, AudioSource source)
    {
        // BGM���ƈ�v����T�E���h������
        SoundObject soundObject = bgmList.Find(s => s.name == bgmName);

        // BGM���Đ�
        if (soundObject != null)
        {
            soundObject.Play(source);
        }
        else
        {
            Debug.LogWarning("BGM not found: " + bgmName);
        }
    }

    // SE���Đ�����֐�
    public void PlaySE(string seName, AudioSource source)
    {
        // SE���ƈ�v����T�E���h������
        SoundObject soundObject = seList.Find(s => s.name == seName);

        // SE���Đ�
        if (soundObject != null)
        {
            soundObject.Play(source);
        }
        else
        {
            Debug.LogWarning("SE not found: " + seName);
        }
    }
}
