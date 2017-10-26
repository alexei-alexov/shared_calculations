
using System;


namespace lab3
{
    class Launcher
    {
        static void Main(string[] args)
        {
            int N = CLI.GetInt("Enter size.", 5);

            Console.WriteLine("Welcome!");
            SquareMatrix aMatrix = new SquareMatrix(N);
            aMatrix.Fill((i, j) => 1 + Math.Abs(i - j));
            Console.WriteLine("Here is A matrix:\n{0}", aMatrix);

            SquareMatrix bMatrix = new SquareMatrix(N);
            bMatrix.Fill((i, j) => (i + j < N && i <= j)? CLI.GetDouble(String.Format("Enter [{0},{1}] element", i, j), 1) : 0);
            Console.WriteLine("Here is B matrix:\n{0}", bMatrix);

            Console.WriteLine("Recursive result:\n{0}", aMatrix.RecursiveMultiplication(bMatrix));
            Console.WriteLine("Amount of calculations took: {0}", aMatrix.Calculations);

            Console.WriteLine("Simple multiplication result:\n{0}", aMatrix.SingleAssignmentMultiplication(bMatrix));
            Console.WriteLine("Amount of calculations took: {0}", aMatrix.Calculations);

        }

    }

    class SquareMatrix {
        
        public int Calculations;
        int size;
        public double[,] body = null;
        
        bool needStringRefresh = true;
        string matrixRepr = null;

        public SquareMatrix(int size) {
            this.size = size;
            body = new double[size,size];
        }

        public void Fill(Func<int, int, double> filler) {
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    body[i,j] = filler(i, j);
                }
            }
            needStringRefresh = true;
        }

        public override string ToString() {
            if (needStringRefresh)
                refreshRepr();

            return matrixRepr;
        }


        public SquareMatrix SingleAssignmentMultiplication(SquareMatrix other) {
            Calculations = 0;
            SquareMatrix result = new SquareMatrix(size);
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    for(int k = 0; k < size; k++) {
                        result.body[i, j] += this.body[i, k] * other.body[k, j];
                        Calculations += 1;
                    }
                }
            }
            return result;
        }

        public SquareMatrix RecursiveMultiplication(SquareMatrix other) {
            Calculations = 0;
            SquareMatrix result = new SquareMatrix(size);
            
            Func<int, int, int, double> summator = null;
            summator = (i, j, k) => {
                Calculations += 1;
                return (k == size-1)? (body[i, k] * other.body[k, j] + result.body[i,j]) : (body[i, k] * other.body[k, j] + summator(i, j, k+1));
            };

            for(int i = 0; i < size; i++)
                for(int j = 0; j < size; j++)
                    result.body[i, j] = summator(i, j, 0);

            return result;
        }
        
        private void refreshRepr() {
            string[] rows = new string[size];
            string[] tempRow = new string[size];
            for(int i = 0; i < size; i++) {
                for(int j = 0; j < size; j++) {
                    tempRow[j] = String.Format("{0:##0.##}", body[i, j]);
                }
                rows[i] = String.Join(" ", tempRow);
            }
            matrixRepr = String.Join("\n", rows);
            needStringRefresh = false;
        }
    }

    public class CLI {

        static void showPrompt(string prompt, string current) {
            Console.WriteLine(prompt);
            Console.Write("[{0}]> ", current);
        }

        static string[] BOOL_YES = {"yes", "y", "true", "da", "sure"};
        static string[] BOOL_NO = {"no", "n", "false", "net", "not sure"};

        public static bool GetBool(string prompt, bool d) {
            showPrompt(prompt, d.ToString());
            string s = Console.ReadLine().Trim().ToLower();
            if (Array.Exists(BOOL_YES, e => s.Equals(e))) {
                return true;
            }
            else if (Array.Exists(BOOL_NO, e => s.Equals(e))) {
                return false;
            }
            return d;
        }

        public static int GetInt(string prompt, int d) {
            showPrompt(prompt, d.ToString());
            return ParseInt(d);
        }

        public static double GetDouble(string prompt, double d) {
            showPrompt(prompt, d.ToString());
            return ParseDouble(d);
        }

        public static int ParseInt(int d) {
            int result = -1;
            bool success = Int32.TryParse(Console.ReadLine(), out result);
            if (success && result > 0)
                return result;
            return d;
        }

        public static double ParseDouble(double d) {
            double result = -1;
            bool success = Double.TryParse(Console.ReadLine(), out result);
            if (success && result > 0)
                return result;
            return d;
        }

    }
}
