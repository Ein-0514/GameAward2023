using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_matusita : JankBase_iwata
{
    [SerializeReference] float m_boostForceRate;
    [SerializeReference] float m_maxSpeed;

    /// <summary>
    /// �p�����[�^�[�z�u
    /// </summary>
    /// <param name="paramList"></param>
    public override void SetParam(List<float> paramList)
    {
        m_boostForceRate = paramList[0];
        m_maxSpeed = paramList[1];
    }

    public override List<float> GetParam()
    {
        List<float> list = new List<float>();

        list.Add(m_boostForceRate);
        list.Add(m_maxSpeed);

        return list;
    }

    public override void work()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GSManager.transform.GetComponent<GameStatusManager>().GameStatus == GameStatusManager.eGameStatus.E_GAME_STATUS_PLAY)
        //{
        //    if (!this.transform.parent.name.Contains("Core")) return;

        //    //--- ���݂̃X�s�[�h���擾
        //    Rigidbody rigidbody = this.transform.GetComponent<Rigidbody>();
        //    float currentSpeed = rigidbody.velocity.magnitude;

        //    // �u�[�X�g�p�̃x�N�g�����v�Z
        //    Vector3 boostFoce = this.transform.forward.normalized * m_boostForceRate;

        //    // �ő呬�x�ȉ��̎��̂ݏ�������
        //    if (currentSpeed < m_maxSpeed) rigidbody.AddForce(boostFoce);	// �u�[�X�g����

        //    EffectManager.GetComponent<EffectManager_iwata>().PlayEffect(EffectType.E_EFFECT_KIND_JET, this.transform.position);
        //}
    }
}
