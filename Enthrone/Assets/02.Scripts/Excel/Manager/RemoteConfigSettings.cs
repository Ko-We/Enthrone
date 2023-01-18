using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.Authentication;

using System.Threading.Tasks;

public class RemoteConfigSettings : MonoBehaviour
{    
    private static RemoteConfigSettings _instance = null;

    public static RemoteConfigSettings Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Singleton == null");
            }
            return _instance;
        }
    }

    /*************************************************************************************/
    //public static RemoteConfigSettings Instance { get; private set; }


    public struct userAttributes
    {
        // Optionally declare variables for any custom user attributes:
        // ����� ���� ����� �Ӽ��� ���� ������ ���������� �����մϴ�.
        public bool expansionFlag;
    }

    public struct appAttributes
    {
        // Optionally declare variables for any custom app attributes:
        // ���������� ����� ���� �� �Ӽ��� ���� ���� ����:
        public int level;
        public int score;
        public string appVersion;
    }

    public struct filterAttributes
    {
        // Optionally declare variables for attributes to filter on any of following parameters:
        // ���� �Ű����� �� �ϳ��� ���͸��� �Ӽ��� ���� ������ ���������� �����մϴ�.

        public string[] key;
        public string[] type;
        public string[] schemaId;
    }

    // Optionally declare a unique assignmentId if you need it for tracking:
    // ������ �ʿ��� ��� ������ assignId�� ���������� �����մϴ�.
    public string assignmentId;

    // Declare any Settings variables you��ll want to configure remotely:
    // �������� �����Ϸ��� ��� ���� ������ �����մϴ�.
    public int enemyVolume;
    public float enemyHealth;
    public float enemyDamage;

    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // options can be passed in the initializer, e.g if you want to set analytics-user-id or an environment-name use the lines from below:
        // var options = new InitializationOptions()
        //   .SetOption("com.unity.services.core.analytics-user-id", "my-user-id-1234")
        //   .SetOption("com.unity.services.core.environment-name", "production");
        // await UnityServices.InitializeAsync(options);

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        _instance = this;
       
    }
    // Create a function to set your variables to their keyed values:
    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        // You will implement this in the final step.
    }
    // Start is called before the first frame update
    void Start()
    {
        //await InitializeRemoteConfigAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


}
