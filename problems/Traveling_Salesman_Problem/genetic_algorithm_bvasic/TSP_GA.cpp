// GA.cpp
#include "TSP_meta.hpp"
#include "graph.hpp"

#include <vector>
#include <algorithm>
#include <random>
#include <chrono>
#include <iostream>
#include <iomanip>
#include <limits>

// Best Sol : 705.713 with 0.2 Mutaion & 100 Population
// Best Sol : 699.895 with 0.4 Mutaion & 500 Population  + [ELITE = 3]
// Best Sol : 687.455 with 0.4 Mutaion & 1000 Population + [ELITE = 3] using  roulette

// ----- GA Parameters ------
struct GA_Params {
    int map_size;
    int population_size;
    int generations;
    double crossover_rate;
    double mutation_rate;
    std::string selection_method;
};

GA_Params Init_GA_Params() {
    GA_Params params;
    params.map_size = 20;
    params.population_size = 1000;
    params.generations = 1000;
    params.crossover_rate = 0.7;
    params.mutation_rate = 0.4;
    params.selection_method = "roulette";
    return params;
}
//-------------------------------


// ---- GA Individual -----
class Individual : public Solution {
    public:
       

        Individual (const std::vector<g_Node>& city_map){
            path = city_map;                      // copy every city
            std::shuffle(path.begin(), path.end(), RNG::generator);
            Cost_Evaluation(*this);               // calculate cost
        }

        // 交配：Order Crossover (OX)
        Individual crossover(const Individual& other) const {
            int n = path.size();
            Individual child = *this;

            std::uniform_int_distribution<int> dist(0, n-1);
            int start = dist(RNG::generator);
            int end   = dist(RNG::generator);
            if (start > end) std::swap(start, end);
          // 1. child 在 start~end 保留自己的基因，其餘位置先填 -1
            std::vector<g_Node> new_path(n, g_Node());
            std::vector<bool> used(n, false);
            for (int i = start; i <= end; i++) {
                new_path[i] = path[i];
                // 標記該 city 已使用
                used[std::distance(path.begin(), 
                    std::find(path.begin(), path.end(), path[i]))] = true;
            }
            // 2. 從 other.path 按順序，把還沒用過的 city 填回 child
            int idx = (end + 1) % n;
            for (int i = 0; i < n; i++) {
                const g_Node& gene = other.path[(end+1+i)%n];
                // find gene 在原父本的 index
                auto it = std::find(path.begin(), path.end(), gene);
                int gene_idx = std::distance(path.begin(), it);
                if (!used[gene_idx]) {
                    new_path[idx] = gene;
                    used[gene_idx] = true;
                    idx = (idx + 1) % n;
                }
            }
            child.path = new_path;
            Cost_Evaluation(child);
            return child;
        }

        // 突變：單點交換
        void mutate(double mutation_rate) {
            std::uniform_real_distribution<double> prob(0.0, 1.0);
            if (prob(RNG::generator) < mutation_rate) {
                std::uniform_int_distribution<int> dist(0, path.size()-1);
                int i = dist(RNG::generator);
                int j = dist(RNG::generator);
                std::swap(path[i], path[j]);
                Cost_Evaluation(*this);
            }
        }
};

auto cmp_individual = [](const Individual& a, const Individual& b){
    return a.cost < b.cost;
};

// -- End --
 

// Selection  Methods
// 從 population 中隨機挑 k 個，取 cost 最小者
Individual tournament_select(const std::vector<Individual>& population, int k ) {
    std::uniform_int_distribution<int> dist(0, population.size()-1);
    Individual best = population[dist(RNG::generator)];
    for (int i = 1; i < k; i++) {
        const Individual& cand = population[dist(RNG::generator)];
        if (cand.cost < best.cost) best = cand;
    }
    return best;
}

Individual roulette_select(const std::vector<Individual>& pop) {
    int n = pop.size();
    // 1. 計算 fitness 並累加
    std::vector<double> cumulative(n);
    double sum = 0;
    for (int i = 0; i < n; i++) {
        double fitness = 1.0 / pop[i].cost;
        sum += fitness;
        cumulative[i] = sum;
    }
    // 2. 隨機落點
    std::uniform_real_distribution<double> dist(0.0, sum);
    double r = dist(RNG::generator);
    // 3. 找到第一個 cumulative >= r 的索引
    auto it = std::lower_bound(cumulative.begin(), cumulative.end(), r);
    int idx = std::distance(cumulative.begin(), it);
    return pop[idx];
}

// Selection End ...





Individual Run_Genetic_Algorithm(
    const std::vector<g_Node>& city_map,
    const GA_Params& params
) {
    // 1. 初始化族群
    std::vector<Individual> population;
    population.reserve(params.population_size);
    for (int i = 0; i < params.population_size; i++)
        population.emplace_back(city_map);

    // 2. 依 cost 排序（方便取精英）
    std::sort(population.begin(), population.end(), cmp_individual);

    // 3. 演化主迴圈
    for (int gen = 1; gen <= params.generations; gen++) {
        std::vector<Individual> new_pop;
        // 精英保留
        const int ELITE = 3;
        for (int i = 0; i < ELITE; i++)
            new_pop.push_back(population[i]);

        // 產生剩餘新族群
        while ((int)new_pop.size() < params.population_size) {
            // 選擇父母
            Individual parent1 = 
                (params.selection_method == "roulette") 
                ? roulette_select(population)
                : tournament_select(population, 5);

            Individual parent2 = 
                (params.selection_method == "roulette") 
                ? roulette_select(population)
                : tournament_select(population, 5);
            
            // 交配或直接複製
            std::uniform_real_distribution<double> prob(0.0,1.0);
            Individual child = (prob(RNG::generator) < params.crossover_rate)
                               ? parent1.crossover(parent2)
                               : parent1;
            // 突變
            child.mutate(params.mutation_rate);
            new_pop.push_back(child);
        }

        // 替換族群並排序
        population = std::move(new_pop);
        std::sort(population.begin(), population.end(), cmp_individual);

        // 每隔一段印出進度
        if (gen % 50 == 0) {
            std::cout << "Gen " <<std::setw(4)<< gen
                      << " Best Cost = " << population.front().cost
                      << "\n";
        }
    }

    // 回傳最優解
    return population.front();
}


int main() {
    
    GA_Params params = Init_GA_Params(); // 初始化參數

    
    std::vector<g_Node> city_map = generate_map(params.map_size); // 產生隨機城市地圖

    // Start Evolution & Genetic Algorithm
    Individual best = Run_Genetic_Algorithm(city_map, params);



    std::cout << "\nBest Path Cost: " << best.cost << std::endl;
    for (auto& node : best.path) 
        std::cout << node.name << " -> ";
    std::cout << "Start\n";

    return 0;
}

