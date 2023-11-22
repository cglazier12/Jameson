using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers.Levels
{
    public class SimpleLevelLoader : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            // Check for a key press to load the predetermined level.
            // For example, pressing the 'L' key will load the 'GroundTest' scene.
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGroundTestLevel();
            }
        }

        private void LoadGroundTestLevel()
        {
            SceneManager.LoadScene("GroundTest");
        }
    }
}