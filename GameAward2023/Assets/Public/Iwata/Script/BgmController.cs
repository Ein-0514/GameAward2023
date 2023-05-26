using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmController : MonoBehaviour
{
    public BgmController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] AudioManager.BGMKind bgm;

    private void Start()
    {
        AudioManager.PlayBGM(bgm);
    }

    private void OnEnable()
    {
        // �V�[���J�ڎ��̃R�[���o�b�N��o�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �V�[���J�ڎ��̃R�[���o�b�N������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �V�[���J�ڎ��̏����������ɋL�q
        if (scene.name == "")
        {
            // ����̃I�u�W�F�N�g�������z�������Ȃ�
        }
    }
}