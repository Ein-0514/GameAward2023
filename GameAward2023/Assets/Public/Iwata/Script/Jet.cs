using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : JankBase_iwata
{
    [SerializeReference] float m_boostForceRate;        //���x
    [SerializeReference] float m_maxSpeed;          //�ō����x

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    /// <summary>
    /// �W�F�b�g�̓���
    /// </summary>
    public override void work()
    {
       //--- ���݂̃X�s�[�h���擾
       Rigidbody rigidbody = this.transform.GetComponent<Rigidbody>();
       float currentSpeed = rigidbody.velocity.magnitude;

       // �u�[�X�g�p�̃x�N�g�����v�Z
       Vector3 boostFoce = this.transform.forward.normalized * m_boostForceRate;

       // �ő呬�x�ȉ��̎��̂ݏ�������
       if (currentSpeed < m_maxSpeed) rigidbody.AddForce(boostFoce);	// �u�[�X�g����

       //�W�F�b�g�̉��̃G�t�F�N�g�\��
       EffectMane.PlayEffect(EffectType.E_EFFECT_KIND_JET, this.transform.position);
    }
}
