using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour
{
    bool flash = true;

    public bool nowFlash = false;

    SpriteRenderer rend;

    float time = 0.0f;

    public float winceTime = 1.0f;

    // Use this for initialization
    public void CharaBlinker()
    {
        rend = GetComponent<SpriteRenderer>();
        if (nowFlash == false)
        {
            StartCoroutine("Flashing");
        }
    }

    IEnumerator Flashing()
    {
        nowFlash = true;

        while (flash && time < winceTime)
        {
            time += Time.deltaTime * 10;
            yield return new WaitForSeconds(0.1f);
            rend.enabled = !rend.enabled;
            yield return true;
        }
        time = 0.0f;
        rend.enabled = true;
        nowFlash = false;
        yield break;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextTime)
        {
            //renderer.enabled = !renderer.enabled;
            rend.enabled = !rend.enabled;

            nextTime += interval;
        }
    }
    */
}
