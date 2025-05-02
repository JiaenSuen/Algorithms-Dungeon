// use backtracking -> depth first search go if not feasible then return 

#include <iostream>
#include <chrono>
using namespace std;


class Sudoku{
    private:
        int maze[9][9];
        bool flag = false;

    // Algorithm :: Solver of Sudoku {

        // Check whether It is feasible 
        bool is_valid(int row, int col, int num){
            for(int i = 0; i < 9; i++){
                if(maze[row][i] == num) return false;
                if(maze[i][col] == num) return false;
            }
    
            int startRow = row / 3 * 3;
            int startCol = col / 3 * 3;
            for(int i = 0; i < 3; i++) // Subgrids Processing
                for(int j = 0; j < 3; j++)
                    if(maze[startRow + i][startCol + j] == num)
                        return false;
                
            return true;
        }


        bool solve(int r = 0, int c = 0){
            if(r == 9) return true;    // All row complete
            if(c == 9) return solve(r + 1, 0); // Next Column
        
            if(maze[r][c] != 0) return solve(r, c + 1);  
        
            for(int k = 1; k <= 9; k++){
                if(is_valid(r, c, k)){
                    maze[r][c] = k;
                    if(solve(r, c + 1)) return true;
                    maze[r][c] = 0;  // Backtracking
                }
            }
        
            return false; // Not Feasible
        }

    // } :: Solver of Sudoku ;

    public:

        void run_solver(){
            auto start = chrono::high_resolution_clock::now();
            if(solve()){
                auto end = chrono::high_resolution_clock::now();  
                chrono::duration<double, std::milli> elapsed = end - start;

                cout << "Feasible Solution of Sudoku : " << endl;
                show_board();
                cout << "Time taken: " << elapsed.count() << " ms" << endl;
            }else{
                cout << "Could Not Be Solved...." << endl;
            }
        }
        
        // io for sudoku board
        void show_board(){
            cout<<"-------------"<<endl;
            for (unsigned short i = 0; i < 9; i++){
                cout<<"|";
                for (unsigned short j = 0; j < 9; j++){
                    cout<<maze[i][j];
                    if(j%3==2)cout<<"|";
                }
                cout<<endl;
                if(i%3==2)cout<<"-------------"<<endl;
            }
        }
        void scan_maze(){
            cout<<"Import Sudoku puzzles (with . as blank) : \n";
            for(int i=0;i<9;i++){
                for(int j=0;j<9;j++){
                    char temp;cin>>temp;
                    if(temp=='.')maze[i][j] = 0;
                    else maze[i][j] = temp -'0';
                }
            }
        }


    
 
};

int main(){
    Sudoku sodoku; 
    sodoku.scan_maze(); 
    system("CLS");
    sodoku.show_board(); 
    cout<<"\n\n";
    sodoku.run_solver();

    system("pause");
    return 0;
}


// Question Bank of Sudolu
/* Q1. 
5 3 . . 7 . . . .
6 . . 1 9 5 . . .
. 9 8 . . . . 6 .
8 . . . 6 . . . 3
4 . . 8 . 3 . . 1
7 . . . 2 . . . 6
. 6 . . . . 2 8 .
. . . 4 1 9 . . 5
. . . . 8 . . 7 9
*/
/* Q2.
. . . . . 6 . . .
. . 2 . . . . 8 .
. . . . 7 . 9 . 2
. 5 . 4 . . . . .
. . 4 . . . . . 6
. . . 2 . . . 3 .
6 . 1 . . 9 . . .
. 3 . . . . 5 . .
. . . 8 . . . 1 .
*/
/* Q3.
. 2 . . . . . 6 3
3 . . . . 5 4 . 1
. . 1 . . 3 9 8 .
. . . 9 . . . . .
. . . 5 3 8 . . .
. . . . . 6 . . .
. 9 8 3 . . 5 . .
7 . 6 8 . . . . 2
5 1 . . . . . 4 .
*/
/* Q4.
. . 5 3 . . . . .
8 . . . . . . 2 .
. 7 . . 1 . 5 . .
4 . . . . 5 3 . .
. 1 . . 7 . . . 6
. . 3 2 . . . 8 .
. 6 . 5 . . . . 9
. . 4 . . . . 3 .
. . . . . 9 7 . .
*/
/* Q5.
. . . . . . . 1 .
4 . . . . . 2 . .
. . 2 . . . . . .
. . . . 5 . 4 . 7
. . 8 . . . . . .
. . . 2 7 . . . .
. 1 . . . . . 3 .
. . . . . 1 . . 6
. . . . . . . . .
*/