using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBGM : MonoBehaviour
{
    public AudioClip F1Title;
    public AudioClip F2Stage1;
    public AudioClip F3CraftScene;
    public AudioClip nextBGM;

    public float fadeInDuration = 1.0f; // �t�F�[�h����
    public float fadeOutDuration = 1.0f; // �t�F�[�h����

    private AudioSource audioSource;

    public bool isFadeIn = false;
    public bool isFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //����BGM
        audioSource.clip = F1Title;

        //�������ʂ��[���ɂ���
        audioSource.volume = 0.1f;

        //���y�̍Đ�
        StartCoroutine(PlayBGM());

        
    }

    // Update is called once per frame
    void Update()
    {
        //�I�����ꂽ�L�[�ɂ���ċȂ�ύX����
        if (Input.GetKeyDown(KeyCode.F1))
        {
            nextBGM = F1Title;
            isFadeOut = true;
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            nextBGM = F2Stage1;
            isFadeOut = true;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            nextBGM = F3CraftScene;
            isFadeOut = true;
        }

        //�t�F�[�h�C��
        if (isFadeIn == true)
        {
            float volume = audioSource.volume;
            audioSource.clip = nextBGM;      
            audioSource.Play();

            volume += Time.deltaTime / fadeInDuration;
            audioSource.volume = volume;

            if(volume >= 1.0f)
            {
                isFadeIn = false;
            }
        }

        //�t�F�[�h�A�E�g
        if (isFadeOut == true)
        {
            float volume = audioSource.volume;

            volume -= Time.deltaTime / fadeOutDuration;
            audioSource.volume = volume;

            if(volume <= 0.0f)
            {
                isFadeOut = false;
                isFadeIn = true;
                audioSource.Stop();
            }
        }
    }

    // BGM���Đ�����
    public IEnumerator PlayBGM()
    {     
        float volume = 0.0f;

        audioSource.Play();
        while (volume < 1.0f)
        {
            volume += Time.deltaTime / fadeInDuration;
            audioSource.volume = volume;
            yield return null;
        }
    }
}
