using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public float WIND_FORCE = 10.0f; // ���̋���
    public float WIND_RANGE = 5.0f; // ���͈̔�
    public float DISTANCE_ATTENUATION = 1.0f; // �����ɂ�錸����
    public float WIDTH = 5.0f; // ���𔭐�������ʂ̕�
    public float HEIGHT = 5.0f; // ���𔭐�������ʂ̍���
    public Transform windSurface; // ���𔭐�������ʂ�Transform

    void Update()
    {
        // ���𔭐�������ʒu���v�Z����
        Vector3 windPos = windSurface.position + windSurface.forward * WIND_RANGE;

        // �͈͓��̃I�u�W�F�N�g�ɕ��̗͂�������
        Collider[] colliders = Physics.OverlapBox(windPos, new Vector3(WIDTH / 2, HEIGHT / 2, WIND_RANGE));
        foreach (Collider col in colliders)
        {
            // �ڐG�����I�u�W�F�N�g��Rigidbody���A�^�b�`����Ă���ꍇ
            Rigidbody rb = col.GetComponent<Rigidbody>();

            // Player,Core,Junk�̃^�O�������Ă���ꍇ
            if (rb != null /*|| col.CompareTag("Player") || col.CompareTag("Core") || CompareTag("Junk")*/)
            {
                // ���̗͂��v�Z����Rigidbody�ɉ�����
                Vector3 force = GetWindForce(rb.position) * Time.deltaTime;
                rb.AddForce(force, ForceMode.Force);
            }
        }
    }

    // ���̉e����^����͈͂�\������
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        // �͈͂�\������
        Gizmos.DrawWireCube(windSurface.position + windSurface.forward * WIND_RANGE, new Vector3(WIDTH, HEIGHT, WIND_RANGE));
    }

    // ���̗͂��v�Z���郁�\�b�h
    Vector3 GetWindForce(Vector3 pos)
    {
        // ���������ʂ̖@�����v�Z����
        Vector3 normal = -windSurface.forward;

        float distance = Vector3.Distance(windSurface.position, pos); // �ڐG�I�u�W�F�N�g�Ƃ̋������v�Z����

        // ���̋����������ɉ����Č���������
        float attenuation = Mathf.Clamp01(1.0f - distance / WIND_RANGE) * DISTANCE_ATTENUATION;
        // ���̗͂��v�Z����
        Vector3 force = normal * WIND_FORCE * attenuation;

        return force;
    }
}
