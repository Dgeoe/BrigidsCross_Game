using UnityEngine;

public class WebsitePopUp : MonoBehaviour
{
    public void OpenLink(string s)
    {
        Application.OpenURL(s);
    }
}