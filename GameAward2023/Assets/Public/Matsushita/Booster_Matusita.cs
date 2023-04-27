using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_Matusita : Metal
{

    [SerializeReference] float m_boostForceRate;
    [SerializeReference] float m_maxSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// �p�����[�^�[�z�u
    /// </summary>
    /// <param name="paramList"></param>
    public override void SetParam(List<float> paramList)
    {
        m_boostForceRate = paramList[0];
        m_maxSpeed = paramList[1];
    }

    // Update is called once per frame
    void Update()
    {
        //--- ���݂̃X�s�[�h���擾
        Rigidbody rigidbody = this.transform.GetComponent<Rigidbody>();
        float currentSpeed = rigidbody.velocity.magnitude;

        // �u�[�X�g�p�̃x�N�g�����v�Z
        Vector3 boostFoce = this.transform.forward.normalized * m_boostForceRate;

        // �ő呬�x�ȉ��̎��̂ݏ�������
        if (currentSpeed < m_maxSpeed) rigidbody.AddForce(boostFoce);   // �u�[�X�g����
    }
    /// <summary>
    /// �B�R���N���[�g�ɂ��������Ƃ�
    /// </summary>
    public override void HitNailConcrete()
    {

    }
}
