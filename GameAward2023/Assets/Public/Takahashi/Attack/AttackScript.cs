using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeReference] float m_crashRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            //--- ��������x�N�g�����v�Z
            Vector3 vForce = collision.contacts[0].point - this.transform.position;
            vForce = vForce.normalized * m_crashRate;

            // ��������x�N�g����K�p
            collision.gameObject.GetComponent<Rigidbody>().AddForce(vForce, ForceMode.Impulse);
        }
    }
}
