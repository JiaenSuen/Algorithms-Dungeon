#include <iostream>
#include <vector>
#include <iomanip>
using namespace std;

class Eight_Queen //8Q -> N Queen
{
private:
    const unsigned short N;
    vector<int> board;        // 1 dimension array as board   
    vector<bool> col_used;          

    bool isSafe(int row, int col) {
        // from 0 chech to now
        for (int prevRow = 0; prevRow < row; ++prevRow) {
            int prevCol = board[prevRow];
            // Check the diagonal lines
            if (abs(prevCol - col) == abs(prevRow - row))
                return false;
        }
        return true;
    }

    void solve(int row) {
        if (row == N) {
            cout << "Solution #" << setw(3)<< ++solution_count << " : ";
            for (int i = 0; i < N; ++i)
                cout << board[i] << " ";
            cout << endl;

            feasible_solutions.push_back(board);
            return;
        }
    
        for (int col = 0; col < N; ++col) {
            if (col_used[col]) continue;

            if (isSafe(row, col)) {
                board[row] = col;
                col_used[col] = true;
                solve(row + 1);
                col_used[col] = false; 
            }
        }
    }

public:

    int solution_count = 0;
    vector<vector<int>> feasible_solutions;

    Eight_Queen(int size) : N(size), board(size), col_used(size, false) {}

    void solve(){  // User Interface 
        solve(0);
    }
    

};
 

int main() {
    Eight_Queen eq(8);
    eq.solve();
    cout << "Total Number of Eight Queen Feasible Solution : " << eq.solution_count << endl;
    system("pause");
    return 0;
}
