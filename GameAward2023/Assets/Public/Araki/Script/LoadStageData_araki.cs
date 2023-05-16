using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ObjectData
{
	public string m_name;
	public float[] m_pos;
	public float[] m_rot;
}

[System.Serializable]
public class JunkData
{
	public string m_name;
	public float[] m_pos;
	public float[] m_rot;
	public float[] m_params;
}

[System.Serializable]
public class ObjectList
{
	public List<ObjectData> m_objects;
}

[System.Serializable]
public class JunkList
{
	public List<JunkData> m_junks;
}

public static class LoadStageData_araki
{
	/// <summary>
	/// �X�e�[�W��ɃI�u�W�F�N�g��ݒu
	/// </summary>
	public static void SettingStageObjects(string fileName)
	{
		//--- json�t�@�C���̓ǂݍ���
		ObjectList list;
		LoadJsonFile(fileName, out list);

		// �K���N�^��ێ�����e�I�u�W�F�N�g
		GameObject stageObjectPparent = GameObject.Find("StageObject");

		//--- �ǂݍ��񂾃f�[�^����ɏ���
		foreach (ObjectData obj in list.m_objects)
		{
			//--- �f�[�^���쐬
			Object objData = Resources.Load<Object>("Prefabs/" + obj.m_name);
			GameObject prefab = (GameObject)objData;

			//--- �I�u�W�F�N�g�̎q�̏���S�Ď擾
			IEnumerable<Transform> children = prefab.GetComponentsInChildren<Transform>(true);
			int j = 0;
			foreach (Transform child in children)
			{
				//--- ���W
				child.position = new Vector3(
					obj.m_pos[j * 3 + 0], obj.m_pos[j * 3 + 1], obj.m_pos[j * 3 + 2]);

				//--- ��]
				child.localEulerAngles = new Vector3(
					obj.m_rot[j * 3 + 0], obj.m_rot[j * 3 + 1], obj.m_rot[j * 3 + 2]);

				j++;
			}

			//--- �I�u�W�F�N�g�𐶐�
			Transform prefabData = prefab.transform;
			GameObject stageObject = (GameObject)GameObject.Instantiate(
				objData, prefabData.position, prefabData.localRotation);

			// �e�I�u�W�F�N�g�������ꍇ�͏������Ȃ�
			if (stageObjectPparent == null) continue;

			// �e��Jank�ɐݒ�
			stageObject.transform.SetParent(stageObjectPparent.transform);

			stageObject.name = prefab.name;	// ���O���v���n�u�ƈ�v������
		}
	}

	/// <summary>
	/// �ǂݍ��񂾃K���N�^��ݒu
	/// </summary>
	public static void SettingJunks(string fileName)
	{
		//--- json�t�@�C���̓ǂݍ���
		JunkList list;
		LoadJsonFile(fileName, out list);
		
		// �K���N�^��ێ�����e�I�u�W�F�N�g
		GameObject junkPparent = GameObject.Find("Jank");

		//--- �ǂݍ��񂾃f�[�^����ɏ���
		foreach (JunkData junk in list.m_junks)
		{
			//--- �f�[�^���쐬
			Object prefab = Resources.Load<Object>("Prefabs/" + junk.m_name);
			Vector3 pos = new Vector3(junk.m_pos[0], junk.m_pos[1], junk.m_pos[2]);
			Quaternion rot = Quaternion.Euler(junk.m_rot[0], junk.m_rot[1], junk.m_rot[2]);  // X.Z��]�͕K�v�Ȃ�

			// �I�u�W�F�N�g�𐶐�
			GameObject gameObject = (GameObject)GameObject.Instantiate(prefab, pos, rot);	

			//--- �p�����[�^��ݒ�
			List<float> param = new List<float>();
			for (int i = 0; i < junk.m_params.Length; i++)
				param.Add(junk.m_params[i]);

			JankBase_iwata junkBase = gameObject.GetComponent<JankBase_iwata>();
			junkBase.SetParam(param);   // �p�����[�^��ݒ�

			// �e�I�u�W�F�N�g�������ꍇ�͏������Ȃ�
			if (junkPparent == null) continue;

			// �e��Jank�ɐݒ�
			gameObject.transform.SetParent(junkPparent.transform);
		}
	}

	/// <summary>
	/// json�t�@�C���̓ǂݍ���
	/// </summary>
	static void LoadJsonFile<T>(string fileName, out T list)
	{
		//--- json�t�@�C���̓ǂݍ���
		string inputText = Resources.Load<TextAsset>(fileName).ToString();
		list = JsonUtility.FromJson<T>(inputText);  // �ǂݍ��񂾃f�[�^�����X�g��
	}
}