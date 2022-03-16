using UnityEngine;

public class Bumble
{
    public Vector3 jumpForce { get; private set; }
    public bool CanJump
    {
        get { return Time.time - lastJumpedTime > GamePhysics.JumpTime; }
    }

    private Vector3 sideJumpForce;
    private float lastJumpedTime;
    private int maxSideJumps;
    private int currentSideJump;

    public Bumble(float sideJumpPower, float stepWidth)
    {
        currentSideJump = 0;
        lastJumpedTime = 0;
        SetJumpVelocity(sideJumpPower);
        SetSideJumpsLimit(stepWidth);
    }

    private void SetJumpVelocity(float sideJumpPower)
    {
        jumpForce = new Vector3(0, Mathf.Sqrt(Mathf.Abs(2 * Physics.gravity.y * GamePhysics.JumpHeight)), 0);
        sideJumpForce = new Vector3(0, jumpForce.y * sideJumpPower, 1);
    }
    private void SetSideJumpsLimit(float stepWidth)
    {
        var stepWidthHalf = stepWidth / 2;
        var jumpTime = -2 * sideJumpForce.y / Physics.gravity.y;
        var sideJumpDistance = sideJumpForce.z * jumpTime;
        maxSideJumps = (int)(stepWidthHalf / sideJumpDistance);
    }

    public void UpdateJumpTime()
    {
        lastJumpedTime = Time.time;
    }

    public bool CanJumpAside(int direction)
    {
        return Mathf.Abs(currentSideJump + direction) < maxSideJumps;
    }

    public Vector3 GetSideJumpForce(int direction)
    {
        return new Vector3(sideJumpForce.x, sideJumpForce.y, sideJumpForce.z * direction);
    }

    public void UpdateSideJump(int direction)
    {
        currentSideJump += direction;
        UpdateJumpTime();
    }
}
