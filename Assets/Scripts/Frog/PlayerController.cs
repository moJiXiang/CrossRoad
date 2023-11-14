using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  private enum Direction
  {
    Up, Right, Left
  }
  private Rigidbody2D rb;
  private Animator anim;
  private SpriteRenderer sr;
  public float jumpDistance;
  private float moveDistance;
  private Vector2 destination;
  private Vector2 touchPosition;
  private Direction dir;
  private bool buttonHeld;
  private bool isJump;
  private bool canJump;

  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();
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
    if (isJump)
    {
      rb.position = Vector2.Lerp(transform.position, destination, 0.134f);
    }
  }

  private void OnTriggerStay2D(Collider2D other)
  {
    if (other.CompareTag("Border") || other.CompareTag("Car"))
    {
      Debug.Log("Game Over!");
    }

    if (!isJump && other.CompareTag("Obstacle"))
    {
      Debug.Log("Game Over!");
    }
  }

  #region InputController 回调
  public void Jump(InputAction.CallbackContext context)
  {
    if (context.phase == InputActionPhase.Performed && !isJump)
    {
      moveDistance = jumpDistance;
      Debug.Log("JUMP! + " + " " + moveDistance);
      // destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
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
      // destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
      buttonHeld = false;
      canJump = true;
      // isJump = true;
      // TriggerJump();
    }
  }

  public void GetTouchPosition(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      touchPosition = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
      var offset = ((Vector3)touchPosition - transform.position).normalized;
      
      Debug.Log("Offset: " + offset);
      if (Mathf.Abs(offset.x) <= 0.7f)
      {
        dir = Direction.Up;
      }
      else if (offset.x < 0)
      {
        dir = Direction.Left;
      }
      else if (offset.x > 0)
      {
        dir = Direction.Right;
      }
    }
    
  }
  #endregion

  

  /// <summary>
  /// 触发执行跳跃动画
  /// </summary>
  private void TriggerJump()
  {
    canJump = false;
    
    switch (dir)
    {
      case Direction.Left:
        anim.SetBool("isSide", true);
        transform.localScale = Vector3.one;
        destination = new Vector2(transform.position.x - moveDistance, transform.position.y);
        break;
      case Direction.Right:
        anim.SetBool("isSide", true);
        transform.localScale = new Vector3(-1, 1, 1);
        destination = new Vector2(transform.position.x + moveDistance, transform.position.y);
        break;
      case Direction.Up:
        anim.SetBool("isSide", false);
        transform.localScale = Vector3.one;
        destination = new Vector2(transform.position.x, transform.position.y + moveDistance);
        break;
    }
    
    anim.SetTrigger("Jump");
  }

  #region Animation Event

  public void JumpAnimationEvent()
  {
    isJump = true;
    sr.sortingLayerName = "Front";
  }

  public void FinishJumpAnimationEvent()
  {
    isJump = false;
    sr.sortingLayerName = "Middle";
  }

  #endregion
}
