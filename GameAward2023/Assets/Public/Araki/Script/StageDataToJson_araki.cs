using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class StageDataToJson_araki : MonoBehaviour
{
	ObjectList m_list;
	[SerializeReference] string m_fileName;

	// Start is called before the first frame update
	void Start()
	{
		m_list = new ObjectList();
		m_list.m_objects = new List<ObjectData>();

        Debug.Log(m_fileName + "�̐������J�n");

		//--- �q�̏������X�g�Ɋi�[
		for (int i = 0; i < this.transform.childCount; ++i)
		{
			// �q���擾
			Transform obj = this.transform.GetChild(i);

			// �f�[�^�i�[�p�ϐ�
			ObjectData data = new ObjectData();
			
			//--- �I�u�W�F�N�g�̎q�̏���S�Ď擾
			IEnumerable<Transform> children = obj.GetComponentsInChildren<Transform>(true);
			data.m_name = obj.name;
			data.m_pos = new float[children.Count() * 3];
			data.m_rot = new float[children.Count() * 3];
			int j = 0;
			foreach (Transform child in children)
			{
				//--- ���W
				data.m_pos[j * 3 + 0] = child.position.x;
				data.m_pos[j * 3 + 1] = child.position.y;
				data.m_pos[j * 3 + 2] = child.position.z;

				//--- ��]
				data.m_rot[j * 3 + 0] = child.rotation.x;
				data.m_rot[j * 3 + 1] = child.rotation.y;
				data.m_rot[j * 3 + 2] = child.rotation.z;

				j++;
			}
			m_list.m_objects.Add(data);	// ���X�g�ɒǉ�
		}

		// �f�[�^��json�ɕϊ�
		string json = JsonUtility.ToJson(m_list);

        Debug.Log(m_fileName + "�̏������݊J�n");

		// json�t�@�C���ɏ�������(Resources�t�@�C���Ɋi�[)
#if true
		string filePath = Application.dataPath + "/Resources/" + m_fileName + ".json";
#else
		string filePath = Application.dataPath + "/Resources/StageData/" + m_fileName + ".json";
#endif
		StreamWriter sw;
		sw = new StreamWriter(filePath, false);
		sw.Write(json);
		sw.Flush();
		sw.Close();


        Debug.Log(m_fileName + "�̐������I��");
    }

	// Update is called once per frame
	void Update()
	{

	}

    public void Work()
    {
        m_list = new ObjectList();
        m_list.m_objects = new List<ObjectData>();

        Debug.Log(m_fileName + "�̐������J�n");

        //--- �q�̏������X�g�Ɋi�[
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            // �q���擾
            Transform obj = this.transform.GetChild(i);

            // �f�[�^�i�[�p�ϐ�
            ObjectData data = new ObjectData();

            //--- �I�u�W�F�N�g�̎q�̏���S�Ď擾
            IEnumerable<Transform> children = obj.GetComponentsInChildren<Transform>(true);
            data.m_name = obj.name;
            data.m_pos = new float[children.Count() * 3];
            data.m_rot = new float[children.Count() * 3];
            int j = 0;
            foreach (Transform child in children)
            {
                //--- ���W
                data.m_pos[j * 3 + 0] = child.position.x;
                data.m_pos[j * 3 + 1] = child.position.y;
                data.m_pos[j * 3 + 2] = child.position.z;

                //--- ��]
                data.m_rot[j * 3 + 0] = child.rotation.x;
                data.m_rot[j * 3 + 1] = child.rotation.y;
                data.m_rot[j * 3 + 2] = child.rotation.z;

                j++;
            }
            m_list.m_objects.Add(data); // ���X�g�ɒǉ�
        }

        // �f�[�^��json�ɕϊ�
        string json = JsonUtility.ToJson(m_list);

        Debug.Log(m_fileName + "�̏������݊J�n");

        // json�t�@�C���ɏ�������(Resources�t�@�C���Ɋi�[)
#if true
        string filePath = Application.dataPath + "/Resources/" + m_fileName + ".json";
#else
		string filePath = Application.dataPath + "/Resources/StageData/" + m_fileName + ".json";
#endif
        StreamWriter sw;
        sw = new StreamWriter(filePath, false);
        sw.Write(json);
        sw.Flush();
        sw.Close();


        Debug.Log(m_fileName + "�̐������I��");
    }
}
