using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_iwata : MonoBehaviour
{
    public GameObject Core;
    public GameObject Preview;
    public GameObject Jank;
    public GameObject GSMana;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(GSMana.GetComponent<GameStatusManager>().GameStatus)
        {
            case GameStatusManager.eGameStatus.E_GAME_STATUS_JOINT:
                //�\���{�^��
                if (Core.GetComponent<CoreSetting_iwata>().m_rotateFrameCnt <= 0)
                {
                    float axisX = AxisInput.GetAxisRawRepeat("Horizontal");
                    float axisY = (float)AxisInput.GetAxisRawRepeat("Vertical");
                    if (axisX != 0)
                        Core.GetComponent<CoreSetting_iwata>().ChangeFaceX(axisX);
                    else if (axisY != 0)
                        Core.GetComponent<CoreSetting_iwata>().ChangeFaceY(axisY);
                }

                //A�{�^��
                if (Input.GetKeyDown(KeyCode.JoystickButton0))
                {
                    //--- �v���r���[���L���łȂ��ꍇ�̂ݑI���\
                    if (!Preview.activeSelf)
                    {
                        // ����p�̃��C��p��
                        Ray ray = CursorController.GetCameraToRay();
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit))
                        {
                            // �K���N�^�ł͂Ȃ��Ȃ�X���[
                            if (hit.transform.tag != "Jank" && hit.transform.tag != "Player") return;

                            // �v���r���[��L����
                            Preview.SetActive(true);
                            Preview.transform.Find("PreviewBase").GetComponent<PreviewJank>().AttachPreviewJank(hit.collider.gameObject);

                            Jank.GetComponent<JankController>().SelectJank = hit.collider.gameObject;

                            //m_seController.PlaySe("Select");
                        }
                    }
                    else
                    {
                        bool AttachSuccess;
                        AttachSuccess = Core.GetComponent<CoreSetting_iwata>().AttachCore(Jank.GetComponent<JankController>().SelectJank);
                        
                        if(AttachSuccess)
                            Preview.SetActive(false);
                    }
                }

                //B�{�^��
                if (Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    if (!Preview.activeSelf)
                    {
                        Core.GetComponent<CoreSetting_iwata>().ReleaseCore();
                    }
                    else
                    {
                        Jank.GetComponent<JankController>().ReturnJank();
                        Preview.SetActive(false);
                    }
                }

                //X�{�^��
                if (Input.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_ROT;
                }
                break;

            case GameStatusManager.eGameStatus.E_GAME_STATUS_ROT:
                if (Input.GetKey(KeyCode.Joystick1Button4))
                {
                    Core.GetComponent<RotationCore>().RotL();
                }
                if (Input.GetKey(KeyCode.Joystick1Button5))
                {
                    Core.GetComponent<RotationCore>().RotR();
                }
                if (Input.GetKey(KeyCode.Joystick1Button1))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_JOINT;
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_PLAY;
                }

                break;
            case GameStatusManager.eGameStatus.E_GAME_STATUS_PLAY:
                if (Input.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_JOINT;
                }
                break;
        }
        
    }
}
