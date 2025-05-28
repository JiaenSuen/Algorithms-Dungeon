#include <iostream>
#include <vector>
#include "TSP_meta.hpp"
using namespace std;



 

/*  


std::vector<g_Node> init_Solution(std::vector<g_Node>& map ){
    std::mt19937 gen2(RNG::rd());
    std::vector<g_Node> current_solution;
    // Insert nodes into the map in random order (as an initial solution)
    std::vector<g_Node> shuffled_map = map; 
    std::shuffle(shuffled_map.begin(), shuffled_map.end(), gen2);
    for (const auto& node : shuffled_map) 
        current_solution.push_back(node);
    return current_solution;
}


Solution TSP_SA( std::vector<g_Node>& map ) {
    double total_cost = 0.0;
    std::vector<g_Node> current_solution = init_Solution(map);
    double current_cost = Cost_Evaluation(current_solution);

    std::vector<g_Node> best_solution = current_solution;
    double best_cost    = current_cost;



    // Parameters Setting :
    double T = 1000.0;             
    double T_min = 1e-3;           
    double cooling_rate = 0.995;    
    int max_iter = 5000;          
    // Parameters End


    return { best_solution, best_cost };
}
 */


int main() {   
    
    




    return 0;
}



 