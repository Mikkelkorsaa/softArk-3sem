using System;
using System.Collections.Generic;

namespace Sortering;

public static class MergeSort
{
    public static void Sort(List<int> list)
    {
        if (list == null || list.Count <= 1)
            return;
            
        _mergeSort(list, 0, list.Count - 1);
    }

    private static void _mergeSort(List<int> list, int left, int right)
    {
        if (left < right)
        {
            int middle = (left + right) / 2;
            _mergeSort(list, left, middle);
            _mergeSort(list, middle + 1, right);
            Merge(list, left, middle, right);
        }
    }

    private static void Merge(List<int> list, int left, int middle, int right)
    {
        // Create temporary lists
        List<int> leftList = new List<int>();
        List<int> rightList = new List<int>();
        
        // Copy data to temporary lists
        for (int i = left; i <= middle; i++)
            leftList.Add(list[i]);
            
        for (int i = middle + 1; i <= right; i++)
            rightList.Add(list[i]);
            
        int leftIndex = 0;
        int rightIndex = 0;
        int currentIndex = left;
        
        // Merge the two lists back into the original list
        while (leftIndex < leftList.Count && rightIndex < rightList.Count)
        {
            if (leftList[leftIndex] <= rightList[rightIndex])
            {
                list[currentIndex] = leftList[leftIndex];
                leftIndex++;
            }
            else
            {
                list[currentIndex] = rightList[rightIndex];
                rightIndex++;
            }
            currentIndex++;
        }
        
        // Copy any remaining elements from leftList
        while (leftIndex < leftList.Count)
        {
            list[currentIndex] = leftList[leftIndex];
            leftIndex++;
            currentIndex++;
        }
        
        // Copy any remaining elements from rightList
        while (rightIndex < rightList.Count)
        {
            list[currentIndex] = rightList[rightIndex];
            rightIndex++;
            currentIndex++;
        }
    }
}
