using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_iwata : MonoBehaviour
{
    //[SerializeField] private GameManager GameManager;
    int axisX = 0;
    int axisY = 0;

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.GameStatus)
        {
            case GameManager.eGameStatus.E_GAME_STATUS_JOINT:
                switch (GameManager.JointStage.GetComponent<JointStageManager>().JSStatus)
                {//�W�����N�X�e�[�W�̏�Ԃɍ��킹������������
                    case JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_SELECT:   
                        //���X�e�B�b�N
                        if (GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().m_rotateFrameCnt <= 0)
                        {
                            axisX = AxisInput.GetAxisRawRepeat("Horizontal_L");
                            axisY = AxisInput.GetAxisRawRepeat("Vertical_L");
                            if (axisX != 0 || axisY != 0)
                            {
                                GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().InputAxisCore(axisX, axisY);
                            }
                        }

                        //A�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                        {
                            GameObject SelectJank = GameManager.JointStage.Find("JointCanvas").Find("Cursor").GetComponent<CursorController_araki>().SelectJank;
                            // �K���N�^�ł͂Ȃ��Ȃ�X���[
                            if (SelectJank.transform.tag != "Jank" && SelectJank.transform.tag != "Player") return;

                            //�W�����N�R���g���[���[�ɍ��I�����Ă���W�����N��o�^
                            GameManager.JointStage.Find("Jank").GetComponent<JankController>().SelectJank = SelectJank;

                            GameObject clone = Instantiate(SelectJank);
                            clone.GetComponent<JankBase_iwata>().Orizin = SelectJank;
                            GameManager.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT;
                            GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().PutJank(clone);

                        }

                        //B�{�^��
                        if(PadInput.GetKeyDown(KeyCode.JoystickButton1))
                        {
                            GameObject jank = GameManager.JointStage.Find("JointCanvas").Find("Cursor").GetComponent<CursorController_araki>().GetAttachJunk();

                            if(jank)
                            {
                                jank.GetComponent<JankStatus>().DestroyChild();
                                jank.GetComponent<JankBase_iwata>().Orizin.SetActive(true);
                                Destroy(jank);
                            }
                        }

                        //X�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton2))
                        {
                            GameManager.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_ROT;
                        }
                        break;
                        
                    case JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT:
                        //���X�e�B�b�N
                        if (GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().m_rotateFrameCnt <= 0)
                        {
                            axisX = AxisInput.GetAxisRawRepeat("Horizontal_L");
                            axisY = AxisInput.GetAxisRawRepeat("Vertical_L");
                            if (axisX != 0 || axisY != 0)
                            {
                                GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().InputAxisCore(axisX, axisY);
                            }
                        }

                        //A�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                        {
                            if(GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().JointCore())
                                GameManager.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_SELECT;
                        }

                        //B�{�^��
                        if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                        {
                            GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().CanselCore();
                            GameManager.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_SELECT;
                        }

                        //�\���{�^��
                        axisX = AxisInput.GetAxisRawRepeat("Horizontal_PadX");
                        axisY = AxisInput.GetAxisRawRepeat("Vertical_PadX");
                        if(axisX != 0 || axisY != 0)
                        {
                            GameManager.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().AttachJank.GetComponent<JankBase_iwata>().RotJank(axisX, axisY, GameManager.JointStage.Find("Core"));
                        }
                        break;
                        
                }
                break;

            case GameManager.eGameStatus.E_GAME_STATUS_ROT:
                //B�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                {
                    GameManager.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_JOINT;
                }

                //X�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GameManager.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_PLAY;
                }

                //L�{�^��
                if(PadInput.GetKey(KeyCode.JoystickButton4))
                {
                    GameManager.PlayStage.Find("Core(Clone)").GetComponent<Core_Playing>().RotL = true;
                }

                //R�{�^��
                if (PadInput.GetKey(KeyCode.JoystickButton5))
                {
                    GameManager.PlayStage.Find("Core(Clone)").GetComponent<Core_Playing>().RotR = true;
                }
                break;
                
            case GameManager.eGameStatus.E_GAME_STATUS_PLAY:
                //X�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GameManager.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_ROT;
                }
                break;

            case GameManager.eGameStatus.E_GAME_STATUS_POUSE:
                if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                {
                    GameManager.GameStatus = GameManager.LastGameStatus;
                    GameManager.LastGameStatus = GameManager.eGameStatus.E_GAME_STATUS_POUSE;
                }
                break;
        }

        //�I�v�V�����{�^��
        if(PadInput.GetKeyDown(KeyCode.JoystickButton7))
        {
            if(GameManager.GameStatus != GameManager.eGameStatus.E_GAME_STATUS_POUSE)
            {
                GameManager.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_POUSE;
            }
            else
            {
                GameManager.GameStatus = GameManager.LastGameStatus;
                GameManager.LastGameStatus = GameManager.eGameStatus.E_GAME_STATUS_POUSE;
            }
        }
    }
}
