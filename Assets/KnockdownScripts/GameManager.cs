using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform canTower;

    private Can[] allCans;
    private PlatformTrigger activePlatform;
    public bool IsGameActive { get; private set; } = false;
    public bool IsUnlimited { get; private set; } = false;
    public int BallsLeft { get; private set; } = 0;
    public float TimeRemaining { get; private set; } = 0f;
    public int CansKnocked { get; private set; } = 0;
    private int lastLoggedSecond = -1;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        FetchPyramidCans();
    }

    private void FetchPyramidCans()
    {
        if (canTower != null)
            allCans = canTower.GetComponentsInChildren<Can>();
    }

    public void StartGame(bool unlimited, int balls, float time, PlatformTrigger sender)
    {
        StopAllCoroutines();

        activePlatform = sender;

        FetchPyramidCans();

        IsUnlimited = unlimited;
        BallsLeft = balls;
        TimeRemaining = time;
        CansKnocked = 0;
        IsGameActive = true;

        foreach (Can can in allCans)
        {
            if (can != null) can.ResetCan();
        }

        Debug.Log($"Game Started ({sender.gameObject.name})! Total Cans: {allCans.Length}");
    }

    private void Update()
    {
        if (!IsGameActive || IsUnlimited) 
            return;

        TimeRemaining -= Time.deltaTime;

        int currentSecond = Mathf.CeilToInt(TimeRemaining);

        if (currentSecond != lastLoggedSecond && currentSecond >= 0)
        {
            lastLoggedSecond = currentSecond;
            Debug.Log($"[Timer] Time Remaining: {currentSecond}s");
        }

        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            EndGame(false);
        }
    }

    public bool CanThrowBall()
    {
        if (!IsGameActive) 
            return false;
        if (IsUnlimited) 
            return true;
        return BallsLeft > 0;
    }

    public void OnBallThrown()
    {
        if (IsUnlimited) 
            return;

        BallsLeft--;
        if (BallsLeft <= 0)
        {
            StartCoroutine(CheckChallengeResult());
        }
    }

    public void CheckCanStatuses()
    {
        if (!IsGameActive) 
            return;

        int count = 0;
        foreach (Can can in allCans)
        {
            if (can != null && can.IsKnockedDown)
            {
                count++;
            }
        }

        CansKnocked = count;
        Debug.Log($"Knocked Cans: {CansKnocked}/{allCans.Length}");

        if (allCans.Length > 0 && CansKnocked >= allCans.Length)
        {
            EndGame(true);
        }
    }

    private IEnumerator CheckChallengeResult()
    {
        yield return new WaitForSeconds(3f);

        if (IsGameActive)
        {
            CheckCanStatuses();
            if (CansKnocked < allCans.Length)
            {
                EndGame(false);
            }
        }
    }
    public void ResetGameFromPlatform(PlatformTrigger callingPlatform)
    {
        if (activePlatform == callingPlatform)
        {
            ResetGame();
        }
    }

    public void ResetGame()
    {
        StopAllCoroutines(); 
        activePlatform = null;

        IsGameActive = false;
        CansKnocked = 0;
        BallsLeft = 0;
        TimeRemaining = 0f;

        FetchPyramidCans();

        foreach (Can can in allCans)
        {
            if (can != null) 
                can.ResetCan();
        }

        Debug.Log("Game Reset!");
    }

    private void EndGame(bool won)
    {
        IsGameActive = false;

        if (won)
            Debug.Log("WINNER!");
        else
            Debug.Log("LOSER!");
    }
}