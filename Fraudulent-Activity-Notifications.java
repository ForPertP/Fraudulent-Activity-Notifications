import java.io.*;
import java.math.*;
import java.security.*;
import java.text.*;
import java.util.*;
import java.util.concurrent.*;
import java.util.function.*;
import java.util.regex.*;
import java.util.stream.*;
import static java.util.stream.Collectors.joining;
import static java.util.stream.Collectors.toList;

class Result {

    /*
     * Complete the 'activityNotifications' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY expenditure
     *  2. INTEGER d
     */

    public static void insertInOrder(List<Integer> arr, int x) {
        int pos = Collections.binarySearch(arr, x);
        if (pos < 0) {
            pos = -(pos + 1);
        }
        arr.add(pos, x);
    }

    public static void removeFromOrdered(List<Integer> arr, int x) {
        int pos = Collections.binarySearch(arr, x);
        if (pos >= 0) {
            arr.remove(pos);
        }
    }

    public static int activityNotifications(List<Integer> expenditure, int d) {
        boolean even = (d % 2 == 0);
        int notifications = 0;

        List<Integer> ordered = new ArrayList<>(expenditure.subList(0, d));
        Collections.sort(ordered);

        for (int i = d; i < expenditure.size(); i++) {
            double median;
            if (even) {
                median = (ordered.get(d / 2 - 1) + ordered.get(d / 2)) / 2.0;
            } else {
                median = ordered.get(d / 2);
            }

            if (2 * median <= expenditure.get(i)) {
                notifications++;
            }

            removeFromOrdered(ordered, expenditure.get(i - d));
            insertInOrder(ordered, expenditure.get(i));
        }

        return notifications;
    }

}

public class Solution {
    public static void main(String[] args) throws IOException {
        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(System.getenv("OUTPUT_PATH")));

        String[] firstMultipleInput = bufferedReader.readLine().replaceAll("\\s+$", "").split(" ");

        int n = Integer.parseInt(firstMultipleInput[0]);

        int d = Integer.parseInt(firstMultipleInput[1]);

        List<Integer> expenditure = Stream.of(bufferedReader.readLine().replaceAll("\\s+$", "").split(" "))
            .map(Integer::parseInt)
            .collect(toList());

        int result = Result.activityNotifications(expenditure, d);

        bufferedWriter.write(String.valueOf(result));
        bufferedWriter.newLine();

        bufferedReader.close();
        bufferedWriter.close();
    }
}
