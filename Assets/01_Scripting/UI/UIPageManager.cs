using UnityEngine;
using System.Collections.Generic;
public class UIPageManager : MonoBehaviour
{
    [SerializeField] private List<UIPage> pages = new();
    [SerializeField] private int currentIndex = 0;
    [SerializeField] private UIPage currentPage;
    private void Start()
    {
        if (pages.Count == 0 && currentPage == null)
        {
            Debug.LogError("UI PAGES NOT SET UP CORRECTLY");
            return;
        }

        if (currentPage == null) currentPage = pages[currentIndex];

        else if (!pages.Contains(currentPage))
        {
            pages.Add(currentPage);
            currentIndex = pages.Count - 1;
        }
        else currentIndex = pages.IndexOf(currentPage);

        foreach(UIPage page in pages)
        {
            page.gameObject.SetActive(currentPage == page);
        }
    }
    public void NextPage()
    {
        if (pages.Count <= 1) return;

        currentIndex++;
        if (currentIndex >= pages.Count) currentIndex = 0;
        SelectPage(pages[currentIndex]);
    }
    public void PreviousPage()
    {
        if (pages.Count <= 1) return;

        currentIndex--;
        if (currentIndex < 0) currentIndex = pages.Count - 1;
        SelectPage(pages[currentIndex]);
    }
    public void GoToPage(int index)
    {
        if (pages.Count <= 1) return;
        if (index < 0 || index >= pages.Count) return;

        SelectPage(pages[index]);
    }
    public void GoToPage(UIPage page)
    {
        if (pages.Count <= 1) return;
        if (!pages.Contains(page)) return;

        SelectPage(page);
    }
    public void GoToPage(string name)
    {
        if (pages.Count <= 1) return;
        foreach (UIPage page in pages)
        {
            if (page.Name == name)
            {
                SelectPage(page);
                break;
            }
        }
    }
    private void SelectPage(UIPage targetPage)
    {
        currentPage.gameObject.SetActive(false);
        currentPage = targetPage;
        currentPage.gameObject.SetActive(true);
        currentIndex = pages.IndexOf(currentPage);
    }
}