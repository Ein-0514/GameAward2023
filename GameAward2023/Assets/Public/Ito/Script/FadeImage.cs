using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{

    public float fadeTime = 3.0f; // �t�F�[�h�ɂ����鎞�ԁi�b�j
    private float currentTime = 0.0f; // ���݂̌o�ߎ���

    private Image BlindFadeImage;
    private float Alpha = 0.0f;
    public bool isFade = false;
    public bool isClear = false;
    // Start is called before the first frame update
    void Start()
    {
        BlindFadeImage = GetComponent<Image>();
        BlindFadeImage.color = new Color(BlindFadeImage.color.r, BlindFadeImage.color.g, BlindFadeImage.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && BlindFadeImage.color.a == 0.0f)
        {
            isFade = true;
        }

        if(isFade == true)
        {
            //���l���v�Z����
            Alpha = Mathf.Clamp01(currentTime / fadeTime);
            
            //�J���[�̍X�V
            BlindFadeImage.color = new Color(BlindFadeImage.color.r, BlindFadeImage.color.g, BlindFadeImage.color.b, Alpha);

            // �o�ߎ��Ԃ����Z����
            currentTime += Time.deltaTime;

            // �t�F�[�h������������X�N���v�g�𖳌�������
            if (BlindFadeImage.color.a == 1.0f)
            {
                isFade = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && BlindFadeImage.color.a >= 0.99f)
        {
            isClear = true;
            currentTime = fadeTime;
        }

        if (isClear == true)
        {
            //���l���v�Z����
            Alpha = Mathf.Clamp01(currentTime / fadeTime);

            //�J���[�̍X�V
            BlindFadeImage.color = new Color(BlindFadeImage.color.r, BlindFadeImage.color.g, BlindFadeImage.color.b, Alpha);

            // �o�ߎ��Ԃ����Z����
            currentTime -= Time.deltaTime;

            // �t�F�[�h������������X�N���v�g�𖳌�������
            if (BlindFadeImage.color.a == 0.0f)
            {
                isClear = false;
            }
        }
    }
}
