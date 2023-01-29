using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Target : NetworkBehaviour
{
    private Animator animator;
    private Score score;

    private bool blockAnim;

    private void Start()
    {
        animator = GetComponent<Animator>();
        score = FindObjectOfType<Score>();
    }

    public void TargetHit()
    {
        animator.SetTrigger("hit");
        blockAnim = true;
        CmdHitAnimation();
    }
    [Command(requiresAuthority = false)]
    public void CmdTargetHit(NetworkConnectionToClient sender = null) => score.ChangePoint(sender, 1);

    [Command(requiresAuthority = false)]
    private void CmdHitAnimation() => RpcHitAnimation();
    [ClientRpc]
    private void RpcHitAnimation()
    {
        if (!blockAnim)
            animator.SetTrigger("hit");

        blockAnim = false;
    }
}