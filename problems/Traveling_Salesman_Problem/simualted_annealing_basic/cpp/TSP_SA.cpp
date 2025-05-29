#include <iostream>
#include <vector>
#include "graph.hpp"
using namespace std;



 
struct Solution {
    vector<g_Node> best_solution;
    double best_cost;
};

 


vector<g_Node> init_Solution(vector<g_Node>& map ){
    std::mt19937 gen2(rd());
    vector<g_Node> current_solution;
    g_Node start_node("O", 0, 0);
    current_solution.push_back(start_node);

    // Insert nodes into the map in random order (as an initial solution)
    std::vector<g_Node> shuffled_map = map; 
    std::shuffle(shuffled_map.begin(), shuffled_map.end(), gen2);

    for (const auto& node : shuffled_map) 
        current_solution.push_back(node);

    current_solution.push_back(start_node);  
    return current_solution;
}


Solution TSP_SA( vector<g_Node>& map ) {
    double total_cost = 0.0;
    vector<g_Node> current_solution = init_Solution(map);
    double current_cost = Cost_Evaluation(current_solution);

    vector<g_Node> best_solution = current_solution;
    double best_cost    = current_cost;



    // Parameters Setting :
    double T = 1000.0;             
    double T_min = 1e-3;           
    double cooling_rate = 0.995;    
    int max_iter = 1000;          
    // Parameters End

     
    std::mt19937 gen2(rd());

    // Annealing : 
    int iter = 0;
    std::uniform_real_distribution<double> real_dist(0.0, 1.0);
    
    while (T > T_min && iter < max_iter){
        cout<<"Iter : "<<iter<<"\tTemp : "<<T<<"\tCost : "<<current_cost<<endl;
        vector<g_Node> new_solution = current_solution;
        uniform_int_distribution<int> index_dist(1, new_solution.size() - 2);
        int i = index_dist(gen2);
        int j = index_dist(gen2);
        while (i == j) j = index_dist(gen2);
        swap(new_solution[i], new_solution[j]);


        double new_cost = Cost_Evaluation(new_solution);
        double delta = new_cost - current_cost;



        if (delta < 0 || real_dist(gen2) < std::exp(-delta / T)) {
            current_solution = new_solution;
            current_cost = new_cost;

            if (current_cost < best_cost) {
                best_solution = current_solution;
                best_cost = current_cost;
            }
        }

        T *= cooling_rate;
        iter++;
    }

     
    return { best_solution, best_cost };
}



int main()
{   
    MAP map = generate_map(20);
    cout<<"\n\n";
    Solution Solution =  TSP_SA(map);
    cout<<"Solution :\n";
    for (auto& node:Solution.best_solution)cout<<" "<<node.name<<"\t";
    cout<<endl;
    cout<<" Cost : "<< setw(10)<<Solution.best_cost;
    return 0;
}



 