using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Transform pivot;
    public Transform launchPoint;
    public GameObject ballPrefab;
    public float launchSpeed = 20f;
    public Vector3 lastMousePosition;
    public float rotationSpeed = 180f;
    
    public LineRenderer lineRenderer;
    public int numberOfPoints = 100;
    public float timeInterval = 0.1f;

    bool isMouseDown;
    void Start()
    {
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 changeInMousePos = Input.mousePosition - lastMousePosition;
            float rotationAmountX = changeInMousePos.x * rotationSpeed * Time.deltaTime;
            float rotationAmountY = changeInMousePos.y * rotationSpeed * Time.deltaTime;

            pivot.Rotate(-Vector3.forward * rotationAmountX * Time.deltaTime);
            pivot.Rotate(Vector3.right * rotationAmountY * Time.deltaTime);

            Trajectory();

            lastMousePosition = Input.mousePosition;
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        lastMousePosition = Input.mousePosition;
        lineRenderer.enabled = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
        lineRenderer.enabled = false;
    }

    void Shoot()
    {
        GameObject newBall = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity);
        newBall.GetComponent<Rigidbody>().linearVelocity = launchSpeed * launchPoint.up;
    }

    void Trajectory() {
        Vector3 origin = launchPoint.position;
        Vector3 initialVelocity = launchSpeed * launchPoint.up;
        lineRenderer.positionCount = numberOfPoints;
        float time = 0f;

        for(int i = 0; i < numberOfPoints; i++) {
            var x = (initialVelocity.x * time) + 0.5f * (Physics.gravity.x * time * time);
            var y = (initialVelocity.y * time) + 0.5f * (Physics.gravity.y * time * time);
            var z = (initialVelocity.z * time) + 0.5f * (Physics.gravity.z * time * time);
            Vector3 point = new Vector3(x,y, z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeInterval;
        }
    }
}
