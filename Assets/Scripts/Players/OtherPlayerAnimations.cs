using UnityEngine;

public class OtherPlayerAnimations : MonoBehaviour
{
    public Animator animatorRobot_1, animatorRobot_2;

    private Animator animator;
    private PlayerManager playerManager;
    private bool[] inputs;

    private void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        animator = playerManager.id == 1 ? animatorRobot_1 : animatorRobot_2;
    }

    private void Update()
    {
        if(inputs == playerManager.inputs)
            return;
        
        inputs = playerManager.inputs;
        
        if (inputs[0])
            animator.SetFloat("Horizontal", 1);
        else if(inputs[1])
            animator.SetFloat("Horizontal", -1);
        else
            animator.SetFloat("Horizontal", 0);
        
        if (inputs[2])
            animator.SetFloat("Vertical", 1);
        else if(inputs[3])
            animator.SetFloat("Vertical", -1);
        else
            animator.SetFloat("Vertical", 0);
    }
}
