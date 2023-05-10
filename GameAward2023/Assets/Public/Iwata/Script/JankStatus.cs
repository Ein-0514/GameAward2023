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

    public enum eJankTag
    {
        E_JANK_TAG_NORMAL = 0,
        E_JANK_TAG_CORE,
        E_JANK_TAG_METAL,
        E_JANK_TAG_DRUM,
        E_JANK_TAG_MAX
    }

    [Tooltip("�ݒu�ς݂̎��Ɏg���t���O")]
    [SerializeField] AttachFlag m_colliderFlags;    //�E��E��E���E�Ă��E�鎞�Ɏg�E��E��E�t�E��E��E�O

    [Tooltip("���u���̎��Ɏg���t���O")]
    [SerializeField] AttachFlag m_collisionFlags;     //�E��E��E��E��E��E�t�E��E��E�鎞�Ɏg�E��E��E�t�E��E��E�O

    [SerializeField] eJankTag m_JankTag;

    protected List<GameObject> m_ConnectedChild = new List<GameObject>();      //���̃W�����N�ɂ���ꂽ�W�����N��o�^����
    
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

    public void DestroyChild()
    {
        foreach(GameObject child in m_ConnectedChild)
        {
            child.GetComponent<JankStatus>().DestroyChild();
            child.GetComponent<JankBase_iwata>().Orizin.SetActive(true);
            Destroy(child);
        }
    }

    public eJankTag JankTag
    {
        get { return m_JankTag; }
    }

    public GameObject ConnectedChild
    {
        set { m_ConnectedChild.Add(value); }
    }

    public List<GameObject> ConnectedChildList
    {
        get { return m_ConnectedChild; }
    }

}
