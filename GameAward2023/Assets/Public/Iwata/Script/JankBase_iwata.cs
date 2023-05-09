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

    /// <summary>
    /// �e�W�����N�̃p�����[�^���擾����
    /// </summary>
    public abstract List<float> GetParameterList();

    // Start is called before the first frame update
    protected void Start()
    {
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
        BoxCollider thiscollider = GetComponent<BoxCollider>();
        BoxCollider corecollider = trans.GetComponent<BoxCollider>();
        float[] dot = new float[3];
        dot[0] = Vector3.Dot(transform.forward, Vector3.forward);
        dot[1] = Vector3.Dot(transform.right, Vector3.forward);
        dot[2] = Vector3.Dot(transform.up, Vector3.forward);

        int nearestValue = 0;
        float nearestDistance = float.MaxValue;
        for (int i = 0; i < 3; i++)
        {
            float distance = Mathf.Abs(Mathf.Abs(dot[i]) - 1f);
            if (distance < nearestDistance)
            {
                nearestValue = i;
                nearestDistance = distance;
            }
        }
        switch (nearestValue)
        {
            case 0:
                pos.z -= corecollider.size.z / 2.0f;     //�t����ʂ̑傫���̔������炷
                pos.z -= thiscollider.size.z / 2.0f;        //�����̔������炷
                break;
            case 1:
                pos.z -= corecollider.size.z / 2.0f;     //�t����ʂ̑傫���̔������炷
                pos.z -= thiscollider.size.x / 2.0f;        //�����̔������炷
                break;
            case 2:
                pos.z -= corecollider.size.z / 2.0f;     //�t����ʂ̑傫���̔������炷
                pos.z -= thiscollider.size.y / 2.0f;        //�����̔������炷
                break;
        }
        this.transform.position = pos;      //���炵�Č��߂����W�Ɉړ�����
        this.transform.parent = core;
        core.Rotate(0.0f, 10.0f, 0.0f, Space.World);       //�ꎞ�I��0�C0�C0�ɂ��Ă�����]��߂�
    }

    public void RotJank(int axisX, int axisY, Transform core)
    {
        Transform trans = core.GetComponent<CoreSetting_iwata>().SelectFace;
        core.Rotate(0.0f, -10.0f, 0.0f, Space.World);      //�R�A�̌X�����ꎞ�I��0�C0�C0�ɖ߂�
        if (axisX != 0)
        {
            transform.Rotate(0.0f, 90.0f * axisX, 0.0f, Space.World);
        }
        else if(axisY != 0)
        {
            transform.Rotate(90.0f * axisY, 0.0f, 0.0f, Space.World);
        }
        core.GetComponent<CoreSetting_iwata>().CheckCanAttach();
        Vector3 pos = trans.transform.position;     //�t����ʂ̍��W�擾
        BoxCollider thiscollider = GetComponent<BoxCollider>();
        BoxCollider corecollider = trans.GetComponent<BoxCollider>();
        float[] dot = new float[3];
        dot[0] = Vector3.Dot(transform.forward, Vector3.forward);
        dot[1] = Vector3.Dot(transform.right, Vector3.forward);
        dot[2] = Vector3.Dot(transform.up, Vector3.forward);
        
        int nearestValue = 0;
        float nearestDistance = float.MaxValue;
        for (int i = 0; i < 3; i++)
        {
            float distance = Mathf.Abs(Mathf.Abs(dot[i]) - 1f);
            if (distance < nearestDistance)
            {
                nearestValue = i;
                nearestDistance = distance;
            }
        }
        switch (nearestValue)
        {
            case 0:
                pos.z -= corecollider.size.z / 2.0f;     //�t����ʂ̑傫���̔������炷
                pos.z -= thiscollider.size.z / 2.0f;        //�����̔������炷
                break;
            case 1:
                pos.z -= corecollider.size.z / 2.0f;     //�t����ʂ̑傫���̔������炷
                pos.z -= thiscollider.size.x / 2.0f;        //�����̔������炷
                break;
            case 2:
                pos.z -= corecollider.size.z / 2.0f;     //�t����ʂ̑傫���̔������炷
                pos.z -= thiscollider.size.y / 2.0f;        //�����̔������炷
                break;
        }

        this.transform.position = pos;      //���炵�Č��߂����W�Ɉړ�����
        core.Rotate(0.0f, 10.0f, 0.0f, Space.World);       //�ꎞ�I��0�C0�C0�ɂ��Ă�����]��߂�
    }

    public GameObject Orizin
    {
        get { return m_Origin; }
        set { m_Origin = value; }
    }
}
