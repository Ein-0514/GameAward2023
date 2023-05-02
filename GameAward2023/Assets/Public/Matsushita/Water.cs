using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float slowForce = 2f; // �������Ɠ��������߂̗͂̑傫��
    public Vector3 areaSize = Vector3.one; // �������Ɠ������G���A�̑傫��
    private Collider areaCollider; // �������Ɠ������G���A�̃R���C�_�[

    /// <summary>
    /// �p�����[�^�[�z�u
    /// </summary>
    /// <param name="paramList"></param>
    public void SetParam(List<float> paramList)
    {
        slowForce = paramList[0];
        areaSize.x = paramList[1];
        areaSize.y = paramList[2];
        areaSize.z = paramList[3];
    }

    private void Start()
    {
        if (areaCollider == null)
        {
            areaCollider = GetComponent<Collider>();
            if (areaCollider == null)
            {
                areaCollider = gameObject.AddComponent<BoxCollider>();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // �͈͓��̕��̂ɑ΂��đ��x������������͂�������
        if (other.attachedRigidbody != null)
        {
            Debug.Log("�X���[");
            Vector3 slowForceVector = -other.attachedRigidbody.velocity.normalized * slowForce;
            other.attachedRigidbody.AddForce(slowForceVector, ForceMode.Acceleration);
        }
    }

    private void OnDrawGizmos()
    {
        // �G���A�͈̔͂�\������
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }

    private void OnValidate()
    {
        if (areaCollider == null)
        {
            areaCollider = GetComponent<Collider>();
            if (areaCollider == null)
            {
                areaCollider = gameObject.AddComponent<BoxCollider>();
            }
        }
    }
}
