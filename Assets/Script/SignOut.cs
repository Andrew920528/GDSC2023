using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class SignOut : MonoBehaviour
{
    private AuthManager authManager;
    private FirebaseAuth auth;
    private FirebaseUser User;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        if (authManager == null)
        {
            authManager = FindObjectOfType<AuthManager>();
        }
    }

    public void SignOutButton()
    {
        authManager.LogOut();
    }
}
