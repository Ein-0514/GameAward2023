using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JankBase_iwata : JankStatus
{
    [SerializeField] Vector3 m_StartPos;      //�J�n���̃|�W�V����
    [SerializeField] Quaternion m_StartRot;      //�J�n���̉�]
    [SerializeField] GameObject m_Origin;     //�N���[���Ȃ猳�̃I�u�W�F�N�g������悤

    /// <summary>
    /// �e�W�����N���L�̏������s��
    /// </summary>
    public abstract void work();

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        m_StartPos = transform.position;      //�������W��o�^
        m_StartRot = transform.rotation;      //������]��o�^
    }

    ///<summary>
    ///�K���N�^���K���N�^�R�ɖ߂����̃K���N�^�̏���
    ///</summary>
    public void ReturnJank()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.transform.position = m_StartPos;
        this.transform.rotation = m_StartRot;
    }

    /// <summary>
    /// �K���N�^���R�A�ɂ��鎞�̃K���N�^�̏���
    /// </summary>
    /// <param name="trans">�@����R�A�̃g�����X�t�H�[���@</param>
    public void JointJank(Transform trans)
    {
        this.transform.parent = null;       //�e�̓o�^������

        this.transform.rotation = Quaternion.identity;      //��]��������

        Transform CoreTrans = trans.parent;     //�R�A�̑匳���擾
        CoreTrans.Rotate(0.0f, -10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
        this.transform.parent = CoreTrans;      //�R�A��e�Ƃ��ēo�^����

        Vector3 pos = trans.transform.position;     //�t����ʂ̍��W�擾
        pos.z -= trans.localScale.z / 2.0f;     //�t����ʂ̑傫���̔������炷
        pos.z -= this.transform.localScale.z / 2.0f;        //�����̔������炷
        this.transform.position = pos;      //���炵�Č��߂����W�Ɉړ�����

        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;        //�͂�������Ă��ړ���]���Ȃ��悤�ɌŒ�

        CoreTrans.Rotate(0.0f, 10.0f, 0.0f, Space.World);       //�ꎞ�I��0�C0�C0�ɂ��Ă�����]��߂�

        FixedJoint joint = this.gameObject.AddComponent<FixedJoint>();      //FixrdJoint��ǉ�
        joint.connectedBody = trans.GetComponent<Rigidbody>();      //conenectedBody�ɂ����ʂ�o�^
    }

    /// <summary>
    /// ���u�������K���N�^�̏���
    /// </summary>
    /// <param name="trans">�I��ʂ̏��</param>
    public void SetJank(Transform trans)
    {
        Transform CoreTrans = trans.parent;     //�R�A�̑匳���擾

        this.transform.rotation = Quaternion.identity;      //��]��������

        PutJank(trans, CoreTrans);

        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;        //�͂�������Ă��ړ���]���Ȃ��悤�ɌŒ�
        
    }

    public void PutJank(Transform trans, Transform core)
    {
        core.Rotate(0.0f, -10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
        this.transform.parent = null;
        Vector3 pos = trans.transform.position;     //�t����ʂ̍��W�擾
        pos.z -= trans.localScale.z / 2.0f;     //�t����ʂ̑傫���̔������炷
        pos.z -= this.transform.localScale.z / 2.0f;        //�����̔������炷
        this.transform.position = pos;      //���炵�Č��߂����W�Ɉړ�����
        this.transform.parent = core;
        core.Rotate(0.0f, 10.0f, 0.0f, Space.World);       //�ꎞ�I��0�C0�C0�ɂ��Ă�����]��߂�
    }

    public GameObject Orizin
    {
        get { return m_Origin; }
        set { m_Origin = value; }
    }
}
