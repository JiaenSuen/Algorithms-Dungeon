#ifndef GRAPH
#define GRAPH
#include<string>
#include<iostream>
#include<iomanip>
#include <cmath>
#include <limits>
#include <functional>
#include <vector>
#include <random>

struct g_Node {
    std::string name;
    int x , y ;
    g_Node(){}
    g_Node(std::string name, int x ,int y  ) : name(name) , x(x) , y(y) {}
    
    friend std::ostream& operator<<(std::ostream& os, const g_Node& node) {
        os << std::setw(5) <<node.name << " : " <<std::setw(3) << node.x << " , " <<std::setw(3)<< node.y <<std::endl;
        return os;
    };
};

auto node_distance = [](const g_Node& a, const g_Node& b) -> double {
        return std::sqrt(std::pow(a.x - b.x, 2) + std::pow(a.y - b.y, 2));
};


std::random_device rd;
std::mt19937 generator(0);
//std::mt19937 generator(rd());
g_Node generate_node(){
    int x , y;
    std::uniform_int_distribution<int> unif( -100 , 100 );
    std::uniform_int_distribution<int> letter_dist(0, 25);
    std::uniform_int_distribution<int> number_dist(1, 99);
    char letter = 'A' + letter_dist(generator);
    int number = number_dist(generator);
    std::string id = std::string(1, letter) + std::to_string(number);
    return  { id , unif(generator) , unif(generator)};
}


typedef std::vector<g_Node> MAP;
MAP generate_map(unsigned int map_size){
    std::vector<g_Node> map;
    for (size_t i = 0; i < map_size; i++){
        map.push_back(generate_node());
        std::cout<<(map[i]);
    }
    return map;
}

 
double Cost_Evaluation(std::vector<g_Node>& Solution ){
    double Cost = 0;
    int Length = Solution.size();
    for (size_t i = 1; i < Length ; i++){
        Cost += node_distance(Solution[i-1],Solution[i]);
    }
    return Cost ;
}





#endif
