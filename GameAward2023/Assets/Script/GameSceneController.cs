using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
	[SerializeReference] AudioClip m_stageAudio;
	[SerializeReference] List<GameObject> m_settingObjects;
	bool m_isStart = false;

	public void StartStage()
	{
		//--- �s�v�ȃI�u�W�F�N�g���A�N�e�B�u��
		foreach (GameObject child in m_settingObjects)
			child.SetActive(false);

		AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
		audioSource.Stop();
		audioSource.clip = m_stageAudio;
		audioSource.Play();

		m_isStart = true;
	}

	public bool IsStart
	{
		get { return m_isStart; }
	}
}