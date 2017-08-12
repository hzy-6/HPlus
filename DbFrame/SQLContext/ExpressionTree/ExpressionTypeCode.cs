using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbFrame.SQLContext.ExpressionTree
{
    /// <summary>
    /// 表达式树类型枚举
    /// </summary>
    public enum ExpressionTypeCode
    {
        /// <summary> 未知类型表达式
        /// </summary>
        Unknown = 0,
        /// <summary> 空表达式 null
        /// </summary>
        Null = 1,
        /// <summary> 表示包含二元运算符的表达式。
        /// </summary>
        BinaryExpression = 2,
        /// <summary> 表示一个包含可在其中定义变量的表达式序列的块。
        /// </summary>
        BlockExpression = 3,
        /// <summary> 表示包含条件运算符的表达式。
        /// </summary>
        ConditionalExpression = 4,
        /// <summary> 表示具有常量值的表达式。
        /// </summary>
        ConstantExpression = 5,
        /// <summary> 发出或清除调试信息的序列点。 这允许调试器在调试时突出显示正确的源代码。
        /// </summary>
        DebugInfoExpression = 6,
        /// <summary> 表示类型或空表达式的默认值。
        /// </summary>
        DefaultExpression = 7,
        /// <summary> 表示动态操作。
        /// </summary>
        DynamicExpression = 8,
        /// <summary> 表示无条件跳转。 这包括 return 语句、break 和 continue 语句以及其他跳转。
        /// </summary>
        GotoExpression = 9,
        /// <summary> 表示编制属性或数组的索引。
        /// </summary>
        IndexExpression = 10,
        /// <summary> 表示将委托或 lambda 表达式应用于参数表达式列表的表达式。
        /// </summary>
        InvocationExpression = 11,
        /// <summary> 表示一个标签，可以将该标签放置在任何 Expression 上下文中。 
        /// </summary>
        LabelExpression = 12,
        /// <summary> 描述一个 lambda 表达式。 这将捕获与 .NET 方法体类似的代码块。
        /// </summary>
        LambdaExpression = 13,
        /// <summary> 表示包含集合初始值设定项的构造函数调用。
        /// </summary>
        ListInitExpression = 14,
        /// <summary> 表示无限循环。 可以使用“break”退出它。
        /// </summary>
        LoopExpression = 15,
        /// <summary> 表示访问字段或属性。
        /// </summary>
        MemberExpression = 16,
        /// <summary> 表示调用构造函数并初始化新对象的一个或多个成员。
        /// </summary>
        MemberInitExpression = 17,
        /// <summary> 表示对静态方法或实例方法的调用。
        /// </summary>
        MethodCallExpression = 18,
        /// <summary> 表示创建新数组并可能初始化该新数组的元素。
        /// </summary>
        NewArrayExpression = 19,
        /// <summary> 表示构造函数调用。
        /// </summary>
        NewExpression = 20,
        /// <summary> 表示命名的参数表达式。
        /// </summary>
        ParameterExpression = 21,
        /// <summary> 一个为变量提供运行时读/写权限的表达式。
        /// </summary>
        RuntimeVariablesExpression = 22,
        /// <summary> 表示一个控制表达式，该表达式通过将控制传递到 SwitchCase 来处理多重选择。
        /// </summary>
        SwitchExpression = 23,
        /// <summary> 表示 try/catch/finally/fault 块。
        /// </summary>
        TryExpression = 24,
        /// <summary> 表示表达式和类型之间的操作。
        /// </summary>
        TypeBinaryExpression = 25,
        /// <summary> 表示包含一元运算符的表达式。
        /// </summary>
        UnaryExpression = 26,





    }
}
