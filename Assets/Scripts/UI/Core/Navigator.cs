using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    [SerializeField]
    private Page rootPage;

    [SerializeField]
    private Page[] pages;

    private static bool isNavigating;
    private static Dictionary<string, Page> pageMap;
    private static readonly Stack<Page> pageStack = new();

    private void Awake()
    {
        pageMap = pages.ToDictionary((page) => page.name);
    }

    private void Start()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }

        rootPage.gameObject.SetActive(true);
        pageStack.Push(rootPage);
    }

    public static async UniTask PushPage(string pageName)
    {
        if (isNavigating)
        {
            return;
        }

        isNavigating = true;
        var currentPage = pageStack.Peek();
        var newPage = pageMap[pageName];

        pageStack.Push(newPage);
        await newPage.Show();
        currentPage.gameObject.SetActive(false);
        isNavigating = false;
    }

    public static async UniTask<Page> PopPage()
    {
        if (isNavigating)
        {
            return null;
        }

        isNavigating = true;
        var currentPage = pageStack.Pop();
        var newPage = pageStack.Peek();

        newPage.gameObject.SetActive(true);
        await currentPage.Hide();
        isNavigating = false;
        return currentPage;
    }

    public static async UniTask ShowPopup(string pageName)
    {
        await pageMap[pageName].Show();
    }

    public static async UniTask HidePopup(string pageName)
    {
        await pageMap[pageName].Hide();
    }
}
