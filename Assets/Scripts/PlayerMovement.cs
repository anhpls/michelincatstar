using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isAttacking = false;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking) return;

        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontal * moveSpeed * Time.deltaTime, 0, 0));

        if (horizontal != 0)
        {
            animator.SetBool("isRunning", true);
            transform.localScale = new Vector3(Mathf.Sign(horizontal), 1, 1);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void SetAttacking(bool state)
    {
        isAttacking = state;
    }

    public void AllowMovement()
    {
        isAttacking = false;
    }
}
