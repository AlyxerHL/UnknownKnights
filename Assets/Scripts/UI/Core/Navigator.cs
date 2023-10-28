using System;
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

    private static readonly Queue<Func<UniTask>> navigationQueue = new();
    private static readonly Stack<Page> pageStack = new();
    private static Dictionary<string, Page> pageMap;

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
        ProcessNavigationQueue().Forget();
    }

    private static async UniTaskVoid ProcessNavigationQueue()
    {
        while (true)
        {
            await UniTask.WaitWhile(() => navigationQueue.Count == 0);
            await navigationQueue.Dequeue().Invoke();
        }
    }

    public static async UniTask<Page> PushPage(string pageName)
    {
        var tcs = new UniTaskCompletionSource<Page>();
        navigationQueue.Enqueue(Push);
        return await tcs.Task;

        async UniTask Push()
        {
            var currentPage = pageStack.Peek();
            var newPage = pageMap[pageName];

            pageStack.Push(newPage);
            await newPage.Show();
            currentPage.gameObject.SetActive(false);
            tcs.TrySetResult(newPage);
        }
    }

    public static async UniTask<Page> PopPage()
    {
        var tcs = new UniTaskCompletionSource<Page>();
        navigationQueue.Enqueue(Pop);
        return await tcs.Task;

        async UniTask Pop()
        {
            var currentPage = pageStack.Pop();
            var newPage = pageStack.Peek();

            newPage.gameObject.SetActive(true);
            await currentPage.Hide();
            tcs.TrySetResult(currentPage);
        }
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
