using System.Globalization;

namespace Sortering;

public class InsertionSort
{


    public static void Sort(int[] array)
    {
        for (int i = 1; i < array.Length; i++)
        {
            int next = array[i];
            int j = i;

            bool found = false;

            while (!found && j > 0)
            {
                if (next >= array[j - 1])
                {
                    found = true;
                }
                else
                {
                    array[j] = array[j - 1];
                    j--;
                }
            }
            array[j] = next;
        }

        return;
    }
}
