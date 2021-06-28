using System;
using System.Collections.Generic;

namespace SimpleVisitor
{
    public enum OPERATOR
    {
        // Supports +,-,*,/
        PLUS,
        MINUS,
        MUL,
        DIV
    }

    /// <summary>
    /// Base clss for all Expression
    /// Supports accept method to implement so called "Double Dispatch"
    /// </summary>
    public abstract class Expr
    {
        public abstract double accept(IExprVisitor expr_vis);
    }

    /// <summary>
    /// Our Visitor interface
    /// The purpose of separating Processing of nodes and data storage
    /// is for various transformations on the composites created
    /// </summary>
    public interface IExprVisitor
    {
        double Visit(Number num);
        double Visit(BinaryExpr bin);
        double Visit(UnaryExpr un);
    }

    /// <summary>
    /// Class Number stores an IEEE 754 double precision floating point
    /// </summary>
    public class Number : Expr
    {
        public double NUM { get; set; }
        public Number(double n) { NUM = n; }

        public override double accept(IExprVisitor expr_vis)
        {
            return expr_vis.Visit(this);
        }
    }

    /// <summary>
    /// Class BinaryExpr models a binary expression of the form <Operand> <OPER> <Operand>
    /// </summary>
    public class BinaryExpr : Expr
    {
        public Expr Left { get; set; }
        public Expr Right { get; set; }
        public OPERATOR OP { get; set; }

        public BinaryExpr(Expr l, Expr r, OPERATOR op)
        {
            Left = l;
            Right = r;
            OP = op;
        }

        public override double accept(IExprVisitor expr_vis)
        {
            return expr_vis.Visit(this);
        }
    }

    /// <summary>
    /// Class UnaryExpr models a unary expression of the form <OPER> <OPERAND>
    /// </summary>
    public class UnaryExpr : Expr
    {
        public Expr Right { get; set; }
        public OPERATOR OP { get; set; }

        public UnaryExpr(Expr r, OPERATOR op)
        {
            Right = r;
            OP = OP;
        }

        public override double accept(IExprVisitor expr_vis)
        {
            return expr_vis.Visit(this);
        }
    }

    public class TreeEvaluatorVisitor : IExprVisitor
    {
        public double Visit(Number num)
        {
            return num.NUM;
        }

        public double Visit(BinaryExpr bin)
        {
            switch (bin.OP)
            {
                case OPERATOR.PLUS:
                    {
                        return bin.Left.accept(this) + bin.Right.accept(this);
                    }
                case OPERATOR.MUL:
                    {
                        return bin.Left.accept(this) * bin.Right.accept(this);
                    }
                case OPERATOR.DIV:
                    {
                        return bin.Left.accept(this) / bin.Right.accept(this);
                    }
                case OPERATOR.MINUS:
                    {
                        return bin.Left.accept(this) - bin.Right.accept(this);
                    }
            }

            return double.NaN;
        }

        public double Visit(UnaryExpr un)
        {
            switch (un.OP)
            {
                case OPERATOR.PLUS:
                    {
                        return +un.Right.accept(this);
                    }
                case OPERATOR.MINUS:
                    {
                        return -un.Right.accept(this);
                    }
            }

            return double.NaN;
        }
    }

    public class ReversePolishEvaluator : IExprVisitor
    {
        public double Visit(Number num)
        {
            Console.Write(num.NUM + " ");

            return 0;
        }

        public double Visit(BinaryExpr bin)
        {
            bin.Left.accept(this);
            bin.Right.accept(this);

            switch (bin.OP)
            {
                case OPERATOR.PLUS:
                    {
                        Console.Write(" + ");

                        break;
                    }
                case OPERATOR.MUL:
                    {
                        Console.Write(" * ");

                        break;
                    }
                case OPERATOR.DIV:
                    {
                        Console.Write(" / ");

                        break;
                    }
                case OPERATOR.MINUS:
                    {
                        Console.Write(" - ");

                        break;
                    }
            }

            return double.NaN;
        }

        public double Visit(UnaryExpr un)
        {
            un.Right.accept(this);

            switch (un.OP)
            {
                case OPERATOR.PLUS:
                    {
                        Console.Write(" + ");

                        break;
                    }
                case OPERATOR.MINUS:
                    {
                        Console.Write(" - ");

                        break;
                    }
            }

            return double.NaN;
        }
    }

    public class StackEvaluator : IExprVisitor
    {
        private Stack<double> eval_stack = new Stack<double>();

        public double get_value()
        {
            return eval_stack.Pop();
        }

        public StackEvaluator()
        {
            eval_stack.Clear();
        }

        public double Visit(Number num)
        {
            eval_stack.Push(num.NUM);

            return 0;
        }

        public double Visit(BinaryExpr bin)
        {
            bin.Left.accept(this);
            bin.Right.accept(this);

            switch (bin.OP)
            {
                case OPERATOR.PLUS:
                    {
                        eval_stack.Push(eval_stack.Pop() + eval_stack.Pop());

                        break;
                    }
                case OPERATOR.MUL:
                    {
                        eval_stack.Push(eval_stack.Pop() * eval_stack.Pop());

                        break;
                    }
                case OPERATOR.DIV:
                    {
                        eval_stack.Push(eval_stack.Pop() / eval_stack.Pop());

                        break;
                    }
                case OPERATOR.MINUS:
                    {
                        eval_stack.Push(eval_stack.Pop() - eval_stack.Pop());

                        break;
                    }
            }

            return double.NaN;
        }

        public double Visit(UnaryExpr un)
        {
            un.Right.accept(this);

            switch (un.OP)
            {
                case OPERATOR.PLUS:
                    {
                        eval_stack.Push(eval_stack.Pop());

                        break;
                    }
                case OPERATOR.MINUS:
                    {
                        eval_stack.Push(-eval_stack.Pop());

                        break;
                    }
            }

            return double.NaN;
        }
    }
}
