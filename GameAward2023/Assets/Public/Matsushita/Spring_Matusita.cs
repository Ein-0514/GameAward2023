using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring_Matusita : JankBase_iwata
{
    public float springForce = 300.0f; // ���˕Ԃ�̋���
    public float minDot = 0.9f; // �ʏՓ˔����臒l
    public float extraForce = 100.0f; //�ǉ��̗�

    private BoxCollider boxCollider;

    public override void work()
    {

    }

    public override List<float> GetParam()
    {
        List<float> list = new List<float>();

        return list;
    }

    public override void SetParam(List<float> paramList)
    {

    }

    void Start()
    {
        // Box Collider���擾
        boxCollider = GetComponent<BoxCollider>();
        // �����蔻���L����
        boxCollider.isTrigger = false;
    }

    //---------------------------------------------------
    /// <summary>
    /// �p�����[�^�[�z�u
    /// </summary>
    /// <param name="paramList"></param>
    //public override void SetParam(List<float> paramList)
    //{
    //}
    //-----------------------------

    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂����I�u�W�F�N�g��Box Collider�����ꍇ�A�e�ʂł̓����蔻����擾
        if (collision.gameObject.GetComponent<BoxCollider>())
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                // �Փ˓_�̍��W���擾
                Vector3 point = contact.point;

                // �����̂̃��[�J�����W�n�ɕϊ�
                Vector3 localPoint = transform.InverseTransformPoint(point);

                // �e�ʂ̒��S�_����̋������v�Z���A�ŏ��l�����ʂ𓖂������ʂƂ��Ĉ���
                float[] distances = new float[6];
                distances[0] = Mathf.Abs(localPoint.z - boxCollider.center.z + boxCollider.size.z / 2f);
                distances[1] = Mathf.Abs(localPoint.z - boxCollider.center.z - boxCollider.size.z / 2f);
                distances[2] = Mathf.Abs(localPoint.y - boxCollider.center.y + boxCollider.size.y / 2f);
                distances[3] = Mathf.Abs(localPoint.y - boxCollider.center.y - boxCollider.size.y / 2f);
                distances[4] = Mathf.Abs(localPoint.x - boxCollider.center.x + boxCollider.size.x / 2f);
                distances[5] = Mathf.Abs(localPoint.x - boxCollider.center.x - boxCollider.size.x / 2f);

                int minIndex = 0;
                float minDistance = distances[0];
                for (int i = 1; i < 6; i++)
                {
                    if (distances[i] < minDistance)
                    {
                        minDistance = distances[i];
                        minIndex = i;
                    }
                }
                if (minIndex == 2 || minIndex == 5)
                {
                    Debug.Log("Hit face: " + (minIndex + 1));

                    Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

                    // �Ԃ��������肪Rigidbody�������Ă���ꍇ�A���˕Ԃ�
                    if (rb != null)
                    {
                        Vector3 direction = collision.contacts[0].point - transform.position;
                        float dot = Vector3.Dot(direction.normalized, collision.contacts[0].normal);

                        // �ʏՓ˔�����s��
                        if (-dot > minDot)
                        {
                            float force = (springForce + extraForce) * -dot; //�ǉ��̗͂�������
                            rb.AddForce(direction.normalized * force, ForceMode.Force);
                            GetComponent<Rigidbody>().AddForce(-direction.normalized * force, ForceMode.Force);
                        }
                        else
                        {
                            float force = springForce * -dot;
                            rb.AddForce(direction.normalized * force, ForceMode.Force);
                            GetComponent<Rigidbody>().AddForce(-direction.normalized * force, ForceMode.Force);
                        }

                        // ���������ʂ̔ԍ������O�ɏo��
                        Debug.Log("����������: " + minIndex);
                    }
                }
            }
        }
    }
}

