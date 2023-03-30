using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeController : MonoBehaviour
{
	[SerializeReference] AudioSource m_audioSource;
	[SerializeReference] List<AudioClip> m_seClips;

	public void PlaySe(string clipName)
	{
		//--- ����̖��O��T��
		foreach(AudioClip clip in m_seClips)
		{
			// ��v���Ȃ����̓X���[
			if (clipName != clip.name) continue;

			//--- ��v����SE���Đ�
			m_audioSource.clip = clip;
			m_audioSource.Play();
			return;
		}
	}
}