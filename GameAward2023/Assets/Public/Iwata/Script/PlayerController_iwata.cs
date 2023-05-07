using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_iwata : MonoBehaviour
{
    [SerializeField] private GameManager GM;

    // Update is called once per frame
    void Update()
    {
        switch (GM.GameStatus)
        {
            case GameManager.eGameStatus.E_GAME_STATUS_JOINT:
                switch (GM.JointStage.GetComponent<JointStageManager>().JSStatus)
                {//�W�����N�X�e�[�W�̏�Ԃɍ��킹������������
                    case JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_SELECT:   
                        //�\���{�^��
                        if (GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().m_rotateFrameCnt <= 0)
                        {
                            int axisX = AxisInput.GetAxisRawRepeat("Horizontal_PadX");
                            int axisY = AxisInput.GetAxisRawRepeat("Vertical_PadX");
                            if (axisX != 0 || axisY != 0)
                            {
                                GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().InputAxisCore(axisX, axisY);
                            }
                        }

                        //A�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                        {
                            GameObject SelectJank = GM.JointStage.Find("JointCanvas").Find("Cursor").GetComponent<CursorController_araki>().SelectJank;
                            // �K���N�^�ł͂Ȃ��Ȃ�X���[
                            if (SelectJank.transform.tag != "Jank" && SelectJank.transform.tag != "Player") return;

                            //�W�����N�R���g���[���[�ɍ��I�����Ă���W�����N��o�^
                            GM.JointStage.Find("Jank").GetComponent<JankController>().SelectJank = SelectJank;

                            GameObject clone = Instantiate(SelectJank);
                            clone.GetComponent<JankBase_iwata>().Orizin = SelectJank;
                            GM.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT;
                            GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().PutJank(clone);

                        }

                        ////B�{�^��
                        if(PadInput.GetKeyDown(KeyCode.JoystickButton1))
                        {
                            //�J�[�\������Ray���o��
                            //�Ԃ�������̐e��Core����Ȃ���΃R���e�j���[
                            //���O��Core_Child��CoreCenter��������R���e�j���[
                            //�R�A�R���g���[���[�Ƀ����[�X�R�A������ĂԂ������̂������āA�I���W�����A�N�e�B�u�ɖ߂�
                        }

                        //if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                        //{
                        //    if (!GM.JointStage.Find("Preview").gameObject.activeSelf)
                        //    {
                                
                        //    }
                        //    else
                        //    {
                        //        GM.JointStage.Find("Jank").GetComponent<JankController>().ReturnJank();
                        //        GM.JointStage.Find("Preview").gameObject.SetActive(false);
                        //    }
                        //}
                        //------------------
                        //�펞�I�����Ă���ʂ�����킯����Ȃ�����Remove�ǂ����悤
                        //-------------------------------

                        //X�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton2))
                        {
                            GM.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_ROT;
                        }
                        break;


                    case JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT:
                        //�\���{�^��
                        if (GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().m_rotateFrameCnt <= 0)
                        {
                            int axisX = AxisInput.GetAxisRawRepeat("Horizontal_PadX");
                            int axisY = AxisInput.GetAxisRawRepeat("Vertical_PadX");
                            if (axisX != 0 || axisY != 0)
                            {
                                GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().InputAxisCore(axisX, axisY);
                            }
                        }

                        //A�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                        {
                            if(GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().JointCore())
                                GM.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_SELECT;
                        }

                        //B�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                        {
                            GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().CanselCore();
                            GM.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_SELECT;
                        }

                        //L�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton4))
                        {
                            GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().AttachJank.GetComponent<JankBase_iwata>().RotJank(1, 0, GM.JointStage.Find("Core"));
                        }

                        //L�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton5))
                        {
                            GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().AttachJank.GetComponent<JankBase_iwata>().RotJank(-1, 0, GM.JointStage.Find("Core"));
                        }
                        break;
                        
                }
                break;

            case GameManager.eGameStatus.E_GAME_STATUS_ROT:
                //B�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                {
                    GM.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_JOINT;
                }

                //X�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GM.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_PLAY;
                }

                //L�{�^��
                if(PadInput.GetKey(KeyCode.JoystickButton4))
                {
                    GM.PlayStage.Find("Core(Clone)").GetComponent<Core_Playing>().RotL = true;
                }

                //R�{�^��
                if (PadInput.GetKey(KeyCode.JoystickButton5))
                {
                    GM.PlayStage.Find("Core(Clone)").GetComponent<Core_Playing>().RotR = true;
                }
                break;
                
            case GameManager.eGameStatus.E_GAME_STATUS_PLAY:
                //X�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GM.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_ROT;
                }
                break;
        }
    }
}
