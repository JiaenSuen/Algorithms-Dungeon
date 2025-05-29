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

    double distance_to(g_Node& dest){
        return std::sqrt(std::pow(this->x - dest.x, 2) + std::pow(this->y - dest.y, 2));
    }



    bool operator==(const g_Node& other) const {
        return this->x == other.x && this->y == other.y && this->name == other.name ;
    }
};

namespace path_utils {



    auto node_distance = [](const g_Node& a, const g_Node& b) -> double {
            return std::sqrt(std::pow(a.x - b.x, 2) + std::pow(a.y - b.y, 2));
    };




}





namespace RNG {
    std::random_device rd;
    std::mt19937 generator(rd());
    std::mt19937 generator_fixed(0);
    //std::mt19937 generator(rd());
}


g_Node generate_node(){
    int x , y;
    std::uniform_int_distribution<int> unif( -100 , 100 );
    std::uniform_int_distribution<int> letter_dist(0, 25);
    std::uniform_int_distribution<int> number_dist(1, 99);
    char letter = 'A' + letter_dist(RNG::generator_fixed);
    int number = number_dist(RNG::generator_fixed);
    std::string id = std::string(1, letter) + std::to_string(number);
    return  { id , unif(RNG::generator_fixed) , unif(RNG::generator_fixed)};
}
 
std::vector<g_Node> generate_map(unsigned int map_size){
    std::vector<g_Node> map;
    for (size_t i = 0; i < map_size; i++){
        map.push_back(generate_node());
        std::cout<<(map[i]);
    }
    return map;
}








#endif
