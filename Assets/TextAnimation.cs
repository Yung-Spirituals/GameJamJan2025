using UnityEngine;
using TMPro;
using System.Collections;

public class TextAnimation : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] float timeBetweenCharacters;
    [SerializeField] float timeBetweenWords;
    int i = 0;


    public string[] stringarray;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EndCheck();
    }

    void EndCheck()
    {
        if(i<= stringarray.Length - 1)
        {
            _textMeshPro.text = stringarray[i];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        _textMeshPro.ForceMeshUpdate();
        int totalVisibleChar = _textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleChar + 1);
            _textMeshPro.maxVisibleCharacters = visibleCount;

            if(visibleCount >= totalVisibleChar)
            {
                i += 1;
                Invoke("EndCheck", timeBetweenWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}
