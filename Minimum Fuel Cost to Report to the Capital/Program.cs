Solution s = new Solution();
var roads = new int[6][] { new int[] { 3, 1 }, new int[] { 3, 2 }, new int[] { 1, 0 }, new int[] { 0, 4 }, new int[] { 0, 5 }, new int[] { 4, 6 } };
int seats = 2;

var answer = s.MinimumFuelCost(roads, seats); 
Console.WriteLine(answer);

class Solution
{
  public long MinimumFuelCost(int[][] roads, int seats)
  {
    // Step 1 - create adjacency list
    if (roads.Length == 0) return 0;
    var adj = new Dictionary<int, List<int>>();
    foreach (var road in roads)
    {
      var src = road[0];
      var dest = road[1];
      if (!adj.ContainsKey(src)) adj.Add(src, new List<int>());
      if (!adj.ContainsKey(dest)) adj.Add(dest, new List<int>());

      adj[src].Add(dest);
      adj[dest].Add(src);
    }
    long fuel = 0;
    // Perform Dfs
    int Dfs(int node, int parent)
    {
      int passengers = 1;
      foreach (var nei in adj[node])
      {
        if (nei != parent)
        {
          passengers += Dfs(nei, node);
        }
      }
      if (node != 0)
      {
        var currentFuel = (passengers / seats) + (passengers % seats > 0 ? 1 : 0);
        fuel += currentFuel;
      }
      return passengers;
    }

    // start DFS from 0 as we have to reach to this Node from all nodes.
    Dfs(0, 0);
    return fuel;
  }
}

