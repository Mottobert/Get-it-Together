using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicFaceChangeController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] leftLooking;
    [SerializeField]
    private Sprite[] rightLooking;

    [SerializeField]
    private Sprite[] upLooking;
    [SerializeField]
    private Sprite[] downLooking;

    [SerializeField]
    private Sprite[] happyLooking;
    [SerializeField]
    private Sprite[] jumpLooking;

    [SerializeField]
    private Image image;

    [SerializeField]
    private ComicFaceChangeManager faceChangeManager;

    private Sprite[] activeSprites;

    private int spriteIndex = 0;

    private void Start()
    {
        StartHappyLooking();
    }

    public void StartLeftLooking()
    {
        StopAllCoroutines();
        activeSprites = leftLooking;
        image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        spriteIndex = 0;
        CycleSprites();
    }

    public void StartRightLooking()
    {
        StopAllCoroutines();
        activeSprites = rightLooking;
        image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        spriteIndex = 0;
        CycleSprites();
    }

    public void StartUpLooking()
    {
        StopAllCoroutines();
        activeSprites = upLooking;
        image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        spriteIndex = 0;
        CycleSprites();
    }

    public void StartDownLooking()
    {
        StopAllCoroutines();
        activeSprites = downLooking;
        image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        spriteIndex = 0;
        CycleSprites();
    }

    public void StartHappyLooking()
    {
        StopAllCoroutines();
        activeSprites = happyLooking;
        image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        spriteIndex = 0;
        CycleSprites();
    }

    public void StartJumpLooking()
    {
        StopAllCoroutines();
        activeSprites = jumpLooking;
        image.gameObject.transform.localScale = new Vector3(1, 1, 1);
        spriteIndex = 0;
        CycleSprites();
    }

    public void CycleSprites()
    {
        image.sprite = activeSprites[spriteIndex];

        if(activeSprites.Length > 1)
        {
            StartCoroutine("CycleSpriteIndex", activeSprites.Length);
        }
    }

    IEnumerator CycleSpriteIndex(int maxIteration)
    {
        if(spriteIndex < maxIteration - 1)
        {
            spriteIndex++;
        }
        else
        {
            spriteIndex = 0;
        }
        
        yield return new WaitForSeconds(3f);

        CycleSprites();
    }
}
