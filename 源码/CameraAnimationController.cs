using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimationController : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void cameracontroller()
    {
        animator.enabled = true;
        animator.Play(0);
    }
    private void Update()
    {
        AnimatorStateInfo animatorInfo;
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animatorInfo.normalizedTime > 1.0f)
            animator.enabled = false;
    }
}
