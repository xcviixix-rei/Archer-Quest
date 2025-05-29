using UnityEngine;

public class FadeAfterDeathBehaviour : StateMachineBehaviour
{
    public float fadeDuration = 1f;
    public float fadeDelay = 0.25f;
    private float fadeTimer = 0f;
    private float fadeDelayTimer = 0f;
    SpriteRenderer spriteRenderer;
    GameObject gameObject;
    Color initialColor;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fadeTimer = 0f;
        fadeDelayTimer = 0f;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        gameObject = animator.gameObject;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (fadeDelay > 0 && fadeDelayTimer < fadeDelay)
        {
            fadeDelayTimer += Time.deltaTime;
        }
        else
        {
            fadeTimer += Time.deltaTime;
            float newAlpha = initialColor.a * (1f - fadeTimer / fadeDuration);
            spriteRenderer.color = new Color(
                initialColor.r,
                initialColor.g,
                initialColor.b,
                newAlpha
            );
            if (fadeTimer > fadeDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
