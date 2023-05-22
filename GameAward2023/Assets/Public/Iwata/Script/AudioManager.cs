using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum BGMKind
    {
        E_BGM_KIND_TITLE = 0,   //  0
        E_BGM_KIND_STAGE1,      //  1
        E_BGM_KIND_MAX
    }
    public enum SEKind
    {
        E_SE_KIND_SELECT = 0,   //  2
        E_SE_KIND_HOGE,         //  3
        E_SE_KIND_KEYMOVE,      //  4 "�^�C�g���A�I�v�V������ʁA�X�e�[�W�I���ł̃L�[���͂̍ۂɍĐ�����B"
        E_SE_KIND_KETTEI,       //  5 "�^�C�g���A�I�v�V������ʁA�X�e�[�W�I���ł̑���̌��������ۂɍĐ��B"
        E_SE_KIND_CANCEL,       //  6 "�^�C�g���A�I�v�V������ʁA�X�e�[�W�I���ł̑����߂������ۂɍĐ��B"
        E_SE_KIND_BEEP,         //  7 "�^�C�g���A�I�v�V������ʁA�X�e�[�W�I���ł̑�������肵���ۂɑI��s�̋��A�K�w��I�����Ă����ꍇ�Đ��B"
        E_SE_KIND_GAMESTART,    //  8 "�^�C�g����ʂł͂��߂���A���������I��������ԂŌ���{�^�����������ƍĐ��B"
        E_SE_KIND_ASSEMBLE,     //  9 "�K���N�^���R�A�̖ʂɑg�ݗ��Ă����̌��ʉ��B"
        E_SE_KIND_PREV_KEYMOVE, // 10 "�v���r���[��ʂŌ����A�ʂ�I������ۂ̃L�[�ړ����ɍĐ�����B"
        E_SE_KIND_NOISE,        // 11 "�g�ݗ��ĉ�ʁB�K���N�^�ɃJ�[�\�������킹���ۂɏo��v���r���[��ʂ̍����Ɠ����ɍĐ��B"
        E_SE_KIND_MONITORON,    // 12 "�g�ݗ��ĉ�ʁB�K���N�^�ɃJ�[�\�������킹���ۂɏo��v���r���[��ʂ̍����\����A�K���N�^�̃v���r���[��\������ۂɍĐ��B"
        E_SE_KIND_MONITOROFF,   // 13 "�g�ݗ��ĉ�ʁB�K���N�^�ɃJ�[�\�������킹���ۂɏo��v���r���[��ʂ̍����\����A�K���N�^�̃v���r���[��\������ۂɍĐ��B"
        E_SE_KIND_OPTION,       // 14 "���j���[���J���֘A�����{�^���������ƍĐ������B�^�C�g���ł̃I�v�V�����������Ă��Đ�"
        E_SE_KIND_WIND,         // 15 "�X�e�[�W���I�u�W�F�N�g�̑����@���甭�����镗�̌��ʉ��B�����@������ꍇ�͍Đ���������"
        E_SE_KIND_FIRE,         // 16 "�X�e�[�W���I�u�W�F�N�g�̔R���Ă���h�����ʂ̉��̌��ʉ��B"
        E_SE_KIND_HARETU,       // 17 "�X�v�����O�K���N�^�̃^�C�����B�R���N���[�g�ɐڐG����Ɣj�􂵁A���̍ۍĐ������r�d"
        E_SE_KIND_EXPLOTION,    // 18 "����K���N�^�̃h�����ʂ��_���[�W�I�u�W�F�N�g�̔R���Ă���h�����ʂɐG���Ɣ������A���̍ۂɍĐ�"
        E_SE_KIND_MAX
    }

    public static Dictionary<BGMKind, string> BgmTypeNames = new Dictionary<BGMKind, string>()
    {
        { BGMKind.E_BGM_KIND_TITLE,     "TITLE" },
        { BGMKind.E_BGM_KIND_STAGE1,    "STAGE1" },
        { BGMKind.E_BGM_KIND_MAX,       "MAX" },
    };

    public static Dictionary<SEKind, string> SeTypeNames = new Dictionary<SEKind, string>()
    {
        { SEKind.E_SE_KIND_SELECT,          "SELECT" },
        { SEKind.E_SE_KIND_HOGE,            "HOGE" },
        { SEKind.E_SE_KIND_KEYMOVE,         "KEYMOVE" },
        { SEKind.E_SE_KIND_KETTEI,          "KETTEI" },
        { SEKind.E_SE_KIND_CANCEL,          "CANCEL" },
        { SEKind.E_SE_KIND_BEEP,            "BEEP" },
        { SEKind.E_SE_KIND_GAMESTART,       "GAMESTART" },
        { SEKind.E_SE_KIND_ASSEMBLE,        "ASSEMBLE" },
        { SEKind.E_SE_KIND_PREV_KEYMOVE,    "PREV_KEYMOVE" },
        { SEKind.E_SE_KIND_NOISE,           "NOISE" },
        { SEKind.E_SE_KIND_MONITORON,       "MONITORON" },
        { SEKind.E_SE_KIND_MONITOROFF,      "MONITOROFF" },
        { SEKind.E_SE_KIND_OPTION,          "OPTION" },
        { SEKind.E_SE_KIND_WIND,            "WIND" },
        { SEKind.E_SE_KIND_FIRE,            "FIRE" },
        { SEKind.E_SE_KIND_HARETU,          "HARETU" },
        { SEKind.E_SE_KIND_EXPLOTION,       "EXPLOTION" },
        { SEKind.E_SE_KIND_MAX,             "MAX" },
        
    };

    public static string BGMTypeToString(BGMKind BGMType)
    {
        if (BgmTypeNames.ContainsKey(BGMType))
        {
            return BgmTypeNames[BGMType];
        }
        else
        {
            return BGMType.ToString();
        }
    }

    public static string SETypeToString(SEKind SeType)
    {
        if (SeTypeNames.ContainsKey(SeType))
        {
            return SeTypeNames[SeType];
        }
        else
        {
            return SeType.ToString();
        }
    }

    public static AudioManager instance;

    [SerializeField] public List<AudioClip> bgms = new List<AudioClip>();
    [SerializeField] public List<AudioClip> ses = new List<AudioClip>();
    [SerializeField] private static float bgmVolume = 1.0f;
    [SerializeField] private static float seVolume = 1.0f;
    private static AudioSource bgmSource;
    private static AudioSource[] seSource = new AudioSource[(int)SEKind.E_SE_KIND_MAX];

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;
        bgmSource.volume = bgmVolume;

        for (int i = 0; i < ses.Count; i++)
        {
            if (ses[i] == null) continue;
            seSource[i] = gameObject.AddComponent<AudioSource>();
            seSource[i].volume = seVolume;
            seSource[i].clip = ses[i];
        }
    }

    public static void PlayBGM(BGMKind index)
    {
        bgmSource.clip = instance.bgms[(int)index];
        bgmSource.Play();
    }

    public static void StopBGM()
    {
        bgmSource.Stop();
    }

    public static void PlaySE(SEKind index)
    {
        Debug.Log((int)index + ":" + seSource[(int)index].name);
        seSource[(int)index].Play();
    }

    public static float BGMvolume
    {
        get { return bgmVolume; }
        set { bgmVolume = value; bgmSource.volume = bgmVolume; }
    }

    public static float SEvolume
    {
        get { return seVolume; }
        set { seVolume = value; for (int i = 0; i < instance.ses.Count; i++) { seSource[i].volume = seVolume; } }
    }
}