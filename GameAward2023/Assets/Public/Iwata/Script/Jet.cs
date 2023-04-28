using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : JankBase
{
    [SerializeReference] float m_boostForceRate;
    [SerializeReference] float m_maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void work()
    {
                    //--- ���݂̃X�s�[�h���擾
       Rigidbody rigidbody = this.transform.GetComponent<Rigidbody>();
       float currentSpeed = rigidbody.velocity.magnitude;

       // �u�[�X�g�p�̃x�N�g�����v�Z
       Vector3 boostFoce = this.transform.forward.normalized * m_boostForceRate;

       // �ő呬�x�ȉ��̎��̂ݏ�������
       if (currentSpeed < m_maxSpeed) rigidbody.AddForce(boostFoce);	// �u�[�X�g����

       EffectMane.PlayEffect(EffectType.E_EFFECT_KIND_JET, this.transform.position);
        

    }
}
