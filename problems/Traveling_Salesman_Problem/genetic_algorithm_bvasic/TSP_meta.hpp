#ifndef TSP
#define TSP
#include "graph.hpp"



class Solution {
    public:
        std::vector<g_Node> path;
        double cost;
};


double Cost_Evaluation(Solution& solution){
    std::vector<g_Node> path;
    path = solution.path;
    path.insert(path.begin(), g_Node("Start", 0, 0)); // Start  
    
    double Cost = 0;
    for (size_t i = 1; i < path.size(); i++){
        Cost += path_utils::node_distance(path[i-1], path[i]);
    }
    Cost += path_utils::node_distance(path.back(), path.front());
    solution.cost = Cost;
    return Cost;
}

#endif