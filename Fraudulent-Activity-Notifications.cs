using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{
    private static double SearchMedian(int[] countingSort, int d)
    {
        bool isEven = d % 2 == 0;
        int count = 0;
        int medianIndex1 = -1;
        int medianIndex2 = -1;
        int medianCount = isEven ? d / 2 : (d + 1) / 2;

        for (int i = 0; i < countingSort.Length; i++)
        {
            count += countingSort[i];

            if (medianIndex1 == -1 && count >= medianCount)
                medianIndex1 = i;

            if (isEven && medianIndex2 == -1 && count >= medianCount + 1)
                medianIndex2 = i;

            if (!isEven && medianIndex1 != -1)
                return medianIndex1;

            if (isEven && medianIndex2 != -1)
                return (medianIndex1 + medianIndex2) / 2.0;
        }

        return 0;
    }
    
    public static int activityNotifications(List<int> expenditure, int d)
    {
        int notifications = 0;
        int maxExpenditure = expenditure.Max();

        int[] countingSort = new int[maxExpenditure + 1];

        for (int i = 0; i < d; i++)
            countingSort[expenditure[i]]++;

        for (int i = d; i < expenditure.Count; i++)
        {
            double median = SearchMedian(countingSort, d);

            if (expenditure[i] >= 2 * median)
                notifications++;

            countingSort[expenditure[i - d]]--;
            countingSort[expenditure[i]]++;
        }

        return notifications;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int d = Convert.ToInt32(firstMultipleInput[1]);

        List<int> expenditure = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(expenditureTemp => Convert.ToInt32(expenditureTemp)).ToList();

        int result = Result.activityNotifications(expenditure, d);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
