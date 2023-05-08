using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SreelFlame : JankBase_iwata
{
    public int value = 5;   //�U�������Ƃ��̕ǂɊ|����{��
    FixedJoint joint;
    
    /// <summary>
    /// �S���̓���
    /// </summary>
    public override void work()
    {

    }

    public override List<float> GetParameterList()
    {
        List<float> list = new List<float>();

        return list;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            joint = collision.transform.GetComponent<FixedJoint>();

            joint.breakForce /= value;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            joint.breakForce *= value;
        }
    }
}
