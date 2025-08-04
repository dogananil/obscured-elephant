using UnityEngine;
using Cysharp.Threading.Tasks;

/// <summary>
/// Entry point for the game. Initializes all CardMatch systems at runtime.
/// </summary>
public class Bootstrap : MonoBehaviour
{
    // Called automatically by Unity when the scene starts
    private async void Start()
    {
        Debug.Log("⏳ Booting CardMatch systems...");

        // Initialize all systems
        await CardMatch.Boot();

        Debug.Log("✅ All systems booted!");

    }
}
