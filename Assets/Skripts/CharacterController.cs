using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{
    [SerializeField] public float mJumpForce;
    [Range(0, .6f)] [SerializeField] public float mMovementSmoothing;
    [SerializeField] public bool mAirControl;
    [SerializeField] public LayerMask mGround;
    [SerializeField] public LayerMask mEnemy;
    [SerializeField] public Transform mGroundCheck;
    [SerializeField] public Transform mCeilingCheck;
    [SerializeField] public Transform mEnemyCheck;

    private Rigidbody2D mRigidBody;
    private PolygonCollider2D mPolygonCollider;

    const float GROUND_RADIUS = 0.1f;
    const float CEILING_RADIUS = 0.2f;
    const float ENEMY_HIT_DISTANCE = 0.8f;

    private bool mGrounded;
    private bool mWasCrouching;
    private bool mFacingRight = true;
    private bool mJumpTriggered;
    private bool mTouchingEnemy;

    private Vector3 mVelocity = Vector3.zero;
  
    private void Awake()
    {
        mRigidBody = GetComponent<Rigidbody2D>();
        mPolygonCollider = GetComponent<PolygonCollider2D>();
        
    }

    private void FixedUpdate()
    {
        bool wasGrounded = mGrounded;
        mGrounded = false;

        mGrounded = Physics2D.OverlapCircle(mGroundCheck.position, GROUND_RADIUS, mGround);
        mTouchingEnemy = Physics2D.OverlapCircle(mEnemyCheck.position, ENEMY_HIT_DISTANCE, mEnemy);

        if (mTouchingEnemy)
        {
            mPolygonCollider.isTrigger = true;
        }

        if (!mJumpTriggered)
        {
            if (mGrounded && !wasGrounded)
            {
                GameEvents.current.onLand();
            }
        }
        else
        {
            mJumpTriggered = false;
        }
    }

    public void Move(float move, bool jump)
    {
        
        if ((mGrounded || mAirControl) && !mTouchingEnemy)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, mRigidBody.velocity.y);
            mRigidBody.velocity = Vector3.SmoothDamp(mRigidBody.velocity, targetVelocity, ref mVelocity, mMovementSmoothing);

            if (move > 0 && !mFacingRight)
            {
                Flip();
            }
            else if (move < 0 && mFacingRight)
            {
                Flip();
            }
        }

        if ((mGrounded && jump) && !mTouchingEnemy)
        {
            mGrounded = false;
            mJumpTriggered = true;
            mRigidBody.AddForce(new Vector2(0f, mJumpForce * 100));
            GameEvents.current.onJump();
        }
    }

    private void Flip()
    {
        mFacingRight = !mFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
