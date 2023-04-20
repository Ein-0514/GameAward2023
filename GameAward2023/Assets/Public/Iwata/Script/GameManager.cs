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

    [SerializeField] private GameObject m_PlayStage;        //�v���C�p�̊�
    [SerializeField] private GameObject m_JointStage;       //�g�ݗ��ėp�̊�
    [SerializeField] private Dictionary<string, GameObject> m_Objects = new Dictionary<string, GameObject>();   //�����s���Ă�����ň�����I�u�W�F�N�g��o�^

    [SerializeField] private eGameStatus m_GameStatus;  //�Q�[���̏��

    // Start is called before the first frame update
    void Start()
    {
        m_GameStatus = eGameStatus.E_GAME_STATUS_JOINT;     //�Q�[���̏�Ԃ̏�����

        //�A�N�e�B�u�̊��̃I�u�W�F�N�g�̏���o�^
        foreach (Transform childTransform in m_JointStage.transform)
        {
            GameObject childObject = childTransform.gameObject;
            string objectName = childObject.name;

            // Dictionary�ɓo�^����
            m_Objects[objectName] = childObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject PlayStage
    {
        get { return m_PlayStage; }
    }

    public GameObject JointStage
    {
        get { return m_JointStage; }
    }

    public Dictionary<string, GameObject> Objects
    {
        get { return m_Objects; }
    }

    public eGameStatus GameStatus
    {
        get { return m_GameStatus; }
        set { m_GameStatus = value; }
    }
}
