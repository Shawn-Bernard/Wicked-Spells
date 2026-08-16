using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoolEvent isWalking;

    private const string IS_WALKING = "IsWalking";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void IsWalking(bool boolValue)
    {
        if (boolValue) 
        {
            //Debug.Log("Is walking");
        }
        else
        {
            //Debug.Log("Isnt walking");
        }
        //animator.SetBool(IS_WALKING, boolValue);
    }

    private void OnEnable()
    {
        if (isWalking != null)
        {
            isWalking.gameEvent += IsWalking;
        }
    }

    private void OnDisable()
    {
        if (isWalking != null)
        {
            isWalking.gameEvent -= IsWalking;
        }
    }
}
