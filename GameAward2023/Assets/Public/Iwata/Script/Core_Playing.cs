using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Playing : ObjectBase
{
    [SerializeField] GameManager gm;
    [SerializeField] static Quaternion startRot;
    static bool start = false;
    bool m_RotL = false;
    bool m_RotR = false;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        start = true;
    }

    private void FixedUpdate()
    {
        switch(gm.GameStatus)
        {
            case GameManager.eGameStatus.E_GAME_STATUS_ROT:
                if(m_RotL)
                {
                    transform.Rotate(0.0f, 2.0f, 0.0f);
                    m_RotL = false;
                }
                if(m_RotR)
                {
                    transform.Rotate(0.0f, -2.0f, 0.0f);
                    m_RotR = false;
                }
                break;
            case GameManager.eGameStatus.E_GAME_STATUS_PLAY:
                // �q�I�u�W�F�N�g����Parent�N���X���p�������X�N���v�g���擾����
                JankBase_iwata[] scripts = GetComponentsInChildren<JankBase_iwata>();

                // �擾�����X�N���v�g��work�֐������s����
                foreach (JankBase_iwata script in scripts)
                {
                    script.work();
                }
                break;
        }
    }

    public void ResetPlayCore()
    {
        startRot = Quaternion.identity;
        start = false;
    }

    public bool StartFlag
    {
        set { start = value; }
        get { return start; }
    }

    public Quaternion StartRot
    {
        set { startRot = value; }
        get { return startRot; }
    }

    public bool RotL
    {
        set { m_RotL = value; }
    }

    public bool RotR
    {
        set { m_RotR = value; }
    }
}
