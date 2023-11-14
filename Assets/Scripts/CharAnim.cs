using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharAnim : MonoBehaviour
{

    //void Start()//3D모델러들을 각자 메테리얼을 사용하는 코드(커스터마이징 느낌)
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
        //애니매이션 클립을 가져오는 것
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        int count = clips.Length;
        for (int iNum = 0; iNum < count; iNum++)
        {
            AnimationClip clip = clips[iNum];
            if (clip.name.Contains("Dance_")) // Contains는 뒤 value값을 포함한 것을 찾아주는 bool문
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
        //bool splint = Input.GetKey(KeyCode.LeftShift); // 쉬프트눌르면 뛰게 만드는 키 기본알고리즘

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
