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
    }

    public void SignOutButton()
    {
        if (User != null)
        {
            User = null;
            auth.SignOut();
        }
        SceneManager.LoadScene(0);
    }
}
