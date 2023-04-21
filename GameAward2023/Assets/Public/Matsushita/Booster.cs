using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : Metal
{

    [SerializeReference] float m_boostForceRate;
    [SerializeReference] float m_maxSpeed;

    // Start is called before the first frame update
    void Start()
    {

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
    /// <summary>
    /// �~�d��ɂ��������Ƃ�
    /// </summary>
    public override void HitCapacitor()
    {
    }
}
