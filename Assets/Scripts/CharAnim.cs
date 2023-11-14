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
}
