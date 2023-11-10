using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  private Rigidbody2D rb;
  private Animator anim;
  public float jumpDistance;
  private float moveDistance;
  private Vector2 destination;
  private bool buttonHeld;
  private bool isJump;
  private bool canJump;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }

  private void Update()
  {
    // if (destination.y - this.transform.position.y < 0.1f )
    // {
    //   isJump = false;
    // }
    if (canJump)
    {
      TriggerJump();
    }
  }

  private void FixedUpdate()
  {
    // 每0.02秒执行
    Debug.Log("FiexedUpdate: " + isJump);
    if (isJump)
    {
      rb.position = Vector2.Lerp(transform.position, destination, 0.134f);
    }
  }

  #region InputController 回调
  public void Jump(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && !isJump)
    {
      moveDistance = jumpDistance;
      Debug.Log("JUMP! + " + " " + moveDistance);
      destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
      canJump = true;
      // isJump = true;
      // TriggerJump();
    }
  }

  public void LongJump(InputAction.CallbackContext context)
  {
    if (context.performed && !isJump)
    {
      moveDistance = jumpDistance * 2;
      buttonHeld = true;;
    }

    if (context.canceled && buttonHeld)
    {
      Debug.Log("LONG JUMP! + " + " " + moveDistance);
      destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
      buttonHeld = false;
      canJump = true;
      // isJump = true;
      // TriggerJump();
    }
  }
  #endregion

  
  public void GetTouchPosition(InputAction.CallbackContext context) {}

  /// <summary>
  /// 触发执行跳跃动画
  /// </summary>
  private void TriggerJump()
  {
    canJump = false;
    anim.SetTrigger("Jump");
  }

  #region Animation Event

  public void JumpAnimationEvent()
  {
    isJump = true;
    Debug.Log("JumpAnimationEvent: " + isJump);
  }

  public void FinishJumpAnimationEvent()
  {
    isJump = false;
    Debug.Log("FinishJumpAnimationEvent: " + isJump);
  }

  #endregion
}
