using UnityEngine;

public class TiltMeshAnimator : MonoBehaviour
{
    [SerializeField] private Transform meshTransform;
    [SerializeField] private float verticalLeanAngle = 60f;
    [SerializeField] private float horizontalLeanAngle = 60f;
    [SerializeField] private float tiltSpeed = 5f;
    private Vector2 currentTarget = Vector2.zero;
    private Quaternion initialRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialRotation = meshTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation = initialRotation;
        if (currentTarget != Vector2.zero)
        {
            targetRotation = Quaternion.Euler(currentTarget.y * verticalLeanAngle, 0, -currentTarget.x * horizontalLeanAngle) * initialRotation;
        }
        meshTransform.localRotation = Quaternion.Slerp(meshTransform.localRotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    public void UpdateTargetVector(Vector2 newTarget)
    {
        currentTarget = newTarget;
    }
}
