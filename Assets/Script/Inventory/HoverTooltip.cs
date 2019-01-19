using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTooltip : MonoBehaviour
{
    float show_delay = 1.0f;
    float to_visible_dur = 0.5f;
    float to_invisible_dur = 0.2f;
    bool follow = true;

    private void Start()
    {
        StartCoroutine(Display());
    }

    private void Update()
    {
        if(follow)
            transform.position = Input.mousePosition;
    }

    IEnumerator Display()
    {
        bool do_loop = true;
        float show_elapsed_time = 0.0f;
        float elapsed_time = 0.0001f;

        while (do_loop)
        {
            if (show_elapsed_time > show_delay)
            {
                GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, elapsed_time / to_visible_dur);
                elapsed_time += Time.deltaTime;
            }

            show_elapsed_time += Time.deltaTime;

            if (GetComponent<CanvasGroup>().alpha == 1)
                do_loop = false;

            yield return null;
        }
    }

    IEnumerator UnDisplay()
    {
        bool do_loop = true;
        float elapsed_time = 0.0f;
        float start_alpha = GetComponent<CanvasGroup>().alpha;

        while (do_loop)
        {
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(start_alpha, 0, elapsed_time / to_invisible_dur);

            elapsed_time += Time.deltaTime;

            if (GetComponent<CanvasGroup>().alpha == 0)
                do_loop = false;

            yield return null;
        }
    }

    public void Leave()
    {
        StartCoroutine(UnDisplay());
        follow = false;
        Destroy(this.gameObject, to_invisible_dur);
    }
}
