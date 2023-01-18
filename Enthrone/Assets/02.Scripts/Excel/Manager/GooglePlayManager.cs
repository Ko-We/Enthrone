using UnityEngine;
using System.Collections;
//using CodeStage.AntiCheat.ObscuredTypes;

// 구글 플레이 사용
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GooglePlayManager : MonoBehaviour
{
    //public GameObject[] GPGSLogInOutButton;
    

    
    //public GameObject[] GPGSLogInOutButton;

    int highScore;
    private void Awake()
    {
        StartGooglePlayActivate();
    }


    public void StartGooglePlayActivate()
    {
#if UNITY_ANDROID
        PlayGamesClientConfiguration _GPGConfig = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(_GPGConfig);
        PlayGamesPlatform.DebugLogEnabled = true;
        
        // 구글 플레이 게임즈 활성화
        PlayGamesPlatform.Activate();

        // 구글 플레이 게임즈 인증
        Social.localUser.Authenticate(StartGooglePlayGamesLoginCallBack);
#endif
    }
    // 구글 플레이 게임즈 인증 콜백 함수



    void StartGooglePlayGamesLoginCallBack(bool result)
    {
        // 인증에 성공 했다면
        if (result)
        {
            //GPGSLogInOutButton[0].SetActive(false);
           // GPGSLogInOutButton[1].SetActive(true);
        }
        else
        {
         //   GPGSLogInOutButton[0].SetActive(true);
           // GPGSLogInOutButton[1].SetActive(false);
        }
    }
    public void LigIne()
    {
        StartGooglePlayActivate();
    }



    public void AchievementSet(int level)
    {
        if (level >= 300)
        {
            AchievementCheck(5);
        }
        else if (level >= 200)
        {
            AchievementCheck(4);
        }
        else if (level >= 100)
        {
            AchievementCheck(3);
        }
        else if (level >= 50)
        {
            AchievementCheck(2);
        }
        else if (level >= 10)
        {
            AchievementCheck(1);
        }
        else if (level == 1)
        {
            AchievementCheck(0);
        }
    }

    public void DragonAchievementSet(int num)
    {
        AchievementCheck(num);
    }


    public void BestLevelReport(int level)
    {
        if (Social.localUser.authenticated)
        {
            //Social.ReportScore(level, GPGSIds.leaderboard_level, LeaderBoardScoreSetCallback);
        }
    }

    void AchievementSetCallback(bool result)
    {

    }
    //public Text test;
    void LeaderBoardScoreSetCallback(bool result)
    {
        // test.text = result.ToString();
    }




    public void AchievementCheck(int num)
    {
        if (Social.localUser.authenticated)
        {
            Social.LoadAchievements(achievements => {
                if (achievements != null)
                {
                    if (!achievements[num].completed)
                    {

                        //if (num == 0)
                        //{
                        //    Social.ReportProgress(GPGSIds.achievement_beginner, 100f, AchievementSetCallback);
                        //}
                        //else if (num == 1)
                        //{
                        //    Social.ReportProgress(GPGSIds.achievement_rookie, 100f, AchievementSetCallback);
                        //}
                        //else if (num == 2)
                        //{
                        //    Social.ReportProgress(GPGSIds.achievement_verteran, 100f, AchievementSetCallback);
                        //}
                        //else if (num == 3)
                        //{
                        //    Social.ReportProgress(GPGSIds.achievement_expert, 100f, AchievementSetCallback);
                        //}
                        //else if (num == 4)
                        //{
                        //    Social.ReportProgress(GPGSIds.achievement_elite, 100f, AchievementSetCallback);
                        //}
                        //else if (num == 5)
                        //{
                        //    Social.ReportProgress(GPGSIds.achievement_master, 100f, AchievementSetCallback);
                        //}
                    }
                }
            });
        }
    }





    public void OnGooglePlayGamesAchievementUI()
    {
        // 구글 업적 UI를 실행함
        if (Social.localUser.authenticated)
        {
            //_messageText.text = "로그아웃 안됨";
            Social.ShowAchievementsUI();
        }
        else
        {
            //_messageText.text = "재로그인";
            Social.localUser.Authenticate(StartGooglePlayGamesLoginCallBack);
        }
    }


    public void OnGooglePlayGamesLeaderBoardUI()
    {
#if UNITY_ANDROID
        if (Social.localUser.authenticated)
        {
            //_messageText.text = "로그아웃 안됨";
            //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(GPGSIds.leaderboard_level);
        }
        else
        {
            //_messageText.text = "재로그인";
            Social.localUser.Authenticate(StartGooglePlayGamesLoginCallBack);
        }
#endif
    }

    public void GooglePlayReActive()
    {
        Social.localUser.Authenticate(StartGooglePlayGamesLoginCallBack);
    }
    public void OnGooglePlayLogoutButtonClick()
    {
        //_messageText.text = "";
        GooglePlayDeActive();
    }

    // 구글 플레이 게임즈 인증 해제 요청
    public void GooglePlayDeActive()
    {
#if UNITY_ANDROID
        PlayGamesPlatform play = (PlayGamesPlatform)Social.Active;
        if (play != null)
        {
            // 구글 게임즈 플레이로 인증 했을 경우 인증을 해제함
            play.SignOut();
            //_messageText.text = "구글 플레이 로그아웃 성공함";
            //GPGSLogInOutButton[0].SetActive(true);
            //GPGSLogInOutButton[1].SetActive(false);
        }
        else
        {
            //_messageText.text = "구글 플레이에 로그인 하지 않았음";

        }
#endif
    }

    

}
