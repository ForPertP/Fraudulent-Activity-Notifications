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

