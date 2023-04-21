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

    [SerializeField] private StageManagerBase m_PlayStage;        //�v���C�p�̊�
    [SerializeField] private StageManagerBase m_JointStage;       //�g�ݗ��ėp�̊�

    [SerializeField] private eGameStatus m_GameStatus;  //�Q�[���̏��

    // Start is called before the first frame update
    void Start()
    {
        m_GameStatus = eGameStatus.E_GAME_STATUS_JOINT;     //�Q�[���̏�Ԃ̏�����
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public StageManagerBase PlayStage
    {
        get { return m_PlayStage; }
    }

    public StageManagerBase JointStage
    {
        get { return m_JointStage; }
    }


    public eGameStatus GameStatus
    {
        get { return m_GameStatus; }
        set { m_GameStatus = value; }
    }
}
