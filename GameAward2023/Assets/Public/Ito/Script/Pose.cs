using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pose : MonoBehaviour
{
    [SerializeReference] private GameObject optionScreen;
    [SerializeReference] private GameObject checkStage;
    [SerializeReference] private GameObject checkTitle;

    private GameObject Stage;
    private GameObject Stage2;
    private GameObject Option;
    private GameObject Option2;
    private GameObject Title;
    private GameObject Title2;

    private YNChoice ynStageChoice;
    private YNChoice ynTitleChoice;

    private int poseNum;            //�|�[�Y��ʑI�𔻒�p

    static public bool activePose;        //�|�[�Y��ʂ̕\������p
    static public bool activetoStage;     //�X�e�[�W�m�F��ʕ\������
    static public bool activePoseOption;  //�I�v�V�����\������
    static public bool activetoTitle;     //�^�C�g���m�F��ʕ\������

    private void Start()
    {
        Stage   = GameObject.Find("StageSelect");
        Stage2  = GameObject.Find("StageSelect2");
        Option  = GameObject.Find("Option");
        Option2 = GameObject.Find("Option2");
        Title   = GameObject.Find("BackTitle");
        Title2  = GameObject.Find("BackTitle2");
        ynStageChoice = checkStage.GetComponent<YNChoice>();

        optionScreen.SetActive(false);
        checkStage.SetActive(false);
        checkTitle.SetActive(false);

        activePose = false;
        poseNum = 0;
    }

    private void Update()
    {        
        if (activePose)
        {
            //�I���̍X�V
            poseNum -= AxisInput.GetAxisRawRepeat("Vertical_PadX");

            //�I���̃��[�v
            poseNum += 3;
            poseNum %= 3;

            //����UI�̍X�V
            ChangePoseActive();

            //�I���̌���
            if(PadInput.GetKeyDown(KeyCode.JoystickButton0))
            {
                ActiveCanvas();
                activePose = false;
            }         
        }
        
        //�\������Ă����ʂ̂ݓ���(�ǂ��炩�ɓ���)
        //�|�[�Y��ʂ݂̂Ȃ�S�ăX���[
        if(activetoStage)
        {
            ynStageChoice.Update();

            //��߂�
            if(!activetoStage)
            {
                checkStage.SetActive(false);
                activePose = true;
            }
        }
        if(activetoTitle)
        {
            ynTitleChoice.Update();

            if (!activetoTitle)
            {
                checkTitle.SetActive(false);
                activePose = true;
            }
        }
    }

    private void ActiveCanvas()
    {
        switch (poseNum)
        {
            case 0:
                activetoStage = true;

                //�X�e�[�W�I���ւ̊m�F���
                checkStage.SetActive(true);
                optionScreen.SetActive(false);
                checkTitle.SetActive(false);
                break;

            case 1:
                activePoseOption = true;

                //�I�v�V������ʕ\��
                optionScreen.SetActive(true);
                checkTitle.SetActive(false);
                checkTitle.SetActive(false);
                break;

            case 2:
                activetoTitle = true;

                //�^�C�g���ւ̊m�F���
                checkTitle.SetActive(true);
                optionScreen.SetActive(false);
                checkStage.SetActive(false);
                break;
        }
    }

    private void ChangePoseActive()
    {
        switch (poseNum)
        {
            case 0:
                //�X�e�[�W�������Ă���
                Stage.SetActive(false);
                Stage2.SetActive(true);
                Option.SetActive(true);
                Option2.SetActive(false);
                Title.SetActive(true);
                Title2.SetActive(false);
                break;

            case 1:
                //�I�v�V�����������Ă���
                Stage.SetActive(true);
                Stage2.SetActive(false);
                Option.SetActive(false);
                Option2.SetActive(true);
                Title.SetActive(true);
                Title2.SetActive(false);
                break;

            case 2:
                //�^�C�g���������Ă���
                Stage.SetActive(true);
                Stage2.SetActive(false);
                Option.SetActive(true);
                Option2.SetActive(false);
                Title.SetActive(false);
                Title2.SetActive(true);
                break;
        }        
    }

    /// <summary>
    /// ���̊֐����Ăяo��Script��SirializeRefarence��poseCanvas���擾������
    /// �I���I�t�؂�ւ���̂Ɠ����ɌĂяo������
    /// </summary>
    static public void ActivePoseSCreen()
    {
        //�Q�[���V�[���̒�~
        Time.timeScale = 0;
        //�\������̍X�V
        activePose = true;
    }
    static public void NonActivePoseSCreen()
    {
        //�Q�[���V�[���̍Đ�
        Time.timeScale = 1;
        //�\������̍X�V
        activePose = false;
    }
}
