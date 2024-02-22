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

// Test cases 4 and 5 are timing out.

class Result
{
    public static void InsertInOrder(List<int> arr, int x)
    {
        int pos = arr.BinarySearch(x);
        if (pos < 0)
        {
            pos = ~pos;
        }
        arr.Insert(pos, x);
    }

    public static void RemoveFromOrdered(List<int> arr, int x)
    {
        int pos = arr.BinarySearch(x);
        if (pos >= 0)
        {
            arr.RemoveAt(pos);
        }
    }

    public static int activityNotifications(List<int> expenditure, int d)
    {
        bool even = (d % 2 == 0);
        int notifications = 0;

        List<int> ordered = expenditure.GetRange(0, d).ToList();
        ordered.Sort();

        for (int i = d; i < expenditure.Count; i++)
        {
            double median;
            if (even)
            {
                median = (ordered[d / 2 - 1] + ordered[d / 2]) / 2.0;
            }
            else
            {
                median = ordered[d / 2];
            }

            if (2 * median <= expenditure[i])
            {
                notifications++;
            }

            RemoveFromOrdered(ordered, expenditure[i - d]);
            InsertInOrder(ordered, expenditure[i]);
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
