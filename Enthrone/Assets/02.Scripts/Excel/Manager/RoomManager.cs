using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using WeightedRandomization;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance // singlton
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<RoomManager>();
                if (instance == null)
                {
                    var instanceContainer = new GameObject("RoomManager");
                    instance = instanceContainer.AddComponent<RoomManager>();
                }
            }
            return instance;
        }
    }
    private static RoomManager instance;

    MyObject myChar;

    public Camera _camera;
    public GameObject StageInfo_Sign;
    public int RoomNum;
    [SerializeField]
    private GameObject Player;
    public Transform PlayerCamera;
    public Transform CAMERAPOS;
    public GameObject CongEffect;
    [SerializeField]
    private GameObject MonsterProjectile;
    [SerializeField]
    private GameObject PlayerObjectManager;

    public GameObject Stage;
    public GameObject Stage_Parent;
    public GameObject DontTouchBtn_Panel;
    public GameObject BossGfitBox;
    public GameObject BossGiftBoxEffect;
    public GameObject CurrentDoor;
    public GameObject CurrentStage;
    public GameObject EquipmentItem;
    public GameObject ActiveItem;
    public GameObject Tutorial_Stone;
    public List<int> RandomStage = new List<int>();
    public bool[] ShopCostboard = new bool[5];
    public GameObject[] ShopBoard;

    //public List<GameObject> UniqueItem = new List<GameObject>();
    //public List<GameObject> Equipment = new List<GameObject>();
    public List<GameObject> altarNum = new List<GameObject>();
    public List<GameObject> ActivealtarNum = new List<GameObject>();
    public List<int> CarryEquipment = new List<int>();
    public List<int> NormalEquipment = new List<int>();
    public List<int> RareEquipment = new List<int>();
    public List<int> UniqueEquipment = new List<int>();
    
    public List<int> CarryAntique = new List<int>();
    public List<int> NormalAntique = new List<int>();
    public List<int> RareAntique = new List<int>();
    public List<int> UniqueAntique = new List<int>();


    private IEnumerator GiftBox;

    public bool BossRewardDropCheck = false;

    public int TutoCheck;
    public bool TutoNextMapStartCheck = false;

    [System.Serializable] //이걸 안해주면 2차원 배열 안보임
    public class SmallPositionArray
    {
        public List<GameObject> StartPosition = new List<GameObject>();
    }
    [System.Serializable] //이걸 안해주면 2차원 배열 안보임
    public class MidiumPositionArray
    {
        public List<GameObject> StartPosition = new List<GameObject>();
    }
    [System.Serializable] //이걸 안해주면 2차원 배열 안보임
    public class LargePositionArray
    {
        public List<GameObject> StartPosition = new List<GameObject>();
    }
    [System.Serializable] //이걸 안해주면 2차원 배열 안보임
    public class BossPositionArray
    {
        public List<GameObject> StartPosition = new List<GameObject>();
    }
    [System.Serializable] //이걸 안해주면 2차원 배열 안보임
    public class FinalBossPositionArray
    {
        public List<GameObject> StartPosition = new List<GameObject>();
    }
    //보스 방
    

    public int currentStage = 0;

    int LastStage = 5;

    public bool OpenDoorCheck = false;

    public bool MidiumRoom;
    public bool LargeRoom;
    public bool EquipmentRoom;      //장비방
    public bool AntiqueRoom;        //유물방

    public SmallPositionArray[] SmallPositionArrays;
    public MidiumPositionArray[] MidiumPositionArrays;
    public LargePositionArray[] LargePositionArrays;
    public BossPositionArray[] bossPositionArrays;
    public FinalBossPositionArray[] FinalBossPositionArrays;

    //장비 방
    public GameObject EquipmentRoomPostion;
    //유물 방
    public GameObject AntiqueRoomPostion;

    public List<Sprite> FinalRoomBackground = new List<Sprite>();
    public List<GameObject> TutorialStage = new List<GameObject>();
    public List<Sprite> TutorialRoomBackground = new List<Sprite>();
    public GameObject Tutorial_Parent;
    public GameObject Tutorial_Map;
    public GameObject StartPostionEnthrone;

    public bool playerInThisRoom = false;
    public bool isClearRoom = false;
    // Start is called before the first frame update
    private void Awake()
    {
        myChar = MyObject.MyChar;        

        GameObject Players = GameObject.Find("Player");

        for (int i = 0; i < Players.transform.childCount; i++)
        {
            if (Players.transform.GetChild(i).gameObject.activeSelf == true)
            {
                Player = Players.transform.GetChild(i).gameObject;
            }
        }

    }
    void Start()
    {
        if (!myChar.Tutorial)
        {
            NextStage();

            Time.timeScale = 0.0f;
        }
        else if (myChar.Tutorial)
        {
            Tutorial();
            myChar.SelectHero = 1;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCheck();
        if (!myChar.Tutorial)
        {
            DoorCheck();
            Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

            if ((myChar.Stage % 10) != 9)
            {
                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
            }
            StageInfo_Sign.transform.GetChild(0).GetComponent<TextMeshPro>().text = (myChar.Stage + 1).ToString();
            if (myChar.Stage % 10 == 1)
            {
                myChar.GiftboxDropCheck = false;
            }         
            if (myChar.Stage % 10 != 0)
            {
                BossRewardDropCheck = false;
            }
            if ((myChar.Chapter + 1 % 10) == 3 || (myChar.Chapter + 1 % 10) == 6)
            {
                switch (RoomNum)
                {
                    case 1:
                        myChar.SelectLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = FinalRoomBackground[0];
                        break;
                    case 2:
                        myChar.SelectLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = FinalRoomBackground[1];
                        break;
                    case 3:
                        myChar.SelectLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = FinalRoomBackground[2];
                        break;
                }
            }
        }
        else if (myChar.Tutorial)
        {
            if (myChar.TutorialNum != 5)
            {
                myChar.GiftboxDropCheck = false;
            }
            Tutorial();
            switch (RoomNum)
            {
                case 1:
                    myChar.SelectLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TutorialRoomBackground[0];
                    break;
                case 2:
                    myChar.SelectLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TutorialRoomBackground[1];
                    break;
                case 3:
                    myChar.SelectLocation.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = TutorialRoomBackground[2];
                    break;
            }
        }

        if (myChar.Stage % 10 == 3 || myChar.Stage % 10 == 6)
        {
            for (int i = 0; i < ShopCostboard.Length; i++)
            {
                if (ShopCostboard[i])
                {
                    ShopBoard[i].SetActive(true);
                }
                else
                {
                    ShopBoard[i].SetActive(false);
                }
            }
        }

    }

    private void PlayerCheck()
    {
        GameObject Players = GameObject.Find("Player");

        for (int i = 0; i < Players.transform.childCount; i++)
        {
            if (Players.transform.GetChild(i).gameObject.activeSelf == true)
            {
                Player = Players.transform.GetChild(i).gameObject;
            }
        }
    }

    private void DoorCheck()
    {
        //방에 따라 문 종류 오픈해주기 0 : 일반 문 / 1 : 보스 문 / 2 : 최종보스 문 / 3 : 보상 문 / 4 : 상점 문
        if (((myChar.Stage % 10) == 0) || ((myChar.Stage % 10) == 1) || ((myChar.Stage % 10) == 3) || 
            ((myChar.Stage % 10) == 4) || ((myChar.Stage % 10) == 6) || ((myChar.Stage % 10) == 7))
        {
            CurrentDoor = CurrentStage.transform.parent.Find("Door").transform.GetChild(0).gameObject;
            CurrentDoor.SetActive(true);
        }
        else if (((myChar.Stage % 10) == 2) || ((myChar.Stage % 10) == 5))
        {
            CurrentDoor = CurrentStage.transform.parent.Find("Door").transform.GetChild(4).gameObject;
            CurrentDoor.SetActive(true);
        }
        else if (((myChar.Stage % 10) == 9))
        {
            if ((myChar.Chapter + 1 % 10) != 3 && (myChar.Chapter + 1 % 10) != 6)
            {
                if (myChar.Stage != 49)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CurrentDoor = CurrentStage.transform.parent.Find("Door_" + i).transform.GetChild(1).gameObject;
                        CurrentDoor.SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CurrentDoor = CurrentStage.transform.parent.Find("Door_" + i).transform.GetChild(2).gameObject;
                        CurrentDoor.SetActive(true);
                    }
                }
            }
            else if ((myChar.Chapter + 1 % 10) == 3 || (myChar.Chapter + 1 % 10) == 6)
            {
                if (myChar.Stage != 29)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CurrentDoor = CurrentStage.transform.parent.Find("Door_" + i).transform.GetChild(1).gameObject;
                        CurrentDoor.SetActive(true);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CurrentDoor = CurrentStage.transform.parent.Find("Door_" + i).transform.GetChild(2).gameObject;
                        CurrentDoor.SetActive(true);
                    }
                }
            }
            
            
        }
        else if (((myChar.Stage % 10) == 8))
        {
            CurrentDoor = CurrentStage.transform.parent.Find("Door").transform.GetChild(3).gameObject;
            CurrentDoor.SetActive(true);
        }

    }

    //몬스터 처리후 다음맵으로 이동하는 문열어주는 코드
    public void DoorOpen()
    {
        if (!myChar.Tutorial)
        {
            if (((myChar.Stage % 10) != 8) && ((myChar.Stage % 10) != 9))
            {
                if (CurrentDoor)
                {
                    CurrentDoor.GetComponent<Animator>().enabled = true;
                }
                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                //if (myChar.Stage != myChar.BasicStage + 2)
                //{
                //    if (CurrentDoor)
                //    {
                //        CurrentDoor.GetComponent<Animator>().enabled = true;
                //    }
                //}
                //else
                //{
                //    if (CurrentDoor)
                //    {
                //        CurrentDoor.GetComponent<Animator>().enabled = true;
                //    }
                //}
            }
            else if (((myChar.Stage % 10) == 8))
            {
                myChar.RewardGetCheck = false;
                CurrentDoor.GetComponent<Animator>().enabled = true;
            }
            else if ((myChar.Stage % 10) == 9)
            {
                if (myChar.RewardGetCheck)
                {
                    if ((myChar.Chapter + 1 % 10) != 3 && (myChar.Chapter + 1 % 10) != 6)
                    {
                        if (myChar.Stage < 49)
                        {
                            if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(0).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 0).transform.GetChild(1).gameObject;
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(1).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 1).transform.GetChild(1).gameObject;
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;                                
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(2).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 2).transform.GetChild(1).gameObject;
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else
                            {
                                StageInfo_Sign.SetActive(false);
                            }
                        }
                        else
                        {
                            if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(0).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 0).transform.GetChild(2).gameObject;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(1).transform.GetChild(0).transform.childCount == 0)
                            {                                
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 1).transform.GetChild(2).gameObject;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(2).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 2).transform.GetChild(2).gameObject;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else
                            {
                                StageInfo_Sign.SetActive(false);
                            }
                        }
                    }
                    else if ((myChar.Chapter + 1 % 10) == 3 || (myChar.Chapter + 1 % 10) == 6)
                    {
                        if (myChar.Stage < 29)
                        {
                            if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(0).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 0).transform.GetChild(1).gameObject;
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;                                
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(1).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 1).transform.GetChild(1).gameObject;
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;                                
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(2).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 2).transform.GetChild(1).gameObject;
                                //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;                                
                            }
                            else
                            {
                                StageInfo_Sign.SetActive(false);
                            }
                        }
                        else
                        {
                            if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(0).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 0).transform.GetChild(2).gameObject;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(1).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 1).transform.GetChild(2).gameObject;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(2).transform.GetChild(0).transform.childCount == 0)
                            {
                                CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 2).transform.GetChild(2).gameObject;
                                StageInfo_Sign.SetActive(true);
                                StageInfo_Sign.transform.position = CurrentDoor.transform.position + new Vector3(0, 1, 0);
                                CurrentDoor.GetComponent<Animator>().enabled = true;
                            }
                            else
                            {
                                StageInfo_Sign.SetActive(false);
                            }
                        }
                    }
                    
                }

                //맵의 altar자리의 하단에 아이템이 있는지를 확인하기위해선 이코드를 수정해서써야뒴 윗코드는 시작하자마자 리스트에 넣어놓은 곳만비교 잘못됨 추후수정필요
                //if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(0).transform.GetChild(0).childCount == 0)
                //{
                //    CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 0).transform.GetChild(0).gameObject;
                //    //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                //    CurrentDoor.GetComponent<Animator>().enabled = true;
                //}
                //else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(1).transform.GetChild(0).childCount == 0)
                //{
                //    CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 1).transform.GetChild(0).gameObject;
                //    //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                //    CurrentDoor.GetComponent<Animator>().enabled = true;
                //}
                //else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(2).transform.GetChild(0).childCount == 0)
                //{
                //    CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 2).transform.GetChild(0).gameObject;
                //    //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                //    CurrentDoor.GetComponent<Animator>().enabled = true;
                //}
            }
        }
    }

    public void NextStage()
    {
        StageInfo_Sign.SetActive(false);
        if ((myChar.Chapter + 1 % 10) != 3 && (myChar.Chapter + 1 % 10) != 6)
        {
            BasicStage();
        }
        else
        {
            HeroStage();
        }
        if (MonsterProjectile.transform.childCount > 0)
        {
            for (int i = 0; i < MonsterProjectile.transform.childCount; i++)
            {
                Destroy(MonsterProjectile.transform.GetChild(i).gameObject);
            }
        }
        if (PlayerObjectManager.transform.childCount > 0)
        {
            for (int i = 0; i < PlayerObjectManager.transform.childCount; i++)
            {
                Destroy(PlayerObjectManager.transform.GetChild(i).gameObject);
            }
        }
       
    }
    private void BasicStage()
    {
        if (myChar.Stage == 50)
        {
            //myChar.Finished = true;
            //myChar.CrownGetBgm = true;
            GameManager.Instance.WinPanelOpen(0f);
            GameManager.Instance.ClearSkip_Btn.SetActive(true);
            return;
        }
        GameManager.Instance.StageClear();
        myChar.MHpMultiIndex++;
        //if (myChar.Stage == 0)
        //{
        //    //GameManager.Instance.LoadingPanel.SetActive(true);
        //    int StageNum = Random.Range(0, RandomStage.Count);
        //    myChar.SelectedStage = RandomStage[StageNum];
        //    RandomStage.RemoveAt(StageNum);
        //}
        if (myChar.SelectLocation)
        {
            Destroy(myChar.SelectLocation.transform.parent.gameObject);
        }
        myChar.Stage++;
        //맵 크기 체크
        MapCheck(); 

        // 최종보스클리어 후 나올화면
        if (currentStage > LastStage) { return; }

        if (myChar.Stage % 10 != 0) // 일반맵
        {
            if (!MidiumRoom && !LargeRoom && !EquipmentRoom && !AntiqueRoom)
            {
                //int arrayIndex = myChar.Chapter;
                int arrayIndex = myChar.SelectedStage;

                //스테이지 랜덤 선택
                int randomIndex = Random.Range(0, SmallPositionArrays[myChar.Chapter].StartPosition.Count);

                Stage = Instantiate(SmallPositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);


                //해당 방에 진입했을때 몬스터 오픈해줌
                Stage.transform.Find("Monster").gameObject.SetActive(true);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                //PlayerCamera.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;

                SmallPositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else if (MidiumRoom)
            {
                //스테이지 랜덤 선택
                int randomIndex = Random.Range(0, MidiumPositionArrays[myChar.Chapter].StartPosition.Count);

                Stage = Instantiate(MidiumPositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);


                //해당 방에 진입했을때 몬스터 오픈해줌
                Stage.transform.Find("Monster").gameObject.SetActive(true);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                //PlayerCamera.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;

                MidiumPositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else if (LargeRoom)
            {
                int randomIndex = Random.Range(0, LargePositionArrays[myChar.Chapter].StartPosition.Count);

                Stage = Instantiate(LargePositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);


                //해당 방에 진입했을때 몬스터 오픈해줌
                Stage.transform.Find("Monster").gameObject.SetActive(true);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                //PlayerCamera.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;

                LargePositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else if (EquipmentRoom)
            {
                for (int i = 0; i < ShopCostboard.Length; i++)
                {
                    ShopCostboard[i] = true;
                }

                myChar.GiftboxDropCheck = false;

                Stage = Instantiate(EquipmentRoomPostion, transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                HeroStartPostion(CurrentStage.transform.position);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                myChar.ActiveItemCost.Clear();
                GameManager.Instance.StoreUI.SetActive(true);
                ItemManager();

                GameManager.Instance.StoreCostCheck();
            }
            else if (AntiqueRoom)
            {
                myChar.GiftboxDropCheck = false;

                Stage = Instantiate(AntiqueRoomPostion, transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                //보스방 진입전 상태 체크를위해 false로 변경해주는것
                myChar.StartCheck = false;

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);
                
                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                AntiqueManager();
            }
            //else if (myChar.Chapter >= 3)
            //{
            //    BossRewardDropCheck = false;
            //    myChar.SelectedStage = 4;
            //    Stage = Instantiate(StartPostionEnthrone, transform.position, Quaternion.identity);
            //    Stage.transform.parent = Stage_Parent.transform;
            //    Stage.transform.localPosition = new Vector3(0, 0, 0);

            //    CurrentStage = Stage.transform.Find("StartPostion").gameObject;

            //    Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
            //    //Player.transform.position = CurrentStage.transform.position;
            //    HeroStartPostion(CurrentStage.transform.position);
            //    Stage.transform.Find("Monster").gameObject.SetActive(true);
            //    DontTouchBtn_Panel.SetActive(true);

            //    myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

            //    CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

            //    for (int i = 0; i < myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).childCount; i++)
            //    {
            //        if (i == (myChar.EnthroneHeroNum))
            //        {
            //            myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(true);
            //        }
            //        else if (i != (myChar.EnthroneHeroNum))
            //        {
            //            myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(false);
            //        }
            //    }

            //    /*이전 코드*/
            //    //BossRewardDropCheck = false;
            //    //myChar.SelectedStage = 4;
            //    //CurrentStage = StartPostionEnthrone;
            //    //Player.transform.parent.transform.position = CurrentStage.transform.parent.Find("BackGround").transform.position;
            //    ////Player.transform.position = CurrentStage.transform.position;
            //    //HeroStartPostion(CurrentStage.transform.position);
            //    //CurrentStage.transform.parent.Find("Monster").gameObject.SetActive(true);
            //    //DontTouchBtn_Panel.SetActive(true);

            //    //myChar.SelectLocation = CurrentStage.transform.parent.Find("BackGround").gameObject;

            //    //CAMERAPOS.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;

            //    //for (int i = 0; i < myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).childCount; i++)
            //    //{
            //    //    if (i == (myChar.EnthroneHeroNum))
            //    //    {
            //    //        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(true);
            //    //    }
            //    //    else if (i != (myChar.EnthroneHeroNum))
            //    //    {
            //    //        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(false);
            //    //    }
            //    //}
            //    /*이전 코드*/
            //}
        }
        else
        {
            if (myChar.Stage != 50)
            {
                int randomIndex = Random.Range(0, bossPositionArrays[myChar.Chapter].StartPosition.Count);
                BossRewardDropCheck = false;
                Stage = Instantiate(bossPositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);
                Stage.transform.Find("Monster").gameObject.SetActive(true);
                DontTouchBtn_Panel.SetActive(true);

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                bossPositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else
            {
                BossRewardDropCheck = false;
                Stage = Instantiate(FinalBossPositionArrays[myChar.Chapter].StartPosition[0], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);
                Stage.transform.Find("Monster").gameObject.SetActive(true);
                DontTouchBtn_Panel.SetActive(true);

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                //for (int i = 0; i < myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).childCount; i++)
                //{
                //    if (i == (myChar.EnthroneHeroNum))
                //    {
                //        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(true);
                //    }
                //    else if (i != (myChar.EnthroneHeroNum))
                //    {
                //        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(false);
                //    }
                //}
            }
            //BossRewardDropCheck = false;
            //myChar.SelectedStage = 4;
            //Stage = Instantiate(StartPostionEnthrone, transform.position, Quaternion.identity);
            //Stage.transform.parent = Stage_Parent.transform;
            //Stage.transform.localPosition = new Vector3(0, 0, 0);

            //CurrentStage = Stage.transform.Find("StartPostion").gameObject;

            //Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
            ////Player.transform.position = CurrentStage.transform.position;
            //HeroStartPostion(CurrentStage.transform.position);
            //Stage.transform.Find("Monster").gameObject.SetActive(true);
            //DontTouchBtn_Panel.SetActive(true);

            //myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

            //CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

            //for (int i = 0; i < myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).childCount; i++)
            //{
            //    if (i == (myChar.EnthroneHeroNum))
            //    {
            //        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(true);
            //    }
            //    else if (i != (myChar.EnthroneHeroNum))
            //    {
            //        myChar.SelectLocation.transform.parent.Find("Throne").GetChild(1).GetChild(i).gameObject.SetActive(false);
            //    }
            //}
            //Shop맵
            //if (myChar.Stage == (myChar.BasicStage + 2))
            //{
            //    for (int i = 0; i < ShopCostboard.Length; i++)
            //    {
            //        ShopCostboard[i] = true;
            //    }
            //    myChar.StartCheck = false;

            //    Stage = Instantiate(StartPostionShop, transform.position, Quaternion.identity);
            //    Stage.transform.parent = Stage_Parent.transform;
            //    Stage.transform.localPosition = new Vector3(0, 0, 0);

            //    CurrentStage = Stage.transform.Find("StartPostion").gameObject;
            //    Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

            //    //Player.transform.position = StartPostionShop[myChar.SelectedStage].transform.position;
            //    HeroStartPostion(CurrentStage.transform.position);
            //    Stage.transform.Find("Shop").gameObject.SetActive(true);

            //    myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

            //    CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;
            //    GameManager.Instance.StoreUI.SetActive(true);
            //    ActiveItemManager();
            //    GameManager.Instance.StoreCostCheck();

            //    /*이전코드*/
            //    //CurrentStage = StartPostionShop[myChar.SelectedStage];
            //    //Player.transform.parent.transform.position = StartPostionShop[myChar.SelectedStage].transform.parent.Find("BackGround").transform.position;
            //    ////Player.transform.position = StartPostionShop[myChar.SelectedStage].transform.position;
            //    //HeroStartPostion(StartPostionShop[myChar.SelectedStage].transform.position);
            //    //StartPostionShop[myChar.SelectedStage].transform.parent.Find("Shop").gameObject.SetActive(true);

            //    //myChar.SelectLocation = CurrentStage.transform.parent.Find("BackGround").gameObject;

            //    //CAMERAPOS.transform.position = StartPostionShop[myChar.SelectedStage].transform.parent.Find("CameraPos").position;
            //    //GameManager.Instance.StoreUI.SetActive(true);
            //    //ActiveItemManager();
            //    //GameManager.Instance.StoreCostCheck();
            //    /*이전코드*/

            //    //PlayerCamera.transform.position = StartPostionShop[myChar.SelectedStage].transform.parent.Find("CameraPos").position;
            //}
            //Boss맵
            //else if (myChar.Stage == (myChar.BasicStage + 3))
            //{
            //    BossRewardDropCheck = false;
            //    GameManager.Instance.StoreUI.SetActive(false);
            //    DontTouchBtn_Panel.SetActive(true);

            //    Stage = Instantiate(StartPostionBoss[myChar.SelectedStage], transform.position, Quaternion.identity);
            //    Stage.transform.parent = Stage_Parent.transform;
            //    Stage.transform.localPosition = new Vector3(0, 0, 0);

            //    CurrentStage = Stage.transform.Find("StartPostion").gameObject;
            //    Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
            //    //Player.transform.position = StartPostionBoss[myChar.SelectedStage].transform.position;
            //    HeroStartPostion(CurrentStage.transform.position);
            //    Stage.transform.Find("Monster").gameObject.SetActive(true);

            //    myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

            //    CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;
            //    myChar.ActiveItemCost.Clear();

            //    /*이전코드*/
            //    //BossRewardDropCheck = false;
            //    //GameManager.Instance.StoreUI.SetActive(false);
            //    //DontTouchBtn_Panel.SetActive(true);
            //    //CurrentStage = StartPostionBoss[myChar.SelectedStage];
            //    //Player.transform.parent.transform.position = StartPostionBoss[myChar.SelectedStage].transform.parent.Find("BackGround").transform.position;
            //    ////Player.transform.position = StartPostionBoss[myChar.SelectedStage].transform.position;
            //    //HeroStartPostion(StartPostionBoss[myChar.SelectedStage].transform.position);
            //    //StartPostionBoss[myChar.SelectedStage].transform.parent.Find("Monster").gameObject.SetActive(true);

            //    //myChar.SelectLocation = CurrentStage.transform.parent.Find("BackGround").gameObject;

            //    //CAMERAPOS.transform.position = StartPostionBoss[myChar.SelectedStage].transform.parent.Find("CameraPos").position;
            //    //myChar.ActiveItemCost.Clear();
            //    /*이전코드*/

            //    //PlayerCamera.transform.position = StartPostionBoss[myChar.SelectedStage].transform.parent.Find("CameraPos").position;
            //}
            ////Reward맵
            //else if (myChar.Stage == (myChar.BasicStage + 4))
            //{
            //    myChar.StartCheck = false;
            //    if (myChar.Chapter < 3)
            //    {
            //        Stage = Instantiate(StartPostionEquipment[myChar.SelectedStage], transform.position, Quaternion.identity);
            //        Stage.transform.parent = Stage_Parent.transform;
            //        Stage.transform.localPosition = new Vector3(0, 0, 0);

            //        CurrentStage = Stage.transform.Find("StartPostion").gameObject;
            //        Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
            //        //Player.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.position;
            //        HeroStartPostion(CurrentStage.transform.position);
            //        Stage.transform.Find("Monster").gameObject.SetActive(true);

            //        myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

            //        CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;
            //        EquipmentManager();

            //        /*이전코드*/
            //        //CurrentStage = StartPostionEquipment[myChar.SelectedStage];
            //        //Player.transform.parent.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("BackGround").transform.position;
            //        ////Player.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.position;
            //        //HeroStartPostion(StartPostionEquipment[myChar.SelectedStage].transform.position);
            //        //StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("Monster").gameObject.SetActive(true);

            //        //myChar.SelectLocation = CurrentStage.transform.parent.Find("BackGround").gameObject;

            //        //CAMERAPOS.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("CameraPos").position;
            //        //EquipmentManager();
            //        /*이전코드*/
            //    }


            //    if (myChar.Chapter == 3)
            //    {
            //        Debug.Log("여기찍힘?");
            //        CurrentStage = StartPostionEquipment[myChar.SelectedStage];
            //        Player.transform.parent.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("BackGround").transform.position;
            //        Player.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.position;
            //        HeroStartPostion(StartPostionEquipment[myChar.SelectedStage].transform.position);
            //        StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("Monster").gameObject.SetActive(true);

            //        myChar.SelectLocation = CurrentStage.transform.parent.Find("BackGround").gameObject;

            //        CAMERAPOS.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("CameraPos").position;
            //        EquipmentManager();

            //    }


            //    //CurrentStage = StartPostionEquipment[myChar.SelectedStage];
            //    //Player.transform.parent.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("BackGround").transform.position;
            //    //Player.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.position;
            //    //StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("Monster").gameObject.SetActive(true);

            //    //CAMERAPOS.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("CameraPos").position;

            //    //PlayerCamera.transform.position = StartPostionEquipment[myChar.SelectedStage].transform.parent.Find("CameraPos").position;
            //}
        }

        CurrentStage.transform.parent.gameObject.SetActive(true);
        //currentStage++;
        //// 최종보스클리어 후 나올화면
        //if (currentStage > LastStage) { return; }

        //if (currentStage % 5 != 0) // 일반맵
        //{
        //    int arrayIndex = currentStage / 5;

        //    int randomIndex = Random.Range(0, StartPositionArrays[arrayIndex].StartPosition.Count);
        //    Player.transform.parent.transform.position = StartPositionArrays[arrayIndex].StartPosition[randomIndex].transform.parent.Find("BackGround").transform.position;
        //    Player.transform.position = StartPositionArrays[arrayIndex].StartPosition[randomIndex].transform.position;
        //    StartPositionArrays[arrayIndex].StartPosition[randomIndex].transform.parent.Find("Monster").gameObject.SetActive(true);
        //    CAMERAPOS.transform.position = StartPositionArrays[arrayIndex].StartPosition[randomIndex].transform.parent.Find("CameraPos").position;
        //    PlayerCamera.transform.position = StartPositionArrays[arrayIndex].StartPosition[randomIndex].transform.parent.Find("CameraPos").position; ;

        //    StartPositionArrays[arrayIndex].StartPosition.RemoveAt(randomIndex);
        //}
        //else
        //{
        //    //아이템 방
        //    if (currentStage % 10 == 5) 
        //    {
        //        int randomIndex = Random.Range(0, StartPostionItem.Count);
        //        Player.transform.position = StartPostionItem[randomIndex].position;
        //    }
        //    else
        //    {
        //        //최종보스
        //        if (currentStage == LastStage)
        //        {
        //            Player.transform.position = StartPostionLastBoss.position;
        //        }
        //        // 최종보스외 보스
        //        else
        //        {
        //            int randomIndex = Random.Range(0, StartPostionBoss.Count);
        //            Player.transform.position = StartPostionBoss[randomIndex].position;
        //            StartPostionBoss.RemoveAt(currentStage / 10);
        //        }
        //    }

        //}
    }
    private void HeroStage()
    {
        if (myChar.Stage == 30)
        {
            //myChar.Finished = true;
            //myChar.CrownGetBgm = true;
            GameManager.Instance.WinPanelOpen(0f);
            GameManager.Instance.ClearSkip_Btn.SetActive(true);
            return;
        }
        GameManager.Instance.StageClear();

        myChar.MHpMultiIndex++;

        if (myChar.SelectLocation)
        {
            Destroy(myChar.SelectLocation.transform.parent.gameObject);
        }
        myChar.Stage++;
        //맵 크기 체크
        HeroMapCheck();

        // 최종보스클리어 후 나올화면
        if (currentStage > LastStage) { return; }

        if (myChar.Stage % 10 != 0) // 일반맵
        {
            if (!MidiumRoom && !LargeRoom && !EquipmentRoom && !AntiqueRoom)
            {
                //int arrayIndex = myChar.Chapter;
                int arrayIndex = myChar.SelectedStage;

                //스테이지 랜덤 선택
                int randomIndex = Random.Range(0, SmallPositionArrays[myChar.Chapter].StartPosition.Count);

                Stage = Instantiate(SmallPositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);


                //해당 방에 진입했을때 몬스터 오픈해줌
                Stage.transform.Find("Monster").gameObject.SetActive(true);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                //PlayerCamera.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;

                SmallPositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else if (MidiumRoom)
            {
                //스테이지 랜덤 선택
                int randomIndex = Random.Range(0, MidiumPositionArrays[myChar.Chapter].StartPosition.Count);

                Stage = Instantiate(MidiumPositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);


                //해당 방에 진입했을때 몬스터 오픈해줌
                Stage.transform.Find("Monster").gameObject.SetActive(true);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                //PlayerCamera.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;

                MidiumPositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else if (LargeRoom)
            {
                int randomIndex = Random.Range(0, LargePositionArrays[myChar.Chapter].StartPosition.Count);

                Stage = Instantiate(LargePositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);


                //해당 방에 진입했을때 몬스터 오픈해줌
                Stage.transform.Find("Monster").gameObject.SetActive(true);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                //PlayerCamera.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;

                LargePositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else if (EquipmentRoom)
            {
                for (int i = 0; i < ShopCostboard.Length; i++)
                {
                    ShopCostboard[i] = true;
                }

                Stage = Instantiate(EquipmentRoomPostion, transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                HeroStartPostion(CurrentStage.transform.position);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                myChar.ActiveItemCost.Clear();
                GameManager.Instance.StoreUI.SetActive(true);
                ItemManager();

                GameManager.Instance.StoreCostCheck();
            }
            else if (AntiqueRoom)
            {
                Stage = Instantiate(AntiqueRoomPostion, transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                //보스방 진입전 상태 체크를위해 false로 변경해주는것
                myChar.StartCheck = false;

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                //캐릭터 부모 위치 이동
                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;

                //플레이하는 캐릭터 시작위치 조정
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                AntiqueManager();
            }
        }
        else
        {
            if (myChar.Stage != 30)
            {
                int randomIndex = Random.Range(0, bossPositionArrays[myChar.Chapter].StartPosition.Count);
                BossRewardDropCheck = false;
                Stage = Instantiate(bossPositionArrays[myChar.Chapter].StartPosition[randomIndex], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);
                Stage.transform.Find("Monster").gameObject.SetActive(true);
                DontTouchBtn_Panel.SetActive(true);

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;

                bossPositionArrays[myChar.Chapter].StartPosition.RemoveAt(randomIndex);
            }
            else
            {
                BossRewardDropCheck = false;
                Stage = Instantiate(FinalBossPositionArrays[myChar.Chapter].StartPosition[0], transform.position, Quaternion.identity);
                Stage.transform.parent = Stage_Parent.transform;
                Stage.transform.localPosition = new Vector3(0, 0, 0);

                CurrentStage = Stage.transform.Find("StartPostion").gameObject;

                Player.transform.parent.transform.position = Stage.transform.Find("BackGround").transform.position;
                //Player.transform.position = CurrentStage.transform.position;
                HeroStartPostion(CurrentStage.transform.position);
                Stage.transform.Find("Monster").gameObject.SetActive(true);
                DontTouchBtn_Panel.SetActive(true);

                myChar.SelectLocation = Stage.transform.Find("BackGround").gameObject;

                CAMERAPOS.transform.position = Stage.transform.Find("CameraPos").position;
            }
        }
        CurrentStage.transform.parent.gameObject.SetActive(true);
    }
    private void MapCheck()
    {
        if (((myChar.Stage % 10) == 3) || ((myChar.Stage % 10) == 6))
        {
            MidiumRoom = false;
            LargeRoom = false;
            EquipmentRoom = true;
            AntiqueRoom = false;
        }
        else if ((myChar.Stage % 10) == 9)
        {
            MidiumRoom = false;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = true;

            GameManager.Instance.StoreUI.SetActive(false);
        }
        else if (myChar.Stage == 8 || myChar.Stage == 18 || myChar.Stage == 25 || myChar.Stage == 28 || myChar.Stage == 35
            || myChar.Stage == 38 || myChar.Stage == 41 || myChar.Stage == 42 || myChar.Stage == 44 || myChar.Stage == 47)
        {
            MidiumRoom = true;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = false;

            GameManager.Instance.StoreUI.SetActive(false);
        }
        else if (myChar.Stage == 45 || myChar.Stage == 48)
        {
            MidiumRoom = false;
            LargeRoom = true;
            EquipmentRoom = false;
            AntiqueRoom = false;

            GameManager.Instance.StoreUI.SetActive(false);
        }
        else if ((myChar.Stage % 10) == 0)
        {
            MidiumRoom = true;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = false;
        }
        else
        {
            MidiumRoom = false;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = false;

            GameManager.Instance.StoreUI.SetActive(false);
        }
    }
    private void HeroMapCheck()
    {
        if (((myChar.Stage % 10) == 3) || ((myChar.Stage % 10) == 6))
        {
            MidiumRoom = false;
            LargeRoom = false;
            EquipmentRoom = true;
            AntiqueRoom = false;
        }
        else if ((myChar.Stage % 10) == 9)
        {
            MidiumRoom = false;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = true;

            GameManager.Instance.StoreUI.SetActive(false);
        }
        else if (myChar.Stage == 4 || myChar.Stage == 8 || myChar.Stage == 14 || myChar.Stage == 18 || myChar.Stage == 21
            || myChar.Stage == 22 || myChar.Stage == 25 || myChar.Stage == 27 )
        {
            MidiumRoom = true;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = false;

            GameManager.Instance.StoreUI.SetActive(false);
        }
        else if (myChar.Stage == 24 || myChar.Stage == 28)
        {
            MidiumRoom = false;
            LargeRoom = true;
            EquipmentRoom = false;
            AntiqueRoom = false;

            GameManager.Instance.StoreUI.SetActive(false);
        }
        else if ((myChar.Stage % 10) == 0)
        {
            MidiumRoom = true;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = false;
        }
        else
        {
            MidiumRoom = false;
            LargeRoom = false;
            EquipmentRoom = false;
            AntiqueRoom = false;

            GameManager.Instance.StoreUI.SetActive(false);
        }
    }
    private void AntiqueManager()
    {
        AntiqueRoom_itemSort();

        for (int i = 0; i < 3; i++)
        {
            //int TypeNum = Random.Range(0, 100);
            WeightedRandomizer<float> TypeNum = new WeightedRandomizer<float>();

            TypeNum.RandomPercent(1, 0.7f);
            TypeNum.RandomPercent(2, 0.2f);
            TypeNum.RandomPercent(3, 0.1f);
            
            float AntiqueWin = TypeNum.GetNext();

            Transform altarPos = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).transform.GetChild(0);

            if (AntiqueWin == 1)
            {
                int AntiqueNum = Random.Range(0, NormalAntique.Count);

                GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + NormalAntique[AntiqueNum]);
                ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = NormalAntique[AntiqueNum];
                NormalAntique.RemoveAt(AntiqueNum);

                //if (AntiqueNum < 30)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2000);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2000;
                //}
                //else if (AntiqueNum >= 30 && AntiqueNum < 60)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2001);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2001;
                //}
                //else if (AntiqueNum >= 60 && AntiqueNum < 70)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2002);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2002;
                //}
                //else if (AntiqueNum >= 70 && AntiqueNum < 100)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2003);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2003;
                //}
            }
            else if (AntiqueWin == 2)
            {
                int AntiqueNum = Random.Range(0, RareAntique.Count);

                GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + RareAntique[AntiqueNum]);
                ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = RareAntique[AntiqueNum];
                RareAntique.RemoveAt(AntiqueNum);

                //if (AntiqueNum < 30)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2004);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2004;
                //}
                //else if (AntiqueNum >= 30 && AntiqueNum < 60)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2005);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2005;
                //}
                //else if (AntiqueNum >= 60 && AntiqueNum < 70)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2006);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2006;
                //}
                //else if (AntiqueNum >= 70 && AntiqueNum < 100)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2007);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2007;
                //}
            }
            else if (AntiqueWin == 3)
            {
                int AntiqueNum = Random.Range(0, UniqueAntique.Count);

                GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + UniqueAntique[AntiqueNum]);
                ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = UniqueAntique[AntiqueNum];
                UniqueAntique.RemoveAt(AntiqueNum);

                //int AntiqueNum = Random.Range(0, 100);

                //if (AntiqueNum < 30)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2008);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2008;
                //}
                //else if (AntiqueNum >= 30 && AntiqueNum < 60)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2009);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2009;
                //}
                //else if (AntiqueNum >= 60 && AntiqueNum < 70)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2010);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2010;
                //}
                //else if (AntiqueNum >= 70 && AntiqueNum < 100)
                //{
                //    GameObject ProduceAntiquity = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
                //    ProduceAntiquity.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + 2011);
                //    ProduceAntiquity.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
                //    ProduceAntiquity.transform.localScale = new Vector3(1f, 1f, 1f);
                //    ProduceAntiquity.GetComponent<ActiveItemScript>().ItemNum = 2011;
                //}
            }
           
        }
    }
    public void EquipmentDestroy()
    {
        //다음맵으로 넘어갈때 현재 노출된 아이템 삭제해주기

        for (int i = 0; i < 3; i++)
        {
            if (altarNum[i].transform.childCount != 0)
            {
                Destroy(altarNum[i].transform.GetChild(0).gameObject);
            }
        }
    }
    private void ItemManager()
    {
        EquipmentRoom_itemSort();
        myChar.EquipmentCost.Clear();

        for (int i = 0; i < 4; i++)
        {
            //int TypeNum = Random.Range(0, 100);
            WeightedRandomizer<float> TypeNum = new WeightedRandomizer<float>();

            TypeNum.RandomPercent(1, 0.5f);
            TypeNum.RandomPercent(2, 0.3f);
            TypeNum.RandomPercent(3, 0.2f);

            float EquipmentWin = TypeNum.GetNext();

            //int ProduceEquipmentNum = Random.Range(0, 19);
            Transform altarPos = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).transform.GetChild(0);

            //if (TypeNum < 10)
            //{
            //    int AttributeStoneNum = Random.Range(0, 4);

            //    GameObject ProduceActive = Instantiate(Resources.Load("02_Equipment/Activeitem/" + AttributeStoneNum), altarPos.position, Quaternion.identity) as GameObject;
            //    ProduceActive.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).GetChild(0).transform;
            //    ProduceActive.transform.localScale = new Vector3(1f, 1f, 1f);
            //    ProduceActive.GetComponent<OrbScript>().ItemNum = AttributeStoneNum + 1000;      // +1000을 해준건 엑셀스크립트 index맞추기위함
            //    myChar.EquipmentCost.Add(10);
            //}
            
            if (EquipmentWin == 1)
            {
                NormalItemCheck(altarPos, i);
            }
            else if (EquipmentWin == 2)
            {
                RareItemCheck(altarPos, i);
            }
            else if (EquipmentWin == 3)
            {
                UniqueItemCheck(altarPos, i);
            }
        }
        /*이전 코드*/
        //for (int i = 0; i < 4; i++)
        //{
        //    int ProduceEquipmentNum = Random.Range(0, 19);
        //    Transform altarPos = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).transform.GetChild(0);
        //    if (ProduceEquipmentNum <= 3)
        //    {
        //        GameObject ProduceActive = Instantiate(Resources.Load("02_Equipment/Activeitem/" + ProduceEquipmentNum), altarPos.position, Quaternion.identity) as GameObject;
        //        ProduceActive.transform.parent = ActivealtarNum[i].transform;
        //        ProduceActive.transform.localScale = new Vector3(1f, 1f, 1f);
        //        ProduceActive.GetComponent<OrbScript>().ActiveitemNum = ProduceEquipmentNum + 1000;      // +1000을 해준건 엑셀스크립트 index맞추기위함
        //        myChar.ActiveItemCost.Add(10);
        //    }
        //    else
        //    {
        //        GameObject ProduceActive = Instantiate(ActiveItem, altarPos.position, Quaternion.identity);
        //        ProduceActive.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Activeitem/" + ProduceEquipmentNum.ToString());
        //        ProduceActive.transform.parent = ActivealtarNum[i].transform;
        //        ProduceActive.transform.localScale = new Vector3(1f, 1f, 1f);
        //        ProduceActive.GetComponent<ActiveItemScript>().ActiveitemNum = ProduceEquipmentNum + 2000 - 4;       // +2000을 해준건 엑셀스크립트 index맞추기위함이고 -4는 오브랑 액티브아이템 배열따로잇어서 -4해줌
        //        myChar.ActiveItemCost.Add(20);
        //    }


        //    //GameObject ProduceActiveItem = Instantiate()
        //    //Transform 
        //}
    }

    private void NormalItemCheck(Transform ItemPosition, int itemNum)
    {
        if (NormalEquipment.Count > 0)
        {
            int ProduceEquipmentNum = Random.Range(0, NormalEquipment.Count);

            GameObject ProduceEquipment = Instantiate(EquipmentItem, ItemPosition.position, Quaternion.identity);
            ProduceEquipment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + NormalEquipment[ProduceEquipmentNum].ToString());
            //ProduceEquipment.transform.parent = altarNum[itemNum].transform;
            ProduceEquipment.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(itemNum).GetChild(0).transform;
            ProduceEquipment.transform.localScale = new Vector3(1, 1, 1);
            ProduceEquipment.GetComponent<EquipmentScript>().EquipmentNum = NormalEquipment[ProduceEquipmentNum];
            NormalEquipment.RemoveAt(ProduceEquipmentNum);
            myChar.EquipmentCost.Add(10);
        }
        else
        {
            bool reGachBool = (Random.value > 0.5f);
            if (reGachBool)
            {
                RareItemCheck(ItemPosition, itemNum);
            }
            else
            {
                UniqueItemCheck(ItemPosition, itemNum);
            }
        }
    }
    private void RareItemCheck(Transform ItemPosition, int itemNum)
    {
        if (RareEquipment.Count > 0)
        {
            int ProduceEquipmentNum = Random.Range(0, RareEquipment.Count);

            GameObject ProduceEquipment = Instantiate(EquipmentItem, ItemPosition.position, Quaternion.identity);
            ProduceEquipment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + RareEquipment[ProduceEquipmentNum].ToString());
            //ProduceEquipment.transform.parent = altarNum[itemNum].transform;
            ProduceEquipment.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(itemNum).GetChild(0).transform;
            ProduceEquipment.transform.localScale = new Vector3(1, 1, 1);
            ProduceEquipment.GetComponent<EquipmentScript>().EquipmentNum = RareEquipment[ProduceEquipmentNum];
            RareEquipment.RemoveAt(ProduceEquipmentNum);
            myChar.EquipmentCost.Add(20);
        }
        else
        {
            bool reGachBool = (Random.value > 0.5f);
            if (reGachBool)
            {
                NormalItemCheck(ItemPosition, itemNum);
            }
            else
            {
                UniqueItemCheck(ItemPosition, itemNum);
            }
        }
    }
    private void UniqueItemCheck(Transform ItemPosition, int itemNum)
    {
        if (UniqueEquipment.Count > 0)
        {
            int ProduceEquipmentNum = Random.Range(0, UniqueEquipment.Count);

            GameObject ProduceEquipment = Instantiate(EquipmentItem, ItemPosition.position, Quaternion.identity);
            ProduceEquipment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + UniqueEquipment[ProduceEquipmentNum].ToString());
            //ProduceEquipment.transform.parent = altarNum[itemNum].transform;
            ProduceEquipment.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(itemNum).GetChild(0).transform;
            ProduceEquipment.transform.localScale = new Vector3(1, 1, 1);
            ProduceEquipment.GetComponent<EquipmentScript>().EquipmentNum = UniqueEquipment[ProduceEquipmentNum];
            UniqueEquipment.RemoveAt(ProduceEquipmentNum);
            myChar.EquipmentCost.Add(30);
        }
        else  //유니크 장비가 없을시 
        {
            bool reGachBool = (Random.value > 0.5f);
            if (reGachBool)
            {
                NormalItemCheck(ItemPosition, itemNum);
            }
            else
            {
                RareItemCheck(ItemPosition, itemNum);
            }
        }
        
    }

    public void BossReward()
    {
        if (!BossRewardDropCheck)
        {
            Invoke("BossGiftBoxDrop", 2f);
            BossRewardDropCheck = true;
        }
    }
    private void EquipmentRoom_itemSort()
    {
        NormalEquipment.Clear();
        RareEquipment.Clear();
        UniqueEquipment.Clear();
        if (myChar.EquipmentUsed.Count > 0)
        {
            for (int i = 0; i < myChar.EquipmentUsed.Count; i++)
            {
                for (int j = 0; j < CarryEquipment.Count; j++)
                {
                    if (CarryEquipment[j] == myChar.EquipmentUsed[i])
                    {
                        CarryEquipment.RemoveAt(j);
                    }
                }
            }
        }
        if (CarryEquipment.Count > 0)
        {
            for (int i = 0; i < CarryEquipment.Count; i++)
            {
                if (CarryEquipment[i] < 30)
                {
                    NormalEquipment.Add(CarryEquipment[i]);
                }
                else if (CarryEquipment[i] < 61)
                {
                    RareEquipment.Add(CarryEquipment[i]);
                }
                else if (CarryEquipment[i] < 88)
                {
                    UniqueEquipment.Add(CarryEquipment[i]);
                }
            }
        }
    }
    private void AntiqueRoom_itemSort()
    {
        NormalAntique.Clear();
        RareAntique.Clear();
        UniqueAntique.Clear();
        //if (myChar.PlayerEquipment.Count > 0)
        //{
        //    for (int i = 0; i < myChar.PlayerEquipment.Count; i++)
        //    {
        //        for (int j = 0; j < CarryAntique.Count; j++)
        //        {
        //            if (CarryAntique[j] == myChar.PlayerEquipment[i])
        //            {
        //                CarryAntique.RemoveAt(j);
        //            }
        //        }
        //    }
        //}
        if (CarryAntique.Count > 0)
        {
            for (int i = 0; i < CarryAntique.Count; i++)
            {
                if (CarryAntique[i] < 2004)
                {
                    NormalAntique.Add(CarryAntique[i]);
                }
                else if (CarryAntique[i] < 2008)
                {
                    RareAntique.Add(CarryAntique[i]);
                }
                else if (CarryAntique[i] < 2012)
                {
                    UniqueAntique.Add(CarryAntique[i]);
                }
            }
        }
    }
    //private IEnumerator BossGiftBoxCoroutine()
    //{        
    //    yield return new WaitForSeconds(3f);
    //    BossRewardDropCheck = true;
    //    if (!BossRewardDropCheck)
    //    {            
    //        CurrentDoor.GetComponent<Animator>().enabled = true;
    //        GameObject GiftBox = Instantiate(BossGfitBox, CurrentStage.transform.parent.Find("BackGround").transform.position, Quaternion.identity);
    //        GameObject GiftBoxEffect = Instantiate(BossGiftBoxEffect, CurrentStage.transform.parent.Find("BackGround").transform.position, Quaternion.identity);
    //        GiftBox.transform.parent = myChar.SelectLocation.transform.GetChild(0).transform;
    //    }        
    //}
    public void BossGiftBoxDrop()
    {
        if (!myChar.Tutorial)
        {
            Vector3 DropPos = CurrentStage.transform.parent.Find("BackGround").transform.position + new Vector3(0, -2, 0);
            if (CurrentDoor.activeSelf)
            {
                CurrentDoor.GetComponent<Animator>().enabled = true;
            }
            //GameObject GiftBox = Instantiate(BossGfitBox, myChar.SelectLocation.transform.position, Quaternion.identity);
            GameObject GiftBoxEffect = Instantiate(BossGiftBoxEffect, DropPos, Quaternion.identity);
            GiftBoxEffect.transform.parent = myChar.SelectLocation.transform.GetChild(0).transform;
        }
        else
        {
            Vector3 DropPos = CurrentStage.transform.parent.Find("BackGround").transform.position;
            
            //if (CurrentDoor.activeSelf)
            //{
            //    CurrentDoor.GetComponent<Animator>().enabled = true;
            //}
            ////GameObject GiftBox = Instantiate(BossGfitBox, myChar.SelectLocation.transform.position, Quaternion.identity);
            //GameObject GiftBoxEffect = Instantiate(BossGiftBoxEffect, DropPos, Quaternion.identity);
            //GiftBoxEffect.transform.parent = myChar.SelectLocation.transform.GetChild(0).transform;
        }

    }
    public void TutorialBossGiftBoxDrop()
    {
        GameObject GiftBoxEffect = Instantiate(BossGiftBoxEffect, CurrentStage.transform.parent.Find("BackGround").transform.position, Quaternion.identity);
    }
    private void HeroStartPostion(Vector3 pos)
    {
        GameObject Players = GameObject.Find("Player");

        for (int i = 0; i < Players.transform.childCount; i++)
        {
            Players.transform.GetChild(i).gameObject.transform.position = pos;
        }
    }
    public void OffTouchPanel()
    {
        if (!myChar.Tutorial)
        {
            DontTouchBtn_Panel.SetActive(false);
        }
        else if (myChar.Tutorial)
        {
            if (myChar.TutorialNum <= 4)
            {
                DontTouchBtn_Panel.SetActive(false);
            }
        }
        
    }
    public void Touch_InfoTF()
    {
        myChar.TouchInfo = true;
    }

    private void Tutorial()
    {
        switch (myChar.TutorialNum)
        {
            case 0:
                if (Tutorial_Parent.transform.childCount == 0)
                {
                    GameManager.Instance.Tutorial_Info.SetActive(true);
                    GameManager.Instance.Tutorial_Anim.SetActive(true);
                    GameManager.Instance.PlayerPanel.SetActive(false);
                    GameManager.Instance.CurrencyPanel.SetActive(false);
                    DontTouchBtn_Panel.SetActive(true);
                    if (myChar.SelectLocation)
                    {
                        myChar.SelectLocation.transform.parent.Find("Monster").gameObject.SetActive(false);
                    }
                    
                    Tutorial_Stage();
                    TutoCheck = myChar.Coin; 
                }
                if (myChar.Coin == TutoCheck + 4)
                {
                    GameManager.Instance.Tutorial_Info.SetActive(false);
                    TutoNextMapStartCheck = false;
                    myChar.TutorialNum = 1;
                    StartCoroutine(TutorialStageDelay());
                }
                break;
            case 1:
                if (!myChar.TutorialNextStage)
                {
                    GameManager.Instance.DoorPanel.SetActive(true);
                    
                }
                if (TutoCheck >= 3)
                {
                    GameManager.Instance.Tutorial_Info.SetActive(false);
                    TutoNextMapStartCheck = false;
                    GameManager.Instance.Tutorial_Anim.transform.GetChild(2).gameObject.SetActive(false);
                    GameManager.Instance.Player.transform.Find("Tutorial_AttackLine").gameObject.SetActive(false);
                    myChar.TutorialNum = 2;
                    StartCoroutine(TutorialStageDelay());
                }
                break;
            case 2:
                if (!myChar.TutorialNextStage)
                {
                    GameManager.Instance.DoorPanel.SetActive(true);
                }
                else
                {
                    if (Time.timeScale >= 1)
                    {
                        if (TutoNextMapStartCheck)
                        {
                            GameManager.Instance.Tutorial_Anim.transform.GetChild(0).gameObject.SetActive(false);
                            for (int i = 0; i < GameManager.Instance.Tutorial_Anim.transform.childCount; i++)
                            {
                                if (i != myChar.TutorialNum)
                                {
                                    GameManager.Instance.Tutorial_Anim.transform.GetChild(i).gameObject.SetActive(false);
                                }
                                else if (i == myChar.TutorialNum)
                                {
                                    GameManager.Instance.Tutorial_Anim.transform.GetChild(i).gameObject.SetActive(true);
                                }
                            }
                        }
                    }
                }

                if (TutoCheck >= 2)
                {
                    GameManager.Instance.Tutorial_Info.SetActive(false);
                    TutoNextMapStartCheck = false;
                    GameManager.Instance.Tutorial_Anim.transform.GetChild(3).gameObject.SetActive(false);
                    myChar.TutorialNum = 3;
                    StartCoroutine(TutorialStageDelay());
                }
                break;
            case 3:
                if (!myChar.TutorialNextStage)
                {
                    GameManager.Instance.DoorPanel.SetActive(true);
                }
                else
                {
                    if (Time.timeScale >= 1)
                    {
                        if (TutoNextMapStartCheck)
                        {
                            GameManager.Instance.Tutorial_Anim.transform.GetChild(0).gameObject.SetActive(false);
                            for (int i = 0; i < GameManager.Instance.Tutorial_Anim.transform.childCount; i++)
                            {
                                if (i != myChar.TutorialNum)
                                {
                                    GameManager.Instance.Tutorial_Anim.transform.GetChild(i).gameObject.SetActive(false);
                                }
                                else if (i == myChar.TutorialNum)
                                {
                                    GameManager.Instance.Tutorial_Anim.transform.GetChild(i).gameObject.SetActive(true);
                                }
                            }
                        }
                    }
                }
                if (TutoCheck >= 1)
                {
                    GameManager.Instance.Tutorial_Info.SetActive(false);
                    TutoNextMapStartCheck = false;
                    myChar.TutorialNum = 4;
                    StartCoroutine(TutorialStageDelay());
                }
                break;
            //case 4:
            //    if (!myChar.TutorialNextStage)
            //    {
            //        GameManager.Instance.DoorPanel.SetActive(true);
            //    }
            //    if (TutoCheck >= 1)
            //    {
            //        GameManager.Instance.Tutorial_Info.SetActive(false);
            //        myChar.TutorialNum = 5;
            //        DontTouchBtn_Panel.SetActive(true);
            //        StartCoroutine(TutorialStageDelay());
            //    }
            //    break;
            //case 5:
            //    if (!myChar.TutorialNextStage)
            //    {
            //        DontTouchBtn_Panel.SetActive(false);
            //        GameManager.Instance.DoorPanel.SetActive(true);
            //    }
            //    if (TutoCheck >= 1)
            //    {
            //        GameManager.Instance.Tutorial_Info.SetActive(false);
            //        myChar.TutorialNum = 6;
            //        StartCoroutine(TutorialStageDelay());
            //    }
            //    break;
            case 4:
                if (!myChar.TutorialNextStage)
                {
                    GameManager.Instance.DoorPanel.SetActive(true);
                    
                }
                if (TutoCheck >= 1)
                {
                    GameManager.Instance.Tutorial_Info.SetActive(false);
                    if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(0).transform.GetChild(0).transform.childCount == 0)
                    {
                        CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 0).transform.GetChild(2).gameObject;
                        //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                        CurrentDoor.GetComponent<Animator>().enabled = true;
                    }
                    else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(1).transform.GetChild(0).transform.childCount == 0)
                    {
                        CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 1).transform.GetChild(2).gameObject;
                        //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                        CurrentDoor.GetComponent<Animator>().enabled = true;
                    }
                    else if (CurrentStage.transform.parent.Find("Reward_Object").GetChild(2).transform.GetChild(0).transform.childCount == 0)
                    {
                        CurrentDoor = CurrentStage.transform.parent.Find("Door_" + 2).transform.GetChild(2).gameObject;
                        //CurrentDoor.GetComponent<BoxCollider2D>().enabled = true;
                        CurrentDoor.GetComponent<Animator>().enabled = true;
                    }
                    myChar.TutorialNum = 5;
                    StartCoroutine(TutorialStageDelay());
                }
                break;
            case 5:
                if (!myChar.TutorialNextStage)
                {
                    GameManager.Instance.DoorPanel.SetActive(true);
                    GameManager.Instance.PlayerPanel.SetActive(true);
                    GameManager.Instance.CurrencyPanel.SetActive(true);
                    DontTouchBtn_Panel.SetActive(true);
                }
                if (TutoCheck >= 1)
                {
                    GameManager.Instance.Tutorial_Info.SetActive(false);
                    //myChar.Tutorial = false;
                    myChar.TutoActiveItemUse = false;
                    //GameManager.Instance.MoveLobbyTime(10f);
                    //Time.timeScale = 0f;
                }
                break;
        }

    }

    public void Tutorial_Stage()
    {
        Tutorial_Map = Instantiate(TutorialStage[myChar.TutorialNum], transform.position, Quaternion.identity);
        Tutorial_Map.transform.parent = Tutorial_Parent.transform;
        Tutorial_Map.transform.localPosition = new Vector3(0, 0, 0);
        CurrentStage = Tutorial_Map.transform.Find("StartPostion").gameObject;
        myChar.SelectLocation = Tutorial_Map.transform.Find("BackGround").gameObject;
        Player.transform.parent.transform.position = myChar.SelectLocation.transform.position;
        HeroStartPostion(CurrentStage.transform.position);
        if (myChar.TutorialNum <= 4)
        {
            CurrentStage.transform.parent.Find("Monster").gameObject.SetActive(true);
        }        
        CAMERAPOS.transform.position = CurrentStage.transform.parent.Find("CameraPos").position;
        myChar.TutorialNextStage = true;
        Time.timeScale = 0.0f;
        if (myChar.TutorialNum > 0)
        {
            Destroy(Tutorial_Parent.transform.GetChild(0).gameObject);
        }
        switch (myChar.TutorialNum)
        {
            case 1:
                //GameManager.Instance.Player.transform.Find("Tutorial_AttackLine").gameObject.SetActive(true);
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    GameObject Orb = Instantiate(Tutorial_Stone, transform.position, Quaternion.identity);
                    Orb.transform.parent = myChar.SelectLocation.transform.parent.Find("Monster").GetChild(i);
                    Orb.transform.localScale = new Vector3(6f, 6f);
                    Orb.transform.localPosition = new Vector3(0, 0, 0);
                }                
                break;
            case 4:
                for (int i = 0; i < 3; i++)
                {
                    CurrentDoor = CurrentStage.transform.parent.Find("Door_" + i).transform.GetChild(2).gameObject;
                    CurrentDoor.SetActive(true);
                }
                AntiqueManager();
                //for (int i = 0; i < 3; i++)
                //{
                //    int ProduceEquipmentNum = Random.Range(0, CarryEquipment.Count);
                //    Transform altarPos = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).transform.GetChild(0);
                //    GameObject ProduceEquipment = Instantiate(EquipmentItem, altarPos.position, Quaternion.identity);
                //    ProduceEquipment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("02_Equipment/Equipment/" + CarryEquipment[ProduceEquipmentNum].ToString());
                //    ProduceEquipment.transform.parent = CurrentStage.transform.parent.Find("Reward_Object").GetChild(i).transform.GetChild(0);
                //    ProduceEquipment.transform.localScale = new Vector3(1, 1, 1);
                //    ProduceEquipment.GetComponent<EquipmentScript>().EquipmentNum = CarryEquipment[ProduceEquipmentNum];
                //}
                break;
            case 5:
                GameManager.Instance.SelectCharacter.GetComponent<PlayerController>()._anim.SetBool("Obtain", false);
                break;
        }
    }
    private void Tutorial_Next()
    {
        GameManager.Instance.Tutorial_Anim.SetActive(false);
        GameManager.Instance.Tutorial_Info.SetActive(false);
        myChar.TutorialNextStage = false;
    }
    public void CongEffectStart()
    {
        StartCoroutine("CongEffectCoroutin");
    }

    IEnumerator CongEffectCoroutin()
    {
        for (int i = 0; i < myChar.SelectLocation.transform.parent.Find("Throne").GetChild(2).childCount; i++)
        {
            yield return new WaitForSeconds(1f);
            GameObject Effect = Instantiate(CongEffect, myChar.SelectLocation.transform.parent.Find("Throne").GetChild(2).GetChild(i).transform.position, Quaternion.identity);
            Effect.transform.parent = myChar.SelectLocation.transform.parent.Find("Throne").GetChild(2).parent;
            SoundManager.Instance.PlaySfx(23);
        }
    }
    IEnumerator TutorialStageDelay()
    {
        TutoCheck = 0;
        yield return new WaitForSeconds(1f);
        Tutorial_Next();
    }

    public IEnumerator GiftBoxOpen_DoorInvisible()
    {
        if (myChar.Chapter + 1 % 10 == 3 || myChar.Chapter + 1 % 10 == 6)
        {
            if (myChar.Stage != 30)
            {
                Debug.Log(22);
                CurrentDoor.transform.parent.gameObject.SetActive(false);
                yield return new WaitForSeconds(5f);
                CurrentDoor.transform.parent.gameObject.SetActive(true);
            }
        }
        else
        {

            Debug.Log(11);
            CurrentDoor.transform.parent.gameObject.SetActive(false);
            yield return new WaitForSeconds(5f);
            CurrentDoor.transform.parent.gameObject.SetActive(true);
        }
        
    }

}
