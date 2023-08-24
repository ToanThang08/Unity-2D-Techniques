using UnityEngine;

public class AnimateController : MonoBehaviour
{
    public Sprite[] spriteSet;
    public float fps;


    public GameObject treeObject;
    private float turnSpeed = 30f;

    private void FixedUpdate()
    {
        if (treeObject != null)
        {
            treeObject.transform.Rotate(new Vector3(0f, 0f, turnSpeed * Time.fixedDeltaTime));
        }
    }
}
