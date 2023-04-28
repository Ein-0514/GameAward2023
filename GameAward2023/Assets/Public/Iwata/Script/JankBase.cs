using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JankBase : JankStatus
{
    [SerializeField] Vector3 StartPos;      //�J�n���̃|�W�V����
    [SerializeField] Quaternion StartRot;      //�J�n���̃|�W�V����

    public abstract void work();

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        StartPos = transform.position;
        StartRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    ///<summary>
    ///�K���N�^���K���N�^�R�ɖ߂����̃K���N�^�̏���
    ///</summary>
    public void ReturnJank()
    {
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        this.transform.position = StartPos;
        this.transform.rotation = StartRot;
    }

    /// <summary>
    /// �K���N�^���R�A�ɂ��鎞�̃K���N�^�̏���
    /// </summary>
    /// <param name="trans">�@����R�A�̃g�����X�t�H�[���@</param>
    public void JointJank(Transform trans)
    {
        this.transform.parent = null;

        this.transform.rotation = Quaternion.identity;

        Transform CoreTrans = trans.parent;
        CoreTrans.Rotate(-10.0f, 0.0f, 0.0f, Space.World);
        this.transform.parent = CoreTrans;

        Transform ChildTrans = CoreTrans.Find(trans.name);
        Vector3 pos = ChildTrans.transform.position;
        Debug.Log(pos);
        pos.z -= ChildTrans.localScale.z / 2.0f;
        pos.z -= this.transform.localScale.z / 2.0f;
        this.transform.position = pos;

        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        CoreTrans.Rotate(10.0f, 0.0f, 0.0f, Space.World);

        FixedJoint joint = this.gameObject.AddComponent<FixedJoint>();
        joint.connectedBody = trans.GetComponent<Rigidbody>();
    }
}
