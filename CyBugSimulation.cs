using UnityEngine;

public class CyBugSimulation : MonoBehaviour
{
    public Sprite cyBugSprite; // Assign the Cy-Bug sprite in the Unity Editor

    private void Start()
    {
        // Create initial Cy-Bug
        CyBug cyBug = gameObject.AddComponent<CyBug>();
        cyBug.Initialize(0, 0, cyBugSprite);
        Debug.Log(cyBug);

        // Simulate the behavior of the Cy-Bug
        for (int i = 0; i < 10; i++)
        {
            cyBug.Move();
            CyBug newCyBug = cyBug.Replicate();

            if (newCyBug != null)
            {
                Debug.Log("New Cy-Bug created! " + newCyBug);
            }

            cyBug.ConsumeCode(); // Try to consume code from nearby objects

            Debug.Log(cyBug);
        }
    }
}
