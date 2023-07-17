using UnityEngine;

public class BehaviorModifier
{
    // Add any custom behavior modification methods here
    public Vector2 ModifyMovement(Vector2 originalMovement)
    {
        // Implement any logic to modify the movement based on consumed scripts
        // For example, you can scale the movement based on the number of consumed scripts
        float scale = 1f + (0.1f * Mathf.Min(5, consumedScripts.Count)); // Limit the scaling factor to 5 max
        return originalMovement * scale;
    }
}
