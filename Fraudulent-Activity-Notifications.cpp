void insertInOrder(vector<int>& arr, int x)
{
    if (x > arr[arr.size()-1])
    {
        arr.push_back(x);
    }
    else
    {
        auto pos = std::lower_bound(arr.begin(), arr.end(), x);
        arr.insert(pos, x);
    }
}

void removeFromOrdered(vector<int>& arr, int x)
{
    auto pos = std::lower_bound(arr.begin(), arr.end(), x);
    arr.erase(pos);
}

int activityNotifications(vector<int> expenditure, int d)
{
    bool even = (d % 2 == 0);
    int notifications = 0;
    
    vector<int> ordered;
    ordered.push_back(expenditure[0]);
    
    for (int i = 1; i < d; i++)
    {
        insertInOrder(ordered, expenditure[i]);
    }

    for (int i = d; i < expenditure.size(); i++)
    {
        double median = 0.0;
        
        if (even)
        {
            median = static_cast<double>(ordered[d/2 - 1] + ordered[d/2])
                   / 2.0;
        }
        else
        {
            median = ordered[d / 2];
        }
        
        if (2 * median <= expenditure[i])
        {
            notifications++;
        }

        removeFromOrdered(ordered, expenditure[i - d]);
        insertInOrder(ordered, expenditure[i]);
    }

    return notifications;
}

int main()
{
    ofstream fout(getenv("OUTPUT_PATH"));

    string first_multiple_input_temp;
    getline(cin, first_multiple_input_temp);

    vector<string> first_multiple_input = split(rtrim(first_multiple_input_temp));

    int n = stoi(first_multiple_input[0]);

    int d = stoi(first_multiple_input[1]);

    string expenditure_temp_temp;
    getline(cin, expenditure_temp_temp);

    vector<string> expenditure_temp = split(rtrim(expenditure_temp_temp));

    vector<int> expenditure(n);

    for (int i = 0; i < n; i++) {
        int expenditure_item = stoi(expenditure_temp[i]);

        expenditure[i] = expenditure_item;
    }

    int result = activityNotifications(expenditure, d);

    fout << result << "\n";

    fout.close();

    return 0;
}

string ltrim(const string &str) {
    string s(str);

    s.erase(
        s.begin(),
        find_if(s.begin(), s.end(), not1(ptr_fun<int, int>(isspace)))
    );

    return s;
}

string rtrim(const string &str) {
    string s(str);

    s.erase(
        find_if(s.rbegin(), s.rend(), not1(ptr_fun<int, int>(isspace))).base(),
        s.end()
    );

    return s;
}

vector<string> split(const string &str) {
    vector<string> tokens;

    string::size_type start = 0;
    string::size_type end = 0;

    while ((end = str.find(" ", start)) != string::npos) {
        tokens.push_back(str.substr(start, end - start));

        start = end + 1;
    }

    tokens.push_back(str.substr(start));

    return tokens;
}

