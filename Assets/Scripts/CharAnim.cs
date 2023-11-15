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

    private bool bWeightChanging = false;
    private float mouseVertical = 0.5f;

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
        doDance();
        switchWeight();
        checkMouseAim();
        
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
    private void doDance()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.CrossFade(listDanceNames[0], 0.1f); 
            // 애니매이션 트랜지션이 연결 안되있어도 구동시키는 법 -> Play, CrossFade는 먼저동작 이후에 작동하게 만들어줌
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
        mouseVertical = Mathf.Clamp(mouseVertical, 0.0f, 1.0f); //최대,최소값 지정 Clamp 사용
        anim.SetFloat("MouseVertical", mouseVertical);
    }
    IEnumerator weightChange(bool _upper) //코루틴 -> update에서 동작하는게 아닌 따로 동작하는 코드
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
