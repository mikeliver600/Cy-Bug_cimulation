using UnityEngine;
using System.Collections.Generic;

public class CyBug : MonoBehaviour
{
    private int x;
    private int y;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    // List to store the consumed script types
    private List<System.Type> consumedScripts = new List<System.Type>();

    // Custom behavior modifier to handle the effects of consumed code
    private BehaviorModifier behaviorModifier = new BehaviorModifier();

    public void Initialize(int initialX, int initialY, Sprite sprite)
    {
        x = initialX;
        y = initialY;

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        animator = gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("CyBugAnimator");
    }

    public void Move()
    {
        // Randomly choose a direction to move
        int dx = Random.Range(-1, 2);
        int dy = Random.Range(-1, 2);

        // Apply behavior modifiers to the movement
        Vector2 modifiedMovement = behaviorModifier.ModifyMovement(new Vector2(dx, dy));

        // Update the position
        x += (int)modifiedMovement.x;
        y += (int)modifiedMovement.y;

        // Update the position of the game object
        transform.position = new Vector3(x, y, 0);
    }

    public CyBug Replicate()
    {
        // Randomly decide whether to replicate or not
        if (Random.value < 0.1f)
        {
            CyBug newCyBug = Instantiate(this);
            newCyBug.Initialize(x, y, spriteRenderer.sprite);

            // Copy the consumed scripts to the new Cy-Bug
            foreach (System.Type scriptType in consumedScripts)
            {
                newCyBug.consumedScripts.Add(scriptType);
            }

            return newCyBug;
        }
        else
        {
            return null;
        }
    }

    public void ConsumeCode()
    {
        // Find all game objects with scripts
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();

            // Copy the behavior of the first script found and add it to the Cy-Bug
            foreach (MonoBehaviour script in scripts)
            {
                if (script != this)
                {
                    System.Type scriptType = script.GetType();

                    // Check if the script type is not already in the consumedScripts list
                    if (!consumedScripts.Contains(scriptType))
                    {
                        consumedScripts.Add(scriptType);
                        ICodeConsumer codeConsumer = script as ICodeConsumer;
                        if (codeConsumer != null)
                        {
                            codeConsumer.AdoptCode(this);
                        }
                        Debug.Log("Cy-Bug consumed and adopted " + scriptType);
                    }
                    return; // Consume only one script for this example
                }
            }
        }
    }

    public override string ToString()
    {
        return string.Format("Cy-Bug at ({0}, {1})", x, y);
    }
}
