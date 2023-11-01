using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PagesRouter : MonoBehaviour
{
    [SerializeField]
    private Page rootPage;

    [SerializeField]
    private Page[] pages;

    private static readonly Queue<Func<UniTask>> transitionQueue = new();
    private static Dictionary<string, Page> pageMap;

    public static Page CurrentPage { get; private set; }

    private void Awake()
    {
        transitionQueue.Clear();
        pageMap = pages.ToDictionary((page) => page.name);
    }

    private void Start()
    {
        foreach (var page in pages)
        {
            page.gameObject.SetActive(false);
        }

        CurrentPage = rootPage;
        CurrentPage.gameObject.SetActive(true);
        ProcessTransitionQueue().Forget();
    }

    private static async UniTaskVoid ProcessTransitionQueue()
    {
        while (true)
        {
            await UniTask.WaitWhile(() => transitionQueue.Count == 0);
            await transitionQueue.Dequeue().Invoke();
        }
    }

    public static async UniTask<Page> GoTo(string pageName)
    {
        var tcs = new UniTaskCompletionSource<Page>();
        transitionQueue.Enqueue(GoTo);
        return await tcs.Task;

        async UniTask GoTo()
        {
            var newPage = pageMap[pageName];
            if (newPage == CurrentPage)
            {
                tcs.TrySetResult(newPage);
                return;
            }

            await CurrentPage.Hide();
            CurrentPage.gameObject.SetActive(false);
            newPage.gameObject.SetActive(true);
            await newPage.Show();

            CurrentPage = newPage;
            tcs.TrySetResult(newPage);
        }
    }
}
