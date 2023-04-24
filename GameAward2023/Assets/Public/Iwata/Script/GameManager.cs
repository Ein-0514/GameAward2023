using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�Q�[���̏�Ԃ̃t���O
    public enum eGameStatus
    {
        E_GAME_STATUS_START = 0,
        E_GAME_STATUS_JOINT,
        E_GAME_STATUS_ROT,
        E_GAME_STATUS_PLAY,
        E_GAME_STATUS_POUSE,
        E_GAME_STATUS_END,

        E_GAME_STATUS_MAX
    }

    [SerializeField] private Transform m_PlayStage;        //�v���C�p�̊�
    [SerializeField] private Transform m_JointStage;       //�g�ݗ��ėp�̊�

    [SerializeField] private eGameStatus m_GameStatus;  //�Q�[���̏��
    [SerializeField] private eGameStatus m_lastGameStatus;  //�Q�[���̏��

    // Start is called before the first frame update
    void Start()
    {
        m_GameStatus = eGameStatus.E_GAME_STATUS_JOINT;     //�Q�[���̏�Ԃ̏�����
        m_lastGameStatus = m_GameStatus;                    //�O�t���[���̏�Ԃ�ێ�
    }

    // Update is called once per frame
    void Update()
    {
        if(m_GameStatus != m_lastGameStatus)
        {
            switch(m_lastGameStatus)
            {
                case eGameStatus.E_GAME_STATUS_JOINT:
                    switch (m_GameStatus)
                    {
                        case eGameStatus.E_GAME_STATUS_ROT:
                            m_JointStage.gameObject.SetActive(false);
                            m_PlayStage.gameObject.SetActive(true);
                            m_JointStage.Find("Jank").GetComponent<JankController>().SelectJank.GetComponent<JankStatus>().UndoSize();
                            Vector3 startpos = m_PlayStage.Find("Start").transform.position;
                            GameObject core = Instantiate(m_JointStage.Find("Core").gameObject, startpos, Quaternion.identity);
                            core.transform.parent = m_PlayStage.transform;
                            Destroy(core.GetComponent<CoreSetting_iwata>());
                            core.AddComponent<Core_Playing>();
                            break;
                    }
                    break;
                case eGameStatus.E_GAME_STATUS_ROT:
                    switch(m_GameStatus)
                    {
                        case eGameStatus.E_GAME_STATUS_JOINT:
                            m_JointStage.gameObject.SetActive(true);
                            m_PlayStage.gameObject.SetActive(false);
                            Destroy(m_PlayStage.Find("Core(Clone)").gameObject);
                            break;
                        case eGameStatus.E_GAME_STATUS_PLAY:
                            foreach(Transform child in m_PlayStage.Find("Core(Clone)").transform)
                            {
                                child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                            }
                            
                            break;
                    }
                    break;
                case eGameStatus.E_GAME_STATUS_PLAY:
                    switch (m_GameStatus)
                    {
                        case eGameStatus.E_GAME_STATUS_ROT:
                            Destroy(m_PlayStage.Find("Core(Clone)").gameObject);
                            Vector3 startpos = m_PlayStage.Find("Start").transform.position;
                            GameObject core = Instantiate(m_JointStage.Find("Core").gameObject, startpos, Quaternion.identity);
                            core.transform.parent = m_PlayStage.transform;
                            Destroy(core.GetComponent<CoreSetting_iwata>());
                            core.AddComponent<Core_Playing>();
                            break;

                    }
                    break;                                                                                                                                                                                                                                                                                           
            }


            m_lastGameStatus = m_GameStatus;
        }
    }

    public Transform PlayStage
    {
        get { return m_PlayStage; }
    }

    public Transform JointStage
    {
        get { return m_JointStage; }
    }


    public eGameStatus GameStatus
    {
        get { return m_GameStatus; }
        set { m_GameStatus = value; }
    }
}
