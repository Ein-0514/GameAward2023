using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JankStatus : ObjectBase
{
    [System.Serializable]
    public struct AttachFlag
    {
        public bool m_plusZ;
        public bool m_minusZ;
        public bool m_plusX;
        public bool m_minusX;
        public bool m_plusY;
        public bool m_minusY;
    }

    [Tooltip("�������Ă��鎞�Ɏg���t���O")]
    [SerializeField] AttachFlag m_colliderFlags;    //�������Ă��鎞�Ɏg���t���O

    [Tooltip("������t���鎞�Ɏg���t���O")]
    [SerializeField] AttachFlag m_collisionFlags;     //������t���鎞�Ɏg���t���O

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    /// <summary>
    /// �ݒu�ς݂̃I�u�W�F�N�g�̑g�ݗ��Ă��ʂ��̔���
    /// </summary>
    public bool CanColliderFlags(Transform core)
    {
        //--- �g�ݗ��Ă���ʂ�S�Đ􂢏o��
        List<Vector3> attachVector = GetAttachVector();

        core.Rotate(0.0f, -10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�

        //--- �g�ݗ��Ă���ʂ��𔻒肷��
        for (int i = 0; i < attachVector.Count; i++)
        {
            if (Vector3.Distance(attachVector[i], Vector3.forward) > 0.5f) continue;

            core.Rotate(0.0f, 10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
            return true;
        }

        core.Rotate(0.0f, 10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
        return false;
    }

    /// <summary>
    /// ���u���̃I�u�W�F�N�g�̑g�ݗ��Ă��ʂ��̔���
    /// </summary>
    public bool CanCollisionFlags(Transform core)
    {
        //--- �g�ݗ��Ă���ʂ�S�Đ􂢏o��
        List<Vector3> attachVector = GetRotVector();
        
        core.Rotate(0.0f, -10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
        //--- �g�ݗ��Ă���ʂ��𔻒肷��
        for (int i = 0; i < attachVector.Count; i++)
        {
            if (Vector3.Distance(attachVector[i], Vector3.forward) > 0.5f) continue;

            core.Rotate(0.0f, 10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
            return true;
        }

        core.Rotate(0.0f, 10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
        return false;
    }

    /// <summary>
    /// �g�ݗ��Ă���ʂ��擾
    /// </summary>
    /// <returns></returns>
    public List<Vector3> GetAttachVector()
    {
        //--- �g�ݗ��Ă���ʂ�S�Đ􂢏o��
        List<Vector3> attachVector = new List<Vector3>();
        if (m_colliderFlags.m_plusZ)    attachVector.Add(this.transform.forward);
        if (m_colliderFlags.m_minusY)   attachVector.Add(-this.transform.forward);
        if (m_colliderFlags.m_plusX)    attachVector.Add(this.transform.right);
        if (m_colliderFlags.m_minusX)   attachVector.Add(-this.transform.right);
        if (m_colliderFlags.m_plusY)    attachVector.Add(this.transform.up);
        if (m_colliderFlags.m_minusY)   attachVector.Add(-this.transform.up);

        return attachVector;
    }

    public List<Vector3> GetRotVector()
    {
        //--- �g�ݗ��Ă���ʂ�S�Đ􂢏o��
        List<Vector3> rotVector = new List<Vector3>();
        if (m_collisionFlags.m_plusZ)    rotVector.Add(this.transform.forward);
        if (m_collisionFlags.m_minusY)   rotVector.Add(-this.transform.forward);
        if (m_collisionFlags.m_plusX)    rotVector.Add(this.transform.right);
        if (m_collisionFlags.m_minusX)   rotVector.Add(-this.transform.right);
        if (m_collisionFlags.m_plusY)    rotVector.Add(this.transform.up);
        if (m_collisionFlags.m_minusY)   rotVector.Add(-this.transform.up);

        return rotVector;
    }
}
