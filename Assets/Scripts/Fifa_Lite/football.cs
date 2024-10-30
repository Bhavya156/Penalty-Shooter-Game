using System.Collections;
using UnityEngine;

public class football : MonoBehaviour
{
    public static football instance;
    public GameObject dragCenter;
    public GameObject parent;
    private float dragAreaRadius;
    private Vector3 offset;
    public float launchSpeed = 3f;

    public LineRenderer lineRenderer;
    public int numberOfPoints = 100;
    public float timeInterval = 0.1f;
    bool isMouseDown;
    Rigidbody ballRb;
    private Vector3 initialPos;
    private Quaternion initialRotation;
    private Vector3 tempPos;

    public LineRenderer dragLine;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        dragCenter.SetActive(false);
        lineRenderer.enabled = false;
        ballRb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        initialRotation = transform.rotation;

        Renderer dragAreaRenderer = dragCenter.GetComponent<Renderer>();
        if (dragAreaRenderer != null)
        {
            dragAreaRadius = dragAreaRenderer.bounds.size.z / 2f;
        }
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -Camera.main.transform.position.z;
            tempPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mousePosition.z));
            tempPos = ConstrainedArea(tempPos, dragCenter.transform.position, dragAreaRadius);
            DrawDragLine(initialPos, new Vector3(tempPos.x, initialPos.y, tempPos.y));

            Trajectory(initialPos, new Vector3(tempPos.x, initialPos.y, tempPos.y));
        }
    }

    Vector3 ConstrainedArea(Vector3 position, Vector3 center, float radius)
    {
        offset = position - center;
        return center + Vector3.ClampMagnitude(offset, radius);
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
        dragCenter.SetActive(true);
        lineRenderer.enabled = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        
        Shoot(dragLine.GetPosition(0) - dragLine.GetPosition(1));

        GoalKeeper.instance.ReactToShot(transform);

        lineRenderer.enabled = false;
        dragCenter.SetActive(false);
        
        StartCoroutine(DeactivateSelf());
    }

    void Shoot(Vector3 force)
    {
        StartCoroutine(playAnim(force));
    }

    void DrawDragLine(Vector3 start, Vector3 end) {
        dragLine.SetPosition(0, start);
        dragLine.SetPosition(1, end);
    }

    void Trajectory(Vector3 start, Vector3 end) {
        Vector3 force = start - end;
        Vector3 origin = transform.position;
        Vector3 initialVelocity = new Vector3(force.x, 0.5f, force.z) * launchSpeed;

        lineRenderer.positionCount = numberOfPoints;
        float time = 0f;

        for (int i = 0; i < numberOfPoints; i++)
        {
            var x = (initialVelocity.x * time) + 0.5f * (Physics.gravity.x * time * time);
            var y = (initialVelocity.y * time) + 0.5f * (Physics.gravity.y * time * time);
            var z = (initialVelocity.z * time) + 0.5f * (Physics.gravity.z * time * time);
            Vector3 point = new Vector3(x,y,z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeInterval;
        }
    }

    IEnumerator playAnim(Vector3 force) {
        Player.instance.PlayAnimation();
        yield return new WaitForSeconds(0.55f);
        ballRb.AddForce(new Vector3(force.x, 0.5f, force.z) * launchSpeed, ForceMode.Impulse);
        yield return new WaitForSeconds(1.35f);
        Player.instance.StopAnimation();
    }

    IEnumerator DeactivateSelf() {
        yield return new WaitForSeconds(4f);
        SpawnBalls spawnBalls = GameObject.Find("Spawn_Manager").GetComponent<SpawnBalls>();
        if (spawnBalls != null) {
            spawnBalls.isBallPresent = false;
            transform.root.gameObject.SetActive(false);
            transform.position = initialPos;
            transform.rotation = initialRotation;
            StopBall();
            spawnBalls.ballsUsedByPlayer++;
        }
    }

    void StopBall() {
        ballRb.linearVelocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
    }
}