using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SreelFlame : JankBase_iwata
{
    public int value = 5;   //�U�������Ƃ��̕ǂɊ|����{��
    FixedJoint joint;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// �S���̓���
    /// </summary>
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name.Contains("Cage"))
        {
            Debug.Log("���x:" + rb.velocity);
            Vector3 force = rb.velocity * 150f; // �K�X����
            Debug.Log("force:" + force);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }
    }
}
