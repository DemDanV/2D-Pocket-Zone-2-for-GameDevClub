using UnityEngine;

public class RotateTowardsTarget : MonoBehaviour
{
    [SerializeField] EnemyLocator enemyLocator;
    [SerializeField] Transform upperRef;
    [SerializeField] Transform lowerRef;
    [SerializeField] Transform rightHandRef;


    public bool rotateTowardsTarget = true;

    private void Update()
    {
        float inputX = InputManager.Controls.Player.Move.ReadValue<Vector2>().x;
        float targetDirection = 0;

        bool needRotateToTarget = rotateTowardsTarget && enemyLocator.Target != null;
        bool needRotateToInput = Mathf.Abs(inputX) > float.Epsilon;

        if (needRotateToTarget)
            targetDirection = enemyLocator.Target.position.x - transform.position.x;

        int upperRotateSign = 0;
        int lowerRotateSign = 0;

        if (needRotateToTarget)
        {
            upperRotateSign = GetRotationSign(targetDirection, upperRef.localScale.x);
        }
        else if (needRotateToInput)
        {
            upperRotateSign = GetRotationSign(inputX, upperRef.localScale.x);
        }

        if (needRotateToInput)
        {
            lowerRotateSign = GetRotationSign(inputX, lowerRef.localScale.x);
        }
        else if (needRotateToTarget)
        {
            lowerRotateSign = GetRotationSign(targetDirection, lowerRef.localScale.x);
        }

        if (upperRotateSign != 0)
            RotateUpper(upperRotateSign);
        if (lowerRotateSign != 0)
            RotateLower(lowerRotateSign);
    }

    int GetRotationSign(float directionSign, float compareSign)
    {
        if (directionSign < 0 && compareSign > 0)
            return -1;
        else if (directionSign > 0 && compareSign < 0)
            return 1;

        return 0;
    }

    private void RotateUpper(int sign)
    {
        if(sign < 0)
            rightHandRef.localScale = new Vector3(-1, -1 , 1);
        else
            rightHandRef.localScale = Vector3.one;

        upperRef.localScale = new Vector3(sign, 1, 1);
    }

    void RotateLower(int sign)
    {
        lowerRef.localScale = new Vector3(sign, 1, 1);
    }
}
