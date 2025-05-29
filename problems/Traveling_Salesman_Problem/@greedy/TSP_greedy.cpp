#include <iostream>
#include <vector>
#include <random>
#include <cmath>
#include <limits>
#include <functional>
#include <iomanip>
using namespace std;



struct g_Node {
    string name;
    int x , y ;
    g_Node(){}
    g_Node(string name, int x ,int y  ) : name(name) , x(x) , y(y) {}
    
    friend ostream& operator<<(ostream& os, const g_Node& node) {
        os << setw(5) <<node.name << " : " <<setw(3) << node.x << " , " <<setw(3)<< node.y <<endl;
        return os;
    };
};

auto node_distance = [](const g_Node& a, const g_Node& b) -> double {
        return std::sqrt(std::pow(a.x - b.x, 2) + std::pow(a.y - b.y, 2));
};


struct Edge {
    g_Node sourse;
    g_Node destination;
    double weight;
    Edge(g_Node sour , g_Node dest , double weight) 
        : sourse(sour) , destination(dest) , weight(weight) {}
};




g_Node generate_node(){
    int x , y;
    random_device rd;
    mt19937 generator(rd());
    uniform_int_distribution<int> unif( -100 , 100 );
    uniform_int_distribution<int> letter_dist(0, 25);
    uniform_int_distribution<int> number_dist(1, 99);
    char letter = 'A' + letter_dist(generator);
    int number = number_dist(generator);
    string id = string(1, letter) + to_string(number);
    return  { id , unif(generator) , unif(generator)};
}



pair< vector<g_Node> , double > run_TSP_in_greedy (vector<g_Node>& map )  {
    vector<g_Node> result;
    double total_cost = 0.0;
    g_Node current_node = {"O" , 0 , 0};
    result.push_back(current_node);

    while (!map.empty()) {
        auto it = std::min_element(
            map.begin(), map.end(),
            [&](const g_Node& a, const g_Node& b) {
                return node_distance(current_node, a) < node_distance(current_node, b);
            });
        total_cost += node_distance(current_node, *it);
        current_node = *it;
        result.push_back(current_node);
        map.erase(it);
    }
    total_cost += node_distance(current_node, {"O", 0, 0});
    result.push_back({"O", 0, 0});
    return {result, total_cost};
}




int main()
{   
    vector<g_Node> map;
    for (size_t i = 0; i < 10; i++){
        map.push_back(generate_node());
        cout<<(map[i]);
    }
    cout<<"\n\n";
    pair< vector<g_Node> , double > result =  run_TSP_in_greedy(map);
    cout<<"Solution :\n";
    for (auto& node:result.first)cout<<" "<<node.name<<"\t";
    cout<<endl;
    cout<<" Cost : "<< setw(10)<<result.second;
    return 0;
}
