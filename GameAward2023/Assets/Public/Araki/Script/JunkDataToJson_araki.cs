using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class JunkDataToJson_araki : MonoBehaviour
{
	JunkList m_list;
	[SerializeReference] string m_fileName;

	// Start is called before the first frame update
	void Start()
	{
		m_list = new JunkList();
		m_list.m_junks = new List<JunkData>();

		//--- �q�̏������X�g�Ɋi�[
		for (int i = 0; i < this.transform.childCount; ++i)
		{
			// �q���擾
			Transform child = this.transform.GetChild(i);

			//--- �f�[�^���i�[
			JunkData data = new JunkData();
			data.m_pos = new float[3];
			data.m_name = child.name;
			data.m_pos[0] = child.position.x;
			data.m_pos[1] = child.position.y;
			data.m_pos[2] = child.position.z;
			data.m_rot = new float[3];
			data.m_rot[0] = child.localRotation.x;
			data.m_rot[1] = child.localRotation.y;
			data.m_rot[2] = child.localRotation.z;
			data.m_params = new float[2/*���炩�̕��@�Ŏ擾���Ă����p�����[�^�̃��X�g�̒���*/];
			// for()�Ńf�[�^���i�[����

			m_list.m_junks.Add(data); // ���X�g�ɒǉ�
		}

		StreamWriter sw;

		// �f�[�^��json�ɕϊ�
		string json = JsonUtility.ToJson(m_list);

		// json�t�@�C���ɏ�������(Resources�t�@�C���Ɋi�[)
		string filePath = Application.dataPath + "/Resources/" + m_fileName + ".json";
		sw = new StreamWriter(filePath, false);
		sw.Write(json);
		sw.Flush();
		sw.Close();

		Debug.Log("�t�@�C���̏o�͂ɐ������܂����B");
	}

	// Update is called once per frame
	void Update()
	{

	}
}