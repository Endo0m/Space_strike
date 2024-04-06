using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerDieBehavior : Health, IDieBehavior
{
    public Animator animator;

    public override void Die()
    {
        animator.SetTrigger("Die"); // ���� � ��� ���� �������� ������
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        // ��������� ��������� �������� ������
        Invoke(nameof(LoadLoseScene), 0.5f);
    }

    void LoadLoseScene()
    {
        SceneManager.LoadScene("Lose");
    }
}
