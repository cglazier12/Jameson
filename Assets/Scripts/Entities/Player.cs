using System;
using UnityEngine;
using Sirenix.OdinInspector;
using Actions;

namespace Entities
{
    public class Player : BasicMovement
    {
        [SerializeField]
        private Animator anim;
        
        [BoxGroup("Attack")]
        [SerializeField]
        private bool isAttackPressed;
        
        [BoxGroup("Attack")]
        [SerializeField, MinValue(0f)]
        private float attackDelay = 0.3f;
        
        [BoxGroup("Animation")]
        [SerializeField, ReadOnly]
        private int currentAnimationId; // current animation id
        
        [BoxGroup("Animation")]
        [SerializeField, ReadOnly]
        private readonly int run = Animator.StringToHash("run");
        
        [BoxGroup("Animation")]
        [SerializeField, ReadOnly]
        private readonly int idle = Animator.StringToHash("idle");
        
        [BoxGroup("Animation")]
        [SerializeField, ReadOnly]
        private readonly int jump = Animator.StringToHash("jump");
        
        protected new void Awake() // Remove 'override' and use 'new' if hiding base member
        {
            base.Awake(); // Call base method if needed
            anim = GetComponent<Animator>();
        }

        protected new void Update() // Remove 'override' and use 'new' if hiding base member
        {
            base.Update(); // Call base method if needed
            HandleAttackInput();
            DetermineAnimationState();
        }

        protected new void FixedUpdate() // Remove 'override' and use 'new' if hiding base member
        {
            base.FixedUpdate(); // Call base method if needed
            DetermineAnimationState();
        }
    
        [Button]
        private void HandleAttackInput()
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                isAttackPressed = true;
                // Implement attack logic here or call a method handling the attack
            }
        }
    
        private void DetermineAnimationState()
        {
            if (IsJumping)
            {
                ChangeAnimationState(jump);
            }
            else if (IsMoving)
            {
                ChangeAnimationState(run);
            }
            else
            {
                ChangeAnimationState(idle);
            }
        }
    
        [Button]
        private void ChangeAnimationState(int animationId)
        {
            if (currentAnimationId == animationId) return;
            
            anim.Play(animationId);
            currentAnimationId = animationId;
        }
    }
}
