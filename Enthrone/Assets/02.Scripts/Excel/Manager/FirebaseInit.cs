using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var app = FirebaseApp.DefaultInstance;
        });
    }

    public void SendFirebaseEvent(string _eventName, params string[] _params)
    {
        Parameter[] parameters = new Parameter[_params.Length];
        for (var i = 0; i < _params.Length; i++) parameters[i] = new Parameter($"param{i + 1}", _params[i]);
        FirebaseAnalytics.LogEvent(_eventName, parameters);
    }
    private void Login()
    {
        
        // Log an event with no parameters.
        Firebase.Analytics.FirebaseAnalytics
          .LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);

        // Log an event with a float parameter
        Firebase.Analytics.FirebaseAnalytics
          .LogEvent("progress", "percent", 0.4f);

        // Log an event with an int parameter.
        Firebase.Analytics.FirebaseAnalytics
          .LogEvent(
            Firebase.Analytics.FirebaseAnalytics.EventPostScore,
            Firebase.Analytics.FirebaseAnalytics.ParameterScore,
            42
          );

        // Log an event with a string parameter.
        Firebase.Analytics.FirebaseAnalytics
          .LogEvent(
            Firebase.Analytics.FirebaseAnalytics.EventJoinGroup,
            Firebase.Analytics.FirebaseAnalytics.ParameterGroupId,
            "spoon_welders"
          );

        // Log an event with multiple parameters, passed as a struct:
        Firebase.Analytics.Parameter[] LevelUpParameters = {
  new Firebase.Analytics.Parameter(
    Firebase.Analytics.FirebaseAnalytics.ParameterLevel, 5),
  new Firebase.Analytics.Parameter(
    Firebase.Analytics.FirebaseAnalytics.ParameterCharacter, "mrspoon"),
  new Firebase.Analytics.Parameter(
    "hit_accuracy", 3.14f)
};
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
          Firebase.Analytics.FirebaseAnalytics.EventLevelUp,
          LevelUpParameters);
    }
}
