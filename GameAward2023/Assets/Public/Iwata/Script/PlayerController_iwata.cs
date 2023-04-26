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
                            // ����p�̃��C��p��
                            Ray ray = CursorController.GetCameraToRay(GM.JointStage.Find("JointCamera").gameObject);
                            RaycastHit hit;

                            //�J�[�\�����牜�Ɍ����ă��C���΂�
                            if (Physics.Raycast(ray, out hit))
                            {
                                // �K���N�^�ł͂Ȃ��Ȃ�X���[
                                if (hit.transform.tag != "Jank" && hit.transform.tag != "Player") return;

                                //�W�����N�R���g���[���[�ɍ��I�����Ă���W�����N��o�^
                                GM.JointStage.Find("Jank").GetComponent<JankController>().SelectJank = hit.collider.gameObject;

                                GameObject clone = Instantiate(hit.collider.gameObject);
                                GM.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT;
                                GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().PutJank(clone);
                            }
                        }

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
                        break;

                    case JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_JOINT:
                        break;
                }




                ////�\���{�^��
                //if (GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().m_rotateFrameCnt <= 0)
                //{
                //    int axisX = AxisInput.GetAxisRawRepeat("Horizontal_PadX");
                //    int axisY = AxisInput.GetAxisRawRepeat("Vertical_PadX");
                //    if(axisX != 0 || axisY != 0)
                //    {
                //        GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().InputAxisCore(axisX, axisY);
                //    }
                //}
                
                ////A�{�^��
                //if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                //{
                //    // ����p�̃��C��p��
                //    Ray ray = CursorController.GetCameraToRay(GM.JointStage.Find("JointCamera").gameObject);
                //    RaycastHit hit;
                    
                //    //�J�[�\�����牜�Ɍ����ă��C���΂�
                //    if (Physics.Raycast(ray, out hit))
                //    {
                //        // �K���N�^�ł͂Ȃ��Ȃ�X���[
                //        if (hit.transform.tag != "Jank" && hit.transform.tag != "Player") return;

                //        //�W�����N�R���g���[���[�ɍ��I�����Ă���W�����N��o�^
                //        GM.JointStage.Find("Jank").GetComponent<JankController>().SelectJank = hit.collider.gameObject;

                //        GameObject clone = Instantiate(hit.collider.gameObject);
                //        GM.JointStage.GetComponent<JointStageManager>().JSStatus = JointStageManager.eJointStageStatus.E_JOINTSTAGE_STATUS_PUT;
                //        GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().PutJank(clone);
                //    }
                //}

                ////B�{�^��
                //if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                //{
                //    if (!GM.JointStage.Find("Preview").gameObject.activeSelf)
                //    {
                //        GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().ReleaseCore();
                //    }
                //    else
                //    {
                //        GM.JointStage.Find("Jank").GetComponent<JankController>().ReturnJank();
                //        GM.JointStage.Find("Preview").gameObject.SetActive(false);
                //    }
                //}


                //////L�{�^��
                ////if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Q))
                ////{
                ////    if (Preview.activeSelf)
                ////    {
                ////        Jank.GetComponent<JankController>().SelectJank.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
                ////    }
                ////}

                //////R�{�^��
                ////if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.E))
                ////{
                ////    if (Preview.activeSelf)
                ////    {
                ////        Jank.GetComponent<JankController>().SelectJank.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
                ////    }
                ////}

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
