using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using TMPro;

public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public GameObject loginPanel;
    public TMP_InputField emailLoginField;
    public TMP_InputField passwordLoginField;
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public GameObject registerPanel;
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;
    public TMP_Text warningRegisterText;

    private void Awake()
    {
        StartCoroutine(CheckAndFixDependenciesAsync());

    }

    private IEnumerator CheckAndFixDependenciesAsync()
    {
        // Check that all the necessary dependencies for Firebase are present in the system

        var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();

        yield return new WaitUntil(() => dependencyTask.IsCompleted);

        dependencyStatus = dependencyTask.Result;
        if (dependencyStatus == DependencyStatus.Available)
        {
            // If they are available then initialize Firebase
            InitializeFirebase();
            yield return new WaitForEndOfFrame();

            // Check if user can log in automatically
            StartCoroutine(CheckForAutoLogin());
        }
        else
        {
            Debug.LogError("Could not resolve all Firebase Dependencies: " + dependencyStatus);
        }
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        // Set the Firebase Instance Object
        auth = FirebaseAuth.DefaultInstance;
    }

    private IEnumerator CheckForAutoLogin()
    {
        if (User != null)
        {
            var reloadUserTask = User.ReloadAsync();

            yield return new WaitUntil(() => reloadUserTask.IsCompleted);

            AutoLogin();    
        }
        else
        {
            loginPanel.SetActive(true);
        }
    }

    private void AutoLogin()
    {
        if (User != null)
        {
            int mapSceneId = 2;
            StaticData.username = User.DisplayName;
            SceneManager.LoadSceneAsync(mapSceneId);
        }
        else
        {
            loginPanel.SetActive(true);
        }
    }

    public void ActivateLoginPanel(bool login)
    {
        loginPanel.SetActive(login);
        registerPanel.SetActive(!login);
    }

    // Function for the login button
    public void LoginButton()
    {
        // Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }

    // Function for the register button
    public void RegisterButton()
    {
        if (!registerPanel.activeSelf)
        {
            registerPanel.SetActive(true);
        }
        // Call the login coroutine passing the email and password
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }

    private IEnumerator Login(string email, string password)
    {
        // Call the Firebase auth signin function passing the email and password
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        // Wait until the task completes
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            // If there are errors, handle them
            Debug.LogWarning(message: $"Failed to register task with {loginTask.Exception}");
            FirebaseException firebaseException = loginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseException.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            // User is now logged in
            User = loginTask.Result;
            Debug.LogFormat("User Signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";

            int mapSceneId = 2;
            StaticData.username = User.DisplayName;
            SceneManager.LoadSceneAsync(mapSceneId);
        }
    }

    private IEnumerator Register(string email, string password, string username)
    {
        if (username == "")
        {
            // If the username field is blank show a warning
            warningLoginText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            // If the passwords don't match show a warning
            warningRegisterText.text = "Passwords do not match!";
        }
        else
        {
            // Call the Firebase auth signin function passing the email and password
            Debug.Log("Before calling firebase register");
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            // Wait until the task completes
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);

            Debug.Log("After calling firebase register");

            if (registerTask.Exception != null)
            {
                // If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {registerTask.Exception}");
                FirebaseException firebaseException = registerTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseException.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                // User has now been created

                User = registerTask.Result;
                if (User != null)
                {
                    // Create a User Profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = username };

                    // Call the Firebase auth update user profile function passing the profile with the username
                    var profileTask = User.UpdateUserProfileAsync(profile);
                    yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

                    if (profileTask.Exception != null)
                    {
                        // If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {profileTask.Exception}");
                        FirebaseException firebaseException = profileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseException.ErrorCode;

                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        // Username is not set
                        // Now return to login screen
                        Debug.Log("Username is Set");
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

    // Test method for skipping login. Keep for playing as guest.
    public void SkipLogin()
    {
        int mapSceneId = 2;
        SceneManager.LoadScene(mapSceneId);
    }
}