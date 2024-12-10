using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using System.Threading.Tasks;
using System;

public class FirebaseInitializing : MonoBehaviour
{

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(OnDependencyStatusReceived);
    }

    private void OnDependencyStatusReceived(Task<DependencyStatus> task) 
    {
        try
        {
            if (!task.IsCompletedSuccessfully)
                throw new Exception("Could not resolve all Firebase dependencies", task.Exception);

            var status = task.Result;
            if (status != DependencyStatus.Available)
                throw new Exception($"Could not resolve all Firebase dependencies, {status}");

            Debug.Log($"Firebase initializing successfull");
        }

        catch (Exception ex) 
        {
            Debug.LogException(ex);
        }
        
    }

}
