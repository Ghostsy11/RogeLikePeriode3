using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponVanish : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float disSolveTime = 1.0f;
    [SerializeField] SpriteRenderer spriteRenderer;
    private int disSolveAmount = Shader.PropertyToID("_DissolveAmount");
    private int verticalSolveAmount = Shader.PropertyToID("_VerticalDissolve");

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("Was Preesed");
            StartCoroutine(Vanish(true, false));
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("Was Preesedd");
            StartCoroutine(Apper(true));

        }

    }


    private IEnumerator Vanish(bool useDissovle, bool useApper)
    {
        float elapsedTime = 0f;
        while (elapsedTime < disSolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDisSolve = Mathf.Lerp(0, 1f, (elapsedTime / disSolveTime));
            float lerpedDisSolveVertical = Mathf.Lerp(0, 1.1f, (elapsedTime / disSolveTime));

            if (useDissovle)
            {
                spriteRenderer.material.SetFloat(disSolveAmount, lerpedDisSolve);

            }
            if (useApper)
            {

                spriteRenderer.material.SetFloat(verticalSolveAmount, lerpedDisSolveVertical);
            }

            yield return null;
            StopCoroutine(Vanish(false, false));
        }
    }

    private IEnumerator Apper(bool useApper)
    {
        float elapsedTime = 0f;
        while (elapsedTime < disSolveTime)
        {
            spriteRenderer.material.SetFloat(disSolveAmount, 0);
            spriteRenderer.material.SetFloat(verticalSolveAmount, 1);

            elapsedTime += Time.deltaTime;
            float lerpedDisSolve = Mathf.Lerp(1, 0f, (elapsedTime / disSolveTime));
            float lerpedDisSolveVer = Mathf.Lerp(1, 0f, (elapsedTime / disSolveTime));
            if (useApper)
            {
                //spriteRenderer.material.SetFloat(disSolveAmount, lerpedDisSolve);
                spriteRenderer.material.SetFloat(verticalSolveAmount, lerpedDisSolveVer);

            }
            yield return null;
            StopCoroutine(Apper(false));
        }
    }

}
