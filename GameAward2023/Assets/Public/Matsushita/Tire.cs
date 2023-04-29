using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tire : JunkBase
{
    [SerializeReference] float m_bounceRate;
    [SerializeField] private float explosionForce = 100f; // ������
    [SerializeField] private float explosionRadius = 10f; // �������a

    private void OnCollisionStay(Collision collision)   // �u�ԓI�ɑ傫�ȗ͂�������ƃ^�C�������Ă��܂���
    {
        //--- �ǂƏՓ˂������̏���
        if (collision.transform.tag == "Wall")
        {
            //--- ��������x�N�g�����v�Z
            Vector3 vToSelf = this.transform.position - collision.contacts[0].point;
            vToSelf.y = 0.0f;   // Y�������ւ̈ړ��𖳎�
            vToSelf = vToSelf.normalized * m_bounceRate;

            // ��������x�N�g����K�p
            this.GetComponent<Rigidbody>().AddForce(vToSelf, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// �p�����[�^�[�z�u
    /// </summary>
    /// <param name="paramList"></param>
    public override void SetParam(List<float> paramList)
    {
        m_bounceRate = paramList[0];
    }


    /// <summary>
    /// �B�R���N���[�g�ɂ��������Ƃ�
    /// </summary>
    public override void HitNailConcrete()
    {
        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, 3.0F);
            }
        }

    }
}
