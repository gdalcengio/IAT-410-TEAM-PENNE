using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class CharController : MonoBehaviour
{
    #region Public Properties
    // public float runSpeed = 5;
    // public float acceleration = 20;
    // public float jumpSpeed = 5;
    // public float gravity = 15;
    // public Vector2 influence = new Vector2(5, 5);

    #endregion
    //--------------------------------------------------------------------------------
    #region Private Properties
    AnimationManager animator;
    Vector3 defaultScale;
    float groundY;
    bool grounded;
    float stateStartTime;

    float timeInState {
        get { return Time.time - stateStartTime; }
    }

    const string kIdleAnim = "Idle";
    const string kRunAnim = "Run";
    const string kJumpStartAnim = "JumpStart";
    // const string kSkillAnim = "Skill";

    enum State {
        Idle,
        RunningRight,
        RunningLeft,
        JumpingUp,
        // Skill
    }
    State state;
    Vector2 velocity;
    float horzInput;
    bool jumpJustPressed;
    bool jumpHeld;
    // bool skillPressed;

    #endregion
    //--------------------------------------------------------------------------------
    #region MonoBehaviour Events
    void Start() {
        animator.GetComponent<Animator>();
        defaultScale = transform.localScale;
        groundY = transform.position.y;
    }
    
    void Update() {
        // Gather inputs
        horzInput = Input.GetAxisRaw("Horizontal");
        jumpJustPressed = Input.GetButtonDown("Jump");
        jumpHeld = Input.GetButton("Jump");
        grounded = GetComponent<PlayerController>().getIsGrounded();

        // skillPressed = Input.GetButtonDown("Transpose");
        // skillPressed = Input.GetButtonDown("Fissure");
        // skillPressed = Input.GetButtonDown("Geyser");
        // skillPressed = Input.GetKeyDown(KeyCode.M);
        // skillPressed = Input.GetButtonDown("Dive");

        // Update state
        ContinueState();
    }

    #endregion
    //--------------------------------------------------------------------------------
    #region Private Methods
    void SetOrKeepState(State state) {
        if (this.state == state) return;
        EnterState(state);
    }

    void ExitState() {
        animator.Stop();
    }

    void EnterState(State state) {
        ExitState();
        switch (state) {
        // case State.Idle:
        //     animator.Play(kIdleAnim);
        //     break;
        // case State.RunningLeft:
        //     animator.Play(kRunAnim);
        //     // Face(-1);
        //     break;
        // case State.RunningRight:
        //     animator.Play(kRunAnim);
        //     // Face(1);
        //     break;
        // case State.JumpingUp:
        //     animator.Play(kJumpStartAnim);
        //     // velocity.y = jumpSpeed;
        //     break;
        // case State.Skill:
        //     animator.Play(kSkillAnim);
        //     break;

        default:
            animator.Play(kRunAnim);
            break;
        }

        this.state = state;
        stateStartTime = Time.time;
    }

    void ContinueState() {
        switch (state) {
        
        case State.Idle:
            RunOrJump();
            // Skill();
            break;
        
        case State.RunningLeft:
        case State.RunningRight:
            // if (!RunOrJump() && !Skill())
            if (!RunOrJump()) EnterState(State.Idle);
            break;

        case State.JumpingUp:
            // if (velocity.y < 0) EnterState(State.Idle);
            // if (jumpJustPressed && grounded && !Skill())
            if (jumpJustPressed && grounded) EnterState(State.JumpingUp);
            break;

        // case State.Skill:
        //     // if (!RunOrJump() && Skill()) 
        //     if (!RunOrJump())EnterState(State.Skill);
        //     break;
        }
    }

    bool RunOrJump() {
        if (jumpJustPressed && grounded) SetOrKeepState(State.JumpingUp);
        else if (horzInput < 0) SetOrKeepState(State.RunningLeft);
        else if (horzInput > 0) SetOrKeepState(State.RunningRight);
        else return false;
        return true;
    }

    // bool Skill()
    // {
    //     if (skillPressed) {
    //         SetOrKeepState(State.Skill);
    //         return true;
    //     }
    //     return false;
    // }


    void Face(int direction) {
        transform.localScale = new Vector3(defaultScale.x * direction, defaultScale.y, defaultScale.z);
    }

    #endregion
}
