using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using System.Text;
//using NaughtyAttributes;

public class Remote_Config // : Singleton<Remote_Config>
{
    //public GameObject go_check_version_panel;
    //public UILabel lb_app_version;
    //public UILabel lb_recent_version;
    //string recent_version;

    //StringBuilder stringbuilder;
    //string coupon_value;
    //string[] split_coupon;

    //string personal_value;
    //string day;
    //internal string sale_day;

    //WaitForSecondsRealtime time = new WaitForSecondsRealtime(300);
    //bool bl_force_quit = false;
    //bool bl_Inspection = false;

    //public struct userAttirbutes { };
    //public struct appAttirbutes { };

    //private void OnEnable()
    //{
    //    StartCoroutine(Force_Quit());
    //    //ConfigManager.FetchCompleted += Version_Check;
    //    //ConfigManager.FetchConfigs<userAttirbutes, appAttirbutes>(new userAttirbutes(), new appAttirbutes());


    //    ConfigManager.FetchCompleted += Inspection;
    //    ConfigManager.FetchConfigs<userAttirbutes, appAttirbutes>(new userAttirbutes(), new appAttirbutes());

    //    ConfigManager.FetchCompleted += Check_SaleDay;
    //    ConfigManager.FetchConfigs<userAttirbutes, appAttirbutes>(new userAttirbutes(), new appAttirbutes());
    //}

    //IEnumerator Force_Quit()
    //{
    //    while (true)
    //    {
    //        yield return time;

    //        ConfigManager.FetchCompleted += Check_Remote;
    //        ConfigManager.FetchConfigs<userAttirbutes, appAttirbutes>(new userAttirbutes(), new appAttirbutes());
    //    }
    //}

    //public void Inspection(ConfigResponse response)
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)  //인터넷 연결되어 있지 않는 상태이면
    //    {

    //        UIManager.Instance.Open_Gameobject(Openning_M.Instance.go_internet_panel);
    //    }
    //    else
    //    {
    //        bl_Inspection = ConfigManager.appConfig.GetBool("Inspection");
    //        ConfigManager.FetchCompleted -= Inspection;

    //        if (bl_Inspection)
    //        {
    //            UIManager.Instance.Open_Gameobject(Openning_M.Instance.go_Emergency_panel);
    //        }
    //        else
    //        {
    //            ConfigManager.FetchCompleted += Version_Check;
    //            ConfigManager.FetchConfigs<userAttirbutes, appAttirbutes>(new userAttirbutes(), new appAttirbutes());
    //        }

    //    }
    //}


    //void Check_Remote(ConfigResponse response)
    //{
    //    bl_force_quit = ConfigManager.appConfig.GetBool("ForceQuit");
    //    recent_version = ConfigManager.appConfig.GetString("Version");

    //    if (Application.version.ToString() != recent_version)
    //    {
    //        if (bl_force_quit) Application.Quit();
    //    }

    //    ConfigManager.FetchCompleted -= Check_Remote;
    //}

    //public void Version_Check(ConfigResponse response)
    //{
    //    if (Application.internetReachability == NetworkReachability.NotReachable)  //인터넷 연결되어 있지 않는 상태이면
    //    {

    //        UIManager.Instance.Open_Gameobject(Openning_M.Instance.go_internet_panel);
    //    }
    //    else
    //    {
    //        recent_version = ConfigManager.appConfig.GetString("Version");

    //        if (float.Parse(Application.version) < float.Parse(recent_version))
    //        {
    //            lb_app_version.text = Application.version;
    //            lb_recent_version.text = recent_version;
    //            go_check_version_panel.SetActive(true);
    //        }

    //        ConfigManager.FetchCompleted -= Version_Check;
    //    }
    //}

    //public void App_Update_Btn()
    //{
    //    //ConfigManager.FetchCompleted -= Version_Check;
    //    Application.OpenURL("market://details?id=com.artifact.uforpg");
    //}

    //public void Exit_Btn()
    //{
    //    //ConfigManager.FetchCompleted -= Version_Check;
    //    UIManager.Instance.ExitGame();
    //}

    //public void Coupon_Btn()
    //{
    //    ConfigManager.FetchCompleted += Coupon_Check;
    //    ConfigManager.FetchConfigs<userAttirbutes, appAttirbutes>(new userAttirbutes(), new appAttirbutes());
    //}

    //public void Personal_Btn()
    //{
    //    ConfigManager.FetchCompleted += Personal_Reward;
    //    ConfigManager.FetchConfigs<userAttirbutes, appAttirbutes>(new userAttirbutes(), new appAttirbutes());
    //}

    //public void Coupon_Check(ConfigResponse response)
    //{
    //    coupon_value = ConfigManager.appConfig.GetString(Menu_M.Instance.lb_coupon.text);

    //    if (coupon_value != "")
    //    {
    //        if (Save_M.Instance.coupon.Check_coupon(coupon_value))  //쿠폰을 사용한 적 없다면
    //        {
    //            split_coupon = coupon_value.Split('/');

    //            for (int i = 0; i < split_coupon.Length; i++)  //아이템 지급하는 부분
    //            {
    //                if (split_coupon[i] != "0")
    //                {
    //                    MailBox_M.Instance.Set_Mail_Item(i, int.Parse(split_coupon[i]), 1);
    //                    Debug.Log(i + " / " + split_coupon[i]);
    //                }

    //            }
    //            Save_M.Instance.coupon.Use_Coupon(coupon_value);
    //            UIManager.Instance.Show_Message("MESSAGE_031");
    //        }
    //        else
    //        {
    //            UIManager.Instance.Show_Message("MESSAGE_032");
    //        }
    //    }
    //    else
    //    {
    //        UIManager.Instance.Show_Message("MESSAGE_030");
    //    }

    //    ConfigManager.FetchCompleted -= Coupon_Check;
    //}

    //public void Personal_Reward(ConfigResponse response)
    //{
    //    personal_value = ConfigManager.appConfig.GetString(R_GameM.Instance.lb_g_id.text);

    //    if (personal_value != "")
    //    {
    //        split_coupon = personal_value.Split('/');
    //        day = split_coupon[split_coupon.Length - 1];  //마지막에 입력된 날짜

    //        if (Save_M.Instance.coupon.Check_Personal(day))  //리스트에 있는 날짜와 코드날짜 비교
    //        {
    //            //사용하지않은거라면
    //            for (int i = 0; i < split_coupon.Length - 1; i++)
    //            {
    //                if (split_coupon[i] != "0")
    //                {
    //                    MailBox_M.Instance.Set_Mail_Item(i, int.Parse(split_coupon[i]), 2);
    //                }
    //            }
    //            Save_M.Instance.coupon.Use_Personal(day);
    //        }

    //    }

    //    ConfigManager.FetchCompleted -= Personal_Reward;
    //    MailBox_M.Instance.Check_Mail();
    //}

    //void Check_SaleDay(ConfigResponse response)
    //{
    //    sale_day = ConfigManager.appConfig.GetString("SaleDay");


    //    ConfigManager.FetchCompleted -= Check_SaleDay;
    //}
}