using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectList : MonoBehaviour
{
	[SerializeReference] List<GameObject> m_rectList;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	/// <summary>
	/// �摜�̐؂�ւ�
	/// </summary>
	/// <param name="index">���ԍ�</param>
	/// <param name="image">�摜�f�[�^</param>
	public void ChangeImage(int index, Sprite image)
	{
		if (index >= m_rectList.Count) return;
		Image currentImage = m_rectList[index].GetComponent<Image>();
		if (currentImage == null) return;
		currentImage.sprite = image;
	}

	/// <summary>
	/// �摜�̃X�P�[���ύX
	/// </summary>
	/// <param name="index">���ԍ�</param>
	/// <param name="scale">�X�P�[��</param>
	public void SetSizeImage(int index, float scale)
	{
		if (index >= m_rectList.Count) return;
		RectTransform rect = m_rectList[index].GetComponent<RectTransform>();
		if (rect == null) return;
		rect.localScale = new Vector3(scale, scale, scale);
	}
}