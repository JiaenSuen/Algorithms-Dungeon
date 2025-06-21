#ifndef EVALUATION_HPP
#define EVALUATION_HPP

#include <iostream>
#include <vector>
#include <algorithm>
#include <queue>
#include <unordered_map>
#include "config.hpp"

using namespace std;

// 計算 schedule (含通訊延遲)
inline ScheduleResult Calculate_schedule(
    const vector<int>& ss,
    const vector<int>& ms,
    const Config& config)
{
    int T = config.theTCount;
    int P = config.thePCount;
    vector<double> startTime(T, 0.0), endTime(T, 0.0), procFree(P, 0.0);

    for (int idx = 0; idx < T; ++idx) {
        int t = ss[idx];
        int p = ms[t];
        double ready = 0;
        auto it = config.predMap.find(t);
        if (it != config.predMap.end()) {
            for (auto& pr : it->second) {
                int from = pr.first;
                double vol = pr.second;
                int pf = ms[from];
                double comm = (pf != p) ? vol * config.theCommRate[pf][p] : 0;
                ready = max(ready, endTime[from] + comm);
            }
        }
        startTime[t] = max(ready, procFree[p]);
        endTime[t]   = startTime[t] + config.theCompCost[t][p];
        procFree[p]  = endTime[t];
    }
    double makespan = *max_element(endTime.begin(), endTime.end());
    return { startTime, endTime, makespan };
}

// 檢查 schedule 可行性 (含通訊延遲)
inline bool is_feasible(
    const ScheduleResult& res,
    const Config& cfg,
    const vector<int>& ms,
    bool show = false)
{
    for (auto& edge : cfg.theTransDataVol) {
        int from = (int)edge[0], to = (int)edge[1];
        double vol = edge[2];
        int pf = ms[from], pt = ms[to];
        double comm = (pf != pt) ? vol * cfg.theCommRate[pf][pt] : 0;
        if (res.endTime[from] + comm > res.startTime[to]) {
            if (show) {
                cerr << "[Error] Task " << from
                     << "→" << to
                     << " violates: end(" << res.endTime[from]
                     << ") + comm(" << comm
                     << ") > start(" << res.startTime[to] << ")\n";
            }
            return false;
        }
    }
    return true;
}

// 拓樸排序重建 ss
inline vector<int> rebuild_ss_by_toposort(
    const Config& cfg,
    const vector<int>& orig_ss)
{
    int T = cfg.theTCount;
    vector<int> indeg(T, 0);
    vector<vector<int>> adj(T);
    // 建圖 (from -> to)
    for (auto& e : cfg.theTransDataVol) {
        int u = (int)e[0], v = (int)e[1];
        adj[u].push_back(v);
        indeg[v]++;
    }
    // 優先佇列：按照 orig_ss 的位置先後
    vector<int> pos(T);
    for (int i = 0; i < T; ++i) pos[orig_ss[i]] = i;
    auto cmp = [&](int a, int b){ return pos[a] > pos[b]; };
    priority_queue<int, vector<int>, decltype(cmp)> pq(cmp);
    // push indegree=0
    for (int i = 0; i < T; ++i)
        if (indeg[i] == 0) pq.push(i);

    vector<int> new_ss;
    new_ss.reserve(T);
    while (!pq.empty()) {
        int u = pq.top(); pq.pop();
        new_ss.push_back(u);
        for (int v : adj[u]) {
            if (--indeg[v] == 0)
                pq.push(v);
        }
    }
    return new_ss;
}

inline ScheduleResult Solution_Function(
    Solution& sol,
    const Config& config,
    bool show_adjust = false)
{
    int T = config.theTCount;
    // 初次檢查 ss, ms
    {
        vector<int> chk = sol.ss;
        sort(chk.begin(), chk.end());
        for (int i = 0; i < T; ++i)
            if (chk[i] != i) {
                cerr << "[Error] Invalid ss contents\n";
                return {{}, {}, -1};
            }
    }
    if ((int)sol.ms.size() != T) {
        cerr << "[Error] ms size mismatch\n";
        return {{}, {}, -1};
    }
    // 初次排程
    auto result = Calculate_schedule(sol.ss, sol.ms, config);
    // 若不可行，就重建 ss
    if (!is_feasible(result, config, sol.ms, show_adjust)) {
        if (show_adjust)
            cerr << "[Info] Rebuilding task order via topological sort...\n";
        sol.ss = rebuild_ss_by_toposort(config, sol.ss);
        result = Calculate_schedule(sol.ss, sol.ms, config);
        if (!is_feasible(result, config, sol.ms, show_adjust)) {
            cerr << "[Error] Even after toposort, still infeasible.\n";
            return {{}, {}, -1};
        }
        if (show_adjust)
            cout << "[Adjusted] Used topological sort to enforce feasibility.\n";
    }
    sol.cost = result.makespan;
    return result;
}

#endif  // EVALUATION_HPP
