using System;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace lab2
{
    class ExpressionNo8 {

        static void Main(string[] args) {
            Console.WriteLine(Matrix<double>.Build.Dense(4, 4, 1) * Vector<double>.Build.Dense(4, 1));
            ExpressionNo8 expr = new ExpressionNo8();
        }

        bool debug;
        int n;

        Random _r = null;
        public Random r {
            get { if (_r == null) _r = new Random(); return _r;}
            set { _r = value; }
        }

        private Object bl = new Object();
        Vector<double> _b = null;
        public Vector<double> b {
            get { 
                lock(bl) { if (_b == null) _b = Vector<double>.Build.Dense(n, i => 8.0 / (1+i)); }
                return _b;
            }
            set { this._b = value; }
        }

        Vector<double> b1;
        Vector<double> c1;

        private Object y1l = new Object();
        Vector<double> _y1 = null;
        Vector<double> y1 {
            get {
                Console.WriteLine("y1 get");
                lock(y1l) { if (_y1 == null) _y1 = A * b; }
                Console.WriteLine("y1 after check ... {0}", _y1);
                return _y1;
            }
            set { _y1 = value; }
        }

        private Object y2l = new Object();
        Vector<double> _y2 = null;
        Vector<double> y2 {
            get {
                lock(y2l) { if (_y2 == null) _y2 = A1 * (b1 + c1); }
                return _y2;
            }
            set { _y2 = value; }
        }

        Matrix<double> A;
        Matrix<double> A1;
        Matrix<double> A2;
        Matrix<double> B1;
        Matrix<double> B2;


        private Object C2l = new Object();
        Matrix<double> _C2 = null;
        Matrix<double> C2 {
            get {
                
                lock(C2l) { if (_C2 == null) _C2 = Matrix<double>.Build.Dense(n, n, (i,j)=>1/((i+1)+(j+1)+2)); }
                return _C2;
            }
            set { _C2 = value; }
        }

        private Object Y3l = new Object();
        Matrix<double> _Y3 = null;
        Matrix<double> Y3 {
            get {
                lock(Y3l) { if (_Y3 == null) _Y3 = A2 * (B2 + C2); }
                return _Y3;
            }
            set { _Y3 = value; }
        }

        public ExpressionNo8(int n, Matrix<double> A, Matrix<double> A1,
                Vector<double> b1, Vector<double> c1,
                Matrix<double> A2, Matrix<double> B2) {

            this.n = n;
            this.A = A;

            this.A1 = A1;
            this.b1 = b1;
            this.c1 = c1;

            this.A2 = A2;
            this.B2 = B2;

        }

        public ExpressionNo8() {
            
            int n = CLI.GetInt("Please, enter n", 8);
            bool debug = CLI.GetBool("Debug mode?", false);
            
            Matrix<double> A = null;
            bool ans = CLI.GetBool("Random matrix A?", true);
            if (ans)
                A = Matrix<double>.Build.Dense(n, n, (i, j) => 1+r.Next(n));
            else
                A = CLI.GetMatrix("Enter A matrix.", n, n);
            Console.WriteLine(A);

            Matrix<double> A1 = null;
            ans = CLI.GetBool("Random matrix A1?", true);
            if (ans)
                A1 = Matrix<double>.Build.Dense(n, n, (i, j) => 1+r.Next(n));
            else
                A1 = CLI.GetMatrix("Enter A1 matrix.", n, n);
            Console.WriteLine(A1);

            Vector<double> b1 = null;
            ans = CLI.GetBool("Random vector b1?", true);
            if (ans)
                b1 = Vector<double>.Build.Dense(n, i => 1+r.Next(n));
            else
                b1 = CLI.GetVector("Enter b1 vector.", n);
            Console.WriteLine(b1);

            Vector<double> c1 = null;
            ans = CLI.GetBool("Random vector c1?", true);
            if (ans)
                c1 = Vector<double>.Build.Dense(n, i => 1+r.Next(n));
            else
                c1 = CLI.GetVector("Enter c1 vector.", n);
            Console.WriteLine(c1);

            Matrix<double> A2 = null;
            ans = CLI.GetBool("Random matrix A2?", true);
            if (ans)
                A2 = Matrix<double>.Build.Dense(n, n, (i, j) => 1+r.Next(n));
            else
                A2 = CLI.GetMatrix("Enter A2 matrix.", n, n);
            Console.WriteLine(A2);
            
            Matrix<double> B2 = null;
            ans = CLI.GetBool("Random matrix B2?", true);
            if (ans)
                B2 = Matrix<double>.Build.Dense(n, n, (i, j) => 1+r.Next(n));
            else
                B2 = CLI.GetMatrix("Enter B2 matrix.", n, n);
            Console.WriteLine(B2);

            calculate(n, A, A1, b1, c1, A2, B2, debug);
        }


        private void calculate(int n, Matrix<double> A, Matrix<double> A1,
                Vector<double> b1, Vector<double> c1,
                Matrix<double> A2, Matrix<double> B2, bool debug=false) {

            this.debug = debug;
            //
            this.n = n;
            this.A = A;

            this.A1 = A1;
            this.b1 = b1;
            this.c1 = c1;

            this.A2 = A2;
            this.B2 = B2;
            Console.WriteLine("Result: {0}", (y1 * Y3 * y1 * y2 * y2)*(y2 * Y3 * y2 * y1 + y1));
        }

        private void reset() {

        }
        
    }

    class CLI {

        static void showPrompt(string prompt, string current) {
            Console.WriteLine(prompt);
            Console.Write("[{0}]> ", current);
        }

        static string[] BOOL_YES = {"yes", "y", "true", "da", "sure"};
        static string[] BOOL_NO = {"yes", "y", "true", "da", "sure"};

        public static bool GetBool(string prompt, bool d) {
            showPrompt(prompt, d.ToString());
            string s = Console.ReadLine().Trim().ToLower();
            if (Array.Exists(BOOL_YES, e => s.Equals(e))) {
                return true;
            }
            else if (Array.Exists(BOOL_YES, e => s.Equals(e))) {
                return false;
            }
            return d;
        }

        public static int GetInt(string prompt, int d) {
            showPrompt(prompt, d.ToString());
            return ParseInt(d);
        }

        public static int ParseInt(int d) {
            int result = -1;
            bool success = Int32.TryParse(Console.ReadLine(), out result);
            if (success && result > 0)
                return result;
            return d;
        }

        public static Vector<double> GetVector(string prompt, int w) {
            int d = 1;
            Vector<double> result = Vector<double>.Build.Dense(w, d);
            Console.WriteLine(prompt);
            
            for(int j = 0; j < w; j++) {
                showPrompt("Enter element.", (j+1).ToString());
                result[j] = ParseInt(d);
            }
            Console.WriteLine(result);
            
            return result;
        }

        public static Matrix<double> GetMatrix(string prompt, int w, int h) {
            int d = 1;
            Matrix<double> result = Matrix<double>.Build.Dense(w, h, d);
            Console.WriteLine(prompt);

            for(int i = 0; i < h; i++) {
                for(int j = 0; j < w; j++) {
                    showPrompt("Enter element.", String.Format("{0}, {1}", (j+1), (i+1)));
                    result[i,j] = ParseInt(d);
                }
                Console.WriteLine(result);
            }
            return result;
        }

    }
}
