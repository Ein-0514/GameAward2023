using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
	[RuntimeInitializeOnLoadMethod]
	static void Initialize()
	{
		Application.targetFrameRate = 60;   //�Q�[����FPS��ݒ�
	}
}