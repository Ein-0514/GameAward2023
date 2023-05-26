using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WorldSelect_Ito : MonoBehaviour
{
    public enum WorldNum
    {
        World3 = 1,     //��R�K�w
        World2,         //��Q�K�w
        World1,         //��P�K�w
        MAX
    }
    public enum StageNum
    {
        Stage1 = 1,     //�X�e�[�W�P
        Stage2,         //�X�e�[�W�Q
        Stage3,         //�X�e�[�W�R
        Stage4,         //�X�e�[�W�S
        Stage5,         //�X�e�[�W�T
        Stage6,         //�X�e�[�W�U
        Stage7,         //�X�e�[�W�V
        Stage8,         //�X�e�[�W�W
        Stage9,         //�X�e�[�W�X
        Stage10,        //�X�e�[�W�P�O
        Max,
    }

    //�L�����o�X�擾
    [SerializeReference] GameObject World1;
    [SerializeReference] GameObject World2;
    [SerializeReference] GameObject World3;
    [SerializeReference] GameObject W1Stage;
    [SerializeReference] GameObject W2Stage;
    [SerializeReference] GameObject W3Stage;

    private GameObject worldStageCtrlObj;

    public int unlockstage1Num = 0;�@//�X�e�[�W����p
    public int LoadSceneNum = 0;     //�X�e�[�W���[�h�p
    public static WorldNum worldNum; //���[���h�I���Ǘ��p
    public static StageNum stageNum; //�X�e�[�W�I���Ǘ��p
    private StageNum oldStageNum;    //�X�e�[�W�I���Ǘ��p

    private string Scene;            //�V�[���挈��(���O��Start�Ŏw��)

    private int currentSelectNum;
    private int oldSelectNum;
    private bool activeWorld;
    private bool activeStage;

    private RectList W3RectChange;
    private RectList W2RectChange;
    private RectList W1RectChange;

    // Start is called before the first frame update
    void Start()
    {
        //�R���g���[���I�u�W�F�N�g
        worldStageCtrlObj = GameObject.Find("WorldStageCtrlObj");
        W3RectChange = W3Stage.GetComponent<RectList>();
        W2RectChange = W2Stage.GetComponent<RectList>();
        W1RectChange = W1Stage.GetComponent<RectList>();

        //������
        worldNum = WorldNum.World3;
        stageNum = StageNum.Stage1;
        oldStageNum = StageNum.Max;
        unlockstage1Num = 1;
        activeWorld = true;
        activeStage = false;

        //�V�[���J�ڂ���X�e�[�W��
        Scene = "GameScene_v2.0";

        //�X�e�[�W�Z���N�g��image�̑傫���̏�����
        W3RectChange.SetSizeImage((int)stageNum - 1, 1.1f);
        W2RectChange.SetSizeImage((int)stageNum - 1, 1.1f);
        W1RectChange.SetSizeImage((int)stageNum - 1, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        //�|�[�Y��ʂ̏���
        if(activeWorld)
            WorldSelect();  
        
        //���肵����ʂ̕\��
        if(PadInput.GetKeyDown(KeyCode.JoystickButton0))
        {
            activeWorld = false;
            ActiveStageList();
        }

        //�X�e�[�W�I����ʕ\����̏���
        if(activeStage)
        {
            SelectStage();
        }
       
    }

    private void WorldSelect()
    {
        //�X�e�[�W�I���̓��͏���
        worldNum += AxisInput.GetAxisRawRepeat("Vertical_PadX");

        //�X�e�[�W�I�������[�v�����Ɏ~�߂�
        if (worldNum == WorldNum.World3 - 1)
        {
           AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
           worldNum = WorldNum.World3;
        }
        if (worldNum == WorldNum.MAX)
        {
            AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
            worldNum = WorldNum.World1;
        }

        switch (worldNum)
        {
            case WorldNum.World3:
                World1.SetActive(false);
                World2.SetActive(false);
                World3.SetActive(true);
                break;

            case WorldNum.World2:
                World1.SetActive(false);
                World2.SetActive(true);
                World3.SetActive(false);
                break;

            case WorldNum.World1:
                World1.SetActive(true);
                World2.SetActive(false);
                World3.SetActive(false);
                break;
        }
    }
    private void ActiveStageList()
    {
        switch(worldNum)
        {
            case WorldNum.World1:
                World1.SetActive(false);
                W1Stage.SetActive(true);
                break;

            case WorldNum.World2:
                World2.SetActive(false);
                W2Stage.SetActive(true);
                break;

            case WorldNum.World3:
                World3.SetActive(false);
                W3Stage.SetActive(true);
                break;
        }
        activeStage = true;
    }

    private void SelectStage()
    {
        //�ߋ��̑I��ԍ���ޔ�
        oldStageNum = stageNum;

        //���͏���
        stageNum += AxisInput.GetAxisRawRepeat("Horizontal_PadX");
        stageNum -= AxisInput.GetAxisRawRepeat("Vertical_PadX") * 5;


        if (stageNum == StageNum.Stage1 - 1)
        {
            stageNum = StageNum.Stage10;
        }
        if (stageNum == StageNum.Stage10 + 1)
        {
            stageNum = StageNum.Stage1;
        }
        if(stageNum <= StageNum.Stage1)
        {
            stageNum += 10;
        }
        if(stageNum >= StageNum.Max)
        {
            stageNum -= 10;
        }

        switch (stageNum)
        {
            case StageNum.Stage1:                

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 1)
                    {
                        LoadSceneNum = (int)StageNum.Stage1;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }         

                break;

            case StageNum.Stage2:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 2)
                    {
                        LoadSceneNum = (int)StageNum.Stage2;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage3:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 3)
                    {
                        LoadSceneNum = (int)StageNum.Stage3;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage4:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 4)
                    {
                        LoadSceneNum = (int)StageNum.Stage4;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage5:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 5)
                    {
                        LoadSceneNum = (int)StageNum.Stage5;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage6:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 6)
                    {
                        LoadSceneNum = (int)StageNum.Stage6;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage7:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 7)
                    {
                        LoadSceneNum = (int)StageNum.Stage7;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage8:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 8)
                    {
                        LoadSceneNum = (int)StageNum.Stage8;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage9:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 9)
                    {
                        LoadSceneNum = (int)StageNum.Stage9;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;

            case StageNum.Stage10:

                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    if (unlockstage1Num >= 10)
                    {
                        LoadSceneNum = (int)StageNum.Stage10;
                        SceneManager.LoadScene(Scene);
                    }
                    else
                    {
                        AudioManager.PlaySE(AudioManager.SEKind.E_SE_KIND_BEEP);
                    }
                }
                break;
        }

        if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
        {
            ReturnWorld();
            stageNum = StageNum.Stage1;
        }

        if (oldStageNum == stageNum) return;        
        switch (worldNum)
        {
            case WorldNum.World3:
                W3RectChange.SetSizeImage((int)stageNum - 1, 1.1f);
                W3RectChange.SetSizeImage((int)oldStageNum - 1, 1.0f);
                break;
            case WorldNum.World2:
                W2RectChange.SetSizeImage((int)stageNum - 1, 1.1f);
                W2RectChange.SetSizeImage((int)oldStageNum - 1, 1.0f);
                break;
            case WorldNum.World1:
                W1RectChange.SetSizeImage((int)stageNum - 1, 1.1f);
                W1RectChange.SetSizeImage((int)oldStageNum - 1, 1.0f);
                break;
        }
    }

    private void ReturnWorld()
    {
        switch (worldNum)
        {
            case WorldNum.World1:
                World1.SetActive(true);
                W1Stage.SetActive(false);
                break;

            case WorldNum.World2:
                World2.SetActive(true);
                W2Stage.SetActive(false);
                break;

            case WorldNum.World3:
                World3.SetActive(true);
                W3Stage.SetActive(false);
                break;
        }
        activeStage = false;
        activeWorld = true;
    }
}
