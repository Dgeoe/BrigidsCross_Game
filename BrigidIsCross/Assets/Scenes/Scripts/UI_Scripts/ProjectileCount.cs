using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileCount : MonoBehaviour
{
    // Needs to:
    // on start display amount of shurikens + animation
    // On Button Press display again

    [SerializeField] private Transform startPos;
    [SerializeField] private Transform button;
    [SerializeField] private GameObject prefab;

    [Header("Animation Settings")]
    [SerializeField] private float baseDuration = 0.2f;
    [SerializeField] private float distanceMultiplier = 0.0015f;
    [SerializeField] private float windUpOffset = 30f;
    [SerializeField] private float windUpTime = 0.08f;

    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shurikenWhoosh;

    public GameObject[] crosses;
    public static ProjectileCount Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnUI();
        AnimationBuffer();
    }

    private void SpawnUI()
    {
        int count = Throw.Instance.shurikens;
        crosses = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            // Instantiate in New UI
            Vector3 newPos = new Vector3(startPos.position.x + (216 * i), startPos.position.y, startPos.position.z);

            GameObject cross = Instantiate(prefab, newPos, gameObject.transform.rotation, gameObject.GetComponentInParent<Canvas>().gameObject.transform);

            cross.transform.SetParent(gameObject.transform);
            crosses[i] = cross;

            // Ensure 1st is always on top
            crosses[0].transform.SetAsLastSibling();
            button.SetAsLastSibling();
        }
    }

    public void AnimationBuffer()
    {

        for (int i = 0; i < crosses.Length; i++)
        {
            if (crosses[i] == null) continue;
            Vector3 target = new Vector3(startPos.position.x + (17 * i), startPos.position.y, startPos.position.z);

            float distance = Vector3.Distance(crosses[i].transform.position, target);

            // scale duration based on distance
            float duration = baseDuration + (distance * distanceMultiplier);

            if (i != 0) StartCoroutine(MoveToPosition(crosses[i].transform, target, duration));
        }
    }

    private IEnumerator MoveToPosition(Transform obj, Vector3 target, float duration)
    {
        if (obj == null) yield break;
        yield return new WaitForSeconds(0.5f);

        Vector3 start = obj.position;

        // Moves slightly to right to give a wind up effect
        Vector3 windUpTarget = start + new Vector3(windUpOffset, 0f, 0f);

        float windTime = 0f;

        while (windTime < windUpTime)
        {
            float t = windTime / windUpTime;
            t = Mathf.SmoothStep(0, 1, t);

            obj.position = Vector3.Lerp(start, windUpTarget, t);

            windTime += Time.deltaTime;
            yield return null;
        }

        if (audioSource != null && shurikenWhoosh != null) audioSource.PlayOneShot(shurikenWhoosh);

        // Snap to Start Position
        float time = 0f;
        float arcHeight = 60f;
        float spinSpeed = 720f;

        Vector3 throwStart = obj.position;

        // Random start rotation for nicer spin
        obj.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0, 1, t);

            Vector3 pos = Vector3.Lerp(throwStart, target, t);
            float arc = Mathf.Sin(t * Mathf.PI) * arcHeight;

            obj.position = pos + Vector3.up * arc;
            obj.Rotate(0, 0, spinSpeed * Time.deltaTime);

            time += Time.deltaTime;
            yield return null;
        }

        obj.position = target;
        obj.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void PlayReverseThenForward()
    {
        StopAllCoroutines();
        StartCoroutine(ReverseThenForwardRoutine());
    }

    private IEnumerator ReverseThenForwardRoutine()
    {
        for (int i = 0; i < crosses.Length; i++)
        {
            if (i == 0) continue;
            if (crosses[i] == null) continue;

            Vector3 startTarget = new Vector3(startPos.position.x + (216 * i), startPos.position.y, startPos.position.z);

            float distance = Vector3.Distance(crosses[i].transform.position, startTarget);
            float duration = baseDuration + (distance * distanceMultiplier);

            StartCoroutine(MoveBackToStart(crosses[i].transform, startTarget, duration));
        }

        yield return new WaitForSeconds(1.5f);

        AnimationBuffer();
    }

    private IEnumerator MoveBackToStart(Transform obj, Vector3 target, float duration)
    {
        if (obj == null) yield break;
        if (audioSource != null && shurikenWhoosh != null) audioSource.PlayOneShot(shurikenWhoosh);
        Vector3 start = obj.position;

        float time = 0f;
        float arcHeight = 60f;
        float spinSpeed = 720f;

        while (time < duration)
        {
            float t = time / duration;
            t = Mathf.SmoothStep(0, 1, t);

            Vector3 pos = Vector3.Lerp(start, target, t);
            float arc = Mathf.Sin(t * Mathf.PI) * arcHeight;

            obj.position = pos + Vector3.up * arc;
            obj.Rotate(0, 0, -spinSpeed * Time.deltaTime);

            time += Time.deltaTime;
            yield return null;
        }

        obj.position = target;
        obj.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}