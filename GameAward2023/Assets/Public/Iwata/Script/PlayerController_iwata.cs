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
                if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space))
                {
                    //--- �v���r���[���L���łȂ��ꍇ�̂ݑI���\
                    if (!Preview.activeSelf)
                    {
                        // ����p�̃��C��p��
                        Ray ray = CursorController.GetCameraToRay();
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log(hit.transform.tag);

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
                if (Input.GetKeyDown(KeyCode.JoystickButton1) || Input.GetKeyDown(KeyCode.Backspace))
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
                if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Return))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_ROT;
                }

                //L�{�^��
                if(Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Q))
                {
                    if (Preview.activeSelf)
                    {
                        Jank.GetComponent<JankController>().SelectJank.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
                    }
                }

                //R�{�^��
                if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.E))
                {
                    if (Preview.activeSelf)
                    {
                        Jank.GetComponent<JankController>().SelectJank.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
                    }
                }

                break;

            case GameStatusManager.eGameStatus.E_GAME_STATUS_ROT:
                if (Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Q))
                {
                    Core.GetComponent<RotationCore>().RotL();
                }
                if (Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.E))
                {
                    Core.GetComponent<RotationCore>().RotR();
                }
                if (Input.GetKey(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Backspace))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_JOINT;
                }
                if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Return))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_PLAY;
                }

                break;
            case GameStatusManager.eGameStatus.E_GAME_STATUS_PLAY:
                if (Input.GetKeyDown(KeyCode.JoystickButton2) || Input.GetKeyDown(KeyCode.Return))
                {
                    GSMana.GetComponent<GameStatusManager>().GameStatus = GameStatusManager.eGameStatus.E_GAME_STATUS_JOINT;
                }
                break;
        }
        
    }
}
