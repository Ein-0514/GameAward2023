using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManagerBase : MonoBehaviour
{
    //[SerializeField] private Dictionary<string, GameObject> m_Objects = new Dictionary<string, GameObject>();   //�����s���Ă�����ň�����I�u�W�F�N�g��o�^

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //foreach (Transform childTransform in transform)
        //{
        //    GameObject childObject = childTransform.gameObject;
        //    string objectName = childObject.name;

        //    // Dictionary�ɓo�^����
        //    m_Objects[objectName] = childObject;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("���ہF" + this.transform.childCount);
        //Debug.Log("������nai �F" + m_Objects.Count);

        //if(this.transform.childCount < m_Objects.Count)
        //{//������
        //    OnDisable();
        //}
        //else if(this.transform.childCount > m_Objects.Count)
        //{//������
        //    OnEnable();
        //}

        //List<string> key = new List<string>();
        //Dictionary<string, GameObject> hoge = Objects;

        //foreach (Transform child in this.transform) //���ۂɂ���q�I�u�W�F�N�g
        //{
        //    for(int i = 0; i < hoge.Count; i++)     //Objects�ɓo�^����Ă������
        //    {
        //        if(child == hoge.)
        //    }
        //}

        //List<GameObject> obj = new List<GameObject>();
        //foreach(GameObject child in Objects.Values)
        //{
        //    Debug.Log("obj���F" + child.name);
        //    obj.Add(child);
        //}
        //List<GameObject> now = new List<GameObject>();
        //foreach(GameObject child in transform)
        //{
        //    Debug.Log("���ہF" + child.name);
        //    now.Add(child);
        //}

        //if(obj != now)
        //{
        //    Objects.Clear();

        //    foreach (Transform childTransform in transform)
        //    {
        //        GameObject childObject = childTransform.gameObject;
        //        string objectName = childObject.name;

        //        // Dictionary�ɓo�^����
        //        m_Objects[objectName] = childObject;
        //    }
        //}        //List<GameObject> obj = new List<GameObject>();
        //foreach(GameObject child in Objects.Values)
        //{
        //    Debug.Log("obj���F" + child.name);
        //    obj.Add(child);
        //}
        //List<GameObject> now = new List<GameObject>();
        //foreach(GameObject child in transform)
        //{
        //    Debug.Log("���ہF" + child.name);
        //    now.Add(child);
        //}

        //if(obj != now)
        //{
        //    Objects.Clear();

        //    foreach (Transform childTransform in transform)
        //    {
        //        GameObject childObject = childTransform.gameObject;
        //        string objectName = childObject.name;

        //        // Dictionary�ɓo�^����
        //        m_Objects[objectName] = childObject;
        //    }
        //}
    }

    //public virtual void OnEnable()
    //{

    //    Debug.Log("hueta");

    //    // �q�I�u�W�F�N�g���ǉ����ꂽ�Ƃ��ɌĂ΂��
    //    foreach (Transform childTransform in transform)
    //    {
    //        GameObject childObject = childTransform.gameObject;
    //        if (!m_Objects.ContainsKey(childObject.name))
    //        {
    //            m_Objects.Add(childObject.name, childObject);
    //            Debug.Log("Add:" + childObject.name);
    //        }
    //    }
    //}

    //public virtual void OnDisable()
    //{

    //    Debug.Log("hetta");

    //    // �q�I�u�W�F�N�g���폜���ꂽ�Ƃ��ɌĂ΂��
    //    List<string> removeKeys = new List<string>();

    //    foreach (KeyValuePair<string, GameObject> kvp in m_Objects)
    //    {
    //        if (kvp.Value == null)
    //        {
    //            removeKeys.Add(kvp.Key);
    //        }
    //    }

    //    foreach (string key in removeKeys)
    //    {
    //        m_Objects.Remove(key);
    //        m_Objects[key] = null;
    //        Debug.Log("Remove:" + key);
    //    }
    //}    //public virtual void OnEnable()
    //{

    //    Debug.Log("hueta");

    //    // �q�I�u�W�F�N�g���ǉ����ꂽ�Ƃ��ɌĂ΂��
    //    foreach (Transform childTransform in transform)
    //    {
    //        GameObject childObject = childTransform.gameObject;
    //        if (!m_Objects.ContainsKey(childObject.name))
    //        {
    //            m_Objects.Add(childObject.name, childObject);
    //            Debug.Log("Add:" + childObject.name);
    //        }
    //    }
    //}

    //public virtual void OnDisable()
    //{

    //    Debug.Log("hetta");

    //    // �q�I�u�W�F�N�g���폜���ꂽ�Ƃ��ɌĂ΂��
    //    List<string> removeKeys = new List<string>();

    //    foreach (KeyValuePair<string, GameObject> kvp in m_Objects)
    //    {
    //        if (kvp.Value == null)
    //        {
    //            removeKeys.Add(kvp.Key);
    //        }
    //    }

    //    foreach (string key in removeKeys)
    //    {
    //        m_Objects.Remove(key);
    //        m_Objects[key] = null;
    //        Debug.Log("Remove:" + key);
    //    }
    //}

}
