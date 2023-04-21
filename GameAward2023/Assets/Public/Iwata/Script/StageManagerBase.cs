using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerBase : MonoBehaviour
{
    [SerializeField] private Dictionary<string, GameObject> m_Objects = new Dictionary<string, GameObject>();   //�����s���Ă�����ň�����I�u�W�F�N�g��o�^

    // Start is called before the first frame update
    protected virtual void Start()
    {
        foreach (Transform childTransform in transform)
        {
            GameObject childObject = childTransform.gameObject;
            string objectName = childObject.name;

            // Dictionary�ɓo�^����
            m_Objects[objectName] = childObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.childCount < m_Objects.Count)
        {//������
            OnDisable();
        }
        else if(this.transform.childCount > m_Objects.Count)
        {//������
            OnEnable();
        }
    }

    protected virtual void OnEnable()
    {
        // �q�I�u�W�F�N�g���ǉ����ꂽ�Ƃ��ɌĂ΂��
        foreach (Transform childTransform in transform)
        {
            GameObject childObject = childTransform.gameObject;
            if (!m_Objects.ContainsKey(childObject.name))
            {
                m_Objects.Add(childObject.name, childObject);
                Debug.Log("Add:" + childObject.name);
            }
        }
    }

    protected virtual void OnDisable()
    {
        // �q�I�u�W�F�N�g���폜���ꂽ�Ƃ��ɌĂ΂��
        List<string> removeKeys = new List<string>();

        foreach (KeyValuePair<string, GameObject> kvp in m_Objects)
        {
            if (kvp.Value.transform.parent != transform)
            {
                removeKeys.Add(kvp.Key);
            }
        }

        foreach (string key in removeKeys)
        {
            m_Objects.Remove(key);
            m_Objects[key] = null;
            Debug.Log("Remove:" + key);
        }
    }


    public Dictionary<string, GameObject> Objects
    {
        get { return m_Objects; }
    }

}
