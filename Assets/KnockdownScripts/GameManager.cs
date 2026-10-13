using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private Transform defaultCanTower;

    private Can[] allCans;
    private PlatformTrigger activePlatform;

    public bool IsGameActive { get; private set; } = false;
    public bool IsUnlimited { get; private set; } = false;
    public int BallsLeft { get; private set; } = 0;
    public float TimeRemaining { get; private set; } = 0f;
    public int CansKnocked { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartGame(bool unlimited, int balls, float time, PlatformTrigger sender, Transform platformTower)
    {
        StopAllCoroutines();
        activePlatform = sender;

        Transform towerToUse = platformTower != null ? platformTower : defaultCanTower;

        if (towerToUse != null)
        {
            allCans = towerToUse.GetComponentsInChildren<Can>();
        }
        else
        {
            Debug.LogError($"[GameManager] No Can Tower assigned for platform '{sender.gameObject.name}'!");
            allCans = System.Array.Empty<Can>();
        }

        IsUnlimited = unlimited;
        BallsLeft = balls;
        TimeRemaining = time;
        CansKnocked = 0;
        IsGameActive = true;

        foreach (Can can in allCans)
        {
            if (can != null) can.ResetCan();
        }

        Debug.Log($"Game Started on {sender.gameObject.name}! Tracking {allCans.Length} cans.");
    }

    private void Update()
    {
        if (!IsGameActive || IsUnlimited) return;

        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining <= 0f)
        {
            TimeRemaining = 0f;
            EndGame(false);
        }
    }

    public bool CanThrowBall()
    {
        if (!IsGameActive) return false;
        if (IsUnlimited) return true;
        return BallsLeft > 0;
    }

    public void OnBallThrown()
    {
        if (IsUnlimited) return;

        BallsLeft--;
        if (BallsLeft <= 0)
        {
            StartCoroutine(CheckChallengeResult());
        }
    }

    public void CheckCanStatuses()
    {
        if (!IsGameActive) return;

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

        if (allCans != null)
        {
            foreach (Can can in allCans)
            {
                if (can != null) can.ResetCan();
            }
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