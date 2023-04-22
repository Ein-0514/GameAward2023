using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_iwata : MonoBehaviour
{
    //public GameObject Core;
    //public GameObject CoreClone;
    //public GameObject Preview;
    //public GameObject Jank;
    //public GameObject GSMana;

    [SerializeField] private GameManager GM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Dictionary<string, GameObject> jointobjects = GM.JointStage.Objects;
        //Dictionary<string, GameObject> playobjects = GM.PlayStage.Objects;

        switch (GM.GameStatus)
        {
            case GameManager.eGameStatus.E_GAME_STATUS_JOINT:
                //�\���{�^��
                if (GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().m_rotateFrameCnt <= 0)
                {
                    float axisX = AxisInput.GetAxisRawRepeat("Horizontal_PadX");
                    float axisY = (float)AxisInput.GetAxisRawRepeat("Vertical_PadX");
                    if (axisX != 0)
                        GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().ChangeFaceX(axisX);
                    else if (axisY != 0)
                        GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().ChangeFaceY(axisY);
                }

                //float stick_RH = PadInput.GetAxisRaw("Horizontal_R");
                //float stick_RV = PadInput.GetAxisRaw("Vertical_R");
                //if(stick_RH != 0 || stick_RV != 0)
                //{
                //    objects["JointCanvas"].transform.Find("Cursor").GetComponent<CursorController>().MoveCursor(stick_RH, stick_RV);
                //}

                //A�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton0))
                {
                    //--- �v���r���[���L���łȂ��ꍇ�̂ݑI���\
                    if (!GM.JointStage.Find("Preview").gameObject.activeSelf)
                    {
                        // ����p�̃��C��p��
                        Ray ray = CursorController.GetCameraToRay(GM.JointStage.Find("JointCamera").gameObject);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit))
                        {
                            Debug.Log(hit.transform.tag);

                            // �K���N�^�ł͂Ȃ��Ȃ�X���[
                            if (hit.transform.tag != "Jank" && hit.transform.tag != "Player") return;

                            // �v���r���[��L����
                            GM.JointStage.Find("Preview").gameObject.SetActive(true);
                            //objects["Preview"].transform.Find("PreviewBase").GetComponent<PreviewJank>().AttachPreviewJank(hit.collider.gameObject);

                            GM.JointStage.Find("Jank").GetComponent<JankController>().SelectJank = hit.collider.gameObject;

                            //m_seController.PlaySe("Select");
                        }
                    }
                    else
                    {
                        bool AttachSuccess;
                        AttachSuccess = GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().AttachCore(GM.JointStage.Find("Jank").GetComponent<JankController>().SelectJank);

                        if (AttachSuccess)
                            GM.JointStage.Find("Preview").gameObject.SetActive(false);
                    }
                }

                //B�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton1))
                {
                    if (!GM.JointStage.Find("Preview").gameObject.activeSelf)
                    {
                        GM.JointStage.Find("Core").GetComponent<CoreSetting_iwata>().ReleaseCore();
                    }
                    else
                    {
                        GM.JointStage.Find("Jank").GetComponent<JankController>().ReturnJank();
                        GM.JointStage.Find("Preview").gameObject.SetActive(false);
                    }
                }

                //X�{�^��
                if (PadInput.GetKeyDown(KeyCode.JoystickButton2))
                {
                    GM.GameStatus = GameManager.eGameStatus.E_GAME_STATUS_ROT;
                }

                ////L�{�^��
                //if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Q))
                //{
                //    if (Preview.activeSelf)
                //    {
                //        Jank.GetComponent<JankController>().SelectJank.transform.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
                //    }
                //}

                ////R�{�^��
                //if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetKeyDown(KeyCode.E))
                //{
                //    if (Preview.activeSelf)
                //    {
                //        Jank.GetComponent<JankController>().SelectJank.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));
                //    }
                //}

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
