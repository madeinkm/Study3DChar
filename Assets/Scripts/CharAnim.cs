using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharAnim : MonoBehaviour
{

    //void Start()//3D�𵨷����� ���� ���׸����� ����ϴ� �ڵ�(Ŀ���͸���¡ ����)
    //{
    //    SkinnedMeshRenderer[] skinneds = GetComponentsInChildren<SkinnedMeshRenderer>();
    //    int count = skinneds.Length;
    //    for (int inum = 0; inum < count; inum++)
    //    {
    //        SkinnedMeshRenderer ren = skinneds[inum];
    //        Material mat = ren.material;
    //        ren.material = Instantiate(mat);
    //    }
    //}

    private Animator anim;
    private List<string> listDanceNames = new List<string>();

    private bool bWeightChanging = false;
    private float mouseVertical = 0.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        checkUseableDanceAnim();
    }

    private void checkUseableDanceAnim()
    {
        //�ִϸ��̼� Ŭ���� �������� ��
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        int count = clips.Length;
        for (int iNum = 0; iNum < count; iNum++)
        {
            AnimationClip clip = clips[iNum];
            if (clip.name.Contains("Dance_")) // Contains�� �� value���� ������ ���� ã���ִ� bool��
            {
                listDanceNames.Add(clip.name);
            }
        }
    }


    private void Start()
    {

    }

    void Update()
    {
        moving();
        doDance();
        switchWeight();
        checkMouseAim();
        
    }

    private void moving()
    {
        //bool splint = Input.GetKey(KeyCode.LeftShift); // ����Ʈ������ �ٰ� ����� Ű �⺻�˰���

        //float speedVertical = Input.GetAxis("Vertical");
        //if (splint == false && speedVertical > 0.5f)
        //{
        //    speedVertical = 0.5f;
        //}

        //anim.SetFloat("SpeedVertical", speedVertical);
        
        anim.SetFloat("SpeedVertical", Input.GetAxis("Vertical"));
        anim.SetFloat("SpeedHorizontal", Input.GetAxis("Horizontal"));
    }
    private void doDance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.CrossFade(listDanceNames[0], 0.1f); 
            // �ִϸ��̼� Ʈ�������� ���� �ȵ��־ ������Ű�� �� -> Play, CrossFade�� �������� ���Ŀ� �۵��ϰ� �������
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.CrossFade(listDanceNames[1], 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.CrossFade(listDanceNames[2], 0.1f);
        }
    }
    private void switchWeight()
    {
        if(bWeightChanging == true) 
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                //anim.SetLayerWeight(1, 1.0f);
                StartCoroutine(weightChange(true));
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                //anim.SetLayerWeight(1, 0.0f);
                StartCoroutine(weightChange(false));
            }
        }        
    }
    private void checkMouseAim()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            return;
        }
        mouseVertical += Input.GetAxisRaw("Mouse Y") * Time.deltaTime;
        mouseVertical = Mathf.Clamp(mouseVertical, 0.0f, 1.0f); //�ִ�,�ּҰ� ���� Clamp ���
        anim.SetFloat("MouseVertical", mouseVertical);
    }
    IEnumerator weightChange(bool _upper) //�ڷ�ƾ -> update���� �����ϴ°� �ƴ� ���� �����ϴ� �ڵ�
    {
        float time = 0f;
        bWeightChanging = true;
        if (_upper == true) 
        {
            while (anim.GetLayerWeight(1) < 1)
            {
                time += Time.deltaTime * 5.0f;
                anim.SetLayerWeight(1, Mathf.Lerp(0.0f, 1.0f, time));
                yield return null;
            }
            anim.SetLayerWeight(1, 1.0f);
        }
        else
        {
            while (anim.GetLayerWeight(1) > 0)
            {
                time += Time.deltaTime * 5.0f;
                anim.SetLayerWeight(1, Mathf.Lerp(1.0f, 0.0f, time));
                yield return null;
            }
            anim.SetLayerWeight(1, 0.0f);
        }
        bWeightChanging = false;
    }
}
